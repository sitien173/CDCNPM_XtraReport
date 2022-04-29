const namespaces = require('./reporting-namespaces').namespaces;
const convert_202 = require('@devexpress/analytics-core/utils/20.2/convert').convert;
require('@devexpress/analytics-core/utils/converter');

const packageName = 'devexpress-reporting';
var programArgs = process.argv.slice(2);

convert_202(programArgs[0], packageName, namespaces);