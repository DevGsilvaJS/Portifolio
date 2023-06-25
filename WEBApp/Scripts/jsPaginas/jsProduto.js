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
            { sWidth: '10%', "bSortable": false },
            { sWidth: '5%' },//Numero
            { sWidth: '10%' },//Titulo
            { sWidth: '10%' },//Titulo
            { sWidth: '5%' },//Titulo
            { sWidth: '10%' },//Titulo     
            { sWidth: '10%' },//Titulo  
        ]
    });

}
debugger;
fnListaDados();

//    $.ajax({
//        type: "GET",
//        contentType: "application/json",
//        url: "Fornecedor/listaFornecedor",
//        dataType: "JSON",
//        cache: false,
//        beforeSend: function () {

//        },
//        success: function (result) {

//            if (result != null) {

//                var ListaTFornecedor = result.lsFornecedor;

//                var options = '<option value="0">Selecione o Fornecedor</option>';;
//                $.each(ListaTFornecedor, function (key, val) {

//                    options += '<option value="' + val.FornecedorID + '">' + val.Fantasia + '</option>';
//                });

//                $("#ddlFornecedorID").html(options);

//            }
//        },
//        error: function (jqXHR, exception) {

//        }
//    });
//}

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

function fnDeletarProduto(idProduto) {
    debugger;
    $.ajax({

        type: "GET",
        //contentType: "application/json",
        url: "Produto/Delet",
        data: { idProduto: idProduto.id },
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
        url: "Produto/ListData",
        data: {},
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {
        },
        success: function (result) {
            debugger;

            var Lista = result.lsProduto;
            oTabProduto.clear().draw();

            var ListaProduto = new Array();
            if (Lista.length > 0) {

                for (var i = 0; i < Lista.length; i++) {
                    var btnEditar = '<button id="' + Lista[i].IDPRODUTO + '"  name="btnEdicao" type="button" class="btn  btn-primary" onClick="fnEditarProduto(this)">Editar</button>';
                    var btnExcluir = '<button id="' + Lista[i].IDPRODUTO + '"  name="btnDeletar" type="button" class="btn  btn-danger" onClick="fnDeletarProduto(this)">Deletar</button>';

                    var Linha = [btnEditar + btnExcluir,
                    Lista[i].MATSEQUENCIAL,
                    Lista[i].MATDESCRICAO,
                    Lista[i].MATTIPOPROD,
                    Lista[i].MATGRIFE,
                    Lista[i].MATFANTASIA,
                    formatJSONDate(Lista[i].MATDATACADASTRO),
                    ];
                    ListaProduto[i] = Linha;
                }

                oTabProduto.rows.add(ListaProduto);
            }
        },
        error: function (jqXHR, exception) {
        },
        complete: function () {
        }
    });
}

function fnEditarProduto(idProduto) {
    debugger;

    $.ajax({
        type: "GET",
        contentType: "application/json",
        url: "Produto/GetByID",
        data: { idProduto: idProduto.id },
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {
        },
        success: function (result) {
            debugger;

            STATUS = 'ALTERACAO'
            $("#aCadastro").click();


            fnlsFornecedorUpdate(result.Produto.IDFORNECEDOR);
            fnLsNCMsUpdate(result.Produto.IDNCM);

            _Produto = result.Produto

            $("#txtSequencial").val(result.Produto.MATSEQUENCIAL);
            $("#ddlFornecedorID").val(result.Produto.IDFORNECEDOR);
            $("#txtDescricao ").val(result.Produto.MATDESCRICAO);
            $("#txtDescricaoNF ").val(result.Produto.MATDESCRICAONF);
            $("#sslTipoProduto ").val(result.Produto.MATTIPOPROD);
            $("#sslNCM").val(result.Produto.IDNCM);
            $("#ckcControlEstoque").val(result.Produto.MATCONTROLAESTOQUE);
            $("#txtReferenciaCor").val(result.Produto.MATREFERENCIACOR);
            $("#txtCaracteristica").val(result.Produto.MATCARACTERISTICA);
            $("#txtPerfil").val(result.Produto.MATPERFILUSO);
            $("#txtTamanhoAro").val(result.Produto.MATTAMANHOARO);
            $("#txtMaterial ").val(result.Produto.MATLINHAMATERIAL);
            $("#txtGrife").val(result.Produto.MATGRIFE);
            $("#txtReferencia").val(result.Produto.MATMODELO);
            $("#txtCorFisica").val(result.Produto.MATCORFISICA);
            $("#txtFantasia").val(result.Produto.MATFANTASIA);

        },
        error: function (jqXHR, exception) {
        },
        complete: function () {
        }
    });
}

function fnSalvarDados() {

    debugger;

    _Produto.MATSEQUENCIAL = $("#txtSequencial").val();
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
        url: "Produto/Insert",
        data: JSON.stringify(_Produto),
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {

        },
        success: function (result) {
            debugger;

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
            debugger;

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
            debugger;

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

function lsFornecedor() {

    $.ajax({

        type: "POST",
        contentType: "application/json",
        url: "Produto/lsDados",
        data: {},
        dataType: "JSON",
        cache: false,
        async: false,
        beforeSend: function () {

        },
        success: function (result) {
            debugger;
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

            debugger;

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












