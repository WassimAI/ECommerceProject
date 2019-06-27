﻿$(function () {
    /*
    *Preview Selected Image
    */

    function readURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $("img#imgPreview").attr("src", e.target.result).width(200).height(200);
            }

            reader.readAsDataURL(input.files[0]);
        }
    }

    $("#ImageUpload").change(function () {
        readURL(this);
    });

});