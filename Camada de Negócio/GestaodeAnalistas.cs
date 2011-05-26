using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ETdA.Camada_de_Dados.DataBaseCommunicator;

namespace ETdA.Camada_de_Negócio
{
	class GestaodeAnalistas
	{
		//Métodos
		//void registaAnalista(String nome, String username, String password);

		/* 
		 * Faz login na base de dados
		 * @return se nao conseguir fazer login retorna false
		 */
		public bool login(String username, String password)
		{
			return DataBaseCommunicator.connect("rocket-pc", username, password, "ETdA_" + username);


		}
		//void removeAnalisa(String codAnalista);
		//void logout();

	}
}
