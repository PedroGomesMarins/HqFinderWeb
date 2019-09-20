using HqFinderWeb.Navegação;
using HtmlAgilityPack;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace HqFinderWeb.Extração
{
    public class Extracao
    {
        public List<HtmlNode> FiltraResultados(ChromeDriver driver, Quadrinho hq, List<Resultado> resultados, string xpathResultados, string xpathResultadoTitulo, string xpathNenhumResultadoEncontrado)
        {
            //Tranforma o html do resultado em um formato mais facil de ser manipulado.
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(driver.PageSource);

            //Lista dos nodes (produtos) que se deseja.
            List<HtmlNode> listaResultadosDesejados = new List<HtmlNode>();

            //Encontra no html todos os nodes de cada resultado encontrado.
            var nodesResultados = doc.DocumentNode.SelectNodes(xpathResultados);

            if (verificaResultadosEncontrados(doc, xpathNenhumResultadoEncontrado))
            {
                //Para cada um realiza a extração do titulo do produto.
                foreach (var node in nodesResultados)
                {
                    var nodeNome = node.SelectSingleNode(xpathResultadoTitulo);

                    var nomeResultado = nodeNome.InnerText;

                    //Há a verificação se o produto em questão é o que se procura.
                    var verificaResultado = VerificarCompatibilidade(nomeResultado, hq);

                    if (verificaResultado)
                    {
                        listaResultadosDesejados.Add(node);
                    }
                }
            }

            return listaResultadosDesejados;
        }

        private bool verificaResultadosEncontrados(HtmlDocument doc, string xpathNenhumResultadoEncontrado)
        {
            var node = doc.DocumentNode.SelectSingleNode(xpathNenhumResultadoEncontrado);

            if (node == null)
                return true;
            else
                return false;
        }

        //Para cada node(produto) que se encaixa no que se procura adiciona-se numa lista.
        private Boolean VerificarCompatibilidade(string nomeResultado, Quadrinho hq)
        {
            var patternNome = hq.nome.ToLower();
            var patternVolume = hq.volume;
            var input = nomeResultado.ToLower();

            Match matchNome = Regex.Match(input, patternNome);
            Match matchVolume = Regex.Match(input, patternVolume);

            if (hq.volume.Length > 0)
            {
                if (matchNome.Success && matchVolume.Success)
                    return true;
                else
                    return false;
            }
            else
            {
                if (matchNome.Success)
                    return true;
                else
                    return false;
            }
        }

        //Após filtrar os resultados da busca, surge uma lista com os produtos que batem com os parametros dados.
        //Ocorre a extração de cada dado dos produtos.
        public List<Resultado> ExtrairResultados(List<HtmlNode> listaResultadosDesejados, ChromeDriver driver, List<Resultado> resultados, string xpathNodeLink, string xpathNodePreco, string url, string xpathNodeEditora, Quadrinho hq, string xpathNodeDisponibilidade)
        {
            Navega navegaResultado = new Navega();

            foreach (var node in listaResultadosDesejados)
            {
                Resultado resultado = new Resultado();

                var nodeLink = node.SelectSingleNode(xpathNodeLink);

                var link = nodeLink.Attributes.First().Value;

                var linkResultado = montaLinkResultado(url, link);

                navegaResultado.NavegaResultado(linkResultado, driver, resultado);

                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(driver.PageSource);

                if (filtraEditora(doc, resultado, xpathNodeEditora, hq))
                {
                    if (verificarDisponibilidade(xpathNodeDisponibilidade, doc))
                    {
                        var nodePreço = doc.DocumentNode.SelectSingleNode(xpathNodePreco);

                        resultado.preco = nodePreço.InnerText;

                        resultados.Add(resultado);
                    }
                }
            }

            return resultados;
        }

        public virtual string montaLinkResultado(string url, string link)
        {
            return String.Format(url + link);
        }

        private bool verificarDisponibilidade(string xpathNodeDisponibilidade, HtmlDocument doc)
        {
            var nodeDisponibilidade = doc.DocumentNode.SelectSingleNode(xpathNodeDisponibilidade);

            if (nodeDisponibilidade == null)
                return true;
            else
                return false;
            
        }

        private bool filtraEditora(HtmlDocument doc, Resultado resultado, string xpathNodeEditora, Quadrinho hq)
        {
            var editoraNode = doc.DocumentNode.SelectSingleNode(xpathNodeEditora);

            var editora = editoraNode.InnerText;

            var patternNome = hq.editora.ToLower();
            var input = editora.ToLower();

            Match match = Regex.Match(input, patternNome);

            if (match.Success)
                return true;
            else
                return false;
        }
    }
}