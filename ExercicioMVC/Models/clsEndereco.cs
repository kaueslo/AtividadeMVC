using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ExercicioMVC.Models
{
	public class clsEndereco
	{
		Conexao.clsConexao clsConexao;

		public int idEndereco;
		public int idCliente;
		public string Endereco;
		public string Bairro;
		public string CEP;

		public string colunas = " idEndereco, Endereco, Bairro, CEP, idCliente ";

		public DataTable dtEndereco = new DataTable();

		public clsEndereco strEndereco;

		public clsEndereco(string strConexaoBanco)
		{
			this.clsConexao = new Conexao.clsConexao(strConexaoBanco);
		}

		public int inserir(string Endereco, string Bairro, string CEP, int idCliente)
		{
			try
			{
				string strSql = "INSERT INTO Endereco (Endereco, Bairro, CEP, idCliente) VALUES ";
				strSql += " ('" + Endereco + "','" + Bairro + "','" + CEP + "'," + idCliente + ")";

				DataTable dt = clsConexao.ExecutarSql(strSql);

				int intRetorno = Convert.ToInt32(dt.Rows[0][0]);

				return intRetorno;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		public void Alterar(int idEndereco, string Endereco, string Bairro, string CEP, int idCliente)
		{
			try
			{
				string strSql = "UPDATE Endereco SET Endereco = '" + Endereco + "',";
					   strSql += " Bairro = '" + Bairro + "', CEP = '" + CEP + "',";
					   strSql += " idCliente = " + idCliente + "WHERE idEndereco = " + idEndereco;

				DataTable dt = clsConexao.ExecutarSql(strSql);

				return;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		public DataTable Deletar(int idEndereco)
		{
			try
			{
				string strSql = "DELETE FROM Endereco WHERE idEndereco = " + idEndereco;

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
				string strSql = "SELECT " + colunas + " FROM Endereco";

				return clsConexao.ExecutarSql(strSql);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		public DataTable ObterPorFiltro(int? idEndereco, string strEndereco, string strBairro, string strCEP,
										int? idCliente)
		{
			try
			{
				string strSql = "SELECT " + colunas + " FROM Endereco WHERE 1=1";

				if(idEndereco > 0)
				{
					strSql += " AND idEndereco = " + idEndereco;
				}

				if(strEndereco != null)
				{
					strSql += " AND Endereco = '" + strEndereco + "'";
				}

				if(strBairro != null)
				{
					strSql += " AND Bairro = '" + strBairro + "'";
				}

				if(strCEP != null)
				{
					strSql += " AND CEP = '" + strCEP + "'";
				}

				if(idCliente > 0)
				{
					strSql += " AND idCliente = " + idCliente;
				}

				return clsConexao.ExecutarSql(strSql);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		public clsEndereco ObterPorId(int idEndereco)
		{
			try
			{
				clsEndereco cls = new clsEndereco(this.clsConexao.strConexao);

				cls.idEndereco = idEndereco;

				DataTable dt = cls.ObterPorFiltro(idEndereco, null, null, null, 0);

				if(dt.Rows.Count > 0)
				{
					cls.Endereco = Convert.ToString(dt.Rows[0]["Endereco"]);
					cls.Bairro = Convert.ToString(dt.Rows[0]["Bairro"]);
					cls.CEP = Convert.ToString(dt.Rows[0]["CEP"]);
					cls.idCliente = Convert.ToInt32(dt.Rows[0]["idCliente"]);

					return cls;
				}
				else
				{
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