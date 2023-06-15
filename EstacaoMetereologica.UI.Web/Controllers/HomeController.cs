using EstacaoMetereologica.Model;
using EstacaoMetereologica.Repository;
using EstacaoMetereologica.UI.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;

namespace EstacaoMetereologica.UI.Web.Controllers
{
    public class HomeController : Controller
    {

        private DadosREP _repositorioDados = new DadosREP();

        public ActionResult Index()
        {
            DadosViewMOD Dados = new DadosViewMOD();

            Dados.ListaEstacao = _repositorioDados.ObterTodasEstacao();

            return View(Dados);
        }

        public ActionResult Sobre()
        {
            return View();
        }

        public ActionResult Consultar(Int32 CdEstacao)
        {
            DadosViewMOD Dados = new DadosViewMOD();
            Dados.ListaEstacao = _repositorioDados.ObterTodasEstacao();
            Dados.Estacao = Dados.ListaEstacao.Where(x => x.CdEstacao == CdEstacao).ToList()[0];

            Dados.Estacao.ListaLeitura = _repositorioDados.ObterTodasLeiturasPorCdEstacao(CdEstacao);

            return View(Dados);
        }

        public ActionResult ExportaExcel(Int32 CdEstacao)
        {
            DadosViewMOD Dados = new DadosViewMOD();
            Dados.ListaEstacao = _repositorioDados.ObterTodasEstacao();
            Dados.Estacao = Dados.ListaEstacao.Where(x => x.CdEstacao == CdEstacao).ToList()[0];

            Int32 qtdLinhas = _repositorioDados.ObterQtdLinhaLeitura(CdEstacao);

            Int32 qtdGet = (int)Math.Ceiling((double)qtdLinhas / 10000);

            for (int i = 0; i < qtdGet; i++)
            {
                Int32 primeiraPosicao = i * 9999;

                List<LeituraMOD> listaLeitura = _repositorioDados.ObterLeituraPaginada(CdEstacao, primeiraPosicao);

                Dados.Estacao.ListaLeitura.AddRange(listaLeitura);
            }

            // Criar um novo arquivo Excel
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("NomeDaPlanilha");

            // Definir os cabeçalhos das colunas
            string[] headers = { "Data", "Horário", "Umidade", "Temperatura", "Velocidade do Vento", "Presença da Chuva" };
            for (int i = 0; i < headers.Length; i++)
            {
                worksheet.Cell(1, i + 1).Value = headers[i];
            }

            // Preencher os dados da tabela com os dados da model
            int row = 2; // Linha inicial para os dados
            foreach (var leitura in Dados.Estacao.ListaLeitura)
            {
                worksheet.Cell(row, 1).Value = leitura.DtCadastro.ToShortDateString();
                worksheet.Cell(row, 2).Value = leitura.DtCadastro.ToString("HH:mm:ss");
                worksheet.Cell(row, 3).Value = leitura.TxUmidade;
                worksheet.Cell(row, 4).Value = leitura.TxTemperatura;
                worksheet.Cell(row, 5).Value = leitura.TxVento;
                worksheet.Cell(row, 6).Value = leitura.AoChuva;
                row++;
            }

            // Definir estilo para os cabeçalhos
            var headerStyle = worksheet.Style;
            headerStyle.Font.Bold = true;

            // Aplicar estilo aos cabeçalhos
            worksheet.Range("A1:F1").Style = headerStyle;

            // Ajustar largura das colunas automaticamente
            worksheet.Columns().AdjustToContents();

            // Salvar o arquivo Excel em memória
            var memoryStream = new System.IO.MemoryStream();
            workbook.SaveAs(memoryStream);

            // Definir o nome do arquivo
            string nomeArquivo = "DadosLeitura.xlsx";

            // Retornar o arquivo Excel como um FileResult
            return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nomeArquivo);
        }


    }
}