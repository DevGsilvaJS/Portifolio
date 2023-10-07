oTabSaidaEstoque = null;
oTabListaProdutosSaida = null;
var ListaItensPesquisa = [];
ListaGrid = [];
listaSaida = [];
var ListaItensGrid = new Array();
var posicaoGrid = 0;
_SaidaEstoque = null
IDPRINCIPAL = null;
var custoUnitario = 0;
var valorIpi = 0;
var custoTotal = 0;
var totalNota = 0;
var STATUSPAGINA = "INSERCAO";
var indiceItem = null;

$(document).ready(function () {
    jQueryInit();
});

function jQueryInit() {
    fnCriaTela();
    fnCriaGridItens();
    $("#btnSalvarFormulario").css('display', 'block');
}

function fnCriaTela() {

    oTabSaidaEstoque = $("#tbMovimentacao").DataTable({
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
            { sWidth: '3%', "bSortable": false },
            { sWidth: '10%' },//Numero          
            { sWidth: '10%' },//Titulo
            { sWidth: '10%' },//Titulo
        ]
    });

    fnRetornaObjInclusao();
    fnRetornaListaSaida();
    fnSetDataAtual();
    fnRetornaComboCfops();

    $('#txtValorUnitario').inputmask('9{1,},99');
    $('#txtIPI').inputmask('9{1,},99');
    $('#txtCustoTotal').inputmask('9{1,},99');
    $('#txtMvmvalvenda').inputmask('9{1,},99');

}

function fnCriaGridItens() {

    oTabListaProdutosSaida = $("#tbListaProdutosSaida").DataTable({
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
            { sWidth: '13%', "bSortable": false },
            { sWidth: '20%' },//Numero          
            { sWidth: '20%' },//Titulo
            { sWidth: '20%' },//Titulo
        ]
    });
}

function fnRetornaObjInclusao() {

    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "SaidaEstoque/RetornaObjInclusao",
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {

        },
        success: function (result) {


            _SaidaEstoque = result.ObjInclusao;

        },
        error: function (jqXHR, exception) {
        },
        compvare: function () {
        }
    });

}

function fnRetornaListaSaida() {

    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "SaidaEstoque/RetornaListaSaida",
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {

        },
        success: function (result) {


            listaSaida = result.listaSaida;

        },
        error: function (jqXHR, exception) {
        },
        compvare: function () {
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

    fnLimpaGrid();
    STATUSPAGINA = 'INSERCAO';
    fnBuscarProduto($("#txtFantasia").val());

})

function removerMascara(valor) {

    return valor.replace("R$", "").trim();

}

function substituirPontosPorVirgulas(texto) {
    // Usar o método replace com uma expressão regular para substituir todos os pontos por vírgulas
    return texto.replace(/\./g, ',');
}

function textoParaNumero(texto) {

    return parseFloat(texto.replace(',', '.'));
}

$(document).ready(function () {
    $('#btnSalvarFormulario').on('click', function () {

        fnSalvarSaida();

    });
});

function fnSalvarSaida() {

    ;

    for (var i = 0; i < _SaidaEstoque.ListaSaida.length; i++) {
        _SaidaEstoque.ListaSaida[i].MVMTIPO = "E";
    }

    _SaidaEstoque.FORID = $("#sslFornecedor").val();
    _SaidaEstoque.MVNDATAENTRADA = $("#dtDataMvm").val();
    _SaidaEstoque.MVNNUMNOTA = $("#txtNumeroNota").val();
    _SaidaEstoque.MVNMODELONOTA = $("#sslTipoNf").val();
    _SaidaEstoque.MVNSERIENOTA = $("#txtSerie").val();
    _SaidaEstoque.MVNSUBSERIENOTA = $("#txtSubSerie").val();
    _SaidaEstoque.MVNTOTALNOTA = $("#txtValorNota").val();
    _SaidaEstoque.TbItensSaida.COPID = $("#sslCFOP").val();

    $.ajax({

        type: "POST",
        contentType: "application/json",
        url: "SaidaEstoque/GravarSaidaEstoque",
        data: JSON.stringify(_SaidaEstoque),
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {

        },
        success: function (result) {

            debugger;

            result.retorno = "OK";

            fnAlertRegistroSalvo();



        },
        error: function (jqXHR, exception) {

        },
        compvare: function () {
        }
    });
}

function fnRetornaComboCfops() {

    $.ajax({

        type: "POST",
        contentType: "application/json",
        url: "SaidaEstoque/RetornaComboCfops",
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
        compvare: function () {
        }
    });

}

function fnSetDataAtual() {

    var dataAtual = new Date();

    dataAtual.setMinutes(dataAtual.getMinutes() - dataAtual.getTimezoneOffset());

    var dataFormatada = dataAtual.toISOString().slice(0, 10);

    document.getElementById('dtDataMvm').value = dataFormatada;
}

function fnBuscarProduto(produto) {

    $.ajax({

        type: "POST",
        contentType: "application/json",
        url: "SaidaEstoque/RetornaEntityProduto",
        data: JSON.stringify({ produto: produto }),
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {

        },
        success: function (result) {

            debugger;

            if (result.Produto.length == 0) {
                alert('Nenhum produto encontrado!');
                return false;
            }

            ListaItensPesquisa = result.Produto;


            if (ListaItensPesquisa.length > 0) {

                $('#txtModal').modal('show');

                oTabListaProdutosSaida.clear().draw();

                var Lista = new Array();

                if (ListaItensPesquisa.length > 0) {

                    for (var i = 0; i < ListaItensPesquisa.length; i++) {


                        var btnSalvarItemGrid = '<button id="' + ListaItensPesquisa[i].MATID +
                            '"  name="btnEdicao" type="button" class="btn  btn-primary" onClick="fnPopulaGrid(this)">SALVAR</button>';


                        var Linha = [btnSalvarItemGrid,
                            ListaItensPesquisa[i].MATSEQUENCIAL,
                            ListaItensPesquisa[i].MATFANTASIA,
                            ListaItensPesquisa[i].TbMec.MECQUANTIDADE,
                        ];
                        Lista[i] = Linha;
                    }
                    oTabListaProdutosSaida.rows.add(Lista).draw();
                }

            }



        },
        error: function (jqXHR, exception) {

        },
        compvare: function () {
        }
    });
}

function fnPopulaGrid(ProdutoSelecionado) {

    for (var i = 0; i < ListaItensPesquisa.length; i++) {

        var iTemLista = ListaItensPesquisa[i].MATID.toString();

        if (iTemLista === ProdutoSelecionado.id) {


            IDPRINCIPAL = ListaItensPesquisa[i].MATID;

            $("#txtProduto").val(ListaItensPesquisa[i].MATFANTASIA);

            $("#txtQtd").val('1');

            $("#txtQtdAtual").val(ListaItensPesquisa[i].TbMec.MECQUANTIDADE)

            $('#txtModal').modal('hide');

        }
    }

}

$("#btnAdicionarItemGrid").click(function () {
    debugger;
    if (IDPRINCIPAL == null) {
        return false;
    }

    if (_SaidaEstoque.ListaEntrada.length > 0) {

        for (var i = 0; i < _SaidaEstoque.ListaEntrada.length; i++) {
            if (IDPRINCIPAL === _SaidaEstoque.ListaEntrada[i].MATID) {
                $.alert({
                    title: 'Alerta',
                    content: 'Produto já existe na lista de saída!',
                    theme: 'material',
                    closeIcon: true,
                    backgroundDismiss: false,
                    escapeKey: true,
                    containerFluid: true,
                    contentClass: 'custom-alert-text'
                });

                return false;
            }
        }
    }



    if ($("#txtQtd").val() > $("#txtQtdAtual").val()) {

        $.alert({
            title: 'Alerta',
            content: 'Quantidade preenchida, maior que o estoque atual!',
            theme: 'material',
            closeIcon: true,
            backgroundDismiss: false,
            escapeKey: true,
            containerFluid: true,
            contentClass: 'custom-alert-text' // Aplicando a classe personalizada ao texto do alerta
        });

        return false;
    }

    if (STATUSPAGINA == "INSERCAO") {
        fnPreencheGrid(IDPRINCIPAL);
    }

    else if (STATUSPAGINA == "EDICAO") {
        fnAtualizarItemGrid(indiceItem);
    }


})

function fnPreencheGrid(mATID) {

    debugger;

    
    for (var i = 0; i < ListaItensPesquisa.length; i++) {

        if (mATID === ListaItensPesquisa[i].MATID) {

            _SaidaEstoque.TbItensSaida = new Object();

            _SaidaEstoque.TbItensSaida.MATID = IDPRINCIPAL

            var btnEditar = '<button id="' + _SaidaEstoque.TbItensSaida.MATID + '"  name="btnEdicao" type="button" style="margin:5px;"  class="btn  btn-primary" onClick="fnEditarItemGrid(this)">Editar</button>';
            var btnExcluir = '<button id="' + _SaidaEstoque.TbItensSaida.MATID + '"  name="btnExclusao" type="button" class="btn  btn-danger" onClick="fnExcluirItemGrid(this)">Excluir</button>';

            var Linha = [btnEditar + btnExcluir,
                posicaoGrid,
            MATFANTASIA = $("#txtProduto").val(),
            _SaidaEstoque.TbItensSaida.MVMQUANTIDADE = $("#txtQtd").val(),
            _SaidaEstoque.TbItensSaida.MVMVALUNITARIO = $("#txtValorUnitario").val(),
            ];
            ListaGrid[0] = Linha;
            posicaoGrid += 1;

            oTabSaidaEstoque.rows.add(ListaGrid).draw();

            _SaidaEstoque.ListaEntrada.push(_SaidaEstoque.TbItensSaida);

            fnLimpaGrid();

            IDPRINCIPAL = null;

        }
    }
}

function fnLimpaGrid() {


    $("#txtProduto").val('');
    $("#txtQtd").val('');
    $("#txtValorUnitario").val('');
    $("#txtIPI").val('');
    $("#txtCustoTotal").val('');
    $("#txtMarkup").val('');
    $("#txtMvmvalvenda").val('');
}

function fnExcluirItemGrid(id) {

    for (var i = 0; i < _SaidaEstoque.ListaSaida.length; i++) {
        if (_SaidaEstoque.ListaSaida[i].MATID == id.id) {

            _SaidaEstoque.ListaSaida.splice(i, 1);

            oTabSaidaEstoque.row(i).remove().draw();

        }
    }
}

function fnEditarItemGrid(id) {

    STATUSPAGINA = "EDICAO";
    IDPRINCIPAL = id.id;
    var minhaTabela = $('#tbMovimentacao').DataTable();

    var linha = id.closest('tr');
    indiceItem = linha.querySelector('td:nth-child(2)').textContent;

    $("#txtProduto").val(linha.querySelector('td:nth-child(3)').textContent);
    $("#txtQtd").val(linha.querySelector('td:nth-child(4)').textContent);
    $("#txtValorUnitario").val(linha.querySelector('td:nth-child(5)').textContent);
    $("#txtIPI").val(linha.querySelector('td:nth-child(6)').textContent);
    $("#txtCustoTotal").val(linha.querySelector('td:nth-child(7)').textContent);
    $("#txtMarkup").val(linha.querySelector('td:nth-child(8)').textContent);
    $("#txtMvmvalvenda").val(linha.querySelector('td:nth-child(9)').textContent);


}

function fnAtualizarItemGrid(indiceItem) {


    var minhaTabela = $('#tbMovimentacao').DataTable();

    var linha = minhaTabela.row(indiceItem);
    var valoresDaLinha = linha.data();

    valoresDaLinha[3] = $("#txtQtd").val();
    valoresDaLinha[4] = $("#txtValorUnitario").val();
    valoresDaLinha[5] = $("#txtIPI").val();
    valoresDaLinha[6] = $("#txtCustoTotal").val();
    valoresDaLinha[7] = $("#txtMarkup").val();
    valoresDaLinha[8] = $("#txtMvmvalvenda").val();

    linha.data(valoresDaLinha);


    minhaTabela.draw();

}

