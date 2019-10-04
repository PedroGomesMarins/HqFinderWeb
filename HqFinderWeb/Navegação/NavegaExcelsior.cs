using HqFinderWeb.Extração;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace HqFinderWeb.Navegação
{
    public class NavegaExcelsior : Navega
    {
        public List<Resultado> NavegaPesquisa(Quadrinho hq, List<Resultado> resultados)
        {
            var driver = InicializaDriver();

            driver = NavegaPaginaInicial(driver, getUrl());

            driver = RealizaPesquisa(driver, hq, getXpathPesquisa(), getXpathBotao());

            Thread.Sleep(2000);

            //Ao chegar no Html dos resultados, começa a extração dos dados.
            ExtracaoExcelsior extracao = new ExtracaoExcelsior();
            var resultado = extracao.Extrai(driver, hq, resultados, getUrl());

            driver.Close();

            return resultado;
        }

        private string getXpathBotao()
        {
            return "//span[@class= 'input-group-btn']/button";
        }

        private string getXpathPesquisa()
        {
            return "search-term";
        }

        private string getUrl()
        {
            return "https://excelsiorcomics.com.br/";
        }
    }
}