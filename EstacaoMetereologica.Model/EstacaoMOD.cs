using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacaoMetereologica.Model
{
    public class EstacaoMOD
    {
        public EstacaoMOD()
        {
            ListaLeitura = new List<LeituraMOD>();
        }

        [JsonProperty("cdestacao")]
        public Int32 CdEstacao { get; set; }

        [JsonProperty("txcidade")]
        public String TxCidade { get; set; }

        [JsonProperty("txestado")]
        public String TxEstado { get; set; }

        [JsonProperty("txlatitude")]
        public String TxLatitude { get; set; }

        [JsonProperty("txlongitude")]
        public String TxLongitude { get; set; }

        [JsonProperty("txidtela")]
        public String TxIdTela { get; set; }

        [JsonProperty("qtd")]
        public Int32 Qtd { get; set; }

        public List<LeituraMOD> ListaLeitura { get; set; }
    }
}
