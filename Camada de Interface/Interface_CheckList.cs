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
    public partial class Interface_CheckList : Form
    {
        public Interface_CheckList(long codProjecto, long codAnalise)
        {
            InitializeComponent();
            CheckList.Url = new System.Uri("http://rocket-pc:54749/ETdA/Default.aspx?form=CL&usr=" +
                Camada_de_Dados.ETdA.ETdA.Username + "&anl=" + codAnalise + "&prj" +
                    "=" + codProjecto, System.UriKind.Absolute);
        }

        private void Interface_CheckList_Load(object sender, EventArgs e)
        {

        }

        private void CheckList_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
    }
}
