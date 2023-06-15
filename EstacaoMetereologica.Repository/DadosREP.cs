using EstacaoMetereologica.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EstacaoMetereologica.Repository
{
    public class DadosREP
    {
        public List<LeituraMOD> ObterTodasLeiturasPorCdEstacao(Int32 CdEstacao)
        {
            List<LeituraMOD> ListaLeitura = new List<LeituraMOD>();

            var url = "https://g58346c3a996906-producao.adb.sa-saopaulo-1.oraclecloudapps.com/ords/devuser/Estacao/LeituraDados?CdEstacao=" + CdEstacao;
            var httpClient = new HttpClient();

            var response = httpClient.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            Task.Delay(1000).Wait();
            var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            JObject json = JObject.Parse(content);
            var items = json["items"];

            ListaLeitura = items.ToObject<List<LeituraMOD>>();

            return ListaLeitura;

        }

        public List<EstacaoMOD> ObterTodasEstacao()
        {
            List<EstacaoMOD> ListaEstacao = new List<EstacaoMOD>();

            var url = "https://g58346c3a996906-producao.adb.sa-saopaulo-1.oraclecloudapps.com/ords/devuser/Estacao/Estacao";
            var httpClient = new HttpClient();

            var response = httpClient.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            Task.Delay(1000).Wait();
            var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            JObject json = JObject.Parse(content);
            var items = json["items"];

            ListaEstacao = items.ToObject<List<EstacaoMOD>>();

            return ListaEstacao;

        }

        public Int32 ObterQtdLinhaLeitura(Int32 CdEstacao)
        {

            var url = "https://g58346c3a996906-producao.adb.sa-saopaulo-1.oraclecloudapps.com/ords/devuser/Estacao/LeituraDadosQtd?CdEstacao=" + CdEstacao;
            var httpClient = new HttpClient();

            var response = httpClient.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            Task.Delay(1000).Wait();
            var qtdLinhasLeituras = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            dynamic jsonResponse = Newtonsoft.Json.JsonConvert.DeserializeObject(qtdLinhasLeituras);
            int qtdLinhas = jsonResponse.qtdlinhas;

            return qtdLinhas;

        }

        public List<LeituraMOD> ObterLeituraPaginada(Int32 CdEstacao, Int32 PrimeiraPosicao)
        {

            List<LeituraMOD> ListaLeitura = new List<LeituraMOD>();

            var url = "https://g58346c3a996906-producao.adb.sa-saopaulo-1.oraclecloudapps.com/ords/devuser/Estacao/LeituraDadosPaginado?CdEstacao=" + CdEstacao + "&PrimeiraPosicao=" + PrimeiraPosicao;
            var httpClient = new HttpClient();

            var response = httpClient.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            Task.Delay(1000).Wait();
            var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            JObject json = JObject.Parse(content);
            var items = json["items"];

            ListaLeitura = items.ToObject<List<LeituraMOD>>();

            return ListaLeitura;

        }

    }
}
