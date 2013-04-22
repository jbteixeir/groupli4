using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ETdAnalyser.CamadaInterface
{
    public partial class InterfaceCreditos : Form
    {
        public InterfaceCreditos()
        {
            InitializeComponent();
            this.ShowDialog();
        }

        public static void main()
        {
            var ci = System.Globalization.CultureInfo.InvariantCulture.Clone() as System.Globalization.CultureInfo;
            ci.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            InterfaceCreditos i = new InterfaceCreditos();
        }
    }
}
