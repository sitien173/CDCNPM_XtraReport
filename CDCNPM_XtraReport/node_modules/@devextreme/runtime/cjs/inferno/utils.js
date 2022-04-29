"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var NUMBER_STYLES = [
    'animation-iteration-count',
    'border-image-outset',
    'border-image-slice',
    'border-image-width',
    'box-flex',
    'box-flex-group',
    'box-ordinal-group',
    'column-count',
    'fill-opacity',
    'flex',
    'flex-grow',
    'flex-negative',
    'flex-order',
    'flex-positive',
    'flex-shrink',
    'flood-opacity',
    'font-weight',
    'grid-column',
    'grid-row',
    'line-clamp',
    'line-height',
    'opacity',
    'order',
    'orphans',
    'stop-opacity',
    'stroke-dasharray',
    'stroke-dashoffset',
    'stroke-miterlimit',
    'stroke-opacity',
    'stroke-width',
    'tab-size',
    'widows',
    'z-index',
    'zoom',
];
var uppercasePattern = /[A-Z]/g;
var kebabCase = function (str) { return str.replace(uppercasePattern, '-$&').toLowerCase(); };
var isNumeric = function (value) {
    if (typeof value === 'number')
        return true;
    return !isNaN(Number(value));
};
var getNumberStyleValue = function (style, value) { return (NUMBER_STYLES.indexOf(style) > -1 ? value : value + "px"); };
exports.normalizeStyles = function (styles) {
    if (!(styles instanceof Object))
        return undefined;
    return Object.keys(styles).reduce(function (result, key) {
        var value = styles[key];
        var kebabString = kebabCase(key);
        result[kebabString] = isNumeric(value)
            ? getNumberStyleValue(kebabString, value)
            : value;
        return result;
    }, {});
};
