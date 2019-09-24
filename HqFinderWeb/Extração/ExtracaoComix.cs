using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HqFinderWeb.Extração
{
    public class ExtracaoComix : Extracao
    {
        public List<Resultado> Extrai(ChromeDriver driver, Quadrinho hq, List<Resultado> resultados, string url)
        {
            var list = FiltraResultados(driver, hq, resultados, getXpathResultados(), getXpathResultadoTitulo(), getXpathNenhumResultadoEncontrado());

            var listResultados = ExtrairResultados(list, driver, resultados, getXpathNodeLink(), getXpathNodePreco(), url, getXpathEditora(), hq, getXpathDisponibilidade());

            return listResultados;
        }

        private string getXpathNenhumResultadoEncontrado()
        {
            return "//p[contains(text(),'Sua busca n')]";
        }

        private string getXpathDisponibilidade()
        {
            return "//span[contains(text(),'Fora de estoque')]";
        }

        public string getXpathResultados()
        {
            return "//div[@class = 'page-title']/following-sibling::div/div/following-sibling::div/ul/li";
        }

        public string getXpathResultadoTitulo()
        {
            return ".//div/h2/a";
        }

        public string getXpathNodeLink()
        {
            return ".//div/h2/a";
        }

        public string getXpathNodePreco()
        {
            return "//*[@class = 'regular-price' or @class = 'special-price']/span[@class='price']";
        }
        
        private string getXpathEditora()
        {
            return "//th[contains(text(),'Fornecedor')]/following-sibling::td";
        }

        override
        public string montaLinkResultado(string url, string link)
        {
            return link;
        }
    }
}