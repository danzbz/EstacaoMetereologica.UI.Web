using EstacaoMetereologica.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EstacaoMetereologica.UI.Web.Models
{
    public class DadosViewMOD
    {

        public DadosViewMOD()
        {
            Estacao = new EstacaoMOD();
            ListaEstacao = new List<EstacaoMOD>();
        }

        public EstacaoMOD Estacao { get; set; }

        public List<EstacaoMOD> ListaEstacao { get; set; }


    }
}