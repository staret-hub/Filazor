window.commonFunctions = {
    alertDialog: function (message) {
        alert(message);
    },
    focusElement: function (id) {
        const element = document.getElementById(id);
        element.focus();
    }
}