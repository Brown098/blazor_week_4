window.myBlazor = {
    showAlert: function (msg) {
        alert(msg);

    },
    getlocalTime: function () {
        return new Date().toLocaleString();
    }

};
window.localeString = {
    setItem: function (key, value) {
        localStorage.setItem(key, value);
    },
    
    getItem: function (key) {
        return localStorage.getItem(key);
    },
    removeItem: function (key) {
        localStorage.removeItem(key);
    }
};