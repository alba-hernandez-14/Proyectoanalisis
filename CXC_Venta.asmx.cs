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
    /// Descripción breve de CXC_Venta
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class CXC_Venta : System.Web.Services.WebService
    {

        String servidor;
        String usuario;
        String password;
        String cadenaconexion;

        public CXC_Venta()
        {

            servidor = "localhost:1521 / orcl";
            usuario = "PROYECTOANALISIS";
            password = "1234";
            cadenaconexion = "Data Source=" + servidor + ";User Id=" + usuario + "; Password=" + password + "; ";
        }

        public string conexionString = ConfigurationManager.ConnectionStrings["BaseDatos"].ConnectionString;


        [WebMethod]
        public DataSet VentaMostrar()
        {
            try
            {
                OracleConnection conexion = new OracleConnection(cadenaconexion);//abrir la conexion 
                conexion.Open();     // se inicia la conexion 
                OracleDataAdapter adapter = new OracleDataAdapter("select * from fas_listar_ventas()", conexion);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "fas_listar_ventas()");
                return ds;
            }
            catch (Exception ex)
            {
                // Manejar excepciones aquí
                throw new Exception("Error al intentar obtener datos: " + ex.Message);
            }

        }

        [WebMethod]
        public DataSet VentaBuscar(int Venta_ID)
        {
            try
            {
                OracleConnection conexion = new OracleConnection(cadenaconexion);//abrir la conexion 
                conexion.Open();     // se inicia la conexion 
                OracleDataAdapter adapter = new OracleDataAdapter("select * from fas_buscar_venta(" + Venta_ID + ")", conexion);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "fas_buscar_venta()");
                return ds;
            }
            catch (Exception ex)
            {
                // Manejar excepciones aquí
                throw new Exception("Error al intentar obtener datos: " + ex.Message);
            }

        }

        [WebMethod]
        public String Ventaguardar(String Ven_p_cliente, String Ven_p_empleado, String Ven_p_condicion_pago, String Ven_p_no_autorizacion)
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
                        comando.Parameters.Add(new OracleParameter("p_cliente", Ven_p_cliente));
                        comando.Parameters.Add(new OracleParameter("p_empleado", Ven_p_empleado));
                        comando.Parameters.Add(new OracleParameter("p_condicion_pago", Ven_p_condicion_pago));
                        comando.Parameters.Add(new OracleParameter("p_no_autorizacion", Ven_p_no_autorizacion));
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
        public String Ventasactualizar( int p_venta, String Ven_p_cliente, String Ven_p_empleado, String Ven_p_condicion_pago, String Ven_p_no_autorizacion)
        {
            using (OracleConnection conexion = new OracleConnection())
            {
                try
                {
                    conexion.ConnectionString = this.cadenaconexion;
                    conexion.Open();
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.CommandText = "pas_actualizar_venta";
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Connection = conexion;
                        comando.Parameters.Add(new OracleParameter("p_venta", p_venta));
                        comando.Parameters.Add(new OracleParameter("p_cliente", Ven_p_cliente));
                        comando.Parameters.Add(new OracleParameter("p_empleado", Ven_p_empleado));
                        comando.Parameters.Add(new OracleParameter("p_condicion_pago", Ven_p_condicion_pago));
                        comando.Parameters.Add(new OracleParameter("p_no_autorizacion", Ven_p_no_autorizacion));
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
        public String VentasEliminar(int Ven_p_venta)
        {

            using (OracleConnection conexion = new OracleConnection())
            {
                try
                {
                    conexion.ConnectionString = conexionString;
                    conexion.Open();
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.CommandText = "pas_eliminar_venta";
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Connection = conexion;
                        comando.Parameters.Add(new OracleParameter("p_venta", Ven_p_venta));

                        OracleDataReader read = comando.ExecuteReader();
                        return "datos de usuario eliminados";
                    }
                }
                catch (Exception error)
                {
                    return "error al eliminar";
                    throw error;
                }
            }

        }


        [WebMethod]
        public String VentaCerrar(int Cerrar_p_venta)
        {

            using (OracleConnection conexion = new OracleConnection())
            {
                try
                {
                    conexion.ConnectionString = conexionString;
                    conexion.Open();
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.CommandText = "pas_cerrar_venta";
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Connection = conexion;
                        comando.Parameters.Add(new OracleParameter("p_venta", Cerrar_p_venta));

                        OracleDataReader read = comando.ExecuteReader();
                        return "datos de usuario eliminados";
                    }
                }
                catch (Exception error)
                {
                    return "error al eliminar";
                    throw error;
                }
            }

        }


        [WebMethod]
        public String VentaSumar(int p_venta, string p_valor)
        {
            using (OracleConnection conexion = new OracleConnection())
            {
                try
                {
                    conexion.ConnectionString = conexionString;
                    conexion.Open();
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.CommandText = "pas_sumar_total_venta";
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Connection = conexion;
                        comando.Parameters.Add(new OracleParameter("p_venta", p_venta));
                        comando.Parameters.Add(new OracleParameter("VEN_TOTAL", p_valor));

                        OracleDataReader read = comando.ExecuteReader();
                        return "datos de usuario eliminados";
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
