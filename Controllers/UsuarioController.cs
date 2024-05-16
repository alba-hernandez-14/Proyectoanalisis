using System;
using pruebas.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data;
using Oracle.ManagedDataAccess.Client;
namespace pruebas.Controllers
{
    public class UsuarioController : Controller
    {
        public string conexionString = ConfigurationManager.ConnectionStrings["BDORACLE"].ConnectionString;

        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

     
        public String  guardar(Usuarioen entidad)
        {
            
            using (OracleConnection conexion = new OracleConnection())
            {
                try
                {
                    conexion.ConnectionString = conexionString;
                    conexion.Open();
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.CommandText = "pas_crear_cliente";
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Connection = conexion;
                        comando.Parameters.Add(new OracleParameter("p_razon_social", entidad.CLI_RAZON_SOCIAL));
                        comando.Parameters.Add(new OracleParameter("p_direccion", entidad.CLI_DIRECCION));
                        comando.Parameters.Add(new OracleParameter("p_telefono", entidad.CLI_TELEFONO));
                        comando.Parameters.Add(new OracleParameter("p_email", entidad.CLI_CORREO_ELECTRONICO));
                        comando.Parameters.Add(new OracleParameter("p_tipo_cliente", entidad.CLI_TIPO_CLIENTE));
                        comando.Parameters.Add(new OracleParameter("p_nit", entidad.CLI_NIT));
                        OracleDataReader read = comando.ExecuteReader();

                        return "correcto";
                    }
                }
                catch (Exception error)
                {
                    
                        return "correcto";
                    throw error;
                }
            }


        }

        


    }
}