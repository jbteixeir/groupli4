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
    public partial class Interface_Questionario : Form
    {
        public Interface_Questionario(long codProjecto, long codAnalise)
        {
            InitializeComponent();
            Questionario.Url = new System.Uri("http://rocket-pc:54749/ETdA/Default.aspx?form=QT&usr=" + 
                Camada_de_Dados.ETdA.ETdA.Username + "&anl=" + codAnalise + "&prj" +
                    "=" + codProjecto + "&adminmode=true", System.UriKind.Absolute);
        }
    }
}
