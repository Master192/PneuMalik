function filterCathegoryChange() {

    var selectCathegory = document.getElementById('filter-cathegory');
    var cathegoryType = selectCathegory.options[selectCathegory.selectedIndex].value;
    var cathegoryUrl;      

    switch (cathegoryType)
    {
        case '1':
            cathegoryUrl = '/pneumatiky/osobni-offroad-4x4-suv';
            break;
        case '2':
            cathegoryUrl = '/pneumatiky/uzitkove-zatezove-c';
            break;
        case '3':
            cathegoryUrl = '/pneumatiky/nakladni';
            break;
        case '4':
            cathegoryUrl = '/pneumatiky/industrialni-a-prumyslove';
            break;
        case '5':
            cathegoryUrl = '/pneumatiky/motopneu';
            break;
        case '6':
            cathegoryUrl = '/pneumatiky/zemedelske';
            break;
        case '7':
            cathegoryUrl = '/pneumatiky/atv';
            break;
        case '8':
            cathegoryUrl = '/pneumatiky/duse-a-motovlozky';
            break;
    }

    window.location.href = cathegoryUrl;
}

function filterChange(source) {

    var sources = ['manufacturer', 'season', 'profile', 'width', 'rim'];

    var cathegory = document.getElementById("filter-cathegory").options[selectCathegory.selectedIndex].value;
    var season = document.getElementById("filter-season").options[selectCathegory.selectedIndex].value;
    var manufacturer = document.getElementById("filter-manufacturer").options[selectCathegory.selectedIndex].value;
    var width = document.getElementById("filter-width").options[selectCathegory.selectedIndex].value;
    var rim = document.getElementById("filter-rim").options[selectCathegory.selectedIndex].value;
    var profile = document.getElementById("filter-profile").options[selectCathegory.selectedIndex].value;

    var query = "?cathegory=" + cathegory + "&season=" + season + "&manufacturer=" + manufacturer + "&width=" + width + "&rim=" + rim + "&profile=" + profile;

    var getStatus = new pneuMalik.RequestHelper("GET", "/api/filter/reload" + query);
    getStatus.makeRequest("filterChange").then(function (response) {
        console.log(response);
    }, function (reject) {
        console.log("Filter reload error: " + reject.message);
    });
}

document.addEventListener('DOMContentLoaded', function () {

    // nothing onload yet

}, false);