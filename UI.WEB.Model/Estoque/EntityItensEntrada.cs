﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.WEB.Model.Estoque
{

    [Table("TB_MVM_MOVMATITEM")]
    public class EntityItensEntrada
    {

		public int MVMID { get; set; }
        public int MVNID { get; set; }
        public int COPID { get; set; }
        public int MATID { get; set; }
        public string MVMQUANTIDADE { get; set; }
        public string MVMVALUNITARIO { get; set; }
        public string MVMVALIPI { get; set; }
        public string MVMVALCUSTO { get; set; }
        public string MVMMARKUP { get; set; }
        public string MVMVALVENDA { get; set; }
        public string MVMTIPO { get; set; }

    }
}
