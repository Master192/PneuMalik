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

document.addEventListener('DOMContentLoaded', function () {

    // nothing onload yet

}, false);