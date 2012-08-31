using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdAnalyser.Camada_de_Dados.DataBaseCommunicator
{
    class ParserCreateAnalista
    {

        private static string queryCriarBD(string usr)
        {
            string s1 = "CREATE DATABASE ETdA_" + usr + ";\n" +
                        "";// "GO \n";
            return s1;
        }

		#region querys
        private static string queryCriarUtilizador(string usr, string pw)
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

        private static string createProjecto()
        {
            string c1 = "create table projecto(\n" + 
	                    "codigoProjecto bigint identity primary key,\n" +
	                    "estabelecimento varchar(200) not null,\n" +
	                    "ultimaActualizacao datetime not null,\n" +
                        "activo tinyint not null\n" +
                        ");\n";
            return c1;
        }

        private static string createAnalise()
        {
            string c2 = "create table analise(\n" +
	                    "codigoAnalise bigint identity primary key,\n" +
	                    "codigoProjecto bigint not null Foreign Key references Projecto(codigoProjecto),\n" +
	                    "dataCriacao datetime not null,\n" +
	                    "nomeAnalise varchar(200) not null,\n" +
	                    "tipoAnalise varchar(20) not null,\n" +
	                    "estadoWebCheckList tinyint not null,\n" +
	                    "estadoWebFichaAvaliacao tinyint not null,\n" +
	                    "estadoWebQuestionario tinyint not null,\n" +
                        "activo tinyint not null\n" +
                        ");\n";
            return c2;
        }

        private static string createItem()
        {
            string c3 = "create table item(\n" +
	                    "cod_item bigint identity primary key,\n" +
	                    "nome_item varchar(200) not null,\n" +
	                    "default_item bit not null\n" +
                        ");\n";
            return c3;
        }

        private static string createItemAnalise()
        {
            string c4 = "create table item_analise(\n" +
	                    "cod_item_analise bigint identity primary key,\n" +
	                    "cod_item bigint foreign key references Item(cod_item) not null,\n" +
	                    "codigoAnalise bigint not null foreign key references Analise(codigoAnalise),\n" +
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

        private static string createZona()
        {
            string c5 = "create table zona(\n" +
	                    "cod_zona bigint Identity Primary Key,\n" +
	                    "nome_zona varchar(200) not null\n" +
                        ");\n";
            return c5;
        }

        private static string createZonaAnalise()
        {
            string c6 = "create table zona_analise(\n" +
	                    "cod_zona_analise bigint identity primary key,\n" +
	                    "cod_zona bigint not null Foreign Key references zona(cod_zona),\n" +
	                    "codigoAnalise bigint Foreign Key references Analise(codigoAnalise)\n" +
                        ");\n";
            return c6;
        }

        private static string createRespostaChecklist()
        {
            string c7 = "create table resposta_checkList(\n" +
	                    "cod_resposta_checkList bigint identity primary key,\n" +
	                    "codigoAnalise bigint not null Foreign Key references Analise(codigoAnalise),\n" +
	                    "cod_zona bigint not null Foreign Key references Zona(cod_zona),\n" +
	                    "cod_item bigint not null Foreign Key references Item(cod_item),\n" +
	                    "valor tinyint\n" +
                        ");\n";
            return c7;
        }

        private static string createFichaAvaliacao()
        {
            string c8 = "create table ficha_avaliacao(\n" +
	                    "cod_fichaAvaliacao bigint identity primary key,\n" +
	                    "codigoAnalise bigint not null foreign key references Analise(codigoAnalise),\n" +
	                    "cod_zona bigint not null Foreign Key references zona(cod_zona),\n" +
                        ");\n";

            return c8;
        }

        private static string createTipoEscala()
        {
            string c9 = "CREATE TABLE TipoEscala\n" +
                        "(cod_tipoEscala BIGINT IDENTITY PRIMARY KEY,\n" +
                        "tipoEscalaResposta VARCHAR(200),\n" +
	                    "numeroEscalaResposta INT,\n" +
	                    "default_tipoEscala bit\n" +
                        ");\n";
            return c9;
        }

        private static string createEscalaResposta()
        {
            string c10 = "CREATE TABLE EscalaResposta\n" +
	                     "(cod_EscalaResposta BIGINT IDENTITY PRIMARY KEY,\n" +
	                     "cod_TipoEscala BIGINT,\n" +
	                     "descricaoEscalaResposta VARCHAR(200),\n" +
	                     "valorEscalaResposta INT,\n" +
	                     "FOREIGN KEY (cod_tipoEscala) REFERENCES TipoEscala(cod_TipoEscala)\n" +
	                     ");\n";
            return c10;
        }

        private static string createPerguntaFichaAvaliacao()
        {
            string c11 = "create table pergunta_ficha_avaliacao(\n" +
	                     "cod_pergunta_ficha_avaliacao bigint identity primary key,\n" +
	                     "codigoAnalise bigint not null foreign key references Analise(codigoAnalise),\n" +
	                     "numero_pergunta int not null,\n" +
	                     "cod_item bigint Foreign Key references Item(cod_item),\n" +
	                     "texto varchar(500),\n" +
	                     "cod_tipoEscala bigint not null,\n" +
	                     "FOREIGN Key (cod_tipoEscala) references TipoEscala(cod_tipoEscala)\n" +
                         ");\n";
            return c11;
        }

        private static string createRespostaFichaAvaliacaoNumero()
        {
            string c12 = "create table resposta_ficha_avaliacao_numero(\n" +
	                     "cod_resposta_ficha_avaliacao_numero bigint identity primary key,\n" +
	                     "cod_fichaAvaliacao bigint not null,\n" +
	                     "codigoAnalise bigint not null,\n" +
	                     "numero_pergunta tinyint,\n" +
	                     "valor tinyint,\n" +
	                     "Foreign Key (cod_fichaAvaliacao) references ficha_avaliacao(cod_fichaAvaliacao)\n" +
                         ");\n";
            return c12;
        }

        private static string createQuestionario()
        {
            string c13 = "create table questionario(\n" +
	                     "cod_questionario bigint identity primary key,\n" +
	                     "codigoAnalise bigint not null Foreign Key references Analise(codigoAnalise),\n" +
                         ");\n";
            return c13;
        }

        private static string createPerguntaQuestionario()
        {
            string c14 = "create table pergunta_questionario(\n" +
	                     "cod_pergunta_questionario bigint identity primary key,\n" +
	                     "codigoAnalise bigint not null,\n" +
	                     "numero_pergunta float(23) not null,\n" +
	                     "cod_zona bigint Foreign Key references Zona(cod_zona),\n" +
	                     "cod_item  bigint Foreign Key references item(cod_item),\n" +
	                     "cod_tipoEscala bigint,\n" +
	                     "texto varchar(500) not null,\n" +
	                     "tipo_questao varchar(2) not null,\n" +
	                     "Foreign key (codigoAnalise) references Analise(codigoAnalise),\n" +
	                     "FOREIGN Key (cod_tipoEscala) references TipoEscala(cod_tipoEscala)\n" +
                         ");\n";
            return c14;
        }

        private static string createRespostaQuestionarioNumero()
        {
            string c15 = "create table resposta_questionario_numero(\n" + 
	                     "cod_resposta_questionario_numero bigint identity primary key,\n" +
	                     "cod_questionario bigint not null,\n" +
	                     "codigoAnalise bigint not null,\n" +
	                     "cod_zona bigint foreign key references Zona(cod_zona),\n" +
	                     "numero_pergunta float(23) not null,\n" +
	                     "valor tinyint,\n" +
	                     "cod_pergunta_questionario bigint,\n" +
	                     "Foreign Key (cod_pergunta_questionario) references pergunta_questionario(cod_pergunta_questionario),\n" +
	                     "Foreign Key (cod_questionario) references questionario(cod_questionario),\n" +
	                     "foreign key (codigoAnalise) references analise(codigoAnalise)\n" +
                         ");\n";
            return c15;
        }

        private static string createRespostaQuestionarioString()
        {
            string c16 = "create table resposta_questionario_string(\n" +
	                     "cod_resposta_questionario_string bigint identity primary key,\n" +
	                     "cod_questionario bigint not null,\n" +
	                     "codigoAnalise bigint not null,\n" +
	                     "cod_zona bigint foreign key references Zona(cod_zona),\n" +
	                     "numero_pergunta float(23) not null,\n" +
	                     "valor varchar(20),\n" +
	                     "cod_pergunta_questionario bigint,\n" +
	                     "Foreign Key (cod_pergunta_questionario) references pergunta_questionario(cod_pergunta_questionario),\n" +
	                     "Foreign Key (cod_questionario) references questionario(cod_questionario),\n" +
	                     "foreign key (codigoAnalise) references analise(codigoAnalise)\n" +
                         ");\n";
            return c16;
        }

        private static string createRespostaQuestionarioMemo()
        {
            string c17 = "create table resposta_questionario_memo(\n" +
	                     "cod_resposta_questionario_memo bigint identity primary key,\n" +
	                     "cod_questionario bigint not null,\n" +
	                     "codigoAnalise bigint not null,\n" +
	                     "cod_zona bigint foreign key references Zona(cod_zona),\n" +
	                     "numero_pergunta float(23) not null,\n" +
	                     "valor varchar(3000),\n" +
	                     "cod_pergunta_questionario bigint,\n" +
	                     "Foreign Key (cod_pergunta_questionario) references pergunta_questionario(cod_pergunta_questionario),\n" +
	                     "Foreign Key (cod_questionario) references questionario(cod_questionario),\n" +
	                     "foreign key (codigoAnalise) references analise(codigoAnalise)\n" +
                         ");\n";
            return c17;
        }

        private static string createRespostaFichaAvaliacaoString()
        {
            string c18 = "create table resposta_ficha_avaliacao_string(\n" +
                         "cod_resposta_ficha_avaliacao_string bigint identity primary key,\n" +
                         "cod_fichaAvaliacao bigint not null,\n" +
                         "codigoAnalise bigint not null,\n" +
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

        private static string queryUseDBUser(string usr)
        {
            string q6 = "USE ETdA_" + usr + ";\n" +
                        "";// "GO \n";
            return q6;
        }

        private static string insertsPredefinidos()
        {
            string query =   "INSERT INTO item VALUES ( 'Ruido', 1);" +
                             "INSERT INTO item VALUES ( 'Temperatura', 1);" +
                             "INSERT INTO item VALUES ( 'Iluminação', 1);" +
                             "INSERT INTO item VALUES ( 'Risco Acidente', 1);" +
                             "INSERT INTO item VALUES ( 'Nível de Atenção Requerido', 1);" +
                             "INSERT INTO item VALUES ( 'Actividade Física', 1);" +
                             "INSERT INTO item VALUES ( 'Espaço de Trabalho', 1);" +
                             "INSERT INTO item VALUES ( 'Posturas / Movimento', 1);" +
                             "INSERT INTO item VALUES ( 'Tarefas de elevação', 1);" +
                             "INSERT INTO item VALUES ( 'Comunicação Inter-relação', 1);" +
                             "INSERT INTO item VALUES ( 'Conteúdo', 1);" +
                             "INSERT INTO item VALUES ( 'Tomada de decisões', 1);" +
                             "INSERT INTO item VALUES ( 'Repetitividade', 1);" +
                             "INSERT INTO item VALUES ( 'Restritividade', 1);" +
                             "INSERT INTO zona VALUES ('Comum');" +
                             "INSERT INTO TipoEscala VALUES ('Frequencia', 5, 1);" +
                             "INSERT INTO TipoEscala VALUES ('Qualidade', 5, 1);" +
                             "INSERT INTO TipoEscala VALUES ('Intensidade', 3, 1);" +
                             "INSERT INTO TipoEscala VALUES ('Probabilidade', 5, 1);" +
                             "INSERT INTO TipoEscala VALUES ('Satisfação', 5, 1);" +
                             "INSERT INTO TipoEscala VALUES ('Opção', 2, 1);" +
                             "INSERT INTO TipoEscala VALUES ('Qualidade', 4, 1);" +
                             "INSERT INTO TipoEscala VALUES ('Sexo', 2, 1);" +
                             "INSERT INTO TipoEscala VALUES ('Idade', 1, 1);" +
                             "INSERT INTO TipoEscala VALUES ('Profissao', 0, 1);" +
                             "INSERT INTO TipoEscala VALUES ('Habilitacoes', 6, 1);" +
                             "INSERT INTO TipoEscala VALUES ('Intensidade', 3, 1);" +
                             "INSERT INTO EscalaResposta VALUES ( 1, 'Nunca', 1);" +
                             "INSERT INTO EscalaResposta VALUES ( 1, 'Raramente', 2);" +
                             "INSERT INTO EscalaResposta VALUES ( 1, 'Às Vezes', 3);" +
                             "INSERT INTO EscalaResposta VALUES ( 1, 'Muitas Vezes', 4);" +
                             "INSERT INTO EscalaResposta VALUES ( 1, 'Sempre', 5);" +
                             "INSERT INTO EscalaResposta VALUES ( 2, 'Muito Mau', 1);" +
                             "INSERT INTO EscalaResposta VALUES ( 2, 'Mau', 2);" +
                             "INSERT INTO EscalaResposta VALUES ( 2, 'Razoável', 3);" +
                             "INSERT INTO EscalaResposta VALUES ( 2, 'Bom', 4);" +
                             "INSERT INTO EscalaResposta VALUES ( 2, 'Muito Bom', 5);" +
                             "INSERT INTO EscalaResposta VALUES ( 3, 'Fraca', 1);" +
                             "INSERT INTO EscalaResposta VALUES ( 3, 'Média', 2);" +
                             "INSERT INTO EscalaResposta VALUES ( 3, 'Intensa', 3);" +
                             "INSERT INTO EscalaResposta VALUES ( 4, 'Impossível', 1);" +
                             "INSERT INTO EscalaResposta VALUES ( 4, 'Pouco Provável', 2);" +
                             "INSERT INTO EscalaResposta VALUES ( 4, 'Provável', 3);" +
                             "INSERT INTO EscalaResposta VALUES ( 4, 'Muito Provável', 4);" +
                             "INSERT INTO EscalaResposta VALUES ( 4, 'Certo', 5);" +
                             "INSERT INTO EscalaResposta VALUES ( 5, 'Muito Insatisfeito', 1);" +
                             "INSERT INTO EscalaResposta VALUES ( 5, 'Insatisfeito', 2);" +
                             "INSERT INTO EscalaResposta VALUES ( 5, 'Indiferente', 3);" +
                             "INSERT INTO EscalaResposta VALUES ( 5, 'Satisfeito', 4);" +
                             "INSERT INTO EscalaResposta VALUES ( 5, 'Muito Satisfeito', 5);" +
                             "INSERT INTO EscalaResposta VALUES ( 6, 'Sim', 1);" +
                             "INSERT INTO EscalaResposta VALUES ( 6, 'Não', 2);" +
                             "INSERT INTO EscalaResposta VALUES ( 7, 'Muito Mau', 1);" +
                             "INSERT INTO EscalaResposta VALUES ( 7, 'Mau', 2);" +
                             "INSERT INTO EscalaResposta VALUES ( 7, 'Bom', 3);" +
                             "INSERT INTO EscalaResposta VALUES ( 7, 'Muito Bom', 4);" +
                             "INSERT INTO EscalaResposta VALUES ( 8, 'Feminino', 1);" +
                             "INSERT INTO EscalaResposta VALUES ( 8, 'Masculino', 2);" +
                             "INSERT INTO EscalaResposta VALUES ( 11, 'Até 12º ano', 1);" +
                             "INSERT INTO EscalaResposta VALUES ( 11, 'Licenciatura', 2);" +
                             "INSERT INTO EscalaResposta VALUES ( 11, 'Mestrado', 3);" +
                             "INSERT INTO EscalaResposta VALUES ( 11, 'Doutoramento', 4);" +
                             "INSERT INTO EscalaResposta VALUES ( 11, 'Curso Tecnológico/Profissional', 5);" +
                             "INSERT INTO EscalaResposta VALUES ( 11, 'Outro', 6);" +
                             "INSERT INTO EscalaResposta VALUES ( 12, 'Muito Pouca', 1);" +
                             "INSERT INTO EscalaResposta VALUES ( 12, 'Pouca', 2);" +
                             "INSERT INTO EscalaResposta VALUES ( 12, 'Alguma', 3);" +
                             "INSERT INTO EscalaResposta VALUES ( 12, 'Muita', 4);" +
                             "INSERT INTO EscalaResposta VALUES ( 12, 'Bastante', 5);";
           return query;
        }
		#region
		public static List<string> devolveQuery(string usr, string pw)
        {
            List<string> strs = new List<string>();
            strs.Add(queryCriarBD(usr));
            strs.Add(queryCriarUtilizador(usr, pw));
            strs.Add(queryUseDBUser(usr));
            strs.Add( 
                createProjecto() + createAnalise() + createItem() + createItemAnalise() + createZona() + createZonaAnalise() 
                + createRespostaChecklist() + createFichaAvaliacao() + createTipoEscala() + createEscalaResposta() +
                createPerguntaFichaAvaliacao() + createRespostaFichaAvaliacaoNumero() + createQuestionario() 
                + createPerguntaQuestionario() + createRespostaQuestionarioNumero() +
                createRespostaQuestionarioString() + createRespostaQuestionarioMemo() + createRespostaFichaAvaliacaoString());
            strs.Add(insertsPredefinidos());

            return strs;
        }
    }
}

		#endregion
