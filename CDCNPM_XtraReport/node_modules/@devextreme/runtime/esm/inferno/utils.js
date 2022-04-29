const NUMBER_STYLES = [
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
const uppercasePattern = /[A-Z]/g;
const kebabCase = (str) => str.replace(uppercasePattern, '-$&').toLowerCase();
const isNumeric = (value) => {
    if (typeof value === 'number')
        return true;
    return !isNaN(Number(value));
};
const getNumberStyleValue = (style, value) => (NUMBER_STYLES.indexOf(style) > -1 ? value : `${value}px`);
export const normalizeStyles = (styles) => {
    if (!(styles instanceof Object))
        return undefined;
    return Object.keys(styles).reduce((result, key) => {
        const value = styles[key];
        const kebabString = kebabCase(key);
        result[kebabString] = isNumeric(value)
            ? getNumberStyleValue(kebabString, value)
            : value;
        return result;
    }, {});
};
