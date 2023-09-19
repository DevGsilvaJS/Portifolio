oTabEntradaEstoque = null;
_EntradaEstoque = null;
IDPRINCIPAL = null;
$(document).ready(function () {
    jQueryInit();
});
function jQueryInit() {
    fnCriaTela();
}
function fnCriaTela() {

    oTabEntradaEstoque = $("#tbEntradaEstoque").DataTable({
        "oLanguage": {
            "sProcessing": "Aguarde enquanto os dados são carregados ...",
            "sLengthMenu": "Mostrar _MENU_ registros por pagina",
            "sPaginationType": "bootstrap",
            "sZeroRecords": "Nenhum registro correspondente ao critério encontrado",
            "sInfoEmpty": "Exibindo 0 a 0 de 0 registros",
            "sInfo": "Exibindo de _START_ a _END_ de _TOTAL_ registros",
            "sInfoFiltered": "",
            "sSearch": "Procurar",
            "oPaginate": {
                "sFirst": "Primeiro",
                "sPrevious": "Anterior",
                "sNext": "Próximo",
                "sLast": "Último"
            }
        },

        "iDisplayLength": 50,
        "aLengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]],
        "bRetrieve": false,
        "bFilter": true,
        "bSortClasses": true,
        "bLengthChange": false,
        "bPaginate": true,
        "bInfo": true,
        "bJQueryUI": false,
        "bAutoWidth": false,
        "aaSorting": [[1, "asc"]],
        "aoColumns": [
            { sWidth: '06%', "bSortable": false },
            { sWidth: '20%' },//Numero          
            { sWidth: '20%' },//Titulo
        ]
    });

    fnRetornaObjInclusao();
    fnSetDataAtual();
    fnRetornaComboFornecedores();
    fnRetornaComboCfops();


    debugger;


}
function fnRetornaObjInclusao() {

    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "EntradaEstoque/RetornaObjInclusao",
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {

        },
        success: function (result) {

            debugger;
            _EntradaEstoque = result.ObjInclusao;

        },
        error: function (jqXHR, exception) {
        },
        complete: function () {
        }
    });

}
$(document).ready(function () {
    $('.nav-tabs a').on('click', function (e) {
        e.preventDefault();
        $(this).tab('show');
    });
});
$("#aLista").click(function () {
    $("#btnSalvarFormulario").css('display', 'none');
})
$("#aCadastro").click(function () {
    STATUS = 'INSERCAO';
    $("#btnSalvarFormulario").css('display', 'block');
    fnRetornaObjInclusao();
    /*    fnDadosVendedor();*/
});

$("#btnPesquisarProduto").click(function () {
    alert('aqui');
})
$(document).ready(function () {
    $('#btnSalvarFormulario').on('click', function () {

        switch (STATUS) {
            case 'INSERCAO':
                fnSalvarDados();
                $("#aLista").tab('show');
                fnListaDados();
                break;
            case 'ALTERACAO':
                fnSalvarDados();
                $("#aLista").tab('show');
                fnListaDados();
                break;
            default:
                break;
        }
    });
});
function fnSalvarDados() {

    _EntradaEstoque.TPVID = IDPRINCIPAL;
    _EntradaEstoque.TPVDESCRICAO = $("#txtDescricaoEntradaEstoque").val();
    _EntradaEstoque.TPVDEFAULTVENDA = $("#ckbDefaultVenda").prop("checked") ? 1 : 0;


    $.ajax({

        type: "POST",
        contentType: "application/json",
        url: "EntradaEstoque/GravarEntradaEstoque",
        data: JSON.stringify(_EntradaEstoque),
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {

        },
        success: function (result) {
            debugger;

            result.retorno = "OK";

            fnListaDados();

        },
        error: function (jqXHR, exception) {

        },
        complete: function () {
        }
    });
}

function fnRetornaComboFornecedores() {

    $.ajax({

        type: "POST",
        contentType: "application/json",
        url: "EntradaEstoque/RetornaComboFornecedores",
        data: JSON.stringify(),
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {

        },
        success: function (result) {


            if (result != null) {

                var lista = result.listaFornecedores;

                var options = '<option value="0">Selecione o Fornecedor</option>';;
                $.each(lista, function (key, val) {

                    options += '<option value="' + val.FORID + '">' + val.TbPessoa.PESNOME + '</option>';
                });

                $("#sslFornecedor").html(options);

            }


        },
        error: function (jqXHR, exception) {

        },
        complete: function () {
        }
    });

}

function fnRetornaComboCfops() {

    $.ajax({

        type: "POST",
        contentType: "application/json",
        url: "EntradaEstoque/RetornaComboCfops",
        data: JSON.stringify(),
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {

        },
        success: function (result) {

            debugger;

            if (result != null) {

                var lista = result.listaCfops;

                var options = '<option value="0">Selecione o CFOP</option>';;
                $.each(lista, function (key, val) {

                    options += '<option value="' + val.COPID + '">' + val.COPDESCRICAO + '</option>';
                });

                $("#sslCFOP").html(options);

            }


        },
        error: function (jqXHR, exception) {

        },
        complete: function () {
        }
    });

}

function fnSetDataAtual(){

    var dataAtual = new Date();
    dataAtual.setMinutes(dataAtual.getMinutes() - dataAtual.getTimezoneOffset()); // Ajusta para UTC

    // Formata a data para o formato YYYY-MM-DD, que é o formato esperado pelo input type="date"
    var dataFormatada = dataAtual.toISOString().slice(0, 10);

    // Define a data atual no campo input
    document.getElementById('dtDataMvm').value = dataFormatada;
}



