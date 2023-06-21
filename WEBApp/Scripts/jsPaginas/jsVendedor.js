_Vendedor = null;
oTabVendedor = null;
IDPRINCIPAL = null;

var STATUS = 'CONSULTA';

$(document).ready(function () {
    jQueryInit();
});

function jQueryInit() {
    fnCriaTela();
}

function fnCriaTela() {

    oTabVendedor = $("#tbVendedor").DataTable({
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
            { sWidth: '14%', "bSortable": false },
            { sWidth: '20%' },//Numero          
            { sWidth: '20%' },//Titulo
            { sWidth: '20%' },//Titulo   
            { sWidth: '20%' },//Titulo   

        ]
    });




    fnRetornaObjInclusao();
    fnListaDados();

    //$(document).ready(function () {
    //    $("[data-inputmask]").inputmask();
    //});

    $(document).ready(function () {
        $('#txtTelefone').inputmask('(99) 9999-9999');
    });
    $(document).ready(function () {
        $('#txtCelular').inputmask('(99) 99999-9999');
    });
    $(document).ready(function () {
        $('#txtCpf').inputmask('999.999.999-99');
    });
    $(document).ready(function () {
        $('#txtRG').inputmask('99.999.999-9');
    });
    $(document).ready(function () {
        $('#txtValorCep').inputmask('99999-999');
    });
}

function fnRetornaObjInclusao() {

    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "Vendedor/RetornaObjInclusao",
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {

        },
        success: function (result) {

            debugger;
            _Vendedor = result.ObjInclusao;

        },
        error: function (jqXHR, exception) {
        },
        complete: function () {
        }
    });

}

function fnLimparTela() {

    $("#txtCodigo").val('');
    $("#txtNome").val('');
    $("#ddlStatus").val('');
    $("#txtDDD").val('');
    $("#txtDdd").val('');
    $("#txtCelular").val('');
    $("#dtNascimento").val('');
    $("#txtCpf").val('');
    $("#txtRg").val('');
    $("#txtEmail").val('');
    $("#txtCep").val('');
    $("#txtUf").val('');
    $("#txtCidade").val('');
    $("#ddlTipoEndereco").val('');
    $("#txtLogradouro").val('');
    $("#txtBairro").val('');
    $("#txtNumero").val('');
    $("#txtComplemento").val('');


}

$("#aCadastro").click(function () {
    STATUS = 'INSERCAO';
    $("#btnSalvarFormulario").css('display', 'block');
    fnDadosVendedor();
});

$("#aLista").click(function () {
    $("#btnSalvarFormulario").css('display', 'none');
})

$("#btnBuscarCep").click(function () {
    buscaCep($("#txtValorCep").val());
})

$(document).ready(function () {
    $('.nav-tabs a').on('click', function (e) {
        e.preventDefault();
        $(this).tab('show');
    });
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
                fnAtualizarVendedor();
                $("#aLista").tab('show');
                fnListaDados();
                break;
            default:
                break;
        }
    });
});

function fnSalvarDados() {



    //PESSOA
    _Vendedor.TbPessoa.PESNOME = $("#txtNome").val();
    _Vendedor.TbPessoa.PESSOBRENOME = $("#txtSobrenome").val();
    _Vendedor.TbPessoa.PESTIPO = "F";
    _Vendedor.TbPessoa.PESDOCESTADUAL = $("#txtRG").val();
    _Vendedor.TbPessoa.PESDOCFEDERAL = $("#txtCpf").val();

    //VENDEDOR
    _Vendedor.VNDSEQUENCIAL = $("#txtCodigo").val();
    _Vendedor.VNDSTATUS = $("#ddlStatus").val();
    _Vendedor.VNDNASCIMENTO = fnFormataData($("#dtNascimento").val());

    //TELEFONE
    var dddTelefone = $("#txtTelefone").val().replace(/[-() ]+/g, "").substring(0, 2);
    var telefone = $("#txtTelefone").val().replace(/[-() ]+/g, "").slice(2);
    _Vendedor.TbTelefone.TELDDD = dddTelefone
    _Vendedor.TbTelefone.TELNUMERO = telefone

    //CELULAR
    var dddCelular = $("#txtCelular").val().replace(/[-() ]+/g, "").substring(0, 2);
    var celular = $("#txtCelular").val().replace(/[-() ]+/g, "").slice(2);
    _Vendedor.TbTelefone.TELDDDC = dddCelular
    _Vendedor.TbTelefone.TELCELULAR = celular


    //Endereco
    _Vendedor.TbEndereco.EDNCEP = $("#txtValorCep").val();
    _Vendedor.TbEndereco.EDNUF = $("#ddlUf").val();
    _Vendedor.TbEndereco.EDNCIDADE = $("#txtCidade").val();
    _Vendedor.TbEndereco.EDNLOGRADOURO = $("#txtLogradouro").val();
    _Vendedor.TbEndereco.EDNBAIRRO = $("#txtBairro").val();
    _Vendedor.TbEndereco.EDNNUMERO = $("#txtNumero").val();
    _Vendedor.TbEndereco.EDNCOMPLEMENTO = $("#txtComplemento").val();

    _Vendedor.TbEmail.EMLEMAIL = $("#txtEmail").val();



    $.ajax({

        type: "POST",
        contentType: "application/json",
        url: "Vendedor/InsertVendedor",
        data: JSON.stringify(_Vendedor),
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {

        },
        success: function (result) {

            if (result.retorno == "OK") {
                $.confirm({
                    title: 'Produto cadastrado com sucesso!',
                    buttons: {
                        SIM: function () {

                            fnListaDados();
                        },
                    }
                });
            }

            else {
                alert('Não foi possível salvar o registro!');
            }

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
        url: "Vendedor/ListaVendedores",
        data: {},
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {
        },
        success: function (result) {


            var Lista = result.lsVendedor;
            oTabVendedor.clear().draw();

            var ListaVendedor = new Array();
            if (Lista.length > 0) {

                for (var i = 0; i < Lista.length; i++) {

                    var btnEditar = '<button id="' + Lista[i].PESID + '"  name="btnEdicao" type="button" class="btn  btn-primary" onClick="fnEditarVendedor(this)">Editar</button>';
                    var btnExcluir = '<button id="' + Lista[i].PESID + '"  name="btnDeletar" type="button" class="btn  btn-danger" onClick="fnExcluirVendedor(this)">Deletar</button>';

                    var Linha = [btnEditar + btnExcluir,
                    Lista[i].VNDSEQUENCIAL,
                    Lista[i].TbPessoa.PESNOME,
                    Lista[i].TbEmail.EMLEMAIL,
                    Lista[i].VNDSTATUS == ('1 ') ? "ATIVO" : "INATIVO",
                    ];
                    ListaVendedor[i] = Linha;
                }
                oTabVendedor.rows.add(ListaVendedor).draw();

            }

        },
        error: function (jqXHR, exception) {
        },
        complete: function () {
        }
    });
}

function fnEditarVendedor(idVendedor) {

    $.ajax({
        type: "GET",
        contentType: "application/json",
        url: "Vendedor/GetVendedorID",
        data: { idVendedor: idVendedor.id },
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {
        },
        success: function (result) {


            _Vendedor = result._vendedor

            IDPRINCIPAL = idVendedor.id
            $("#txtCodigo").val(_Vendedor.VNDSEQUENCIAL);
            $("#txtNome").val(_Vendedor.TbPessoa.PESNOME);
            $("#txtSobrenome").val(_Vendedor.TbPessoa.PESSOBRENOME);
            $("#ddlStatus").val(_Vendedor.VNDSTATUS);
            $("#txtTelefone").val(_Vendedor.TbTelefone.TELDDD + _Vendedor.TEL.TELNUMERO)
            $("#txtCelular").val(_Vendedor.TbTelefone.TELDDDC + _Vendedor.TEL.TELCELULAR);
            $("#dtNascimento").val(_Vendedor.VNDNASCIMENTO);
            $("#txtCpf").val(_Vendedor.TbPessoa.PESDOCFEDERAL);
            $("#txtRG").val(_Vendedor.TbPessoa.PESDOCESTADUAL);
            $("#txtEmail").val(_Vendedor.TbEmail.EMLEMAIL);
            $("#txtValorCep").val(_Vendedor.TbEndereco.EDNCEP);
            $("#ddlUf").val(_Vendedor.TbEndereco.EDNUF);
            $("#txtCidade").val(_Vendedor.TbEndereco.EDNCIDADE);
            $("#txtLogradouro").val(_Vendedor.TbEndereco.EDNLOGRADOURO);
            $("#txtBairro").val(_Vendedor.TbEndereco.EDNBAIRRO);
            $("#txtNumero").val(_Vendedor.TbEndereco.EDNNUMERO);
            $("#txtComplemento").val(_Vendedor.TbEndereco.EDNCOMPLEMENTO);


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

function fnExcluirVendedor(idVendedor) {
    $.ajax({
        type: "GET",
        contentType: "application/json",
        url: "Vendedor/ExcluirVendedor",
        data: { idVendedor: idVendedor.id },
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

function fnAtualizarVendedor(_ObjVendedor) {

    _Vendedor.TbPessoa.PESID = IDPRINCIPAL;
    _Vendedor.TbPessoa.PESNOME = $("#txtNome").val();
    _Vendedor.TbPessoa.PESSOBRENOME = $("#txtSobrenome").val();
    _Vendedor.VNDSTATUS = $("#ddlStatus").val();
    _Vendedor.VNDNASCIMENTO = $("#dtNascimento").val();
    _Vendedor.TbPessoa.PESDOCFEDERAL = $("#txtCpf").val();
    _Vendedor.TbPessoa.PESDOCESTADUAL = $("#txtRG").val();
    _Vendedor.TbEmail.EMLEMAIL = $("#txtEmail").val();
    _Vendedor.TbEndereco.EDNCEP = $("#txtValorCep").val();
    _Vendedor.TbEndereco.EDNUF = $("#ddlUf").val();
    _Vendedor.TbEndereco.EDNCIDADE = $("#txtCidade").val();
    _Vendedor.TbEndereco.EDNLOGRADOURO = $("#txtLogradouro").val();
    _Vendedor.TbEndereco.EDNBAIRRO = $("#txtBairro").val();
    _Vendedor.TbEndereco.EDNNUMERO = $("#txtNumero").val();
    _Vendedor.TbEndereco.EDNCOMPLEMENTO = $("#txtComplemento").val();

    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "Vendedor/InsertVendedor",
        data: JSON.stringify(_Vendedor),
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {
        },
        success: function (result) {



        },
        error: function (jqXHR, exception) {
        },
        complete: function () {
        }

    });
}

function fnDadosVendedor() {

    $.ajax({

        type: "GET",
        url: "Vendedor/RetornaSequencial",
        data: {},
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {

        },
        success: function (result) {
            debugger;

            $("#txtCodigo").val(result.retorno);

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

function fnFormataData(data) {

    if (data == "") {
        return "";
    }

    let partes = data.split("-");
    let ano = partes[0];
    let mes = partes[2] - 1; // subtrai 1 porque o método getMonth() começa do zero
    let dia = partes[1];
    let dataFormatada = new Date(ano, mes, dia).toLocaleDateString("pt-BR");

    return dataFormatada
}





