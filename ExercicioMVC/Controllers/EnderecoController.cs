using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExercicioMVC.Controllers
{
    public class EnderecoController : Controller
    {
        public int inserirEndereco(string strEndereco, string strBairro, string strCEP, int idCliente, 
                                    string strConexaoBanco)
		{
			try
			{
                Models.clsEndereco cls = new Models.clsEndereco(strConexaoBanco);

                return cls.inserir(strEndereco, strBairro, strCEP, idCliente);
            }
            catch(Exception ex)
			{
                throw ex;
			}

		}

        public void AlterarEndereco(int idEndereco,  string strEndereco, string strBairro, string strCEP, int idCliente,
									string strConexaoBanco)
		{
			try
			{
				Models.clsEndereco cls = new Models.clsEndereco(strConexaoBanco);

				cls.Alterar(idEndereco, strEndereco, strBairro, strCEP, idCliente);

				return;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		public void DeletarEndereco(int idEndereco, string strConexaoBanco)
		{
			try
			{
				Models.clsEndereco cls = new Models.clsEndereco(strConexaoBanco);

				cls.Deletar(idEndereco);

				return;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		public DataTable obterTodosEndereco(string strConexaoBanco)
		{
			try
			{
				Models.clsEndereco cls = new Models.clsEndereco(strConexaoBanco);

				return cls.ObterTodos();
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		public DataTable ObterPorFiltroEndereco(int? idEndereco, string strEndereco, string strBairro, string strCEP,
											   int? idCliente, string strConexaoBanco)
		{
			try
			{
				Models.clsEndereco cls = new Models.clsEndereco(strConexaoBanco);

				return cls.ObterPorFiltro(idEndereco, strEndereco, strBairro, strCEP, idCliente);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		public ActionResult listaEndereco()
		{
			Models.clsEndereco modelo = new Models.clsEndereco(Convert.ToString(Session["strConexaoBanco"]));

			modelo.dtEndereco = obterTodosEndereco(Convert.ToString(Session["strConexaoBanco"]));

			return View(modelo);
		}

		public ActionResult InserirEndereco()
		{
			Models.clsEndereco modelo = new Models.clsEndereco(Convert.ToString(Session["strConexaoBanco"]));

			modelo.strEndereco = modelo;

			return View("inserirAlterarEndereco", modelo);
		}

		public ActionResult alterarEndereco()
		{
			Models.clsEndereco modelo = new Models.clsEndereco(Convert.ToString(Session["strConexaoBanco"]));

			modelo.strEndereco = modelo;

			return View("inserirAlterarEndereco", modelo);
		}

		[HttpPost]
		public JsonResult InserirAlterarEndereco(int intIdEndereco, string strEndereco, string strBairro, string strCEP,
												int intIdCliente)
		{
			try
			{
				if(intIdEndereco != 0 && intIdEndereco != null)
				{
					AlterarEndereco(Convert.ToInt32(intIdEndereco), strEndereco, strBairro, strCEP, 
						Convert.ToInt32(intIdCliente), Convert.ToString(Session["strConexaoBanco"]));
				}
				else
				{
					inserirEndereco(strEndereco, strBairro, strCEP, Convert.ToInt32(intIdCliente),
						Convert.ToString(Session["strConexaoBanco"]));
				}

				var Retorno = new
				{
					resultado = "Sucesso"
				};

				return Json(Retorno, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				var Retorno = new
				{
					resultado = ex
				};

				return Json(Retorno, JsonRequestBehavior.AllowGet);
			}
		}

		[HttpPost]
		public JsonResult ExcluirEndereco(int intIdEndereco)
		{
			try
			{
				DeletarEndereco(intIdEndereco, Convert.ToString(Session["strConexaoBanco"]));

				var Retorno = new
				{
					resultado = "Sucesso"
				};

				return Json(Retorno, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				var Retorno = new
				{
					resultado = ex
				};

				return Json(Retorno, JsonRequestBehavior.AllowGet);
			}
		}
    }
}