const fs = require('fs');
const path = require('path');

function replaceImports(fileName, packageName, namespaces) {
    var content = fs.readFileSync(fileName).toString();
    var regex = new RegExp("import ([^{].+[^}]) from [\"|']" + packageName + "[^-].+", "g");
    var match = regex.exec(content);
    var variables = [];
    var imports = [];
    while(match) {
        imports.push(match[0]);
        var variable = match[1].trim();
        if(variable.indexOf("* as ") !== -1) variable = variable.replace("* as ", "");
        variables.push(variable)
        match = regex.exec(content);
    }
    var newContent = content.replace(regex, "");
    var currentNamespaces = [];
    variables.forEach(x => {
        var regex = new RegExp(`${x}((\\.\\w+)+)`, "g");
        var test = regex.exec(content);
        while(test) {
            var type = test[1].substring(1);
            if(currentNamespaces.indexOf(type) === -1) {
                currentNamespaces.push({
                    variableName: x,
                    namespace: test[1].substring(1)
                });
            }
            var test = regex.exec(content);
        }
    })
    var replacements = [];
    var headers = {};
    for(var j = 0; j < currentNamespaces.length; j++) {
        for(var i = 0; i < namespaces.length; i++) {
            var namespace = namespaces[i].namespace.substring("DevExpress".length + 1)
            if(currentNamespaces[j].namespace.indexOf(namespace + ".") === 0) {
                var convertTo = currentNamespaces[j].namespace.substring(namespace.length + 1);
                var importVar = convertTo.split('.')[0]
                if(!headers[namespaces[i].variableName])
                    headers[namespaces[i].variableName] = {
                        prefix: namespaces[i].header.prefix,
                        suffix: namespaces[i].header.suffix,
                        imports: []
                    };
                replacements.push({
                    from: [currentNamespaces[j].variableName, currentNamespaces[j].namespace].join('.'),
                    to: convertTo
                })
                if(headers[namespaces[i].variableName].imports.indexOf(importVar) === -1) {
                    headers[namespaces[i].variableName].imports.push(importVar);
                }
                break;
            }
        }
    }
    replacements.forEach((item) => {
        newContent = newContent.replace(item.from, item.to);
    });
    Object.keys(headers).forEach((key) => {
        var item = headers[key];
        newContent = [item.prefix + item.imports.join(', ') + item.suffix, newContent].join("\n");
    })
    fs.writeFileSync(fileName, newContent)
};

function convert(dirname, packageName, namespaces) {
    fs.readdirSync(dirname).forEach(function(filename) {   
        if(filename !== "node_modules") {
            var currentFilePath = path.join(dirname, filename)
            var stat = fs.lstatSync(currentFilePath);
            if(stat.isDirectory()) {
                convert(currentFilePath, packageName, namespaces)
            } else if(path.extname(currentFilePath) === ".ts" || path.extname(currentFilePath) === ".js") {
                replaceImports(currentFilePath, packageName, namespaces);
            }
        }
    });
}

exports.convert = convert;