using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using ETdA.Camada_de_Negócio;

namespace ETdA.Camada_de_Interface
{
    public partial class Interface_Starter : Form
    {
        static volatile Interface_Starter ins;

        static volatile Inicializer inicializer;

        static volatile Thread inicializerThread;

        public static void main()
        {
            ins = new Interface_Starter();
            Interface_Starter.start();
            ins.ShowDialog();
        }

        public Interface_Starter()
        {
            InitializeComponent();
        }

        public static void end()
        {
            //ins.Dispose();
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
                    Interface_Starter.end();
                    InterfaceGuestaoProjectos.main(true);
                }
                else
                {
                    b = GestaodeAnalistas.loadConnectionSuper();

                    if (b)
                    {
                        Interface_Starter.end();
                        InterfaceLogin.main();
                    }
                    else
                    {
                        Interface_Starter.end();
                        Interface_ConfigurarLigacaoBD.main();
                        InterfaceLogin.main();
                    }
                }
            }
            else
            {
                Interface_Starter.end();
                InterfaceLogin.main();
            }
            
        }
    }
}
