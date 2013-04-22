using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using ETdAnalyser.Camada_de_Negócio;

namespace ETdAnalyser.CamadaInterface
{
    public partial class InterfaceStarter : Form
    {
        static volatile InterfaceStarter ins;

        static volatile Inicializer inicializer;

        static volatile Thread inicializerThread;

        public static void main()
        {
            ins = new InterfaceStarter();
            InterfaceStarter.start();
            ins.ShowDialog();
        }

        public InterfaceStarter()
        {
            var ci = System.Globalization.CultureInfo.InvariantCulture.Clone() as System.Globalization.CultureInfo;
            ci.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            InitializeComponent();
        }

        public static void end()
        {
            if(ins!=null)
                ins.Close();
        }

        static void start()
        {
            inicializer = new Inicializer();
            inicializerThread = new Thread(inicializer.Inicializar);

            CheckForIllegalCrossThreadCalls = false;
            inicializerThread.SetApartmentState(ApartmentState.STA);
            inicializerThread.Start();
            
        }
    }

    public class Inicializer
    {
        // This method will be called when the thread is started.
        public void Inicializar()
        {
            if (GestaodeAnalistas.existeFicheiroConfiguracao())
            {
                Boolean b = GestaodeAnalistas.loadConnectionUtilizadorLogado();

                if (b)
                {
                    InterfaceStarter.end();
                    InterfaceGuestaoProjectos.main(true);
                }
                else
                {
                    b = GestaodeAnalistas.loadConnectionSuper();

                    if (b)
                    {
                        InterfaceStarter.end();
                        InterfaceLogin.main();
                    }
                    else
                    {
                        InterfaceStarter.end();
                        InterfaceConfigurarLigacaoBD.main();
                        InterfaceLogin.main();
                    }
                }
            }
            else
            {
                InterfaceStarter.end();
                InterfaceLogin.main();
            }
            
        }
    }
}
