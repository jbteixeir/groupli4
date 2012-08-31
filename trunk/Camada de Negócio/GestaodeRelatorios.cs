using ETdAnalyser.Camada_de_Dados.Classes;
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
            Relatorio relatorio = new ETdAnalyser.Camada_de_Dados.Classes.Relatorio();
            List<Camada_de_Dados.Classes.Zona> zonas = ETdAnalyser.Camada_de_Dados.ETdA.ETdA.getProjecto(codigoProjecto).Analises[codigoAnalise].Zonas;
            List<Camada_de_Dados.Classes.Item> itens = ETdAnalyser.Camada_de_Dados.ETdA.ETdA.getProjecto(codigoProjecto).Analises[codigoAnalise].Itens;

            relatorio.gerarResultadosRelatorio(codigoAnalise, new List<Camada_de_Dados.Classes.Resposta>(), zonas, itens);
            relatorio.gerarEstatisticasRelatorioSexo(codigoAnalise);
            relatorio.gerarEstatisticasIdade(codigoAnalise);
            relatorio.gerarEstatisticasClienteHabitual(codigoAnalise);

            Camada_de_Interface.InterfaceRelatorio.main(codigoProjecto, codigoAnalise, nome_analise, relatorio);
        }
    }
}
