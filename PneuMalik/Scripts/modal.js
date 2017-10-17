function toTheCart(id, price, sText) {

    var countElement = document.getElementById("productCount");
    var count = 1;

    if (countElement) {
        count = countElement.value;
    }

    var getStatus = new pneuMalik.RequestHelper("GET", "/api/eshop/cart?id=" + id + "&count=" + count + "&price=" + price);
    getStatus.makeRequest("toTheCart").then(function (response) {

        openAlertWindow('Položka přidána do košíku.<br >' + sText + '&nbsp;' + count + ' ks');

        var cartRows = JSON.parse(response);
        console.log(cartRows);

        var itemCountElement = document.getElementById("u_Hlavicka11_u_KosikInfo1_hlKosik");
        var cartPriceElement = document.getElementById("u_Hlavicka11_u_KosikInfo1_CenaNakupu")

        var totalPrice = 0;
        for (index = 0; index < cartRows.length; ++index) {

            totalPrice += cartRows[index]["PriceTmp"];
        }

        itemCountElement.innerHTML = cartRows.length == 0 ? "bez položek" : cartRows.length + " položek";
        cartPriceElement.innerHTML = totalPrice + ",00 Kč";

    }, function (reject) {
        console.log("Cart insert error: " + reject.message);
    });
}

function openAlertWindow(sText) {
    
    showCover();

    var alert = document.getElementById("alert");

    if (!alert) {
        alert = document.createElement("div");
        alert.id = "alert";

        var alertObsah = document.createElement("div");
        alertObsah.classList.add("alertobsah");

        var okElement = document.createElement("p");
        okElement.classList.add("fr");
        okElement.innerHTML = "<img src=\"/img/ok.gif\" alt=\"ok\" />";
        alertObsah.appendChild(okElement);

        var textElement = document.createElement("p");
        textElement.id = "paragkosik";
        alertObsah.appendChild(textElement);

        var buttonsElement = document.createElement("p");
        buttonsElement.innerHTML = "<p>"
            + "<input type=\"button\" value=\"Přejít na košík\" class=\"enterA\" onclick=\"document.getElementById('cover').style.display='none';document.getElementById('alert').style.display='none';window.location.href='/eshop/kosik';\" />&nbsp;"
            + "<input type=\"button\" value=\"Pokračovat v nákupu\" class=\"enterA\" onclick=\"document.getElementById('cover').style.display='none';document.getElementById('alert').style.display='none';window.location.reload(true);\" />"
            + "</p>";
        alertObsah.appendChild(buttonsElement);

        var cleanerElement = document.createElement("div");
        cleanerElement.classList.add("cleaner");
        cleanerElement.innerHTML = "<hr />";
        alertObsah.appendChild(cleanerElement);

        alert.appendChild(alertObsah);
        document.getElementById("vse").appendChild(alert);
    }

    document.getElementById("paragkosik").innerHTML = sText;

    alert.style.left = parseInt(((document.documentElement.clientWidth / 2) - (alert.clientWidth/2)) + document.documentElement.scrollLeft)+"px";
    alert.style.top = parseInt(((document.documentElement.clientHeight / 2) - (alert.clientHeight)) + document.documentElement.scrollTop)+"px";
}

function openPreloader() {

    showCover();

    var hidden = document.getElementById("preloader");
    hidden.style.display = "block";

    hidden.style.left = parseInt(((winW() / 2) - (hidden.clientWidth / 2)) + ScrollLeft()) + "px";
    hidden.style.top = parseInt(((winH() / 2) - (hidden.clientHeight)) + ScrollTop()) + "px";
}

function hidePreloader() {

    var cover = document.getElementById("cover");
    cover.style.display = "none";

    var hidden = document.getElementById("preloader");
    hidden.style.display = "none";
}

function showCover() {

    var cover = document.getElementById("cover");
    if (!cover) {
        cover = document.createElement("div");
        cover.innerHTML = "&nbsp;";
        cover.id = "cover";
        document.getElementById("vse").appendChild(cover);
    }

    cover.style.width = parseInt(document.body.clientWidth) + "px";
    cover.style.height = parseInt(document.body.clientHeight) + "px";
}

function ScrollTop() {
    return (document.body.scrollTop || document.documentElement.scrollTop);
}

function ScrollLeft() {
    return (document.body.scrollLeft || document.documentElement.scrollLeft)
}

function winH() {
    if (window.innerHeight)
    /* NN4 a kompatibilní prohlížeče */
        return window.innerHeight;
    else if
   (document.documentElement &&
   document.documentElement.clientHeight)
    /* MSIE6 v std. režimu - Opera a Mozilla
    již uspěly s window.innerHeight */
        return document.documentElement.clientHeight;
    else if
   (document.body && document.body.clientHeight)
    /* starší MSIE + MSIE6 v quirk režimu */
        return document.body.clientHeight;
    else
        return null;
}

function winW() {
    if (window.innerWidth)
    /* NN4 a kompatibilní prohlížeče */
        return window.innerWidth;
    else if
   (document.documentElement &&
   document.documentElement.clientWidth)
    /* MSIE6 v std. režimu - Opera a Mozilla
    již uspěly s window.innerHeight */
        return document.documentElement.clientWidth;
    else if
   (document.body && document.body.clientWidth)
    /* starší MSIE + MSIE6 v quirk režimu */
        return document.body.clientWidth;
    else
        return null;
}
