oTabUsuario = null;
_Usuario = null;
IDPRINCIPAL = null;
$(document).ready(function () {
    jQueryInit();
});

function jQueryInit() {
    fnCriaTela();
}

function fnCriaTela() {

    oTabUsuario = $("#tbUsuario").DataTable({
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
            { sWidth: '12%', "bSortable": false },
            { sWidth: '30%' },//Numero          
            { sWidth: '30%' },//Titulo
            { sWidth: '20%' },//Titulo
        ]
    });

    fnListaDados();

    $(document).ready(function () {
        $('#txtTelefone').inputmask('(99) 9999-9999');
        $('#txtCelular').inputmask('(99) 99999-9999');
    });

}

function fnRetornaObjInclusao() {

    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "Usuario/RetornaObjInclusao",
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {

        },
        success: function (result) {

            debugger;
            _Usuario = result.ObjInclusao;

        },
        error: function (jqXHR, exception) {
        },
        complete: function () {
        }
    });

}

function fnRetornaSequencial() {

    $.ajax({

        type: "GET",
        url: "Usuario/RetornaSequencial",
        data: {},
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {

        },
        success: function (result) {
            debugger;

            $("#txtCodigoUsuario").val(result.retorno);

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
    fnRetornaSequencial();
    /*    fnDadosVendedor();*/
});

$("#btnBuscarCep").click(function () {
    buscaCep($("#txtValorCep").val());
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

    debugger;

    _Usuario.USUSEQUENCIAL = $("#txtCodigoUsuario").val();
    _Usuario.USUSTATUS = $("#ddlStatus").val();
    _Usuario.USUSENHA = $("#txtSenhaUsuario").val();

    _Usuario.TbPessoa.PESNOME = $("#txtNomeUsuario").val();
    _Usuario.TbPessoa.PESSOBRENOME = $("#txtSobreNomeUsuario").val();


    _Usuario.TbEmail.EMLEMAIL = $("#txtEmailUsuario").val();



    var dddTelefone = $("#txtTelefone").val().replace(/[-() ]+/g, "").substring(0, 2);
    var telefone = $("#txtTelefone").val().replace(/[-() ]+/g, "").slice(2);
    _Usuario.TbTelefone.TELDDD = dddTelefone;
    _Usuario.TbTelefone.TELNUMERO = telefone;

    var dddCelular = $("#txtCelular").val().replace(/[-() ]+/g, "").substring(0, 2);
    var celular = $("#txtCelular").val().replace(/[-() ]+/g, "").slice(2);
    _Usuario.TbTelefone.TELDDDC = dddCelular;
    _Usuario.TbTelefone.TELCELULAR = celular;

    _Usuario.TbEmail.EMLEMAIL = $("#txtEmailUsuario").val();
    _Usuario.TbEndereco.EDNCEP = $("#txtCep").val();
    _Usuario.TbEndereco.EDNUF = $("#ddlUf").val();
    _Usuario.TbEndereco.EDNCIDADE = $("#txtCidade").val();
    _Usuario.TbEndereco.EDNBAIRRO = $("#txtBairro").val();
    _Usuario.TbEndereco.EDNLOGRADOURO = $("#txtLogradouro").val();
    _Usuario.TbEndereco.EDNNUMERO = $("#txtNumero").val();
    _Usuario.TbEndereco.EDNCOMPLEMENTO = $("#txtComplemento").val();



    $.ajax({

        type: "POST",
        contentType: "application/json",
        url: "Usuario/GravarUsuario",
        data: JSON.stringify(_Usuario),
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
        url: "Usuario/ListaDados",
        data: {},
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {
        },
        success: function (result) {


            var Lista = result.lsUsuario;
            oTabUsuario.clear().draw();

            var ListaUsuario = new Array();
            if (Lista.length > 0) {

                for (var i = 0; i < Lista.length; i++) {
                    debugger;

                    var btnEditar = '<button id="' + Lista[i].PESID + '"  name="btnEdicao" type="button" class="btn  btn-primary" onClick="fnEditarUsuario(this)">Editar</button>';
                    var btnExcluir = '<button id="' + Lista[i].PESID + '"  name="btnDeletar" type="button" class="btn  btn-danger" onClick="fnExcluirUsuario(this)">Deletar</button>';

                    var Linha = [btnEditar + btnExcluir,
                    Lista[i].TbPessoa.PESNOME,
                    Lista[i].TbEmail.EMLEMAIL,
                    Lista[i].USUSTATUS == ('1') ? "ATIVO" : "INATIVO",
                    ];
                    ListaUsuario[i] = Linha;
                }
                oTabUsuario.rows.add(ListaUsuario).draw();

            }

        },
        error: function (jqXHR, exception) {
        },
        complete: function () {
        }
    });
}

function fnExcluirUsuario(pesid) {


    $.ajax({
        type: "GET",
        contentType: "application/json",
        url: "Usuario/ExcluirUsuario",
        data: { pesid: pesid.id },
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

function fnEditarUsuario(pesid) {

    $.ajax({
        type: "GET",
        contentType: "application/json",
        url: "Usuario/GetUsuarioByID",
        data: { pesid: pesid.id },
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {
        },
        success: function (result) {
            debugger;
            _Usuario = result.retorno

            $("#txtCodigoUsuario").val(_Usuario.USUSEQUENCIAL);
            $("#ddlStatus").val(_Usuario.USUSTATUS);
            $("#txtSenhaUsuario").val(_Usuario.USUSENHA);

            $("#txtNomeUsuario").val(_Usuario.TbPessoa.PESNOME);
            $("#txtSobreNomeUsuario").val(_Usuario.TbPessoa.PESSOBRENOME);


            $("#txtEmailUsuario").val(_Usuario.TbEmail.EMLEMAIL);



            $("#txtTelefone").val(_Usuario.TbTelefone.TELDDD + _Usuario.TbTelefone.TELNUMERO);
            $("#txtCelular").val(_Usuario.TbTelefone.TELDDDC + _Usuario.TbTelefone.TELCELULAR);

            $("#txtEmailUsuario").val(_Usuario.TbEmail.EMLEMAIL);
            $("#txtCep").val(_Usuario.TbEndereco.EDNCEP);
            $("#ddlUf").val(_Usuario.TbEndereco.EDNUF);
            $("#txtCidade").val(_Usuario.TbEndereco.EDNCIDADE);
            $("#txtBairro").val(_Usuario.TbEndereco.EDNBAIRRO);
            $("#txtLogradouro").val(_Usuario.TbEndereco.EDNLOGRADOURO);
            $("#txtNumero").val(_Usuario.TbEndereco.EDNNUMERO);
            $("#txtComplemento").val(_Usuario.TbEndereco.EDNCOMPLEMENTO);


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


