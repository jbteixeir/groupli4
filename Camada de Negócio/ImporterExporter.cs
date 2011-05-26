
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//The csv reader
using LumenWorks.Framework.IO.Csv;


namespace CamadaNegocio
{
    class ImporterExporter
    {
		public static LinkedList<string[]> importaFicheiro(String datapathFile, bool temCabecalho, ref string[] cabecalhos)
        {
            #region normalAproach
			
			LinkedList<string[]> dados = new LinkedList<string[]>();
			string linha;
			StreamReader tr = new StreamReader(datapathFile);

			char[] separator = { ';' };
			if (temCabecalho)
			{
				// le a primeira linha
				linha = tr.ReadLine();
				cabecalhos = linha.Split(separator);
			}

			// le a primeira linha de dados
			while((linha = tr.ReadLine()) != null)
			{
				string[] terms = linha.Split(separator);
				string[] aux = new string[terms.Length];
				for (int i = 0; i < terms.Length; i++)
					aux[i] = terms[i];

				dados.AddLast(aux);
			}

			// close the stream
			tr.Close();

            #endregion

			#region //CSVReader
			//// criar um CSV Reader
			//CsvReader csv = new CsvReader(
			//    new StreamReader(datapathFile), true, ';');

			//int fieldCount = csv.FieldCount;
			//string[] headers = csv.GetFieldHeaders();
			//string[][] dados = new string[fieldCount][];
			//int j = 0;

			////Console.Beep(600, 5000);
			//Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");
			//Console.WriteLine(fieldCount);
			//Console.WriteLine(headers);
			//Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");

			//while (csv.ReadNextRecord())
			//{
			//    for (int i = 0; i < fieldCount; i++)
			//    {
			//        dados[j][i] = csv[i];
			//        Console.Write(string.Format("{0} = {1};", headers[i], csv[i]));
			//        Console.WriteLine();
			//    }
			//    j++;
			//}
			#endregion

			return dados;
        }

		/*public static void exportaParaFicheiro(String datapathFile, List<Formulario> formulario)
		{

		}*/
    }
}
