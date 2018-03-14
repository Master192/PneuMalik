function validateRequiedField(fieldName, imgName, strOK, strNOK) {

    txtElement = document.getElementById(fieldName);

    if (txtElement.value != "") {
        document.getElementById(imgName).src = '/img/yes.png';
        document.getElementById(imgName).alt = strOK;
        document.getElementById(imgName).title = strOK;
    }
    else {
        document.getElementById(imgName).src = '/img/no.png';
        document.getElementById(imgName).alt = strNOK;
        document.getElementById(imgName).title = strNOK;
    }
}

function validateRegexField(fieldName, imgName, strOK, strNOK, Pattern) {

    var regexes = {
        phone: /^(\+420)? ?[0-9]{3} ?[0-9]{3} ?[0-9]{3}$/,
        email: /\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/
    }

    var reg = new RegExp(regexes[Pattern]);
    txtElement = document.getElementById(fieldName);

    if (txtElement.value != "" && reg.test(txtElement.value) == true) {
        document.getElementById(imgName).src = '/img/yes.png';
        document.getElementById(imgName).alt = strOK;
        document.getElementById(imgName).title = strOK;
    }
    else {
        document.getElementById(imgName).src = '/img/no.png';
        document.getElementById(imgName).alt = strNOK;
        document.getElementById(imgName).title = strNOK;
    }
}

function validateCompareField(fieldName, fieldName2, imgName, strOK, strNOK) {

    txtElement = document.getElementById(fieldName);
    txtElement2 = document.getElementById(fieldName2);

    if (txtElement.value != "" && txtElement.value == txtElement2.value) {
        document.getElementById(imgName).src = '/img/yes.png';
        document.getElementById(imgName).alt = strOK;
        document.getElementById(imgName).title = strOK;
    }
    else {
        document.getElementById(imgName).src = '/img/no.png';
        document.getElementById(imgName).alt = strNOK;
        document.getElementById(imgName).title = strNOK;
    }
}

function nakupNaFirmu(elm, elmrow) {

    var checkbox = document.getElementById(elm);
    var row = document.getElementById(elmrow);

    row.style.display = checkbox.checked ? "table-row" : "none";
}

function dodaciAdresaStejna(elm, suffix) {

    var checkbox = document.getElementById(elm);
    var row1 = document.getElementById('mainContent_adresa2a' + suffix);
    var row2 = document.getElementById('mainContent_adresa2b' + suffix);
    var row3 = document.getElementById('mainContent_adresa2c' + suffix);
    var row4 = document.getElementById('mainContent_adresa2d' + suffix);

    row1.style.display = !checkbox.checked ? "table-row" : "none";
    row2.style.display = !checkbox.checked ? "table-row" : "none";
    row3.style.display = !checkbox.checked ? "table-row" : "none";
    row4.style.display = !checkbox.checked ? "table-row" : "none";
}

function directionChange(direction) {

    var zal1 = document.getElementById('mainContent_zak1');
    var zal2 = document.getElementById('mainContent_zaknew');
    var zal3 = document.getElementById('mainContent_zakreg');

    zal1.style.display = direction === 1 ? "block" : "none";
    zal2.style.display = direction === 2 ? "block" : "none";
    zal3.style.display = direction === 3 ? "block" : "none";

    var zalzal1 = document.getElementById('mainContent_td1');
    var zalzal2 = document.getElementById('mainContent_td2');
    var zalzal3 = document.getElementById('mainContent_td3');

    var hidden = document.getElementById('mainContent_hidSmer');
    hidden.value = direction;

    switch (direction) {

        case 1:
            zalzal1.classList.add("this");
            zalzal2.classList.remove("this");
            zalzal3.classList.remove("this");
            break;
        case 2:
            zalzal2.classList.add("this");
            zalzal1.classList.remove("this");
            zalzal3.classList.remove("this");
            break;
        case 3:
            zalzal3.classList.add("this");
            zalzal1.classList.remove("this");
            zalzal2.classList.remove("this");
            break;
    }
}

function Step1Continue() {

    var hidden = document.getElementById('mainContent_hidSmer');
    var direction = hidden.value;

    switch (direction) {

        case "1":
            break;
        case "2":
            break;
        case "3":
            var orderLogin = new pneuMalik.RequestHelper("GET", "/api/eshop/login?user=abc&password=cde");
            orderLogin.makeRequest("orderLogin").then(function (response) {

                var res = JSON.parse(response);

            }, function (reject) {

                alert("Login error: " + reject.message);
            });
            break;
    }
}