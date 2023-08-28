var oTabProduto = null;
var _Produto = new Object();
var _ProdutoCopia = new Object();
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

$("#aCadastro").click(function () {
    STATUS = 'INSERCAO';
    $("#btnSalvarFormulario").css('display', 'block');
    fnDadosProduto();
    fnRetornaObjInclusao();
});

$(document).ready(function () {

    $('#txtPrecoCusto, #txtMarkup, #txtPrecoVenda').on('blur', function () {
        debugger;
        var custoComMask = $("#txtPrecoCusto").val();
        var custoSemMask = custoComMask.substring(3);
        var custoFloat = parseFloat(custoSemMask.replace(',', '.'));

        var vendaComMask = $("#txtPrecoVenda").val();
        var vendaSemMask = vendaComMask.substring(3);
        var vendaFloat = parseFloat(vendaSemMask.replace(',', '.'));

        var markupCalculado = ((vendaFloat - custoFloat) / custoFloat) * 100;
        $('#txtMarkup').val(markupCalculado.toFixed(2));

    });
});

$(document).ready(function () {
    $('#btnSalvarFormulario').on('click', function () {

        switch (STATUS) {
            case 'INSERCAO':
                fnSalvarProduto();
                $("#aLista").tab('show');
                fnListaDados();
                break;
            case 'ALTERACAO':
                fnSalvarProduto();
                $("#aLista").tab('show');
                fnListaDados();
                break;
            default:
                break;
        }
    });
});

function Listagem() {
    fnListaDados();
    $("#liLista").click();
}

function jQueryInit() {

    fnCriaTela();
}

function fnCriaTela() {

    oTabProduto = $("#tbProduto").DataTable({
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
            { sWidth: '9%', "bSortable": false },
            { sWidth: '2%' },//Numero
            { sWidth: '10%' },//Titulo
            { sWidth: '10%' },//Titulo
            { sWidth: '10%' },//Titulo
            { sWidth: '10%' },//Titulo     
            { sWidth: '10%' },//Titulo  
        ]
    });

    fnRetornaComboNCM();
    fnRetornaComboFornecedores();
    fnListaDados();

    $('#txtPrecoCusto').inputmask('R$ 9{1,},99');
    $('#txtPrecoVenda').inputmask('R$ 9{1,},99');

}

$(document).ready(function () {
    $('#txtCorFisica, #txtGrife, #txtMaterial, #txtReferencia').on('input', function () {
        var cor = $('#txtCorFisica').val().substring(0, 4);
        var grife = $('#txtGrife').val().substring(0, 7);
        var material = $('#txtMaterial').val().substring(0, 7);
        var modelo = $('#txtReferencia').val().substring(0, 4);
        var fantasia = grife + material + modelo + cor;
        $('#txtFantasia').val(fantasia);
    });
});

function fnRetornaObjInclusao() {

    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "Produto/RetornaObjInclusao",
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {

        },
        success: function (result) {

            debugger;
            _Produto = result.ObjInclusao;

        },
        error: function (jqXHR, exception) {
        },
        complete: function () {
        }
    });

}

function fnLimparTela() {

    $("#txtSequencial").val('');
    $("#ddlFornecedorID").val('');
    $("#txtDescricao ").val('');
    $("#txtDescricaoNF ").val('');
    $("#sslTipoProduto ").val('');
    $("#sslNCM").val('');
    $("#ckcControlEstoque").val('');
    $("#txtReferenciaCor").val('');
    $("#txtCaracteristica").val('');
    $("#txtPerfil").val('');
    $("#txtTamanhoAro").val('');
    $("#txtMaterial ").val('');
    $("#txtGrife").val('');
    $("#txtReferencia").val('');
    $("#txtCorFisica").val('');
    $("#txtFantasia").val('');
}

function fnExcluirProduto(matid) {

    $.ajax({

        type: "GET",
        //contentType: "application/json",
        url: "Produto/ExcluirProduto",
        data: { matid: matid.id },
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {

        },
        success: function (result) {

            STATUS = 'CONSULTA';
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
        url: "Produto/ListaProdutos",
        data: {},
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {
        },
        success: function (result) {
            debugger;

            var Lista = result.lista;
            oTabProduto.clear().draw();

            var ListaProduto = new Array();
            if (Lista.length > 0) {

                for (var i = 0; i < Lista.length; i++) {
                    debugger;
                    var btnEditar = '<button id="' + Lista[i].MATID + '"  name="btnEdicao" type="button" class="btn  btn-primary" onClick="fnEditarProduto(this)">Editar</button>';
                    var btnExcluir = '<button id="' + Lista[i].MATID + '"  name="btnDeletar" type="button" class="btn  btn-danger" onClick="fnExcluirProduto(this)">Deletar</button>';

                    var Linha = [btnEditar + btnExcluir,
                    Lista[i].MATSEQUENCIAL,
                    Lista[i].MATFANTASIA,
                    Lista[i].TbMpc.MPCPRECOCUSTO,
                    Lista[i].TbMpv.MPVPRECOVENDA,
                    Lista[i].TbGrife.ARGDESCRICAO,
                    formatJSONDate(Lista[i].MATDTCADASTRO),
                    ];
                    ListaProduto[i] = Linha;
                }

                oTabProduto.rows.add(ListaProduto).draw();
            }
        },
        error: function (jqXHR, exception) {
        },
        complete: function () {
        }
    });
}

function fnEditarProduto(matid) {

    $.ajax({
        type: "GET",
        contentType: "application/json",
        url: "Produto/GetProdutoByID",
        data: { matid: matid.id },
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {
        },
        success: function (result) {

            fnlsFornecedorUpdate(result.Produto.IDFORNECEDOR);
            fnLsNCMsUpdate(result.Produto.IDNCM);

            _Produto = result.Produto

            $("#ddlFornecedorID").val(_Produto.FORID);
            $("#txtSequencial").val(_Produto.MATSEQUENCIAL);
            $("#sslTipoProduto").val(_Produto.MATRECSOL);
            $("#sslNCM").val(_Produto.NCMID);
            $("#txtDescricao").val(_Produto.MATDESCRICAO);
            $("#txtDescricaoNF").val(_Produto.MATDESCRICAOECF);
            _Produto.MATCONTROLAEST = $("#ckcControlEstoque").prop("checked") ? 1 : 0;

            _Produto.MATVENDA = $("#ckcItemVendido").prop("checked") ? 1 : 0;
            _Produto.MATACEITANEGATIVO = $("#ckcAceitaEstoqueNegativo").prop("checked") ? 1 : 0;
            $("#txtFantasia").val(_Produto.MATFANTASIA);


            $("#txtMaterial").val(_Produto.TbLinha.ARLDESCRICAO);
            $("#txtGrife").val(_Produto.TbGrife.ARGDESCRICAO);
            $("#txtReferencia").val(_Produto.TbModelo.ARMDESCRICAO);
            $("#txtCorFisica").val(_Produto.TbCor.ARCDESCRICAO);
            $("#txtReferenciaCor").val(_Produto.TbCorNumerica.ACNDESCRICAO);
            $("#txtCaracteristica").val(_Produto.TbSublinha1.AS1DESCRICAO);
            $("#txtPerfil").val(_Produto.TbSublinha2.AS2DESCRICAO);
            $("#txtTamanhoAro").val(_Produto.TbTamanho.ATODESCRICAO);

            debugger;

            var precoCusto = parseFloat(_Produto.TbMpc.MPCPRECOCUSTO);
            var precoVenda = parseFloat(_Produto.TbMpv.MPVPRECOVENDA);

            $("#txtPrecoCusto").val(precoCusto.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' }));
            $("#txtPrecoVenda").val(precoVenda.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' }));

            $("#txtMarkup").val(_Produto.TbMpv.MPVMARKUP);

            $("#txtFantasia").val(result.Produto.MATFANTASIA);


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

function fnSalvarProduto() {
    debugger;

    _Produto.FORID = $("#ddlFornecedorID").val();
    _Produto.MATSEQUENCIAL = $("#txtSequencial").val();
    _Produto.MATRECSOL = $("#sslTipoProduto").val();
    _Produto.NCMID = $("#sslNCM").val();
    _Produto.MATDESCRICAO = $("#txtDescricao").val();
    _Produto.MATDESCRICAOECF = $("#txtDescricaoNF").val();
    _Produto.MATCONTROLAEST = $("#ckcControlEstoque").prop("checked") ? 1 : 0;

    _Produto.MATVENDA = $("#ckcItemVendido").prop("checked") ? 1 : 0;
    _Produto.MATACEITANEGATIVO = $("#ckcAceitaEstoqueNegativo").prop("checked") ? 1 : 0;
    _Produto.MATFANTASIA = $("#txtFantasia").val();


    _Produto.TbLinha.ARLDESCRICAO = $("#txtMaterial").val();
    _Produto.TbGrife.ARGDESCRICAO = $("#txtGrife").val();
    _Produto.TbModelo.ARMDESCRICAO = $("#txtReferencia").val();
    _Produto.TbCor.ARCDESCRICAO = $("#txtCorFisica").val();
    _Produto.TbCorNumerica.ACNDESCRICAO = $("#txtReferenciaCor").val();
    _Produto.TbSublinha1.AS1DESCRICAO = $("#txtCaracteristica").val();
    _Produto.TbSublinha2.AS2DESCRICAO = $("#txtPerfil").val();
    _Produto.TbTamanho.ATODESCRICAO = $("#txtTamanhoAro").val();


    var custoComMask = $("#txtPrecoCusto").val();
    var custoSemMask = custoComMask.substring(3);
    var custoFloat = parseFloat(custoSemMask.replace(',', '.'));

    var vendaComMask = $("#txtPrecoVenda").val();
    var vendaSemMask = vendaComMask.substring(3);
    var vendaFloat = parseFloat(vendaSemMask.replace(',', '.'));

    _Produto.TbMpv.MPVPRECOVENDA = vendaFloat;
    _Produto.TbMpv.MPVMARKUP = $("#txtMarkup").val();
    _Produto.TbMpc.MPCPRECOCUSTO = custoFloat;

    $.ajax({

        type: "POST",
        contentType: "application/json",
        url: "Produto/SalvarProduto",
        data: JSON.stringify(_Produto),
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {

        },
        success: function (result) {
    

            if (result == "OK" || result.result == "OK") {

                $.confirm({
                    title: 'Produto cadastrado com sucesso!',
                    buttons: {
                        SIM: function () {

                            $("#aLista").click();
                            $("#liLista").click();

                            fnListaDados();
                            fnLimparTela();
                        },
                    }
                });
            }

        },
        error: function (jqXHR, exception) {

        },
        complete: function () {
        }
    });
}

function fnUpdate() {

    _Produto.IDFORNECEDOR = $("#ddlFornecedorID").val();
    _Produto.MATDESCRICAO = $("#txtDescricao ").val();
    _Produto.MATDESCRICAONF = $("#txtDescricaoNF ").val();
    _Produto.MATTIPOPROD = $("#sslTipoProduto ").val();
    _Produto.IDNCM = $("#sslNCM").val();
    _Produto.MATCONTROLAESTOQUE = $("#ckcControlEstoque").val();
    _Produto.MATREFERENCIACOR = $("#txtReferenciaCor").val();
    _Produto.MATCARACTERISTICA = $("#txtCaracteristica").val();
    _Produto.MATPERFILUSO = $("#txtPerfil").val();
    _Produto.MATTAMANHOARO = $("#txtTamanhoAro").val();
    _Produto.MATLINHAMATERIAL = $("#txtMaterial ").val();
    _Produto.MATGRIFE = $("#txtGrife").val();
    _Produto.MATMODELO = $("#txtReferencia").val();
    _Produto.MATCORFISICA = $("#txtCorFisica").val();
    _Produto.MATFANTASIA = $("#txtFantasia").val();

    if (_Produto.MATCONTROLAESTOQUE == 'on') {
        _Produto.MATCONTROLAESTOQUE = 'S'
    }
    else {
        _Produto.MATCONTROLAESTOQUE = 'N'
    }

    _Produto.MATITEMVENDIDO = $("#ckcItemVendido").val();

    if (_Produto.MATITEMVENDIDO == 'on') {
        _Produto.MATITEMVENDIDO = 'S'
    }
    else {
        _Produto.MATITEMVENDIDO = 'N'
    }

    _Produto.MATESTOQUENEGATIVO = $("#ckcAceitaEstoqueNegativo").val();

    if (_Produto.MATESTOQUENEGATIVO == 'on') {
        _Produto.MATESTOQUENEGATIVO = 'S'
    }

    else {
        _Produto.MATESTOQUENEGATIVO = 'N'
    }

    $.ajax({

        type: "POST",
        contentType: "application/json",
        url: "Produto/Update",
        data: JSON.stringify(_Produto),
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {

        },
        success: function (result) {


            if (result._Produto == "OK" || result._Produto == "OK") {

                $.alert({
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
        },
        error: function (jqXHR, exception) {

        },
        complete: function () {
        }
    });
}

function fnListarNCM() {

    $.ajax({

        type: "POST",
        contentType: "application/json",
        url: "Produto/lsNCM",
        data: {},
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {

        },
        success: function (result) {


            if (result != null) {
                var ListaNCM = result.lsNCM;
                var options = '<option value="0">Selecione o NCM</option>';;

                $.each(ListaNCM, function (key, val) {
                    options += '<option value="' + val.IDNCM + '">' + val.NCMDESCRICAO + '</option>';
                });
                $("#sslNCM").html(options);
            }

        },
        error: function (jqXHR, exception) {

        },
        complete: function () {
        }
    });
}

function fnlsFornecedorUpdate(idFornecedor) {

    $.ajax({

        type: "GET",
        contentType: "application/json",
        url: "Produto/lsFornecedorUpdate",
        data: { idFornecedor: idFornecedor },
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {

        },
        success: function (result) {

            if (result != null) {
                var ListaFornecedor = result.lsFornecedor;
                var options = '<option value="0">Selecione o Fornecedor</option>';;

                $.each(ListaFornecedor, function (key, val) {
                    options += '<option value="' + val.IDFORNECEDOR + '">' + val.FORRAZAO + '</option>';
                });
                $("#ddlFornecedorID").html(options);
            }

        },
        error: function (jqXHR, exception) {

        },
        complete: function () {
        }
    });

}

function fnLsNCMsUpdate(idNcm) {

    $.ajax({

        type: "GET",
        contentType: "application/json",
        url: "Produto/lsNcmUpdate",
        data: { idNcm: idNcm },
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {

        },
        success: function (result) {


            if (result != null) {
                var ListaNCM = result.lsNCM;
                var options = '';;

                $.each(ListaNCM, function (key, val) {
                    options += '<option value="' + val.IDNCM + '">' + val.NCMDESCRICAO + '</option>';
                });
                $("#sslNCM").html(options);
            }
        },
        error: function (jqXHR, exception) {

        },
        complete: function () {
        }
    });

}

function formatJSONDate(jsonDate) {

    if (jsonDate != null) {

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

function fnCarregarDados() {

    $.ajax({

        type: "GET",
        contentType: "application/json",
        url: "Produto/fnCarregarDados",
        data: {},
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {

        },
        success: function (result) {

            $("#txtSequencial").val(result.retorno);

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
        url: "Produto/RetornaComboFornecedores",
        data: JSON.stringify(),
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {

        },
        success: function (result) {

            if (result != null) {

                var lista = result.lista;

                var options = '<option value="0">Selecione o Fornecedor</option>';;
                $.each(lista, function (key, val) {

                    options += '<option value="' + val.PESID + '">' + val.PESNOME + '</option>';
                });

                $("#ddlFornecedorID").html(options);

            }


        },
        error: function (jqXHR, exception) {

        },
        complete: function () {
        }
    });

}

function fnRetornaComboNCM() {

    $.ajax({

        type: "POST",
        contentType: "application/json",
        url: "Produto/RetornaComboNCM",
        data: JSON.stringify(),
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {

        },
        success: function (result) {

            if (result != null) {

                var lista = result.lista;

                var options = '<option value="0">Selecione o Fornecedor</option>';;
                $.each(lista, function (key, val) {

                    options += '<option value="' + val.NCMID + '">' + val.NCMDESCRICAO + '</option>';
                });

                $("#sslNCM").html(options);

            }


        },
        error: function (jqXHR, exception) {

        },
        complete: function () {
        }
    });

}

function fnDadosProduto() {

    $.ajax({

        type: "GET",
        url: "Produto/RetornaSequencial",
        data: {},
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {

        },
        success: function (result) {

            $("#txtSequencial").val(result.retorno);

        },
        error: function (jqXHR, exception) {
        },
        complete: function () {
        }
    });
}













