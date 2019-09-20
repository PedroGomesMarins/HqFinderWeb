using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HqFinderWeb.Extração
{
    public class ExtracaoBancaGibi : Extracao
    {
        public List<Resultado> Extrai(ChromeDriver driver, Quadrinho hq, List<Resultado> resultados, string url)
        {
            var list = FiltraResultados(driver, hq, resultados, getXpathResultados(), getXpathResultadoTitulo(), getXpathNenhumResultadoEncontrado());

            var listResultados = ExtrairResultados(list, driver, resultados, getXpathNodeLink(), getXpathNodePreco(), url, getXpathEditora(), hq, getXpathDisponibilidade());

            return listResultados;
        }

        private string getXpathNenhumResultadoEncontrado()
        {
            return "//div[@id='core']/text()[contains(.,'Nenhum produto encontrado')]";
        }

        private string getXpathDisponibilidade()
        {
            return "//div[contains(text(),'Fora de estoque')]";
        }

        public string getXpathResultados()
        {
            return "//li[@class='product']";
        }

        public string getXpathResultadoTitulo()
        {
            return ".//a/following-sibling::div/a";
        }

        public string getXpathNodeLink()
        {
            return ".//a";
        }

        public string getXpathNodePreco()
        {
            return "//span[@class='price']";
        }

        private string getXpathEditora()
        {
            return "//div[@class='product_description']/br[3]/following-sibling::text()[1]";
        }
    }
}