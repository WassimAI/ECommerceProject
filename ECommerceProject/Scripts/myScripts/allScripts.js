/*Logout*/
$(function () {
    $("a#logout-btn").on("click", function (e) {
        e.preventDefault();
        var pageURL = $(location).attr("href");

        $.post("/UserAccount/Logout", {returnUrl : pageURL}, function (data) {
            location.href = data.returnUrl;
        })
    });
});