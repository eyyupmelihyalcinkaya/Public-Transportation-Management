// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener("DOMContentLoaded", function () {
    var form = document.querySelector("form");
    form.addEventListener("submit", function (event) {
        var userName = form.querySelector('input[name="Username"]').value;
        var password = form.querySelector('input[name="password"]').value;
        if (userName.trim() === "" || password.trim() === "") {
            alert("Username and password cannot be empty.");
            e.preventDefault();
        }
    });
});
