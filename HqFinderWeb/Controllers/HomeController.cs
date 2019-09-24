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
            List<Resultado> resultadoComix = new List<Resultado>();
            navegaComix(hq, resultadoComix);
            resultados.AddRange(resultadoComix);

            //Navega e extrai as informações e retorna uma lista de quadrinhos desejados.
            List<Resultado> resultadoExcelsior = new List<Resultado>();
            navegaExcelsior(hq, resultadoExcelsior);
            resultados.AddRange(resultadoExcelsior);

            //Navega e extrai as informações e retorna uma lista de quadrinhos desejados.
            List<Resultado> resultadoBancaGibi = new List<Resultado>();
            navegaBancaGibi(hq, resultadoBancaGibi);
            resultados.AddRange(resultadoBancaGibi);

            //Navega e extrai as informações e retorna uma lista de quadrinhos desejados.
            List<Resultado> resultadoPanini = new List<Resultado>();
            navegaPanini(hq, resultadoPanini);
            resultados.AddRange(resultadoPanini);

            ViewBag.Resultados = resultados;
            return View("resultados");
        }

        private void navegaPanini(Quadrinho hq, List<Resultado> dadosPanini)
        {
            NavegaPanini excelsior = new NavegaPanini();
            dadosPanini = excelsior.NavegaPesquisa(hq, dadosPanini);
        }

        private void navegaBancaGibi(Quadrinho hq, List<Resultado> dadosBancaGibi)
        {
            NavegaBancaGibi excelsior = new NavegaBancaGibi();
            dadosBancaGibi = excelsior.NavegaPesquisa(hq, dadosBancaGibi);
        }

        private static void navegaExcelsior(Quadrinho hq, List<Resultado> dadosExcelsior)
        {
            NavegaExcelsior excelsior = new NavegaExcelsior();
            dadosExcelsior = excelsior.NavegaPesquisa(hq, dadosExcelsior);
        }

        private static void navegaComix(Quadrinho hq, List<Resultado> dadosComix)
        {
            NavegaComix comix = new NavegaComix();
            dadosComix = comix.NavegaPesquisa(hq, dadosComix);
        }
    }
}