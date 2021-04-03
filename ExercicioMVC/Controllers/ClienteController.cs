using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExercicioMVC.Controllers
{
    public class ClienteController : Controller
    {
        //Método para realizar o Insert
        public int inserirCliente(string strNomeCompleto, string strConexaoBanco)
		{
            try
            {
                //Instâncio o Model clsCliente, enviando a string de conexão que veio como parâmetro de entrada

                Models.clsCliente cls = new Models.clsCliente(strConexaoBanco);

                //Evoco o método de Inserção criado no Model
                //E como o método Inserir é INT, já retorno ao mesmo tempo
                return cls.inserir(strNomeCompleto);
            }
            catch(Exception ex)
			{
                throw ex;
			}

		}

        //Método para realizar o Update
        public void AlterarCliente(int intIdCliente, string strNomeCompleto, string strConexaoBanco)
		{
			try
			{
                Models.clsCliente cls = new Models.clsCliente(strConexaoBanco);

                cls.Alterar(intIdCliente, strNomeCompleto);

                //Por ser void, pode retornar vázio, irei fazer esse return pra manter o padrão
                return;
			}
            catch(Exception ex)
			{
                throw ex;
			}

		}

        //Método para Deletar um cliente
        public void DeletarCliente(int intIdCliente, string strConexaoBanco)
		{
			try
			{
                Models.clsCliente cls = new Models.clsCliente(strConexaoBanco);

                cls.Deletar(intIdCliente);

                return;
			}
            catch(Exception ex)
			{
                throw ex;
			}
		}

        //Método para trazer os dados com filtros
        public DataTable ObterPorFiltroCliente(int? intIdCliente, string strNomeCompleto, string strConexaoBanco)
		{
			try
			{
                Models.clsCliente cls = new Models.clsCliente(strConexaoBanco);

                return cls.ObterPorFiltro(intIdCliente, strNomeCompleto);
			}
            catch(Exception ex)
			{
                throw ex;
			}
		}

        //Agora vou criar o ActionResult para as Views

        public ActionResult listaCliente()
		{
            //Instâncio a classe de Clientes, enviando a string de Conexão
            //Que está salva na memória
            Models.clsCliente modelo = new Models.clsCliente(Convert.ToString(Session["strConexaoBanco"]));

            //Preencho a DataTable dtCliente, que pertence ao Model com o método obterTodos
            modelo.dtCliente = obterTodosclsCliente(Convert.ToString(Session["strConexaoBanco"]));

            //Retorna para a View o modelo que foi criado com a DataTable

            return View(modelo);
        }

        public ActionResult InserirCliente()
		{
            Models.clsCliente modelo = new Models.clsCliente(Convert.ToString(Session["strConexaoBanco"]));

            modelo.strCliente = modelo;

            //Retorna a criação da View
            //Como vou utilizar a mesma view para Inserir e Alterar, vou especificar o nome
            return View("inserirAlterarCliente", modelo);
        }

        public ActionResult AlterarCliente(int intIdCliente)
		{
            Models.clsCliente modelo = new Models.clsCliente(Convert.ToString(Session["strConexaoBanco"]));

            modelo.strCliente = modelo.obterPorId(intIdCliente);

            //Vou uitliar a mesma View do InserirCliente, mas com um dado carregado agora
            return View("inserirAlterarCliente", modelo);

        }

        //Agora vou criar um JsonResult com os dados para Inserir ou alterar
        [HttpPost]
        public JsonResult InserirAlterarCliente(int? intIdCliente, string strNomeCompleto)
		{
			try
			{
                //Caso o ID seja diferente de 0 ou nulo, ele é um valor que vai ser alterado
                if(intIdCliente != 0 && intIdCliente != null)
				{
                    AlterarCliente(Convert.ToInt32(intIdCliente), strNomeCompleto, Convert.ToString(Session["strConexaoBanco"]));
                }
				//Se o ID for uma das condições acima, é um novo dado
				else
				{
                    inserirCliente(strNomeCompleto, Convert.ToString(Session["strConexaoBanco"]));
				}

                //Retorna uma variável com uma string interna para a View,
                //Para validar o sucesso ou a falha, onde trataramos no catch
                var Retorno = new
                {
                    resultado = "Sucesso"
                };

                return Json(Retorno, JsonRequestBehavior.AllowGet);
			}
            catch(Exception ex)
			{
                //Aqui trataramos o Retorno caso dê erro
                var Retorno = new
                {
                    resultado = ex
                };

                return Json(Retorno, JsonRequestBehavior.AllowGet);
			}
		}

        //Fazendo o método para deletar o Cliente
        [HttpPost]
        public JsonResult ExcluirCliente(int intIdCliente)
        {
            try
            {

                DeletarCliente(intIdCliente, Convert.ToString(Session["strConexaoBanco"]));


                //Retorna uma variável com uma string interna para a View,
                //Para validar o sucesso ou a falha, onde trataramos no catch
                var Retorno = new
                {
                    resultado = "Sucesso"
                };

                return Json(Retorno, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //Aqui trataramos o Retorno caso dê erro
                var Retorno = new
                {
                    resultado = ex
                };

                return Json(Retorno, JsonRequestBehavior.AllowGet);
            }
        }

    }
}