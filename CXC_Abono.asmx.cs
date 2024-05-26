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
    /// Descripción breve de CXC_Abono
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class CXC_Abono : System.Web.Services.WebService
    {

        String servidor;
        String usuario;
        String password;
        String cadenaconexion;

        public CXC_Abono()
        {

            servidor = "localhost:1521 / orcl";
            usuario = "cliente";
            password = "123";
            cadenaconexion = "Data Source=" + servidor + ";User Id=" + usuario + "; Password=" + password + "; ";
        }

        public string conexionString = ConfigurationManager.ConnectionStrings["BaseDatos"].ConnectionString;


        [WebMethod]
        public DataSet AbonoMostrar(int id_abono)
        {
            try
            {
                OracleConnection conexion = new OracleConnection(cadenaconexion);//abrir la conexion 
                conexion.Open();     // se inicia la conexion 
                OracleDataAdapter adapter = new OracleDataAdapter("select * from FAS_LISTAR_ABONOS_POR_VENTA("+id_abono+")", conexion);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "FAS_LISTAR_ABONOS_POR_VENTA()");
                return ds;
            }
            catch (Exception ex)
            {
                // Manejar excepciones aquí
                throw new Exception("Error al intentar obtener datos: " + ex.Message);
            }
        }

        [WebMethod]
        public DataSet AbonoBuscar(int id_abono)
        {
            try
            {
                OracleConnection conexion = new OracleConnection(cadenaconexion);//abrir la conexion 
                conexion.Open();     // se inicia la conexion 
                OracleDataAdapter adapter = new OracleDataAdapter("select * from FAS_BUSCAR_ABONO(" + id_abono + ")", conexion);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "FAS_BUSCAR_ABONO()");
                return ds;
            }
            catch (Exception ex)
            {
                // Manejar excepciones aquí
                throw new Exception("Error al intentar obtener datos: " + ex.Message);
            }

        }

        [WebMethod] 
        public String Abonoguardar(int P_NOVENTA_ID, String P_TIPO_PAGO, String P_NO_AUTORIZACION, float P_FLVALOR)
        {
            using (OracleConnection conexion = new OracleConnection())
            {
                try
                {
                    conexion.ConnectionString = conexionString;
                    conexion.Open();
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.CommandText = "PAS_CREAR_ABONO";
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Connection = conexion;
                        comando.Parameters.Add(new OracleParameter("P_VENTA_ID", P_NOVENTA_ID));
                        comando.Parameters.Add(new OracleParameter("P_TIPO_PAGO", P_TIPO_PAGO));
                        comando.Parameters.Add(new OracleParameter("P_NO_AUTORIZACION", P_NO_AUTORIZACION));
                        comando.Parameters.Add(new OracleParameter("P_VALOR", P_FLVALOR));
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


        [WebMethod]

        public String Abonoactualizar(int P_ABONO_ID, int P_NOVENTA_ID, String P_TIPO_PAGO, String P_NO_AUTORIZACION, float P_FLVALOR)
        {
            using (OracleConnection conexion = new OracleConnection())
            {
                try
                {
                    conexion.ConnectionString = this.cadenaconexion;
                    conexion.Open();
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.CommandText = "PAS_ACTUALIZAR_ABONO";
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Connection = conexion;
                        comando.Parameters.Add(new OracleParameter("P_ABONO_ID", P_ABONO_ID));
                        comando.Parameters.Add(new OracleParameter("P_VENTA_ID", P_NOVENTA_ID));
                        comando.Parameters.Add(new OracleParameter("P_TIPO_PAGO", P_TIPO_PAGO));
                        comando.Parameters.Add(new OracleParameter("P_NO_AUTORIZACION", P_NO_AUTORIZACION));
                        comando.Parameters.Add(new OracleParameter("P_VALOR", P_FLVALOR));
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
        [WebMethod]

        public String AbonoEliminar(int ID_ABONO)
        {

            using (OracleConnection conexion = new OracleConnection())
            {
                try
                {
                    conexion.ConnectionString = conexionString;
                    conexion.Open();
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.CommandText = "PAS_ELIMINAR_ABONO";
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Connection = conexion;
                        comando.Parameters.Add(new OracleParameter("P_ABONO_ID", ID_ABONO));

                        OracleDataReader read = comando.ExecuteReader();
                        return "datos eliminados";
                    }
                }
                catch (Exception error)
                {
                    return "error al eliminar";
                    throw error;
                }
            }

        }








    }
}
