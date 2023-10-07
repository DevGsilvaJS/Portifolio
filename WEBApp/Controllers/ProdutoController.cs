using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.WEB.Model.Estoque;
using UI.WEB.Model.Fiscal.Tabelas_Auxiliares;
using UI.WEB.Model.Outros;
using UI.WEB.WorkFlow.Estoque;
using UI.WEB.WorkFlow.Outros;

namespace WEBApp.Controllers
{
    public class ProdutoController : Controller
    {

        ProdutoWorkFlow wf = new ProdutoWorkFlow();
        ListasGenericasWorkFlow wfListas = new ListasGenericasWorkFlow();
        // GET: Produto
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult RetornaObjInclusao()
        {
            EntityProduto ObjInclusao = new EntityProduto();
            ObjInclusao = wf.RetornaObjInclusao();

            return Json(new
            {
                ObjInclusao = ObjInclusao
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RetornaComboFornecedores()
        {
            List<EntityPessoa> lista = new List<EntityPessoa>();
            lista = wfListas.RetornaComboFornecedores();

            return Json(new
            {
                lista = lista
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RetornaComboNCM()
        {
            List<EntityNCM> lista = new List<EntityNCM>();
            lista = wfListas.RetornaComboNCM();

            return Json(new
            {
                lista = lista
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SalvarProduto(EntityProduto objProduto)
        {
            string sRetorno = wf.GravarProduto(objProduto);

            return Json(new
            {
                lista = sRetorno
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ExcluirProduto(int matid)
        {
            string sRetorno = wf.ExcluirProduto(matid);

            return Json(new
            {
                retorno = sRetorno
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProdutoByID(int matid)
        {
            EntityProduto Produto = new EntityProduto();
            Produto = wf.GetProdutoByID(matid);

            return Json(new
            {
                Produto = Produto
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListaProdutos()
        {

            List<EntityProduto> lista = new List<EntityProduto>();

            lista = wf.ListaProdutos();

            return Json(new
            {
                lista = lista
            }, JsonRequestBehavior.AllowGet);
        }    
        public JsonResult RetornaSequencial()
        {
            string retorno = wf.RetornaSequencial();

            return Json(new
            {
                retorno = retorno
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListaMovimentacoes(int IDPRINCIPAL)
        {

            List<EntityItensEntrada> lista = new List<EntityItensEntrada>();
            lista = wf.ListaMovimentacoes(IDPRINCIPAL);

            return Json(new
            {
                lista = lista
            }, JsonRequestBehavior.AllowGet);
        }
    }
}