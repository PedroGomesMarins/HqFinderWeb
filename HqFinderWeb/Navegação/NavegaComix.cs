using HqFinderWeb.Extração;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace HqFinderWeb.Navegação
{
    public class NavegaComix :Navega
    {
        public List<Resultado> NavegaPesquisa(Quadrinho hq, List<Resultado> resultados)
        {
            var driver = InicializaDriver();

            driver = NavegaPaginaInicial(driver, getUrl());

            driver = RealizaPesquisa(driver, hq, getXpathPesquisa(), getXpathBotao());

            Thread.Sleep(2000);

            //Ao chegar no Html dos resultados, começa a extração dos dados.
            ExtracaoComix extracao = new ExtracaoComix();
            var resultado = extracao.Extrai(driver, hq, resultados, getUrl());

            driver.Close();

            return resultado;
        }

        private string getXpathBotao()
        {
            return "//label[contains(text(),'Pesquisa:')]/following-sibling::button";
        }

        private string getXpathPesquisa()
        {
            return "search";
        }

        private string getUrl()
        {
            return "http://www.comix.com.br/";
        }
    }
}