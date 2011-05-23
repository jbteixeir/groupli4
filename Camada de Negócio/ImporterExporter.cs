using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdA.Camada_de_Negócio
{
    class ImporterExporter
    {
        void importaFicheiro(String datapathFile);
        void exportaParaFicheiro(String datapathFile, List<Formulario> formulario);
    }
}
