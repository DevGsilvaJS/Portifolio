using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.WEB.WorkFlow.Vendas;

namespace WEBApp.Controllers
{
    public class ClienteController : Controller
    {
        ClienteWorkFlow wf = new ClienteWorkFlow();
        // GET: Cliente
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult RetornaObjInclusao()
        {
            EntityCliente obj = new EntityCliente();

            return Json(new
            {
                ObjInclusao = obj
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GravarCLiente(EntityCliente _Cliente)
        {
            string Retorno = "NOTOK";

            Retorno = wf.GravarCLiente(_Cliente);


            return Json(new
            {
                Retorno = Retorno
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RetornaSequencial()
        {

            string retorno = "";

            retorno = wf.RetornaSequencial();

            return Json(new
            {
                retorno = retorno
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListaDados()
        {
            List<EntityCliente> lsCliente = new List<EntityCliente>();

            lsCliente = wf.ListaDados();

            return Json(new
            {
                lsCliente = lsCliente
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetClienteByID(int cliid)
        {
            EntityCliente _Cliente = new EntityCliente();
            _Cliente = wf.GetClienteByID(cliid);

            return Json(new
            {
                retorno = _Cliente
            }, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult SearchClientId(int idCliente)
        //{

        //    EntityCliente _Cliente = new EntityCliente();

        //    try
        //    {
        //        _Cliente = wf.SearchClientId(idCliente);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    return Json(new
        //    {
        //        cliente = _Cliente
        //    }, JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult listaClientes()
        //{
        //    List<EntityCliente> lsCliente = new List<EntityCliente>();

        //    try
        //    {
        //        lsCliente = wf.ListaClientes();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    return Json(new
        //    {
        //        lsCliente = lsCliente
        //    }, JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult DeleteCliente(int idCliente)
        //{
        //    bool Retorno = false;

        //    try
        //    {
        //        Retorno = wf.DeleteCliente(idCliente);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    return Json(new
        //    {
        //        Retorno = Retorno
        //    }, JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult DadosCliente()
        //{

        //    string retorno = wf.DadosCliente();

        //    return Json(new
        //    {
        //        retorno = retorno
        //    }, JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult UpdateCliente(EntityCliente _Cliente)
        //{
        //    string retorno = "";

        //    retorno = wf.UpdateCliente(_Cliente);

        //    return Json(new
        //    {
        //        retorno = retorno
        //    }, JsonRequestBehavior.AllowGet);
        //}
    }
}