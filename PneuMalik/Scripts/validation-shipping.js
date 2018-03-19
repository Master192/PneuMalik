function changeShipping(smer) {

    var infoBox = document.getElementById("saleInfoBox");

    switch (smer) {

        case 1:
        case 4:
            infoBox.style.display = "none";
            break;
        case 2:
        case 3:
        case 5:
            infoBox.style.display = "block";
            break;
    }
}