using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HqFinderWeb.Navegação
{
    public class Navega
    {
        public ChromeDriver InicializaDriver()
        {
            //Cria opção para o navegador
            //Ativa modo fantasma e identifica que tipo de navegador ele sera.
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("--headless", "--user-agent=Mozilla/5.0 (Macintosh; Intel Mac OS X 10_13_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36");

            //Inicializa e navega para o site.
            return new ChromeDriver(chromeOptions);
        }

        public ChromeDriver NavegaPaginaInicial(ChromeDriver driver, string url)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(100);

            driver.Navigate().GoToUrl(url);

            return driver;
        }

        public ChromeDriver RealizaPesquisa(ChromeDriver driver, Quadrinho hq, string xpathPesquisa, string xpathBotao)
        {
            var stringPesquisa = MontaStringPesquisa(hq);

            //No html do site procura pela barra de pesquisa e insere os parametros desejados.
            driver.FindElementById(xpathPesquisa).SendKeys(stringPesquisa);

            //Encontra e clique no botão pesquisar.
            IWebElement botao = driver.FindElementByXPath(xpathBotao);
            botao.Click();

            return driver;
        }

        public string MontaStringPesquisa(Quadrinho hq)
        {
            return String.Format(hq.nome + " " + hq.volume);
        }

        //Navega para um da lista de resultados filtrados.
        public virtual void NavegaResultado(string link, ChromeDriver driver, Resultado resultado, string url)
        {
            var urlResultado = montarLinkResultado(link, url);

            resultado.link = urlResultado;

            driver.Navigate().GoToUrl(urlResultado);
        }

        private string montarLinkResultado(object link, string url)
        {
            return String.Format(url + link);
        }
    }
}