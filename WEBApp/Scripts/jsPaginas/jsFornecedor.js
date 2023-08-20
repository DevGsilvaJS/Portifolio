var oTabFornecedor = null;
var _Fornecedor = new Object();
var STATUS = 'CONSULTA';
var IDPRINCIPAL = null;

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
            { sWidth: '8%', "bSortable": false },
            { sWidth: '20%' },//Numero          
            { sWidth: '20%' },//Titulo
            { sWidth: '20%' },//Titulo
        ]
    });

    fnListaDados();
    $(document).ready(function () {
        $('#txtTelefone').inputmask('(99) 9999-9999');
        $('#txtCelular').inputmask('(99) 99999-9999');
        $('#txtCnpj').inputmask('99.999.999/9999-99');
        $('#txtIe').inputmask('99.999.999-9');
    });
}

$(document).ready(function () {
    $('.nav-tabs a').on('click', function (e) {
        e.preventDefault();
        $(this).tab('show');
    });
});

$("#aCadastro").click(function () {
    debugger;
    STATUS = 'INSERCAO';
    $("#btnSalvarFormulario").css('display', 'block');
    fnRetornaObjInclusao();
    lsCentroCusto();
    lsPlanoContas();
    fnRetornaSequencial();
})

$("#aLista").click(function () {
    fnLimparTela();
})

$("#btnBuscarCep").click(function () {
    buscaCep($("#txtValorCep").val());
})

$(document).ready(function () {
    $('#btnSalvarFormulario').on('click', function () {
        debugger;
        switch (STATUS) {
            case 'INSERCAO':
                fnGravarFornecedor();
                $("#aLista").tab('show');
                fnLimparTela();
                fnListaDados();
                break;
            case 'ALTERACAO':
                fnAtualizarFornecedor();
                $("#aLista").tab('show');
                fnLimparTela();
                fnListaDados();
                break;
            default:
                break;
        }
    });
});

function fnRetornaObjInclusao() {

    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "Fornecedor/RetornaObjInclusao",
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {

        },
        success: function (result) {

            debugger;
            _Fornecedor = result.retorno;

        },
        error: function (jqXHR, exception) {
        },
        complete: function () {
        }
    });

}

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
        url: "Fornecedor/ListarFornecedores",
        data: {},
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {

        },
        success: function (result) {

            var Lista = result.lista;
            oTabFornecedor.clear().draw();

            var ListaFornecedor = new Array();
            debugger;


            if (Lista.length > 0) {
                debugger;
                for (var i = 0; i < Lista.length; i++) {


                    var btnEditar = '<button id="' + Lista[i].TbPessoa.PESID +
                        '"  name="btnEdicao" type="button" class="btn  btn-primary" onClick="fnEditarFornecedor(this)">Editar</button>';

                    var btnExcluir = '<button id="' + Lista[i].TbPessoa.PESID +
                        '"  name="btnDeletar" type="button" class="btn  btn-danger" onClick="fnExcluirFornecedor(this)">Deletar</button>';

                    var Linha = [btnEditar + btnExcluir,
                    Lista[i].TbPessoa.PESNOME,
                    Lista[i].TbPessoa.PESDOCFEDERAL,
                    Lista[i].TbPessoa.PESDOCFEDERAL,
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

function fnGravarFornecedor() {

    debugger;

    _Fornecedor.FORSEQUENCIAL = $("#IDfornecedor").val();
    _Fornecedor.CCUID = $("#CentroCustoID").val();
    _Fornecedor.PCTID = $("#PlanoContasID").val();

    _Fornecedor.TbPessoa.PESNOME = $("#txtFantasia").val();
    _Fornecedor.TbPessoa.PESSOBRENOME = $("#txtRazaoSocial").val();
    _Fornecedor.TbPessoa.PESDOCFEDERAL = $("#txtCnpj").val().replace(/[^\d]/g, '');
    _Fornecedor.TbPessoa.PESDOCESTADUAL = $("#txtIe").val().replace(/[^\d]/g, '');
    _Fornecedor.TbPessoa.PESTIPO = "J";


    var dddTelefone = $("#txtTelefone").val().replace(/[-() ]+/g, "").substring(0, 2);
    var telefone = $("#txtTelefone").val().replace(/[-() ]+/g, "").slice(2);
    _Fornecedor.TbTelefone.TELDDD = dddTelefone;
    _Fornecedor.TbTelefone.TELNUMERO = telefone;

    var dddCelular = $("#txtCelular").val().replace(/[-() ]+/g, "").substring(0, 2);
    var celular = $("#txtCelular").val().replace(/[-() ]+/g, "").slice(2);
    _Fornecedor.TbTelefone.TELDDDC = dddCelular;
    _Fornecedor.TbTelefone.TELCELULAR = celular;

    _Fornecedor.TbEmail.EMLEMAIL = $("#txtEmail").val();
    _Fornecedor.TbEndereco.EDNCEP = $("#txtCep").val();
    _Fornecedor.TbEndereco.EDNUF = $("#ddlUf").val();
    _Fornecedor.TbEndereco.EDNCIDADE = $("#txtCidade").val();
    _Fornecedor.TbEndereco.EDNBAIRRO = $("#txtBairro").val();
    _Fornecedor.TbEndereco.EDNLOGRADOURO = $("#txtLogradouro").val();
    _Fornecedor.TbEndereco.EDNNUMERO = $("#txtNumero").val();
    _Fornecedor.TbEndereco.EDNCOMPLEMENTO = $("#txtComplemento").val();

    $.ajax({

        type: "POST",
        contentType: "application/json",
        url: "Fornecedor/GravarFornecedor",
        data: JSON.stringify(_Fornecedor),
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {

        },
        success: function (result) {
            debugger;

            return false;

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

function fnAtualizarFornecedor() {

    debugger;

    _Fornecedor.TbPessoa.PESID = IDPRINCIPAL;
    _Fornecedor.FORSEQUENCIAL = $("#IDfornecedor").val();
    _Fornecedor.CCUID = $("#CentroCustoID").val();
    _Fornecedor.PCTID = $("#PlanoContasID").val();

    _Fornecedor.TbPessoa.PESNOME = $("#txtFantasia").val();
    _Fornecedor.TbPessoa.PESSOBRENOME = $("#txtRazaoSocial").val();
    _Fornecedor.TbPessoa.PESDOCFEDERAL = $("#txtCnpj").val().replace(/[^\d]/g, '');
    _Fornecedor.TbPessoa.PESDOCESTADUAL = $("#txtIe").val().replace(/[^\d]/g, '');
    _Fornecedor.TbPessoa.PESTIPO = "J";


    var dddTelefone = $("#txtTelefone").val().replace(/[-() ]+/g, "").substring(0, 2);
    var telefone = $("#txtTelefone").val().replace(/[-() ]+/g, "").slice(2);
    _Fornecedor.TbTelefone.TELDDD = dddTelefone;
    _Fornecedor.TbTelefone.TELNUMERO = telefone;

    var dddCelular = $("#txtCelular").val().replace(/[-() ]+/g, "").substring(0, 2);
    var celular = $("#txtCelular").val().replace(/[-() ]+/g, "").slice(2);
    _Fornecedor.TbTelefone.TELDDDC = dddCelular;
    _Fornecedor.TbTelefone.TELCELULAR = celular;

    _Fornecedor.TbEmail.EMLEMAIL = $("#txtEmail").val();
    _Fornecedor.TbEndereco.EDNCEP = $("#txtCep").val();
    _Fornecedor.TbEndereco.EDNUF = $("#ddlUf").val();
    _Fornecedor.TbEndereco.EDNCIDADE = $("#txtCidade").val();
    _Fornecedor.TbEndereco.EDNBAIRRO = $("#txtBairro").val();
    _Fornecedor.TbEndereco.EDNLOGRADOURO = $("#txtLogradouro").val();
    _Fornecedor.TbEndereco.EDNNUMERO = $("#txtNumero").val();
    _Fornecedor.TbEndereco.EDNCOMPLEMENTO = $("#txtComplemento").val();

    $.ajax({

        type: "POST",
        contentType: "application/json",
        url: "Fornecedor/GravarFornecedor",
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
        url: "Fornecedor/ComboPlanoContas",
        data: JSON.stringify(),
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {

        },
        success: function (result) {
            debugger;

            if (result != null) {

                var ListaPlanos = result.retorno;

                var options = '<option value="0">Selecione o Plano de contas</option>';;
                $.each(ListaPlanos, function (key, val) {

                    options += '<option value="' + val.PCTID + '">' + val.PCTDESCRICAO + '</option>';
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
        url: "Fornecedor/ComboCentroCusto",
        data: JSON.stringify(),
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {

        },
        success: function (result) {
            debugger;

            if (result != null) {
                var ListaCDC = result.retorno;
                var options = '<option value="0">Selecione o Plano de contas</option>';;

                $.each(ListaCDC, function (key, val) {
                    options += '<option value="' + val.CCUID + '">' + val.CCUDESCRICAO + '</option>';
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

function fnRetornaSequencial() {

    $.ajax({

        type: "GET",
        url: "Fornecedor/RetornaSequencial",
        data: {},
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {

        },
        success: function (result) {
            debugger;

            $("#IDfornecedor").val(result.retorno);

        },
        error: function (jqXHR, exception) {
        },
        complete: function () {
        }
    });
}

function fnEditarFornecedor(idFornecedor) {

    lsPlanoContas();
    lsCentroCusto();

    $.ajax({

        type: "GET",
        url: "Fornecedor/EditarFornecedor",
        data: { idFornecedor: idFornecedor.id },
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {
        },
        success: function (result) {

            debugger;
            _Fornecedor = result.objFornecedor

            IDPRINCIPAL = idFornecedor.id
            $("#IDfornecedor").val(_Fornecedor.FORSEQUENCIAL);
            $("#CentroCustoID").val(_Fornecedor.CCUID);
            $("#PlanoContasID").val(_Fornecedor.PCTID);
            $("#txtFantasia").val(_Fornecedor.TbPessoa.PESNOME);
            $("#txtRazaoSocial").val(_Fornecedor.TbPessoa.PESSOBRENOME);
            $("#txtCnpj").val(_Fornecedor.TbPessoa.PESDOCFEDERAL);
            $("#txtIe").val(_Fornecedor.TbPessoa.PESDOCESTADUAL);
            $("#txtTelefone").val(_Fornecedor.TbTelefone.TELDDD + _Fornecedor.TbTelefone.TELNUMERO);
            $("#txtCelular").val(_Fornecedor.TbTelefone.TELDDDC + _Fornecedor.TbTelefone.TELCELULAR);
            $("#txtEmail").val(_Fornecedor.TbEmail.EMLEMAIL);
            $("#txtCep").val(_Fornecedor.TbEndereco.EDNCEP);
            $("#ddlUf").val(_Fornecedor.TbEndereco.EDNUF);
            $("#txtCidade").val(_Fornecedor.TbEndereco.EDNCIDADE);
            $("#txtBairro").val(_Fornecedor.TbEndereco.EDNBAIRRO);
            $("#txtLogradouro").val(_Fornecedor.TbEndereco.EDNLOGRADOURO);
            $("#txtNumero").val(_Fornecedor.TbEndereco.EDNNUMERO);
            $("#txtComplemento").val(_Fornecedor.TbEndereco.EDNCOMPLEMENTO);


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

function fnExcluirFornecedor(idFornecedor) {

    $.ajax({

        type: "GET",
        url: "Fornecedor/ExcluirFornecedor",
        data: { idFornecedor: idFornecedor.id },
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {
        },
        success: function (result) {

            $("#aLista").click();
            fnListaDados();

        },
        error: function (jqXHR, exception) {
        },
        complete: function () {
        }
    });
}

function fnLimparTela() {

    IDPRINCIPAL = null
    $("#IDfornecedor").val('');
    $("#CentroCustoID").val('');
    $("#PlanoContasID").val('');
    $("#txtFantasia").val('');
    $("#txtRazaoSocial").val('');
    $("#txtCnpj").val('');
    $("#txtIe").val('');
    $("#txtTelefone").val('');
    $("#txtCelular").val('');
    $("#txtEmail").val('');
    $("#txtCep").val('');
    $("#ddlUf").val('');
    $("#txtCidade").val('');
    $("#txtBairro").val('');
    $("#txtLogradouro").val('');
    $("#txtNumero").val('');
    $("#txtComplemento").val('');
}











