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

    var selectCathegory = document.getElementById('filter-cathegory');
    var cathegory = selectCathegory.options[selectCathegory.selectedIndex].value;

    var selectSeason = document.getElementById('filter-season');
    var season = selectSeason.options[selectSeason.selectedIndex].value;

    var selectManufacturer = document.getElementById('filter-manufacturer');
    var manufacturer = selectManufacturer.options[selectManufacturer.selectedIndex].value;

    var selectWidth = document.getElementById('filter-width');
    var width = selectWidth.options[selectWidth.selectedIndex].value;

    var selectRim = document.getElementById('filter-rim');
    var rim = selectRim.options[selectRim.selectedIndex].value;

    var selectProfile = document.getElementById('filter-profile');
    var profile = selectProfile.options[selectProfile.selectedIndex].value;

    var query = "?cathegory=" + cathegory + "&season=" + season + "&manufacturer=" + manufacturer + "&width=" + width + "&rim=" + rim + "&profile=" + profile;

    var getStatus = new pneuMalik.RequestHelper("GET", "/api/filter/reload" + query);
    getStatus.makeRequest("filterChange").then(function (response) {

        var filter = JSON.parse(response);

        [].forEach.call(document.querySelectorAll('#filter-manufacturer option'), function (elm) {

            if (elm.value == "0") {
                return;
            }

            if (filter.Manufacturers.indexOf(parseInt(elm.value)) < 0) {
                elm.remove();
            }
        });

        [].forEach.call(document.querySelectorAll('#filter-season option'), function (elm) {

            if (filter.Seasons.indexOf(parseInt(elm.value)) < 0) {
                elm.remove();
            }
        });

        [].forEach.call(document.querySelectorAll('#filter-width option'), function (elm) {

            if (elm.value == "0") {
                return;
            }

            if (filter.Widths.indexOf(parseInt(elm.value)) < 0) {
                elm.remove();
            }
        });

        [].forEach.call(document.querySelectorAll('#filter-rim option'), function (elm) {

            if (elm.value == "0") {
                return;
            }

            if (filter.Rims.indexOf(parseInt(elm.value)) < 0) {
                elm.remove();
            }
        });

        [].forEach.call(document.querySelectorAll('#filter-profile option'), function (elm) {

            if (elm.value == "0") {
                return;
            }

            if (filter.Profiles.indexOf(parseInt(elm.value)) < 0) {
                elm.remove();
            }
        });

    }, function (reject) {
        console.log("Filter reload error: " + reject.message);
    });
}

document.addEventListener('DOMContentLoaded', function () {

    // nothing onload yet

}, false);