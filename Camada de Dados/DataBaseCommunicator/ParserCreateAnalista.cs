using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdA.Camada_de_Dados.DataBaseCommunicator
{
    class ParserCreateAnalista
    {

        private static string query1(string usr)
        {
            string s1 = "CREATE DATABASE ETdA_" + usr + ";\n" +
                        "";// "GO \n";
            return s1;
        }

		#region cenas...
        private static string query2(string usr, string pw)
        {
            string s2 = "USE model;\n" + 
                        "CREATE LOGIN " + usr + " \n" +
                        "WITH PASSWORD = '" + pw + "';\n" +
                        "USE ETdA_" + usr + ";\n" +
                        "CREATE USER " + usr + " FOR LOGIN " + usr + ";\n" +
                        "EXEC sp_addrolemember N'db_datareader', N'" + usr + "'\n" +
                        "EXEC sp_addrolemember N'db_datawriter', N'" + usr + "'\n" +
                        "";// "GO \n";

            return s2;
        }

        private static string query3(string usr)
        {
            string s3 = "USE ETdA_" + usr + ";\n" +
                        "";// "GO \n";

            return s3;
        }

        private static string query4()
        {
            string s4 = "CREATE PROCEDURE criarTabelas \n" +
                        "AS \n";

            return s4;
        }

        private static string create1()
        {
            string c1 = "create table projecto(\n" + 
	                    "cod_projecto bigint identity primary key,\n" +
	                    "estabelecimento varchar(20) not null,\n" +
	                    "ultimaActualizacao datetime not null\n" +
                        ");\n";
            return c1;
        }

        private static string create2()
        {
            string c2 = "create table analise(\n" +
	                    "cod_analise bigint identity primary key,\n" +
	                    "cod_projecto bigint not null Foreign Key references Projecto(cod_projecto),\n" +
	                    "dataCriacao datetime not null,\n" +
	                    "nomeAnalise varchar(20) not null,\n" +
	                    "tipoAnalise varchar(20) not null,\n" +
	                    "estadoWebCheckList tinyint not null,\n" +
	                    "estadoWebFichaAvaliacao tinyint not null,\n" +
	                    "estadoWebQuestionario tinyint not null\n" + 
                        ");\n";
            return c2;
        }

        private static string create3()
        {
            string c3 = "create table item(\n" +
	                    "cod_item bigint identity primary key,\n" +
	                    "nome_item varchar(50) not null,\n" +
	                    "default_item bit not null\n" +
                        ");\n";
            return c3;
        }

        private static string create4()
        {
            string c4 = "create table item_analise(\n" +
	                    "cod_item_analise bigint identity primary key,\n" +
	                    "cod_item bigint foreign key references Item(cod_item) not null,\n" +
	                    "cod_analise bigint not null foreign key references Analise(cod_analise),\n" +
	                    "ponderacao_analista float(23) not null,\n" +
	                    "ponderacao_profissional float(23) not null,\n" +
	                    "ponderacao_cliente float(23) not null,\n" +
	                    "inter_vermelho float(23) not null,\n" +
	                    "inter_laranja float(23) not null,\n" +
	                    "inter_amarelo float(23) not null,\n" +
	                    "inter_verdelima float(23) not null,\n" +
	                    "inter_verde float(23) not null,\n" +
	                    "limite_inferior_analista float(23) not null\n" +
                        ");\n";
            return c4;
        }

        private static string create5()
        {
            string c5 = "create table zona(\n" +
	                    "cod_zona bigint Identity Primary Key,\n" +
	                    "nome_zona varchar(50) not null\n" +
                        ");\n";
            return c5;
        }

        private static string create6()
        {
            string c6 = "create table zona_analise(\n" +
	                    "cod_zona_analise bigint identity primary key,\n" +
	                    "cod_zona bigint not null Foreign Key references zona(cod_zona),\n" +
	                    "cod_analise bigint Foreign Key references Analise(cod_analise)\n" +
                        ");\n";
            return c6;
        }

        private static string create7()
        {
            string c7 = "create table resposta_checkList(\n" +
	                    "cod_resposta_checkList bigint identity primary key,\n" +
	                    "cod_analise bigint not null Foreign Key references Analise(cod_analise),\n" +
	                    "cod_zona bigint not null Foreign Key references Zona(cod_zona),\n" +
	                    "cod_item bigint not null Foreign Key references Item(cod_item),\n" +
	                    "valor tinyint\n" +
                        ");\n";
            return c7;
        }

        private static string create8()
        {
            string c8 = "create table ficha_avaliacao(\n" +
	                    "cod_fichaAvaliacao bigint identity primary key,\n" +
	                    "cod_analise bigint not null foreign key references Analise(cod_analise),\n" +
	                    "cod_zona bigint not null Foreign Key references zona(cod_zona),\n" +
                        ");\n";

            return c8;
        }

        private static string create9()
        {
            string c9 = "CREATE TABLE TipoEscala\n" +
                        "(cod_tipoEscala BIGINT IDENTITY PRIMARY KEY,\n" +
                        "tipoEscalaResposta VARCHAR(30),\n" +
	                    "numeroEscalaResposta INT,\n" +
	                    "default_tipoEscala bit\n" +
                        ");\n";
            return c9;
        }

        private static string create10()
        {
            string c10 = "CREATE TABLE EscalaResposta\n" +
	                     "(cod_EscalaResposta BIGINT IDENTITY PRIMARY KEY,\n" +
	                     "cod_TipoEscala BIGINT,\n" +
	                     "descricaoEscalaResposta VARCHAR(30),\n" +
	                     "valorEscalaResposta INT,\n" +
	                     "FOREIGN KEY (cod_tipoEscala) REFERENCES TipoEscala(cod_TipoEscala)\n" +
	                     ");\n";
            return c10;
        }

        private static string create11()
        {
            string c11 = "create table pergunta_ficha_avaliacao(\n" +
	                     "cod_pergunta_ficha_avaliacao bigint identity primary key,\n" +
	                     "cod_analise bigint not null foreign key references Analise(cod_analise),\n" +
	                     "numero_pergunta int not null,\n" +
	                     "cod_item bigint Foreign Key references Item(cod_item),\n" +
	                     "texto varchar(500),\n" +
	                     "cod_tipoEscala bigint not null,\n" +
	                     "FOREIGN Key (cod_tipoEscala) references TipoEscala(cod_tipoEscala)\n" +
                         ");\n";
            return c11;
        }

        private static string create12()
        {
            string c12 = "create table resposta_ficha_avaliacao_numero(\n" +
	                     "cod_resposta_ficha_avaliacao_numero bigint identity primary key,\n" +
	                     "cod_fichaAvaliacao bigint not null,\n" +
	                     "cod_analise bigint not null,\n" +
	                     "numero_pergunta tinyint,\n" +
	                     "valor tinyint,\n" +
	                     "Foreign Key (cod_fichaAvaliacao) references ficha_avaliacao(cod_fichaAvaliacao)\n" +
                         ");\n";
            return c12;
        }

        private static string create13()
        {
            string c13 = "create table questionario(\n" +
	                     "cod_questionario bigint identity primary key,\n" +
	                     "cod_analise bigint not null Foreign Key references Analise(cod_analise),\n" +
                         ");\n";
            return c13;
        }

        private static string create14()
        {
            string c14 = "create table pergunta_questionario(\n" +
	                     "cod_pergunta_questionario bigint identity primary key,\n" +
	                     "cod_analise bigint not null,\n" +
	                     "numero_pergunta float(23) not null,\n" +
	                     "cod_zona bigint Foreign Key references Zona(cod_zona),\n" +
	                     "cod_item  bigint Foreign Key references item(cod_item),\n" +
	                     "cod_tipoEscala bigint,\n" +
	                     "texto varchar(500) not null,\n" +
	                     "tipo_questao varchar(2) not null,\n" +
	                     "Foreign key (cod_analise) references Analise(cod_analise),\n" +
	                     "FOREIGN Key (cod_tipoEscala) references TipoEscala(cod_tipoEscala)\n" +
                         ");\n";
            return c14;
        }

        private static string create15()
        {
            string c15 = "create table resposta_questionario_numero(\n" + 
	                     "cod_resposta_questionario_numero bigint identity primary key,\n" +
	                     "cod_questionario bigint not null,\n" +
	                     "cod_analise bigint not null,\n" +
	                     "cod_zona bigint foreign key references Zona(cod_zona),\n" +
	                     "numero_pergunta float(23) not null,\n" +
	                     "valor tinyint,\n" +
	                     "cod_pergunta_questionario bigint,\n" +
	                     "Foreign Key (cod_pergunta_questionario) references pergunta_questionario(cod_pergunta_questionario),\n" +
	                     "Foreign Key (cod_questionario) references questionario(cod_questionario),\n" +
	                     "foreign key (cod_analise) references analise(cod_analise)\n" +
                         ");\n";
            return c15;
        }

        private static string create16()
        {
            string c16 = "create table respostas_questionarios_string(\n" +
	                     "cod_resposta_questionario_string bigint identity primary key,\n" +
	                     "cod_questionario bigint not null,\n" +
	                     "cod_analise bigint not null,\n" +
	                     "cod_zona bigint foreign key references Zona(cod_zona),\n" +
	                     "numero_pergunta float(23) not null,\n" +
	                     "valor varchar(20),\n" +
	                     "cod_pergunta_questionario bigint,\n" +
	                     "Foreign Key (cod_pergunta_questionario) references pergunta_questionario(cod_pergunta_questionario),\n" +
	                     "Foreign Key (cod_questionario) references questionario(cod_questionario),\n" +
	                     "foreign key (cod_analise) references analise(cod_analise)\n" +
                         ");\n";
            return c16;
        }

        private static string create17()
        {
            string c17 = "create table resposta_questionario_memo(\n" +
	                     "cod_resposta_questionario_memo bigint identity primary key,\n" +
	                     "cod_questionario bigint not null,\n" +
	                     "cod_analise bigint not null,\n" +
	                     "cod_zona bigint foreign key references Zona(cod_zona),\n" +
	                     "numero_pergunta float(23) not null,\n" +
	                     "valor varchar(3000),\n" +
	                     "cod_pergunta_questionario bigint,\n" +
	                     "Foreign Key (cod_pergunta_questionario) references pergunta_questionario(cod_pergunta_questionario),\n" +
	                     "Foreign Key (cod_questionario) references questionario(cod_questionario),\n" +
	                     "foreign key (cod_analise) references analise(cod_analise)\n" +
                         ");\n";
            return c17;
        }

        private static string create18()
        {
            string c18 = "create table resposta_ficha_avaliacao_string(\n" +
                         "cod_resposta_ficha_avaliacao_string bigint primary key,\n" +
                         "cod_fichaAvaliacao bigint not null,\n" +
                         "cod_analise bigint not null,\n" +
                         "numero_pergunta tinyint,\n" +
                         "valor varchar(500),\n" +
                         "Foreign Key (cod_fichaAvaliacao) references ficha_avaliacao(cod_fichaAvaliacao)\n" +
                         ");\n";
            return c18;
        }
		#endregion

		private static string query5()
        {
            string q5 = "";// "GO \n";

            return q5;
        }

        private static string query6(string usr)
        {
            string q6 = "USE ETdA_" + usr + ";\n" +
                        "";// "GO \n";
            return q6;
        }

        private static string query7()
        {
            string q7 = "EXECUTE criarTabelas;\n" +
                        "";// "GO";
            return q7;
        }
		#region
		public static List<string> devolveQuery(string usr, string pw)
        {
            List<string> strs = new List<string>();
            strs.Add(query1(usr));
            strs.Add(query2(usr, pw));
            strs.Add(query3(usr));
            strs.Add(query4() + 
                create1() + create2() + create3() + create4() + create5() +
                create6() + create7() + create8() + create9() + create10() +
                create11() + create12() + create13() + create14() + create15() +
                create16() + create17() + create18() + query5());
            strs.Add(query6(usr));
            strs.Add(query7());

            return strs;
        }
    }
}

		#endregion
