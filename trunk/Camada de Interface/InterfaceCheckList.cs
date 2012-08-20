using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ETdA.Camada_de_Negócio;

namespace ETdA.Camada_de_Interface
{
    public partial class InterfaceCheckList : Form
    {
        public InterfaceCheckList(long codProjecto, long codAnalise)
        {
            InitializeComponent();
            CheckList.ScriptErrorsSuppressed = true;
            CheckList.Url = new System.Uri("http://" + GestaodeAnalistas.nomeServidorWeb() + ":" + GestaodeAnalistas.portaServidorWeb() + "/ETdA/Default.aspx?form=CL&usr=" +
                Camada_de_Dados.ETdA.ETdA.Username + "&anl=" + codAnalise + "&prj" +
                    "=" + codProjecto + "&adminmode=true", System.UriKind.Absolute);
        }

        private void Interface_CheckList_Load(object sender, EventArgs e)
        {

        }

        private void CheckList_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
    }
}
