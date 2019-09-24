using HqFinderWeb.Extração;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace HqFinderWeb.Navegação
{
    public class NavegaPanini : Navega
    {
        public List<Resultado> NavegaPesquisa(Quadrinho hq, List<Resultado> resultados)
        {
            var driver = InicializaDriver();

            driver = NavegaPaginaInicial(driver, getUrl());

            driver = RealizaPesquisa(driver, hq, getXpathPesquisa(), getXpathBotao());

            Thread.Sleep(2000);

            //Ao chegar no Html dos resultados, começa a extração dos dados.
            ExtracaoPanini extracao = new ExtracaoPanini();
            var resultado = extracao.Extrai(driver, hq, resultados, getUrl());

            driver.Close();

            return resultado;
        }

        private string getXpathBotao()
        {
            return "//input[@id = 'search_txt']/following-sibling::a";
        }

        private string getXpathPesquisa()
        {
            return "search_txt";
        }

        private string getUrl()
        {
            return "https://loja.panini.com.br/panini/vitrines/default.aspx";
        }
    }
}