var oTabCliente = null;
var _Cliente = new Object();
var _Endereco = new Object();
var _ClienteCopia = new Object();
var STATUS = 'CONSULTA';

$(document).ready(function () {
    jQueryInit();
});

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
                fnSalvaDados();
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

$("#aCadastro").click(function () {
    debugger;
    STATUS = 'INSERCAO';
    $("#btnSalvarFormulario").css('display', 'block');
    fnDadosCliente();
});

$("#aLista").click(function () {
    $("#btnSalvarFormulario").css('display', 'none');
})

$("#btnBuscarCep").click(function () {

    buscaCep($("#txtValorCep").val());

})

function fnRetornaObjInclusao() {

    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "Cliente/RetornaObjInclusao",
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {

        },
        success: function (result) {

            debugger;
            _Cliente = result.ObjInclusao;

        },
        error: function (jqXHR, exception) {
        },
        complete: function () {
        }
    });

}

function DataFormatada(data) {
    dataFormatada = data.toLocalDateString('pt-BR', { timeZone: 'UTC' });
}

function fnStatusAlteracao() {

    $.confirm({
        title: 'Registro alterado com sucesso!',
        buttons: {
            SIM: function () {

                $("#aLista").click();
                $("#liLista").click();

                fnListaDados();
            },
            //NÃO: function () {
            //    $.alert('Canceled!');
            //},
        }
    });
}

function fnStatusGravacao() {

    $.confirm({
        title: 'Registro salvo com sucesso!',
        buttons: {
            SIM: function () {

                $("#aLista").click();
                $("#liLista").click();

                fnListaDados();
            },
            //NÃO: function () {
            //    $.alert('Canceled!');
            //},
        }
    });
}

function fnListagem() {
    $("#aLista").click();
}

function jQueryInit() {
    fnCriaTela();
}

function fnCriaTela() {

    oTabCliente = $("#tbCliente").DataTable({
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
            { sWidth: '15%' },//Titulo
            { sWidth: '15%' },//Titulo
            { sWidth: '10%' },//Titulo
            { sWidth: '10%' },//Titulo

        ]
    });
    fnRetornaObjInclusao();
    fnListaDados();

    $(document).ready(function () {
        $('#txtTelefoneCliente').inputmask('(99) 9999-9999');
        $('#txtCelularCliente').inputmask('(99) 99999-9999');
        $('#txtCpfCliente').inputmask('999.999.999-99');
        $('#txtRgCliente').inputmask('99.999.999-9');
        $('#txtSalarioCliente').inputmask('9{1,},99');
    });
}

function fnListaDados() {

    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "Cliente/ListaDados",
        data: {},
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {
        },
        success: function (result) {

            debugger;

            var Lista = result.lsCliente;
            oTabCliente.clear().draw();

            var ListaCliente = new Array();
            if (Lista.length > 0) {

                for (var i = 0; i < Lista.length; i++) {

                    var btnEditar = '<button id="' + Lista[i].CLIID + '"  name="btnEdicao" type="button" class="btn  btn-primary" onClick="fnEditarCliente(this)">Editar</button>';
                    var btnExcluir = '<button id="' + Lista[i].CLIID + '"  name="btnDeletar" type="button" class="btn  btn-danger" onClick="fnDeleteCliente(this)">Deletar</button>';

                    var Linha = [btnEditar + btnExcluir,
                    Lista[i].CLISEQUENCIAL,
                    Lista[i].TbPessoa.PESNOME,
                    Lista[i].TbPessoa.PESDOCFEDERAL,
                    Lista[i].TbPessoa.PESDOCESTADUAL,
                    Lista[i].CLISEXO
                    ];
                    ListaCliente[i] = Linha;
                }

                oTabCliente.rows.add(ListaCliente).draw();
            }
        },
        error: function (jqXHR, exception) {
        },
        complete: function () {
        }
    });
}

$("#btnSalvar").click(function () {

    if (STATUS == 'EDICAO') {
        UpdateCliente();
    }

    else {
        fnSalvaDados();
    }


});

function fnSalvaDados() {

    debugger;
    _Cliente.TbPessoa.PESNOME = $("#txtNomeCliente").val();
    _Cliente.TbPessoa.PESSOBRENOME = $("#txtSobreNomeCliente").val();
    _Cliente.TbPessoa.PESDOCFEDERAL = $("#txtCpfCliente").val();
    _Cliente.TbPessoa.PESDOCESTADUAL = $("#txtRgCliente").val();
    _Cliente.TbPessoa.PESDTNASCIMENTO = $("#dtNascimentoCliente").val();

    _Cliente.CLISEQUENCIAL = $("#txtCodigoCliente").val();
    _Cliente.CLISEXO = $("#dllSexoCliente").val();
    _Cliente.CLISALARIO = $("#txtSalarioCliente").val();
    _Cliente.CLIESTADOCIVIL = $("#dllEstadoCivilCliente").val();

    _Cliente.TbEmail.EMLEMAIL = $("#txtEmailCliente").val();

    var dddTelefone = $("#txtTelefoneCliente").val().replace(/[-() ]+/g, "").substring(0, 2);
    var telefone = $("#txtTelefoneCliente").val().replace(/[-() ]+/g, "").slice(2);
    _Cliente.TbTelefone.TELDDD = dddTelefone
    _Cliente.TbTelefone.TELNUMERO = telefone


    var dddCelular = $("#txtCelularCliente").val().replace(/[-() ]+/g, "").substring(0, 2);
    var celular = $("#txtCelularCliente").val().replace(/[-() ]+/g, "").slice(2);
    _Cliente.TbTelefone.TELDDDC = dddCelular
    _Cliente.TbTelefone.TELCELULAR = celular

    _Cliente.TbEndereco.EDNCEP = $("#txtValorCep").val();
    _Cliente.TbEndereco.EDNUF = $("#ddlUf").val();
    _Cliente.TbEndereco.EDNCIDADE = $("#txtCidade").val();
    _Cliente.TbEndereco.EDNLOGRADOURO = $("#txtLogradouro").val();
    _Cliente.TbEndereco.EDNBAIRRO = $("#txtBairro").val();
    _Cliente.TbEndereco.EDNNUMERO = $("#txtNumero").val();
    _Cliente.TbEndereco.EDNCOMPLEMENTO = $("#txtComplemento").val();

    $.ajax({

        type: "POST",
        contentType: "application/json",
        url: "Cliente/GravarCLiente",
        data: JSON.stringify(_Cliente),
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {

        },
        success: function (result) {
            debugger;

            fnListagem();

            if (STATUS == 'EDICAO') {
                fnStatusAlteracao();
                return false;
            }

            if (STATUS == 'INCLUSAO') {

            } {
                fnStatusGravacao();
            }



        },
        error: function (jqXHR, exception) {
        },
        complete: function () {
        }
    });
}

function fnEditarCliente(cliid) {
    debugger;
    $.ajax({

        type: "GET",
        url: "Cliente/GetClienteByID",
        data: { cliid: cliid.id },
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {

        },
        success: function (result) {

            debugger;
            _Cliente = result.retorno;


              $("#txtNomeCliente").val(_Cliente.TbPessoa.PESNOME);
           $("#txtSobreNomeCliente").val(_Cliente.TbPessoa.PESSOBRENOME);
           $("#txtSobreNomeCliente").val(_Cliente.TbPessoa.PESSOBRENOME);
              $("#txtCpfCliente").val(_Cliente.TbPessoa.PESDOCFEDERAL);
              $("#txtRgCliente").val(_Cliente.TbPessoa.PESDOCESTADUAL);
              $("#dtNascimentoCliente").val(_Cliente.TbPessoa.PESDTNASCIMENTO);

              $("#txtCodigoCliente").val(_Cliente.CLISEQUENCIAL);
              $("#dllSexoCliente").val(_Cliente.CLISEXO);
            $("#txtSalarioCliente").val(_Cliente.CLISALARIO);
              $("#dllEstadoCivilCliente").val(_Cliente.CLIESTADOCIVIL);

            $("#txtEmailCliente").val(_Cliente.TbEmail.EMLEMAIL);


            $("#txtTelefoneCliente").val(_Cliente.TbTelefone.TELDDD + _Cliente.TbTelefone.TELNUMERO);
            $("#txtTelefoneCliente").val(_Cliente.TbTelefone.TELDDDC + _Cliente.TbTelefone.TELCELULAR);


             $("#txtValorCep").val(_Cliente.TbEndereco.EDNCEP);
             $("#ddlUf").val(_Cliente.TbEndereco.EDNUF);
             $("#txtCidade").val(_Cliente.TbEndereco.EDNCIDADE);
             $("#txtLogradouro").val(_Cliente.TbEndereco.EDNLOGRADOURO);
             $("#txtBairro").val(_Cliente.TbEndereco.EDNBAIRRO);
             $("#txtNumero").val(_Cliente.TbEndereco.EDNNUMERO);
            $("#txtComplemento").val(_Cliente.TbEndereco.EDNCOMPLEMENTO);



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

function UpdateCliente() {

    debugger;

    _Cliente.CLINOME = $("#txtNome").val();
    _Cliente.CLISOBRENOME = $("#txtSobrenome").val();
    _Cliente.CLICPF = $("#txtCpf").val();
    _Cliente.CLISEXO = $("#selSexo").val();
    _Cliente.CLINASCIMENTO = $("#dtNascimento").val();
    _Cliente.CLIRG = $("#txtRg").val();
    _Cliente.DDDTELEFONE = $("#txtDDD1").val();
    _Cliente.TELEFONE = $("#txtTelefone").val();
    _Cliente.DDDCELULAR = $("#txtDDD2").val();
    _Cliente.CELULAR = $("#txtCelular").val();
    _Cliente.ENDCEP = $("#txtCep").val();
    _Cliente.ENDUF = $("#selUF").val();
    _Cliente.ENDCIDADE = $("#txtCidade").val();
    _Cliente.ENDLOGRADOURO = $("#selLogradouro").val();
    _Cliente.ENDENDERECO = $("#txtEndereco").val();
    _Cliente.ENDBAIRRO = $("#txtBairro").val();
    _Cliente.ENDNUMERO = $("#txtNumero").val();
    _Cliente.ENDCOMPLEMENTO = $("#txtComplemento").val();
    _Cliente.EMAIL = $("#txtEmail").val();


    $.ajax({

        type: "POST",
        contentType: "application/json",
        url: "Cliente/UpdateCliente",
        data: JSON.stringify(_Cliente),
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {


        },
        error: function (jqXHR, exception) {
        },
        complete: function () {
        }
    });
}

function fnDeleteCliente(idCliente) {

    debugger;

    $.ajax({

        type: "GET",
        //contentType: "application/json",
        url: "Cliente/DeleteCliente",
        data: { idCliente: idCliente.id },
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

function formatJSONDate(jsonDate) {

    if (jsonDate != null) {
        debugger;
        var dateString = jsonDate.substr(6);
        var currentTime = new Date(parseInt(dateString));
        var month = currentTime.getMonth() + 1;
        var day = currentTime.getDate();

        if (month <= 9) {
            month = "0" + month;
        }
        if (day <= 9) {
            day = "0" + day;
        }
        var year = currentTime.getFullYear();
        var date = day + "/" + month + "/" + year;

        return date;
    }

}

function fnDadosCliente() {


    $.ajax({

        type: "GET",
        url: "Cliente/RetornaSequencial",
        data: {},
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {

        },
        success: function (result) {

            debugger;

            $("#txtCodigoCliente").val(result.retorno);

        },
        error: function (jqXHR, exception) {
        },
        complete: function () {
        }
    });
}





