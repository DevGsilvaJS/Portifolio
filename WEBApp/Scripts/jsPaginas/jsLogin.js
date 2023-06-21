$(document).ready(function () {

    jQueryInit();

});

function jQueryInit() {

    $("#edtUsuario").val("ADMIN");
    $("#edtSenha").val("ADMIN");
    
}

$("#btnLogar").click(function () {

    fnLogin();
});


$("#edtUsuario").on("input", function () {

    let valor = $(this).val();
    $(this).val(valor.toUpperCase());
});

$("#edtSenha").on("input", function () {

    let valor = $(this).val();
    $(this).val(valor.toUpperCase());
});

function fnLogin() {

    $.ajax({
        type: "POST",
        url: "/Login/ValidaLogin",
        data: { Usuario: $("#edtUsuario").val(), Senha: $("#edtSenha").val() },
        dataType: "JSON",
        beforeSend: function () {        
        },
        success: function (result) {

            debugger;

            //return receitaWorkFlow.RetornaRCTIDPedidoLab(PDLID);
            if (result.isLogado.TbEmail.EMLEMAIL != null) {
                debugger;

                window.location.href = "/Home";

            } else {

                window.location.href = "/Login"
            }
        },
        error: function (jqXHR, exception) {

            if (jqXHR.status === 0) {
                alert('Not connect.n Verify Network.');
            } else if (jqXHR.status == 404) {
                alert('Requested page not found. [404]');
            } else if (jqXHR.status == 500) {
                alert('Internal Server Error [500].');
            } else if (exception === 'parsererror') {
                alert('Requested JSON parse failed.');
                var Url = window.location.origin = window.location.protocol + "//" + window.location.hostname + (window.location.port ? ':' + window.location.port : '');
                window.location.href = Url + "/Login";
            } else if (exception === 'timeout') {
                alert('Time out error.');
            } else if (exception === 'abort') {
                alert('Ajax request aborted.');
            } else {
                alert('Uncaught Error.n' + jqXHR.responseText);
            }
        },
        complete: function () {

        }
    });
}