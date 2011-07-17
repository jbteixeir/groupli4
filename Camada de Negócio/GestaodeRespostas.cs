using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ETdA.Camada_de_Dados.Classes;
using ETdA.Camada_de_Dados.DataBaseCommunicator;

namespace ETdA.Camada_de_Negócio
{
    class GestaodeRespostas
    {
        public static Dictionary<string, List<TipoEscala>> getTipResposta()
        {
            return FuncsToDataBase.getTiposResposta();
        }

        public static TipoEscala getTipoEscala(long codTipoEscala)
        {
            return FuncsToDataBase.selectTipoEscala(codTipoEscala);
        }

        public static Dictionary<string, List<TipoEscala>> insertNovosTipos(Dictionary<string, List<TipoEscala>> lst)
        {
            foreach (List<TipoEscala> ll in lst.Values)
                foreach (TipoEscala te in ll)
                {
                    if (te.Default == 0)
                    {
                        te.Codigo = FuncsToDataBase.insertTipoEscala(te);
                        te.Default = 1;
                        foreach (EscalaResposta er in te.Respostas)
                        {
                            er.CodTipo = te.Codigo;
                            er.CodEscala = FuncsToDataBase.insertEscalaResposta(er);
                        }
                    }
                }
            return lst;
        }
    }
}
