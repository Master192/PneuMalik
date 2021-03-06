﻿document.addEventListener('DOMContentLoaded', function () {

    var getBrands = new pneuMalik.RequestHelper("GET", "/api/configurator/brands");
    getBrands.makeRequest("configuratorBrands").then(function (response) {

        var brands = JSON.parse(response);

        var selectBrands = document.getElementById("select-brand");
        while (selectBrands.firstChild) {
            selectBrands.removeChild(selectBrands.firstChild);
        }

        for (var i = 0; i < brands.length; i++) {

            var brand = brands[i];

            var option = document.createElement("option");
            option.value = brand["BrandCode"];
            option.innerHTML = brand["BrandText"];
            selectBrands.appendChild(option);
        }

        selectBrands.onchange = () => {
            BrandSelected();
        };

    }, function (reject) {
        console.log("Configurator brands: " + reject.message);
    });

}, false);

function BrandSelected() {

    var typesRow = document.getElementById("tr-types");
    typesRow.style.display = "table-row";

    var brand = document.getElementById("select-brand").value;

    var getTypes = new pneuMalik.RequestHelper("GET", "/api/configurator/types?brand=" + brand);
    getTypes.makeRequest("configuratorTypes").then(function (response) {

        var types = JSON.parse(response);

        var selectTypes = document.getElementById("select-types");
        while (selectTypes.firstChild) {
            selectTypes.removeChild(selectTypes.firstChild);
        }

        for (var i = 0; i < types.length; i++) {

            var type = types[i];

            var option = document.createElement("option");
            option.value = type["TypeCode"];
            option.innerHTML = type["TypeText"].replace("from", "Od");
            selectTypes.appendChild(option);
        }

        selectTypes.onchange = () => {
            TypeSelected();
        };

    }, function (reject) {
        console.log("Configurator types: " + reject.message);
    });
}

function TypeSelected() {

    var modelsRow = document.getElementById("tr-models");
    modelsRow.style.display = "table-row";

    var brand = document.getElementById("select-brand").value;
    var type = document.getElementById("select-types").value;

    var getModels = new pneuMalik.RequestHelper("GET", "/api/configurator/models?brand=" + brand + "&type=" + type);
    getModels.makeRequest("configuratorModels").then(function (response) {

        var models = JSON.parse(response);

        var selectModels = document.getElementById("select-models");
        while (selectModels.firstChild) {
            selectModels.removeChild(selectModels.firstChild);
        }

        for (var i = 0; i < models.length; i++) {

            var model = models[i];

            var option = document.createElement("option");
            option.value = model["ModelCode"];
            option.innerHTML = model["ModelText"] + (model["Displacement"] ? " - " + model["Displacement"] + " - " + model["Power"] : "");
            selectModels.appendChild(option);
        }

        selectModels.onchange = () => {
            ModelSelected();
        };

    }, function (reject) {
        console.log("Configurator models: " + reject.message);
    });
}

function ModelSelected() {

    var sizesRow = document.getElementById("tr-sizes");
    sizesRow.style.display = "table-row";

    var brand = document.getElementById("select-brand").value;
    var type = document.getElementById("select-types").value;
    var model = document.getElementById("select-models").value;

    var getSize = new pneuMalik.RequestHelper("GET", "/api/configurator/sizes?model=" + model);
    getSize.makeRequest("configuratorSizes").then(function (response) {

        var sizes = JSON.parse(response);

        var selectSizes = document.getElementById("select-sizes");
        while (selectSizes.firstChild) {
            selectSizes.removeChild(selectSizes.firstChild);
        }

        for (var i = 0; i < sizes.length; i++) {

            var size = sizes[i];

            var option = document.createElement("option");
            option.value = size;
            option.innerHTML = size;
            selectSizes.appendChild(option);
        }

        selectSizes.onchange = () => {
            SizeSelected();
        };

    }, function (reject) {
        console.log("Configurator models: " + reject.message);
    });
}

function SizeSelected() {

    var brand = document.getElementById("select-brand").value;
    var type = document.getElementById("select-types").value;
    var model = document.getElementById("select-models").value;
    var size = document.getElementById("select-sizes").value;
    var results = document.getElementById("configurator-results");
    results.innerHTML = "<img src=\"/img/loading-configurator-results.gif\" />";

    var getDiskList = new pneuMalik.RequestHelper("GET", "/api/configurator/disks?model=" + model + "&size=" + size);
    getDiskList.makeRequest("configuratorDiskList").then(function (response) {

        var disks = JSON.parse(response);

        results.innerHTML = "";
        var resultsTable = "<table><tbody>";

        for (var i = 0; i < disks.length; i++) {

            var disk = disks[i];

            var diskObject = "<tr><td width=\"115\"><strong>"
                + "<a href=\"/disk/" + disk.Id + "/" + disk.Name.replace(" ", "-") + "\" "
                + "title=\"" + disk.Name + "\">" + disk.Name + "</a ></strong></td>"
                + "<td width=\"180\"><div class=\"prod180\">"
                + "šířka kola: " + disk.Width + "; průměr kola: " + disk.Diameter + "</div > "
                + "</td><td></td><td></td><td><strong>Sleva " + disk.Sale + " %</strong></td></tr>"
                + "<tr><td class=\"grn\"><strong>DOSTUPNOST:</strong><br />"
                + "<div class=\"prodinfo\"><span "
                + "onmouseover=\"document.getElementById('info" + disk.Id + "').style.display = 'block'\" "
                + "onmouseout=\"document.getElementById('info" + disk.Id + "').style.display = 'none'\" "
                + "class=\"dostupnostInfo\">INFO</span><div id=\"info" + disk.Id + "\" class=\"pi\">"
                + "<p><strong>Objednejte vložením do košíku.</strong><br />"
                + "Zboží není na skladě.<br >Prověříme Vám jeho dostupnost.<br />"
                + "Info Vám dodáme dnes do 17:00 nebo zítra do 12:00 hod.</p></div></div></td>"
                + "<td><div class=\"prod180\">Parametry: 5/114,3/50/67,1</div></td>"
                + "<td class=\"f15 blue\"><strong>" + disk.Price + " Kč</strong></td>"
                + "<td><input type=\"text\" class=\"boks1\" style=\"width: 20px;\" "
                + "name =\"mn" + disk.Id + "\" value=\"1\" /> ks</td>"
                + "<td><input type=\"button\" value=\"Do košíku\" class=\"enter3\" "
                + "onclick=\"__doPostBack('ctl00$MyPostBack', '1_0_9_0_" + disk.Id + "_70000001');\"></td></tr>";

            resultsTable += diskObject;
        }

        resultsTable += "</tbody></table>";
        results.innerHTML = resultsTable;

    }, function (reject) {
        console.log("Configurator disk list: " + reject.message);
    });
}