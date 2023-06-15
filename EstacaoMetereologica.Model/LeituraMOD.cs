using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacaoMetereologica.Model
{
    public sealed class LeituraMOD
    {
        [JsonProperty("txumidade")]
        public String TxUmidade { get; set; }

        [JsonProperty("txtemperatura")]
        public String TxTemperatura { get; set; }

        [JsonProperty("txvento")]
        public String TxVento { get; set; }

        [JsonProperty("aochuva")]
        public String AoChuva { get; set; }

        [JsonProperty("dtcadastro")]
        public DateTime DtCadastro { get; set; }

    }
}
