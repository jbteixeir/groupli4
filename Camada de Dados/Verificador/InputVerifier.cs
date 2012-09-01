using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdAnalyser.CamadaDados.Classes.Verificador
{
    class Input_Verifier
    {
        public static bool SoNumeros(string s)
        {
            string numeros = "0123456789";
            string limitadores = ".,";
            if (s == "" || !numeros.Contains(s[0])) return false;
            bool found = true;
            for (int i = 1; i < s.Length && found; i++)
                found = numeros.Contains(s[i]) || limitadores.Contains(s[i]);
            return found;
        }

        public static bool SoEspacos(string s)
        {
            if (s == "") return true;
            string possiveis = " \t\n";
            bool found = true;
            for (int i = 0; i < s.Length && found; i++)
                found = possiveis.Contains(s[i]);
            return found;
        }
    }
}
