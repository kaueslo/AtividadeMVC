using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ExercicioMVC.Models
{
	public class clsCliente
	{
		//Chamando a classe de conexão
		Conexao.clsConexao clsConexao;

		//Crio váriaveis para o método obter por ID
		public int? idCliente;
		public string nomeCompleto;

		//Criando a váriavel colunas, assim não se perde os dados quando for escrever no banco
		public string colunas = "idCliente, NomeCompleto";

		//Crio uma variável vazia DataTable onde usarei mais tarde no controller
		public DataTable dtCliente = new DataTable();

		//Crio uma variável vazia do tipo clsCliente para utilizar no controller
		public clsCliente strCliente;

		//Construtor da classe
		public clsCliente(string strConexaoBanco)
		{
			//Fazendo a conexão com o banco
			this.clsConexao = new Conexao.clsConexao(strConexaoBanco);
		}

		//Fazendo o modelo para inserir no banco
		public int inserir(string nomeCompleto)
		{
			try
			{
				string strSql = "INSERT INTO Cliente(NomeCompleto)";
					   strSql += " VALUES ('" + nomeCompleto + "')";

				//O método executaSql é um DataTable que vai retornar o ID do registro
				//O valor é guardado em outra DataTable(dt)
				DataTable dt = clsConexao.ExecutarSql(strSql);

				//Como o valor é guardado na dt, vamos extrair esse valor na linha e coluna 0
				//E retornar ele
				int intRetorno = Convert.ToInt32(dt.Rows[0][0]);

				//Retornamos o ID
				return intRetorno;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		//Fazer o modelo para atualizar o dado no banco
		//Fiz void pois o método alterar não precisa necessariamente retornar um valor, pois o dado já está cadastrado
		public void Alterar(int idCliente,string nomeCompleto)
		{
			try
			{
				//Comando SQL
				string strSql = "UPDATE Cliente SET NomeCompleto = '" + nomeCompleto + "'";
					   strSql += " WHERE idCliente = " + idCliente;

				DataTable dt = clsConexao.ExecutarSql(strSql);

				return;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		//Fazer o modelo para deletar o dado no banco
		public DataTable Deletar(int idCliente)
		{
			try
			{
				string strSql = "DELETE FROM Cliente WHERE idCliente = " + idCliente;

				return clsConexao.ExecutarSql(strSql);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		public DataTable ObterTodos()
		{
			try
			{
				string strSql = "SELECT " + colunas + "FROM Cliente";

				return clsConexao.ExecutarSql(strSql);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		public DataTable ObterPorFiltro(int? idCliente, string nomeCompleto)
		{
			try
			{
				string strSql = "SELECT " + colunas + " FROM Cliente WHERE 1=1";

				if(idCliente > 0)
				{
					strSql += " AND idCliente = " + idCliente;
				}

				if(nomeCompleto != null)
				{
					strSql += " AND nomeCompleto = '" + nomeCompleto + "'";
				}

				return clsConexao.ExecutarSql(strSql);

			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		public clsCliente obterPorId(int idCliente)
		{
			try
			{
				//Vou criar uma variável do tipo clsCliente
				clsCliente cls = new clsCliente(this.clsConexao.strConexao);

				//Instâncio as váriavies idCliente
				cls.idCliente = idCliente;

				//Obtém a consulta dos dados com o filtro idCliente preenchido para ver se tem resultado
				DataTable dt = cls.ObterPorFiltro(idCliente, null);

				//Vou fazer uma condição para verificar se encontrou dados
				if(dt.Rows.Count > 0)
				{
					//Preenche os outros dados(nomeCompleto) que vai vir da DataTable
					cls.nomeCompleto = Convert.ToString(dt.Rows[0]["NomeCompleto"]);

					//retorno com a váriavel do tipo clsCliente preenchida
					return cls;
				}
				else
				{
					//Caso não encontre nada, vai retornar null
					return null;
				}

				
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

	}
}