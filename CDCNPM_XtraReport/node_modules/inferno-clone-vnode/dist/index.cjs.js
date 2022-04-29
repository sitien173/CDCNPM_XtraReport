'use strict';

Object.defineProperty(exports, '__esModule', { value: true });

var inferno = require('inferno');

function combineFrom(first, second) {
    var out = {};
    if (first) {
        for (var key in first) {
            out[key] = first[key];
        }
    }
    if (second) {
        for (var key$1 in second) {
            out[key$1] = second[key$1];
        }
    }
    return out;
}

/*
 directClone is preferred over cloneVNode and used internally also.
 This function makes Inferno backwards compatible.
 And can be tree-shaked by modern bundlers

 Would be nice to combine this with directClone but could not do it without breaking change
*/
/**
 * Clones given virtual node by creating new instance of it
 * @param {VNode} vNodeToClone virtual node to be cloned
 * @param {Props=} props additional props for new virtual node
 * @param {...*} _children new children for new virtual node
 * @returns {VNode} new virtual node
 */
function cloneVNode(vNodeToClone, props, _children) {
    var arguments$1 = arguments;

    var flags = vNodeToClone.flags;
    var children = flags & 14 /* Component */ ? vNodeToClone.props && vNodeToClone.props.children : vNodeToClone.children;
    var childLen = arguments.length - 2;
    var className = vNodeToClone.className;
    var key = vNodeToClone.key;
    var ref = vNodeToClone.ref;
    if (props) {
        if (props.className !== void 0) {
            className = props.className;
        }
        if (props.ref !== void 0) {
            ref = props.ref;
        }
        if (props.key !== void 0) {
            key = props.key;
        }
        if (props.children !== void 0) {
            children = props.children;
        }
    }
    else {
        props = {};
    }
    if (childLen === 1) {
        children = _children;
    }
    else if (childLen > 1) {
        children = [];
        while (childLen-- > 0) {
            children[childLen] = arguments$1[childLen + 2];
        }
    }
    props.children = children;
    if (flags & 14 /* Component */) {
        return inferno.createComponentVNode(flags, vNodeToClone.type, !vNodeToClone.props && !props ? inferno.EMPTY_OBJ : combineFrom(vNodeToClone.props, props), key, ref);
    }
    if (flags & 16 /* Text */) {
        return inferno.createTextVNode(children);
    }
    if (flags & 8192 /* Fragment */) {
        return inferno.createFragment(childLen === 1 ? [children] : children, 0 /* UnknownChildren */, key);
    }
    return inferno.normalizeProps(inferno.createVNode(flags, vNodeToClone.type, className, null, 1 /* HasInvalidChildren */, combineFrom(vNodeToClone.props, props), key, ref));
}

exports.cloneVNode = cloneVNode;
