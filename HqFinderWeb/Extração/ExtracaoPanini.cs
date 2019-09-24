using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HqFinderWeb.Extração
{
    public class ExtracaoPanini : Extracao
    {
        public List<Resultado> Extrai(ChromeDriver driver, Quadrinho hq, List<Resultado> resultados, string url)
        {
            var list = FiltraResultados(driver, hq, resultados, getXpathResultados(), getXpathResultadoTitulo(), getXpathNenhumResultadoEncontrado());

            var listResultados = ExtrairResultados(list, driver, resultados, getXpathNodeLink(), getXpathNodePreco(), url, getXpathEditora(), hq, getXpathDisponibilidade());

            return listResultados;
        }

        private string getXpathNenhumResultadoEncontrado()
        {
            return "//p[contains(text(),'Não foram encontrados resultados para esta pesquisa.')]";
        }

        private string getXpathDisponibilidade()
        {
            return "//p[contains(text(),'Produto indisponível')]";
        }

        public string getXpathResultados()
        {
            return "//div[@class = 'product']";
        }

        public string getXpathResultadoTitulo()
        {
            return "./div/div/following-sibling::div/h4/a";
        }

        public string getXpathNodeLink()
        {
            return "./div/div/a";
        }

        public string getXpathNodePreco()
        {
            return "//span[@class = 'price-sales']";
        }

        private string getXpathEditora()
        {
            return "//strong[contains(text(),'Panini')]";
        }

        override
        public string montaLinkResultado(string url, string link)
        {
            return link;
        }
    }
}