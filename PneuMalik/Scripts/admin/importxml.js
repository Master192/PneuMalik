var pneuMalik;
(function (pneuMalik) {
    "use strict";

    function loadImportXml() {

        var doStockImportButton = document.getElementById("doStockImportButton");
        var doFullImportButton = document.getElementById("doFullImportButton");
        var stockImportStatus = document.getElementById("stockImportStatus");
        var fullImportStatus = document.getElementById("fullImportStatus");

        if (doStockImportButton) {
            doStockImportButton.onclick = (function () {

                window.setInterval(() => {
                    var getStatus = new pneuMalik.RequestHelper("GET", "/api/import/status/");
                    getStatus.makeRequest("getImportStatus").then(function (response) {
                        stockImportStatus.innerHTML = "Stav importu: " + response;
                    }, function (reject) {
                        stockImportStatus.innerHTML = "Chyba importu: " + reject.message;
                    })
                }, 1000);

                var runImport = new pneuMalik.RequestHelper("GET", "/api/import/importstock/");
                runImport.makeRequest("runImportStock");
            });
        }

        if (doFullImportButton) {
            doFullImportButton.onclick = (function () {
                console.log("do full import");
            });
        }
    }

    document.addEventListener("DOMContentLoaded", function () { return loadImportXml(); });
})(pneuMalik || (pneuMalik = {}));