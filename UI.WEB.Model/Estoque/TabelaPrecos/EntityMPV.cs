using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.WEB.Model.Estoque.TabelaPrecos
{
    public class EntityMPV
    {
        public int MPVID { get; set; }
        public int MATID { get; set; }
        public string MPVPRECOVENDA { get; set; }
        public string MPVMARKUP { get; set; }
        public string MPVPRECOPROMO { get; set; }
        public string MPVINICIOPROMO { get; set; }
        public string MPVFIMPROMO { get; set; }
        public string MPVDTALTERACAO { get; set; }
    }
}
