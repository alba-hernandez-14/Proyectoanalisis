using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Linq;
using System.Web;
using System.Web.Services;
using pruebas.Models;
using System.Configuration;

namespace Proyectoanalisis_
{
    /// <summary>
    /// Descripción breve de CXC_Documento
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class CXC_Documento : System.Web.Services.WebService
    {
        String servidor;
        String usuario;
        String password;
        String cadenaconexion;

        public CXC_Documento()
        {
            servidor = "localhost:1521 / orcl";
            usuario = "PROYECTOANALISIS";
            password = "1234";
            cadenaconexion = "Data Source=" + servidor + ";User Id=" + usuario + "; Password=" + password + "; ";
        }
        public string conexionString = ConfigurationManager.ConnectionStrings["BaseDatos"].ConnectionString;


        [WebMethod]
        public DataSet ClientesMostrar()
        {
            try
            {
                OracleConnection conexion = new OracleConnection(cadenaconexion);//abrir la conexion 
                conexion.Open();     // se inicia la conexion 
                OracleDataAdapter adapter = new OracleDataAdapter("select * from fas_listar_clientes()", conexion);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "fas_listar_clientes()");
                return ds;
            }
            catch (Exception ex)
            {
                // Manejar excepciones aquí
                throw new Exception("Error al intentar obtener datos: " + ex.Message);
            }

        }

        [WebMethod]
        public DataSet ClienteBuscar(int cli_cliente)
        {
            try
            {
                OracleConnection conexion = new OracleConnection(cadenaconexion);//abrir la conexion 
                conexion.Open();     // se inicia la conexion 
                OracleDataAdapter adapter = new OracleDataAdapter("select * from fas_buscar_id_cliente(" + cli_cliente + ")", conexion);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "fas_buscar_id_cliente()");
                return ds;
            }
            catch (Exception ex)
            {
                // Manejar excepciones aquí
                throw new Exception("Error al intentar obtener datos: " + ex.Message);
            }

        }

        [WebMethod] //clientes 
        public String Clientesguardar(String CLI_RAZON_SOCIAL, String CLI_DIRECCION, String CLI_TELEFONO, String CLI_CORREO_ELECTRONICO, String CLI_TIPO_CLIENTE, String CLI_NIT)
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
                        comando.Parameters.Add(new OracleParameter("p_razon_social", CLI_RAZON_SOCIAL));
                        comando.Parameters.Add(new OracleParameter("p_direccion", CLI_DIRECCION));
                        comando.Parameters.Add(new OracleParameter("p_telefono", CLI_TELEFONO));
                        comando.Parameters.Add(new OracleParameter("p_email", CLI_CORREO_ELECTRONICO));
                        comando.Parameters.Add(new OracleParameter("p_tipo_cliente", CLI_TIPO_CLIENTE));
                        comando.Parameters.Add(new OracleParameter("p_nit", CLI_NIT));
                        OracleDataReader read = comando.ExecuteReader();

                        return "guardado";
                    }
                }
                catch (Exception error)
                {
                    throw new Exception(error.Message);
                    throw error;
                }
            }

        }


        [WebMethod]//clientes

        public String Clientesactualizar(int cli_cliente, String cli_razon_social, String cli_direccion, String cli_telefono, String cli_correo_electronico, String cli_tipo_cliente, String cli_nit)
        {
            using (OracleConnection conexion = new OracleConnection())
            {
                try
                {
                    conexion.ConnectionString = this.cadenaconexion;
                    conexion.Open();
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.CommandText = "pas_actualizar_cliente";
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Connection = conexion;
                        comando.Parameters.Add(new OracleParameter("p_cliente", cli_cliente));
                        comando.Parameters.Add(new OracleParameter("p_razon_social", cli_razon_social));
                        comando.Parameters.Add(new OracleParameter("p_direccion", cli_direccion));
                        comando.Parameters.Add(new OracleParameter("p_telefono", cli_telefono));
                        comando.Parameters.Add(new OracleParameter("p_email", cli_correo_electronico));
                        comando.Parameters.Add(new OracleParameter("p_tipo_cliente", cli_tipo_cliente));
                        comando.Parameters.Add(new OracleParameter("p_nit", cli_nit));
                        OracleDataReader read = comando.ExecuteReader();

                        return "datos de usuario actualizados";
                    }
                }
                catch (Exception error)
                {
                    return "error";
                    throw error;
                }
            }

        }
        [WebMethod]//clientes

        public String ClientesEliminar(int cli_cliente)
        {

            using (OracleConnection conexion = new OracleConnection())
            {
                try
                {
                    conexion.ConnectionString = conexionString;
                    conexion.Open();
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.CommandText = "pas_eliminar_cliente";
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Connection = conexion;
                        comando.Parameters.Add(new OracleParameter("p_cliente", cli_cliente));

                        OracleDataReader read = comando.ExecuteReader();
                        return "datos de usuario eliminados";
                    }
                }
                catch (Exception error)
                {
                    return "error al eliminar cliente";
                    throw error;
                }
            }

        }






    }
}
