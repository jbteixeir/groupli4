using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdA.Camada_de_Dados.Classes.Estruturas
{
    class Enums
    {
        public enum Numero_Respostas
        {
            Sair_Numero,
            Ignorar_Formulario
        };

        public enum Respostas_Vazias
        {
            Sair_Vazias,
            Ignorar_Formulario,
            Ignorar_Pergunta,
            Ignorar_Pergunta_Nao_QE
        };

        public enum Valores_Respostas
        {
            Sair_Valores,
            Ignorar_Formulario,
            Ignorar_Pergunta,
            Ignorar_Pergunta_Nao_QE
        };

        public enum Resultado_Importacao
        {
            Sucesso,
            Insucesso,
            SR
        };

        public enum Tipo_Formulário
        {
            Questionario,
            Ficha_Avaliacao,
            CheckList
        };
    }
}
