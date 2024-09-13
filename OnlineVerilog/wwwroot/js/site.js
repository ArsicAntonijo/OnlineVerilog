// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function hideShow() {
    //console.log("heheh");
    let list = document.getElementById("exampleList");
    let btn = document.getElementById("nsbtn");
    let nav = document.getElementById("vericalNav");
    let panel = document.getElementById("mainPanel");
    if (list.style.display === "none") {
        list.style.display = "block";
        btn.innerHTML = "<i class=\"fa fa-angle-double-left\"></i>";
        nav.style.width = "15%";
        panel.style.width = "85%";
    } else {
        list.style.display = "none";
        btn.innerHTML = "<i class=\"fa fa-angle-double-right\"></i>";
        nav.style.width = "2%";
        panel.style.width = "98%";
    }
}

function hideList(id) {
    var myList = document.getElementById(id);

    if (myList.style.display === "none") {
        myList.style.display = "block";
    } else {
        myList.style.display = "none";
    }
}