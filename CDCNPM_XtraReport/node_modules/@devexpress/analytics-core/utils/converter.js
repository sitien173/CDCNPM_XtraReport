const namespaces = require('./analytics-core-namespaces').namespaces;
const convert_202 = require('./20.2/convert').convert;

const packageName = '@devexpress/analytics-core';
var programArgs = process.argv.slice(2);

convert_202(programArgs[0], packageName, namespaces)