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

    setDocumentTitle(title) {
        document.title = title;
    },

    setWindowLocation(url) {
        window.location = url;
    }
};

