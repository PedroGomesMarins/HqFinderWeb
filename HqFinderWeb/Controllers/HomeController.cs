using HqFinderWeb.Navegação;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HqFinderWeb.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Post(string text_nome, string text_volume, string text_editora)
        {
            Quadrinho hq = new Quadrinho();
            hq.nome = text_nome;
            hq.volume = text_volume;
            hq.editora = text_editora;

            List<Resultado> resultados = new List<Resultado>();

            //Navega e extrai as informações e retorna uma lista de quadrinhos desejados.
            List<Resultado> resultadoExcelsior = new List<Resultado>();
            navegaExcelsior(hq, resultadoExcelsior);
            resultados.AddRange(resultadoExcelsior);

            ViewBag.Resultados = resultados;
            return View("resultados");
        }
 
        private static void navegaExcelsior(Quadrinho hq, List<Resultado> dadosExcelsior)
        {
            NavegaExcelsior excelsior = new NavegaExcelsior();
            dadosExcelsior = excelsior.NavegaPesquisa(hq, dadosExcelsior);
        }

    }
}