using System.Collections.Generic;
using System.Web.Mvc;
using UI.WEB.WorkFlow;
using UI.WEB.WorkFlow.Vendas.TabelasAuxiliares;

namespace WEBApp.Controllers
{
    public class VendedorController : Controller
    {        // GET: Vendedor

        VendedorWorkFlow wf = new VendedorWorkFlow();
        // GET: Vendedor
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult RetornaObjInclusao()
        {
            EntityVendedor obj = new EntityVendedor();
            obj = wf.RetornaObjInclusao();

            return Json(new
            {
                ObjInclusao = obj
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult InsertVendedor(EntityVendedor vendedor)
        {
            string retorno = "";

            retorno = wf.GravarVendedor(vendedor);

            return Json(new
            {
                _Vendedor = retorno
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListaVendedores()
        {
            List<EntityVendedor> lsVendedor = new List<EntityVendedor>();

            lsVendedor = wf.ListaVendedores();

            return Json(new
            {
                lsVendedor = lsVendedor
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetVendedorID(int idVendedor)
        {
            EntityVendedor _vendedor = new EntityVendedor();

            _vendedor = wf.GetVendedorID(idVendedor);

            return Json(new
            {
                _vendedor = _vendedor
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ExcluirVendedor(int idVendedor)
        {
            string retorno = "";

            retorno = wf.ExcluirVendedor(idVendedor);

            return Json(new
            {
                _vendedor = retorno
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DadosVendedor()
        {
            string UltimoSequencial = "";

            UltimoSequencial = wf.DadosVendedor();

            return Json(new
            {
                retorno = UltimoSequencial.ToString()
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
    }
}
