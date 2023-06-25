
var oTabFornecedor = null;
var _Fornecedor = new Object();
var STATUS = 'CONSULTA';




$(document).ready(function () {

    jQueryInit();
});



function jQueryInit() {

    fnCriaTela();
}

function fnListagem() {
    $.alert({
        title: '',
        content: 'Fornecedor gravado com sucesso!',
    });
    $("#aLista").click();
}

function fnCriaTela() {

    oTabFornecedor = $("#tbFornecedor").DataTable({
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
            { sWidth: '15%', "bSortable": false },
            { sWidth: '1%' },//Numero          
            { sWidth: '20%' },//Titulo
            { sWidth: '10%' },//Titulo
            { sWidth: '10%' },//Titulo           

        ]
    });

    fnListaDados();

    //$("#txtCnpj").mask("99.999.999/9999-99");
    //$("#txtBuscaCep").mask("99999-999");

    //$('#txtTel').mask('(00) 0000-00009');
    //$('#txtTel').blur(function (event) {
    //    if ($(this).val().length == 15) { // Celular com 9 dígitos + 2 dígitos DDD e 4 da máscara
    //        $('#txtTel').mask('(00) 00000-0009');
    //    } else {
    //        $('#txtTel').mask('(00) 0000-00009');
    //    }
    //});


}

$(document).ready(function () {
    $('.nav-tabs a').on('click', function (e) {
        e.preventDefault();
        $(this).tab('show');
    });
});

function fnEdicao() {

    $("#acadastro").click();
}

function fnRetornaCadJaExistente() {
    $.alert({
        title: '',
        content: 'CNPJ do fornecedor já cadastrado!',
    });
}

function fnListaDados() {
    debugger;
    $.ajax({

        type: "POST",
        contentType: "application/json",
        url: "Fornecedor/listaFornecedor",
        data: {},
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {

        },
        success: function (result) {

            var Lista = result.lsFornecedor;
            oTabFornecedor.clear().draw();

            var ListaFornecedor = new Array();


            if (Lista.length > 0) {
                debugger;
                for (var i = 0; i < Lista.length; i++) {


                    var btnEditar = '<button id="' + Lista[i].IDFORNECEDOR +
                        '"  name="btnEdicao" type="button" class="btn  btn-primary" onClick="fnEditFornecedor(this)">Editar</button>';

                    var btnExcluir = '<button id="' + Lista[i].IDFORNECEDOR +
                        '"  name="btnDeletar" type="button" class="btn  btn-danger" onClick="FnDeletFornecedor(this)">Deletar</button>';

                    var Linha = [btnEditar + btnExcluir,
                    Lista[i].IDFORNECEDOR,
                    Lista[i].FORFANTASIA,
                    Lista[i].FORCNPJ,
                    Lista[i].FORIE,
                    ];
                    ListaFornecedor[i] = Linha;
                }
                oTabFornecedor.rows.add(ListaFornecedor).draw();
            }
        },
        error: function (jqXHR, exception) {

        },
        complete: function () {

        }
    });
}

function fnSalvaDados() {

    _Fornecedor.FORFANTASIA = $("#txtFantasia").val();
    _Fornecedor.FORRAZAO = $("#txtRazaoSocial").val();
    _Fornecedor.FORCNPJ = $("#txtCnpj").val();
    _Fornecedor.FORIE = $("#txtIe").val();
    _Fornecedor.IDCENTROCUSTO = $("#CentroCustoID").val();
    _Fornecedor.IDPLANOCONTAS = $("#PlanoContasID").val();
    _Fornecedor.DDDTELEFONE = $("#txtDDDTelefone").val();
    _Fornecedor.TELEFONE = $("#txtTelefone").val();
    _Fornecedor.DDDCELULAR = $("#txtDDDCelular").val();
    _Fornecedor.CELULAR = $("#txtCelular").val();
    _Fornecedor.FOREMAIL = $("#txtEmail").val();
    _Fornecedor.Fantasia = $("#dllRegimeTributario").val();
    _Fornecedor.ENDCEP = $("#txtCep").val();
    _Fornecedor.ENDUF = $("#ddlUf").val();
    _Fornecedor.ENDCIDADE = $("#txtCidade").val();
    _Fornecedor.ENDBAIRRO = $("#txtBairro").val();
    _Fornecedor.ENDLOGRADOURO = $("#txtLogradouro").val();
    _Fornecedor.ENDTIPOENDERECO = $("#ddlTipoEndereco").val();
    _Fornecedor.ENDNUMERO = $("#txtNumero").val();
    _Fornecedor.ENDCOMPLEMENTO = $("#txtComplemento").val();

    $.ajax({

        type: "POST",
        contentType: "application/json",
        url: "Fornecedor/InsertFornecedor",
        data: JSON.stringify(_Fornecedor),
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {

        },
        success: function (result) {
            debugger;

            if (result == "OK" || result.result == "OK") {
                fnListagem();
                _Fornecedor = new Object();
                fnListaDados();
            }
            else {
                fnRetornaCadJaExistente();
            }
        },
        error: function (jqXHR, exception) {

        },
        complete: function () {
        }
    });

}

function fnEditFornecedor(idFornecedor) {

    $.ajax({

        type: "GET",
        url: "Fornecedor/SearchFornecedor",
        data: { idFornecedor: idFornecedor.id },
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {

        },
        success: function (result) {

            debugger;

            STATUS = 'ALTERACAO';

            fnEdicao();

            _Fornecedor = result.fornecedor;

            $("#IDfornecedor").val(_Fornecedor.IDFORNECEDOR);
            $("#txtFantasia").val(_Fornecedor.FORFANTASIA);
            $("#txtRazaoSocial").val(_Fornecedor.FORRAZAO);
            $("#txtCnpj").val(_Fornecedor.FORCNPJ);
            $("#txtIe").val(_Fornecedor.FORIE);
            $("#CentroCustoID").val(_Fornecedor.IDCENTROCUSTO);
            $("#PlanoContasID").val(_Fornecedor.IDPLANOCONTAS);
            $("#txtDDDTelefone").val(_Fornecedor.DDDTELEFONE);
            $("#txtTelefone").val(_Fornecedor.TELEFONE);
            $("#txtDDDCelular").val(_Fornecedor.DDDCELULAR);
            $("#txtCelular").val(_Fornecedor.CELULAR);
            $("#txtEmail").val(_Fornecedor.FOREMAIL);
            $("#txtCep").val(_Fornecedor.ENDCEP);
            $("#ddlUf").val(_Fornecedor.ENDUF);
            $("#txtCidade").val(_Fornecedor.ENDCIDADE);
            $("#txtBairro").val(_Fornecedor.ENDBAIRRO);
            $("#txtLogradouro").val(_Fornecedor.ENDLOGRADOURO);
            $("#ddlTipoEndereco").val(_Fornecedor.ENDTIPOENDERECO);
            $("#txtNumero").val(_Fornecedor.ENDNUMERO);
            $("#txtComplemento").val(_Fornecedor.ENDCOMPLEMENTO);




        },
        error: function (jqXHR, exception) {
        },
        complete: function () {
        }
    });
}

function fnAtualizarDados() {

    var _Fornecedor = new Object();


    _Fornecedor.IDFORNECEDOR = $("#IDfornecedor").val();
    _Fornecedor.FORFANTASIA = $("#txtFantasia").val();
    _Fornecedor.FORRAZAO = $("#txtRazaoSocial").val();
    _Fornecedor.FORCNPJ = $("#txtCnpj").val();
    _Fornecedor.FORIE = $("#txtIe").val();
    _Fornecedor.IDCENTROCUSTO = $("#CentroCustoID").val();
    _Fornecedor.IDPLANOCONTAS = $("#PlanoContasID").val();
    _Fornecedor.DDDTELEFONE = $("#txtDDDTelefone").val();
    _Fornecedor.TELEFONE = $("#txtTelefone").val();
    _Fornecedor.DDDCELULAR = $("#txtDDDCelular").val();
    _Fornecedor.CELULAR = $("#txtCelular").val();
    _Fornecedor.FOREMAIL = $("#txtEmail").val();
    _Fornecedor.Fantasia = $("#dllRegimeTributario").val();
    _Fornecedor.ENDCEP = $("#txtCep").val();
    _Fornecedor.ENDUF = $("#ddlUf").val();
    _Fornecedor.ENDCIDADE = $("#txtCidade").val();
    _Fornecedor.ENDBAIRRO = $("#txtBairro").val();
    _Fornecedor.ENDLOGRADOURO = $("#txtLogradouro").val();
    _Fornecedor.ENDTIPOENDERECO = $("#ddlTipoEndereco").val();
    _Fornecedor.ENDNUMERO = $("#txtNumero").val();
    _Fornecedor.ENDCOMPLEMENTO = $("#txtComplemento").val();

    $.ajax({

        type: "POST",
        contentType: "application/json",
        url: "Fornecedor/AtualizarDados",
        data: JSON.stringify(_Fornecedor),
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {

        },
        success: function (result) {
            debugger;

            if (result == "OK" || result.result == "OK") {
                fnListagem();
                _Fornecedor = new Object();
                fnListaDados();
            }
            else {
                return false;
            }
        },
        error: function (jqXHR, exception) {

        },
        complete: function () {
        }
    });

}

function FnDeletFornecedor(idFornecedor) {

    debugger;


    $.ajax({

        type: "GET",
        contentType: "application/json",
        url: "Fornecedor/DeletFornecedor",
        data: { idFornecedor: idFornecedor.id },
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

function fnBuscaCep() {

    $.getJSON("https://viacep.com.br/ws/" + $("#txtCEP").val() + "/json/?callback=?", function (dados) {


        debugger;

        if (!("erro" in dados)) {



            //Atualiza os campos com os valores da consulta.
            $("#txtEndereco").val(dados.logradouro);
            $("#txtBairro").val(dados.bairro);
            $("#txtCidade").val(dados.localidade);
            $("#txtUf").val(dados.uf);

        } //end if.
        else {
            //CEP pesquisado não foi encontrado.
            limpa_formulário_cep();
            alert("CEP não encontrado.");
        }
    });
}

function lsPlanoContas() {

    $.ajax({

        type: "POST",
        contentType: "application/json",
        url: "PlanoDeContas/ListarPlanos",
        data: JSON.stringify(),
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {

        },
        success: function (result) {

            if (result != null) {

                var ListaPlanos = result.lsPlanos;

                var options = '<option value="0">Selecione o Plano de contas</option>';;
                $.each(ListaPlanos, function (key, val) {

                    options += '<option value="' + val.IDPLANOCONTAS + '">' + val.PLNDESCRICAO + '</option>';
                });

                $("#PlanoContasID").html(options);

            }


        },
        error: function (jqXHR, exception) {

        },
        complete: function () {
        }
    });

}

function lsCentroCusto() {

    $.ajax({

        type: "POST",
        contentType: "application/json",
        url: "CentroDeCusto/ListaCentroCusto",
        data: JSON.stringify(),
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {

        },
        success: function (result) {
            debugger;

            if (result != null) {
                var ListaCDC = result.lsCentroCusto;
                var options = '<option value="0">Selecione o Plano de contas</option>';;

                $.each(ListaCDC, function (key, val) {
                    options += '<option value="' + val.IDCENTROCUSTO + '">' + val.CDCDESCRICAO + '</option>';
                });

                $("#CentroCustoID").html(options);
            }
        },
        error: function (jqXHR, exception) {

        },
        complete: function () {
        }
    });
}










