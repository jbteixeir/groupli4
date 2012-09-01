using ETdAnalyser.CamadaDados.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdAnalyser.Camada_de_Negócio
{
    class GestaodeRelatorios
    {
        public static void GerarRelatório(long codigoProjecto, long codigoAnalise, String nome_analise)
        {
            Relatorio relatorio = new ETdAnalyser.CamadaDados.Classes.Relatorio();
            List<CamadaDados.Classes.Zona> zonas = ETdAnalyser.CamadaDados.ETdA.ETdA.GetProjecto(codigoProjecto).Analises[codigoAnalise].Zonas;
            List<CamadaDados.Classes.Item> itens = ETdAnalyser.CamadaDados.ETdA.ETdA.GetProjecto(codigoProjecto).Analises[codigoAnalise].Itens;

            relatorio.GerarResultadosRelatorio(codigoAnalise, new List<CamadaDados.Classes.Resposta>(), zonas, itens);
            relatorio.GerarEstatisticasRelatorioSexo(codigoAnalise);
            relatorio.GerarEstatisticasIdade(codigoAnalise);
            relatorio.GerarEstatisticasClienteHabitual(codigoAnalise);

            CamadaInterface.InterfaceRelatorio.main(codigoProjecto, codigoAnalise, nome_analise, relatorio);
        }
    }
}
