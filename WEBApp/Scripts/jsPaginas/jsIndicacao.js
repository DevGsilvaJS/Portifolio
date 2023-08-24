oTabIndicacao = null;
_Indicacao = null;
IDPRINCIPAL = null;
$(document).ready(function () {
    jQueryInit();
});

function jQueryInit() {
    fnCriaTela();
}

function fnCriaTela() {

    oTabIndicacao = $("#tbTipoVenda").DataTable({
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
            { sWidth: '08%', "bSortable": false },
            { sWidth: '20%' },//Numero          
            { sWidth: '20%' },//Titulo
            { sWidth: '20%' },//Titulo
        ]
    });

    fnListaDados();

}

function fnRetornaObjInclusao() {

    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "Indicacao/RetornaObjInclusao",
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {

        },
        success: function (result) {

            debugger;

            debugger;
            _Indicacao = result.ObjInclusao;

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
    debugger;

    _Indicacao.INDID = IDPRINCIPAL;
    _Indicacao.INDDESCRICAO = $("#txtDescricaoIndicacao").val();
    _Indicacao.INDDEFAULTVENDA = $("#ckbDefaultVendaIndicacao").prop("checked") ? 1 : 0;


    $.ajax({

        type: "POST",
        contentType: "application/json",
        url: "Indicacao/GravarIndicacao",
        data: JSON.stringify(_Indicacao),
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

function fnListaDados() {

    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "Indicacao/ListaDados",
        data: {},
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {
        },
        success: function (result) {


            var Lista = result.lista;
            oTabIndicacao.clear().draw();

            var ListaIndicacao = new Array();
            if (Lista.length > 0) {

                for (var i = 0; i < Lista.length; i++) {
                    debugger;

                    var btnEditar = '<button id="' + Lista[i].INDID + '"  name="btnEdicao" type="button" class="btn  btn-primary" onClick="fnEditarIndicacao(this)">Editar</button>';
                    var btnExcluir = '<button id="' + Lista[i].INDID + '"  name="btnDeletar" type="button" class="btn  btn-danger" onClick="fnExcluirIndicacao(this)">Deletar</button>';

                    var Linha = [btnEditar + btnExcluir,
                    Lista[i].INDDESCRICAO,
                    Lista[i].INDDEFAULTVENDA,
                    Lista[i].INDSTATUS
                    ];
                    ListaIndicacao[i] = Linha;
                }
                oTabIndicacao.rows.add(ListaIndicacao).draw();

            }

        },
        error: function (jqXHR, exception) {
        },
        complete: function () {
        }
    });
}

function fnExcluirIndicacao(indid) {


    $.ajax({
        type: "GET",
        contentType: "application/json",
        url: "Indicacao/ExcluirIndicacao",
        data: { indid: indid.id },
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {
        },
        success: function (result) {

            fnListaDados();
        },
        error: function (jqXHR, exception) {
        },
        complete: function () {
        }
    });
}

function fnEditarIndicacao(indid) {

    $.ajax({
        type: "GET",
        contentType: "application/json",
        url: "Indicacao/GetIndicacaoByID",
        data: { indid: indid.id },
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {
        },
        success: function (result) {
            debugger;
            _Indicacao = result.retorno

            IDPRINCIPAL = _Indicacao.INDID;
            $("#txtDescricaoIndicacao").val(_Indicacao.INDDESCRICAO);
            $("#ckbDefaultVendaIndicacao").prop("checked", _Indicacao.INDDEFAULTVENDA == 1);

            STATUS = 'ALTERACAO';
            $("#aCadastro").tab('show');
            $("#btnSalvarFormulario").css('display', 'block');
        },
        error: function (jqXHR, exception) {
        },
        complete: function () {
        }
    });
}


