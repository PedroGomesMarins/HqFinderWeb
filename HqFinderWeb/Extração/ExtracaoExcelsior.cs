using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HqFinderWeb.Extração
{
    public class ExtracaoExcelsior : Extracao
    {
        public List<Resultado> Extrai(ChromeDriver driver, Quadrinho hq, List<Resultado> resultados, string url)
        {
            var list = FiltraResultados(driver, hq, resultados, getXpathResultados(), getXpathResultadoTitulo());

            var listResultados = ExtrairResultados(list, driver, resultados, getXpathNodeLink(), getXpathNodePreco(), url, getXpathEditora(), hq, getXpathDisponibilidade());

            return listResultados;
        }

        private string getXpathDisponibilidade()
        {
            return "//p[contains(text(),'Fora de estoque')]";
        }

        public string getXpathResultados()
        {
            return "//div[@id='search-rand']/div[@class='item']";
        }

        public string getXpathResultadoTitulo()
        {
            return ".//div/span[1]/span";
        }

        public string getXpathNodeLink()
        {
            return ".//a";
        }

        public string getXpathNodePreco()
        {
            return "//span[@class='price']/span";
        }

        private string getXpathEditora()
        {
            return "//th[descendant::span[contains(text(),'Editora')]]/following-sibling::td/a";
        }
    }
}