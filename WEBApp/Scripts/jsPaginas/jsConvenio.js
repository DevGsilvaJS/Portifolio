oTabConvenio = null;
_Convenio = null;
IDPRINCIPAL = null;
$(document).ready(function () {
    jQueryInit();
});

function jQueryInit() {
    fnCriaTela();
}

function fnCriaTela() {

    oTabConvenio = $("#tbConvenio").DataTable({
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
            { sWidth: '10%', "bSortable": false },
            { sWidth: '15%' },
            { sWidth: '15%' },
            { sWidth: '15%' },
            { sWidth: '15%' },
            { sWidth: '15%' },
        ]
    });

    fnListaDados();

    $(document).ready(function () {
        $('#txtTelefoneConvenio').inputmask('(99) 9999-9999');
        $('#txtCelularConvenio').inputmask('(99) 99999-9999');
    });

}

function fnRetornaObjInclusao() {

    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "Convenio/RetornaObjInclusao",
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {

        },
        success: function (result) {

            debugger;
            _Convenio = result.retorno;

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

    _Convenio.CVNCONTRATO = $("#txtNumeroContratoConvenio").val();
    _Convenio.TbPessoa.PESNOME = $("#txtEmpresaConvenio").val();
    _Convenio.CVNDESCONTO = $("#txtDescontoConvenio").val();
    _Convenio.CVNOBSERVACAO = $("#txtObservacaoConvenio").val();

    _Convenio.CVNNAOAPARECEVENDA = $("#ckbExibeVendaConvenio").prop("checked") ? 1 : 0;

    var dddTelefone = $("#txtTelefoneConvenio").val().replace(/[-() ]+/g, "").substring(0, 2);
    var telefone = $("#txtTelefoneConvenio").val().replace(/[-() ]+/g, "").slice(2);
    _Convenio.TbTelefone.TELDDD = dddTelefone;
    _Convenio.TbTelefone.TELNUMERO = telefone;

    var dddCelular = $("#txtCelularConvenio").val().replace(/[-() ]+/g, "").substring(0, 2);
    var celular = $("#txtCelularConvenio").val().replace(/[-() ]+/g, "").slice(2);
    _Convenio.TbTelefone.TELDDDC = dddCelular;
    _Convenio.TbTelefone.TELCELULAR = celular;

    _Convenio.TbEndereco.EDNCEP = $("#txtValorCep").val();
    _Convenio.TbEndereco.EDNUF = $("#ddlUf").val();
    _Convenio.TbEndereco.EDNCIDADE = $("#txtCidade").val();
    _Convenio.TbEndereco.EDNBAIRRO = $("#txtBairro").val();
    _Convenio.TbEndereco.EDNLOGRADOURO = $("#txtLogradouro").val();
    _Convenio.TbEndereco.EDNNUMERO = $("#txtNumero").val();
    _Convenio.TbEndereco.EDNCOMPLEMENTO = $("#txtComplemento").val();



    $.ajax({

        type: "POST",
        contentType: "application/json",
        url: "Convenio/GravarConvenio",
        data: JSON.stringify(_Convenio),
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
        url: "Convenio/ListaDados",
        data: {},
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {
        },
        success: function (result) {

            debugger;
            var Lista = result.lsConvenio;
            oTabConvenio.clear().draw();

            var ListaConvenio = new Array();
            if (Lista.length > 0) {

                for (var i = 0; i < Lista.length; i++) {
                    debugger;

                    var btnEditar = '<button id="' + Lista[i].CVNID + '"  name="btnEdicao" type="button" class="btn  btn-primary" onClick="fnEditarConvenio(this)">Editar</button>';
                    var btnExcluir = '<button id="' + Lista[i].CVNID + '"  name="btnDeletar" type="button" class="btn  btn-danger" onClick="fnExcluirConvenio(this)">Deletar</button>';

                    var Linha = [btnEditar + btnExcluir,
                    Lista[i].CVNCONTRATO,
                    Lista[i].TbPessoa.PESNOME,
                    Lista[i].CVNDESCONTO,
                    Lista[i].TbTelefone.TELCELULAR,
                    Lista[i].CVNNAOAPARECEVENDA == ('1') ? "ATIVO" : "INATIVO",
                    ];
                    ListaConvenio[i] = Linha;
                }
                oTabConvenio.rows.add(ListaConvenio).draw();

            }

        },
        error: function (jqXHR, exception) {
        },
        complete: function () {
        }
    });
}

function fnExcluirConvenio(cvnid) {


    $.ajax({
        type: "GET",
        contentType: "application/json",
        url: "Convenio/ExcluirConvenio",
        data: { cvnid: cvnid.id },
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

function fnEditarConvenio(cvnid) {

    $.ajax({
        type: "GET",
        contentType: "application/json",
        url: "Convenio/GetConvenioByID",
        data: { cvnid: cvnid.id },
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {
        },
        success: function (result) {
            debugger;
            _Convenio = result.retorno

            IDPRINCIPAL = _Convenio.CVNID;


            $("#txtNumeroContratoConvenio").val(_Convenio.CVNCONTRATO);
            $("#txtEmpresaConvenio").val(_Convenio.TbPessoa.PESNOME);
            $("#txtDescontoConvenio").val(_Convenio.CVNDESCONTO);
            $("#txtObservacaoConvenio").val(_Convenio.CVNOBSERVACAO);
            $("#ckbExibeVendaConvenio").val(_Convenio.CVNNAOAPARECEVENDA);


            $("#txtTelefoneConvenio").val(_Convenio.TbTelefone.TELDDD + _Convenio.TbTelefone.TELNUMERO);
            $("#txtCelularConvenio").val(_Convenio.TbTelefone.TELDDDC + _Convenio.TbTelefone.TELCELULAR)


            $("#txtValorCep").val(_Convenio.TbEndereco.EDNCEP);
            $("#ddlUf").val(_Convenio.TbEndereco.EDNUF);
            $("#txtCidade").val(_Convenio.TbEndereco.EDNCIDADE);
            $("#txtBairro").val(_Convenio.TbEndereco.EDNBAIRRO);
            $("#txtLogradouro").val(_Convenio.TbEndereco.EDNLOGRADOURO);
            $("#txtNumero").val(_Convenio.TbEndereco.EDNNUMERO);
            $("#txtComplemento").val(_Convenio.TbEndereco.EDNCOMPLEMENTO);


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

$("#btnBuscarCep").click(function () {
    buscaCep($("#txtValorCep").val());
})

function buscaCep(cep) {
    $.ajax({
        url: "https://viacep.com.br/ws/" + cep + "/json/",
        dataType: "json",
        success: function (data) {

            if (data.erro) {
                // Trate o caso em que o CEP informado é inválido
            } else {
                $("#txtLogradouro").val(data.logradouro);
                $("#txtBairro").val(data.bairro);
                $("#txtCidade").val(data.localidade);
                $("#ddlUf").val(data.uf);
                // Use os dados do endereço como quiser
            }
        },
        error: function (xhr, status, error) {
            // Trate os erros da requisição aqui
        }
    });
}


