using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ETdA.Camada_de_Interface
{
    public partial class Interface_Ajuda : Form
    {
        private static Interface_Ajuda ia;

        public Interface_Ajuda()
        {
            InitializeComponent();
            initInterface();
        }

        public static void main()
        {
            ia = new Interface_Ajuda();
        }

        private void initInterface()
        {
            labelTitulo.Text = treeViewAjuda.Nodes[0].Text;

            richTextBox1.Text = preRequisitos(treeViewAjuda.Nodes[0].Text);

            richTextBox2.Text = descricao(treeViewAjuda.Nodes[0].Text);

            this.Visible = true;
        }

        private void treeViewAjuda_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (panel2.Visible == false)
            {

                panel2.Visible = true;
                splitContainer2.Visible = true;
            }
            labelTitulo.Text = e.Node.Text;

            richTextBox1.Text = preRequisitos(e.Node.Text);

            richTextBox2.Text = descricao(e.Node.Text);
        }

        private String preRequisitos(string codAssunto)
        {
            string prereq = "";
            switch (codAssunto)
            {
                case "Glossário":
                    prereq = "Esta secção não contém qualquer pré-requisito;";
                    break;
                case "Requisitos Minimos":
                    prereq = "Esta secção não contém qualquer pré-requisito;";
                    break;
                case "Servidor de Base de Dados":
                    prereq = "Esta secção não contém qualquer pré-requisito;";
                    break;
                case "Registo e Inicio de Sessão":
                    prereq = "Servidor de Base de Dados devidamente configurado;\n";
                    break;
                case "Criar Projecto":
                    prereq = "Servidor de Base de Dados devidamente configurado;\n"+
                             "Registo e Inissio de Sessão efectuados;";
                    break;
                case "Abrir Projecto":
                    prereq = "Servidor de Base de Dados devidamente configurado;\n" +
                             "Registo e Inissio de Sessão efectuados;\n"+
                             "Ter pelo menos um projecto criado;";
                    break;
                case "Apagar Projecto":
                    prereq = "Servidor de Base de Dados devidamente configurado;\n" +
                             "Registo e Inissio de Sessão efectuados;\n" +
                             "Ter pelo menos um projecto criado;";
                    break;
                case "Criar Análise":
                    prereq = "Servidor de Base de Dados devidamente configurado;\n" +
                             "Registo e Inissio de Sessão efectuados;\n" +
                             "Ter pelo menos um projecto criado;";
                    break;
                case "Abrir Análise":
                    prereq = "Servidor de Base de Dados devidamente configurado;\n" +
                             "Registo e Inissio de Sessão efectuados;\n" +
                             "Ter pelo menos um projecto criado;" +
                             "Ter pelo menos uma analise criada;";
                    break;
                case "Apagar Análise":
                    prereq = "Servidor de Base de Dados devidamente configurado;\n" +
                             "Registo e Inissio de Sessão efectuados;\n" +
                             "Ter pelo menos um projecto criado;\n" +
                             "Ter pelo menos uma analise criada;\n";
                    break;
                case "Consultar":
                    prereq = "Servidor de Base de Dados devidamente configurado;\n" +
                             "Registo e Inissio de Sessão efectuados;\n" +
                             "Ter pelo menos um projecto criado;\n" +
                             "Ter pelo menos uma analise criada;";
                    break;
                case "Gerar Formulários Online":
                    prereq = "Servidor de Base de Dados devidamente configurado;\n" +
                             "Registo e Inissio de Sessão efectuados;\n" +
                             "Ter pelo menos um projecto criado;\n" +
                             "Ter pelo menos uma analise criada;";
                    break;
                case "Act. / Desact. Formulários Online":
                    prereq = "Servidor de Base de Dados devidamente configurado;\n" +
                             "Registo e Inissio de Sessão efectuados;\n" +
                             "Ter pelo menos um projecto criado;\n" +
                             "Ter pelo menos uma analise criada;\n" +
                             "Formulários Online gerados;";
                    break;
                case "Inserir Dados Manualmente":
                    prereq = "Servidor de Base de Dados devidamente configurado;\n" +
                             "Registo e Inissio de Sessão efectuados;\n" +
                             "Ter pelo menos um projecto criado;\n" +
                             "Ter pelo menos uma analise criada;\n" +
                             "Formulários Online gerados;";
                    break;
                case "Importar Dados de Ficheiro":
                    prereq = "Esta secção não contém qualquer pré-requisito";
                    break;
                case "Gerar Relatório":
                    prereq = "Servidor de Base de Dados devidamente configurado;\n" +
                             "Registo e Inissio de Sessão efectuados;\n" +
                             "Ter pelo menos um projecto criado;\n" +
                             "Ter pelo menos uma analise criada;\n" +
                             "Formulários Online gerados;";
                    break;
                default:
                    prereq = "Esta secção não contém qualquer pré-requisito;";
                    break;
            }
            return prereq;
        }

        private String descricao(string codAssunto)
        {
            string desc = "";
            switch (codAssunto)
            {
                case "Glossário":
                    desc = "ETdA\n" + "\t Ergonomic Tri-dimensional Analisys (Análise Ergonómica Tridimensional).\n\n" +
                            "Tri-dimensional \n" + "\t Referente a três dimensões (cliente, profissional e analista).\n\n" +
                            "Analista \n" + "\t Especialista em análises ergonómicas.\n\n" +
                            "Área Comum \n" + "\t Espaço total a ser analisado.\n\n" +
                            "Zona \n" + "\t Zona específica de uma área de trabalho.\n\n" +
                            "Actividade \n" + "\t Actividade desenvolvida por cada funcionário/profissional.";
                    break;
                case "Requisitos Minimos":
                    desc = "-> Acesso a servidor Microsoft IIS\n\n" +
                            "-> Acesso a Servidor Microsoft SQLserver com devidas permissões (ver secção Ligação à Base de Dados)\n\n" +
                            "-> Microsoft Windows XP ou superior\n\n" +
                            "-> Microsoft Office Word 2003 ou superior\n\n";
                    break;
                case "Servidor de Base de Dados":
                    desc = "A alteração das definições de ligação pode ser feita através de dois sitios:\n"+
	                        "->Menu de Inicio de sessão\n"+
	                        "->Se já tiver iniciado sessão, carregando em Opções > Avançado > Base de Dados > Editar Ligação\n"+
                            "    \n"+
	                        "De seguida carregue em \"Alterar\", para puder alterar cada um dos campos. Depois da devida alteração carregue em"+
	                        "\"Guardar\", para que os dados sejam actualizados.\n\n"+

	                        "->Servidor de Base de Dados SQL\n"+
	                        "O ETdAnalyser comunica exclusivamente com servidores de base de dados SQLserver, pelo que caso não esteja "+
	                        "configurada uma ligação este não poderá ser executado correctamente.\n\n"+
		                        "\t->Nome do Servidor / Endereço da máquina\n"+
                                    "\t\tCaso o servidor se encontro na mesma rede local poderá indicar o nome na máquina. (Ex: sqlserver-pc)\n" +
                                    "\t\tSe não se encontrar na mesma rede local poderá indicar o nome do dominio da máquina ou o endereço IP " +
			                        "que aponta para esta.(ex: sqlserver.exemplo.org OU 136.125.21.124)\n"+
                                "\t->Nome da Base de Dados\n" +
                                    "\t\tNome da Base de Dados pré-definida para o acesso do Super Utilizador. (ex: model)\n\n" +

	                        "->Administrador / Super Utilizador\n"+
	                        "É necessário a existência de uma conta no servidor SQLserver para gerir as restantes contas dos analistas.\n"+
                                "\t->Permissões\n" +
                                "\tAs seguintes permissões são necessárias\n" +
                                    "\t\t->Criar Bases de Dados\n" +
                                    "\t\t->Criar Utilizadores";
                    break;
                case "Registo e Inicio de Sessão":
                    desc = "->Registo\n"+
		                        "\tPara que possa utilizar o programa deverá-se registar primeiro.\n"+
		                        "\tPara criar um novo registo:\n"+
		                        "\t\t->Se ainda não utilizou utiliza o programa, execute-o e carrege no botão Registar\n"+
		                        "\t\t->Se já tiver iniciado a sessão e pretende criar outro registo carregue em Ficheiro > Terminar Sessão "+
		                            "e de seguida carregue no botão Registar.\n"+
	                        "->Inicio de Sessão\n"+
		                        "\tApós efectuar registo, basta introduzir o nome de utilizador e a palavra-chave e clicar em Entrar\n";
                    break;
                case "Criar Projecto":
                    desc = "Para criar um novo projecto pode:\n" +
                            "\t->Ficheiro > \"Novo Projecto\" (Ctrl + P)\n" +
                            "\t->Barra de Atalhos seleccionando o icon de Novo projecto\n" +
                            "\t->Através do separador Página Inicial carregando em \"Novo Projecto\"\n";
                    break;
                case "Abrir Projecto":
                    desc = "Do lado direito encontra-se um painel que conterá todos os projectos criados, de modo a facilitar o acesso a eles.\n"+
		                   "Para abrir um projecto basta fazer duplo click em cima do nome correspondente, e um novo separador será aberto.";
                    break;
                case "Apagar Projecto":
                    desc = "Ao apagar um projecto todos os dados associados a este (analises, formulários, etc) serão também apagados,\n"+
		                    "Para apagar um projecto:\n"+
		                    "\t->Editar > Apagar Projecto > \"Nome do projecto\"";
                    break;
                case "Criar Análise":
                    desc = "Para criar uma nova analise pode:\n"+
                                "\t->Ficheiro > Nova Analise";
                    break;
                case "Abrir Análise":
                    desc = "Do lado direito encontra-se um painel que conterá todos os projectos criados, assim como as analises pertencentes a cada um destes,\n"+
		                    "de modo a facilitar o acesso a elas.\n"+
		                    "Para abrir uma analise basta fazer duplo click em cima do nome correspondente, e um novo separador será aberto.\n"+
		                    "Para visualizar as analises pertencentes a um projecto carregue no "+" do lado esquerdo do nome do projecto no painel lateral esquerdo.";
                    break;
                case "Apagar Análise":
                    desc = "Ao apagar uma analise todos os dados associados a este (formulários, respostas, etc) serão também apagados,\n"+
		                    "Para apagar uma analise:\n"+
		                    "\t->Editar > Apagar Analise > \"Nome do projecto\" > \"Nome da analise\"";
                    break;
                case "Consultar":
                    desc = "";
                    break;
                case "Gerar Formulários Online":
                    desc = "";
                    break;
                case "Act. / Desact. Formulários Online":
                    desc = "Para que os formulários online não estejam sempre disponíveis para preenchimento, é possível"+
			                "activá-los ou desactivá-los.\n\n"+
			                "Para tal abra o separador da analise pretendida, e na secção \"Formulário\" estão três botões correspondentes"+
			                "a cada um dos tipos de formulários. Carregue neles para activar e/ou desactivar.";
                    break;
                case "Inserir Dados Manualmente":
                    desc = "Para gerar o relatório é necessário abrir um separador da análise pretendida, e clicar em \"Inserir Dados Manualmente\".\n\n"+
			                "De seguida vai aparecer uma janela com três opções ( Questionário, Ficha de Avaliação e CheckList).\n\n"+
			                "Selecionando um destes irá aparecer uma janela com aspecto semelhante aos diferentes tipos de formulários online, "+
			                "onde poderá introduzir os formulários.";
                    break;
                case "Importar Dados de Ficheiro":
                    desc = "";
                    break;
                case "Gerar Relatório":
                    desc = "Para gerar o relatório é necessário abrir um separador da análise pretendida, e clicar em \"Gerar Relatório\".\n"+
                        "\n"+
			                "Do lado direito encontra-se um painel que conterá todos as Zonas/Actividades, assim como os itens pertencentes a cada um destes,\n"+
			                "de modo a facilitar o acesso a eles.\n"+
                            "\n"+
			                "Para visualizar os itens pertencentes a uma zona/actividade carregue no "+" do lado esquerdo do nome da zona/actividade no painel lateral esquerdo.\n"+
                            "\n"+
			                "Fazendo duplo click em cima de um item é possível:\n"+
			                "->Ver o resultado geral de cada item tendo em conta as ponderações\n"+
			                "de cada uma das dimensões\n"+
			                "->Ver o resultado detalhado, onde é indicado o resultado parcial de cada dimensão assim como a sua ponderação\n"+
			                "->Incluir ou não o resultado detalhado no relatório a gerar\n"+
			                "->Documentar observações relativas ao item e ao resultado obtido, que serão incluidas no relatório\n"+
                            "\n"+
			                "Dependendo da quantidade de zonas/actividades e itens e das especificações do computador o tempo de espera poderá variar.\n"+
                            "\n"+
			                "INFO:O relatório será gerado usando a tecnologia Microsoft Office Word.";
                    break;
                default:
                    desc = "Esta secção não contém qualquer descrição";
                    break;
            }
            return desc;
        }
    }
}
