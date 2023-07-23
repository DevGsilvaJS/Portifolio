////$(document).ready(function () {

////});


$("#btnCriar").click(function () {
    fnCriarTables();
});

$("#btnExcluir").click(function () {
    fnDropTables();
});

function fnCriarTables() {

    $.ajax({

        type: "POST",
        contentType: "application/json",
        url: "ResetTables/CriarTables",
        data: JSON.stringify(),
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {

        },
        success: function (result) {
            debugger;

        },
        error: function (jqXHR, exception) {

        },
        complete: function () {
        }
    });

}

function fnDropTables() {

    $.ajax({

        type: "POST",
        contentType: "application/json",
        url: "ResetTables/DeletTables",
        data: JSON.stringify(),
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {

        },
        success: function (result) {
            debugger;

        },
        error: function (jqXHR, exception) {

        },
        complete: function () {
        }
    });

}