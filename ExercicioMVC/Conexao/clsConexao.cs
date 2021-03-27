using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ExercicioMVC.Conexao
{
    public class clsConexao : IDisposable
    {
        public String strConexao { get; set; }
        public SqlConnection dbConexao = new SqlConnection();

        /// <summary>
        /// Construtor de classe.
        /// </summary>
        /// <param name="strConexao"></param>
        public clsConexao(String strConexao)
        {
            
            this.strConexao = strConexao;
            dbConexao = AbrirConexao();
        }

        /// <summary>
        /// Abre uma conexão sql.
        /// </summary>
        /// <returns></returns>
        public SqlConnection AbrirConexao()
        {
            SqlConnection dbConexao = null;
            try
            {
                dbConexao = new SqlConnection(strConexao);
                dbConexao.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dbConexao;
        }

        /// <summary>
        /// Executa um comando sql.
        /// </summary>
        /// <param name="strSql">Commando sql a ser executado.</param>
        /// <param name="sqlTrans">Transação sql, caso não seja utilizado enviar nulo.</param>
        /// <param name="arParametros">Parametros do comando sql. Caso não seja utilizado enviar nulo.</param>
        /// <param name="arValores">Valores dos parametros. Caso não seja utilizado enviar nulo.</param>
        /// <returns></returns>
        public DataTable ExecutarSql(String strSql, SqlTransaction sqlTrans = null, String[] arParametros = null, Object[] arValores = null)
        {
            DataTable dtRetorno = new DataTable();

            try
            {

                //if (sqlTrans != null)
                //{
                //    sqlConexao.BeginTransaction(sqlTrans);
                //}

                strSql += strSql.ToUpper().Contains("INSERT INTO") ? "; SELECT @@IDENTITY;" : "";

                SqlCommand sqlCommand = new SqlCommand(strSql, dbConexao, sqlTrans);

                if (arParametros != null)
                {
                    for (int i = 0; i < arParametros.Count(); i++)
                    {
                        String strTipo = Convert.ToString(arValores[i].GetType());

                        switch (strTipo)
                        {
                            case "System.Byte[]":
                                sqlCommand.Parameters.AddWithValue(arParametros[i], SqlDbType.Binary).Value = arValores[i];
                                break;
                            default:
                                break;
                        }
                    }
                }

                SqlDataAdapter sqaConexao = new SqlDataAdapter();
                sqaConexao.SelectCommand = sqlCommand;


                sqaConexao.Fill(dtRetorno);

            }
            catch (Exception ex)
            {
                if (dbConexao != null)
                {
                    dbConexao.Close();
                }

                throw ex;
            }

            if (sqlTrans == null)
            {
                dbConexao.Close();
            }


            return dtRetorno;
        }

        public void Dispose()
        {
            dbConexao.Close();
        }
    }
}