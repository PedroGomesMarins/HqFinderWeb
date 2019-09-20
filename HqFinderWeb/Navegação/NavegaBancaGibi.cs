using HqFinderWeb.Extração;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace HqFinderWeb.Navegação
{
    public class NavegaBancaGibi : Navega
    {
        public List<Resultado> NavegaPesquisa(Quadrinho hq, List<Resultado> resultados)
        {
            var driver = InicializaDriver();

            driver = NavegaPaginaInicial(driver, getUrl());

            driver = RealizaPesquisa(driver, hq, getXpathPesquisa(), getXpathBotao());

            Thread.Sleep(2000);

            //Ao chegar no Html dos resultados, começa a extração dos dados.
            ExtracaoBancaGibi extracao = new ExtracaoBancaGibi();
            var resultado = extracao.Extrai(driver, hq, resultados, getUrl());

            driver.Close();

            return resultado;
        }

        private string getXpathBotao()
        {
            return "//button[@id = 'search_button']";
        }

        private string getXpathPesquisa()
        {
            return "search_field";
        }

        private string getUrl()
        {
            return "https://www.bancadogibi.com.br/";
        }

        public override string MontaStringPesquisa(Quadrinho hq)
        {
            //Monta a estring de pesquisa que depende do volume ou não.
            if (hq.volume.Length > 0)
            {
                var int1 = Int32.Parse(hq.volume);

                var str = int1.ToString("000");

                return String.Format(hq.nome + " N ° " + str);

            }else
                return String.Format(hq.nome);
        }
    }
}