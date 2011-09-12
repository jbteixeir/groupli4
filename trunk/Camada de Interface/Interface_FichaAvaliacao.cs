using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ETdA.Camada_de_Interface
{
    public partial class Interface_FichaAvaliacao : Form
    {
        public Interface_FichaAvaliacao(long codProjecto, long codAnalise)
        {
            InitializeComponent();
            FichaAvaliacao.Url = new System.Uri("http://rocket-pc:54749/ETdA/Default.aspx?form=FA&usr=" + 
                Camada_de_Dados.ETdA.ETdA.Username + "&anl=" + codAnalise + "&prj" +
                    "=" + codProjecto + "&adminmode=true", System.UriKind.Absolute);
        }
    }
}
