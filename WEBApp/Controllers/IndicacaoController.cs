﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.WEB.WorkFlow.Vendas.TabelasAuxiliares;

namespace WEBApp.Controllers
{
    public class IndicacaoController : Controller
    {
        IndicacaoWorkFlow wf = new IndicacaoWorkFlow();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult RetornaObjInclusao()
        {
            return Json(new
            {
                ObjInclusao = wf.RetornaObjInclusao()
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListaDados()
        {
            List<EntityIndicacao> lista = new List<EntityIndicacao>();

            lista = wf.ListaDados();

            return Json(new
            {
                lista = lista
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ExcluirTipoVenda(int indid)
        {
            string sRetorno = wf.ExcluirIndicacao(indid);

            return Json(new
            {
                retorno = sRetorno
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTipoVendaByID(int tpvid)
        {
            EntityIndicacao _Indicacao = new EntityIndicacao();
            _Indicacao = wf.GetIndicacaoID(tpvid);

            return Json(new
            {
                retorno = _Indicacao
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GravarTipoVenda(EntityIndicacao _Indicacao)
        {
            string sRetorno = wf.GravarIndicacao(_Indicacao);

            return Json(new
            {
                retorno = sRetorno
            }, JsonRequestBehavior.AllowGet);
        }
    }
}