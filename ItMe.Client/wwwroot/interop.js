"use strict";

window.interop = {
    toggleClass(element, className) {
        if (window.interop.disableClass(element, className)) {
            element.classList.remove(className);
            return false;
        }
        else {
            return window.interop.enableClass(element, className);
        }
    },

    setWindowTitle(title) {
        window.title = title;
    },

    setWindowLocation(url) {
        window.location = url;
    }
};

