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
    /// Descripción breve de CXC_DetalleVenta
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class CXC_DetalleVenta : System.Web.Services.WebService
    {

        String servidor;
        String usuario;
        String password;
        String cadenaconexion;

        public CXC_DetalleVenta()
        {
            servidor = "localhost:1521 / orcl";
            usuario = "PROYECTOANALISIS";
            password = "1234";
            cadenaconexion = "Data Source=" + servidor + ";User Id=" + usuario + "; Password=" + password + "; ";
        }
        public string conexionString = ConfigurationManager.ConnectionStrings["BaseDatos"].ConnectionString;

        [WebMethod]//pendiente
        public DataSet DetalleVentaMostrar(int id_DetalleV)
        {
            try
            {
                OracleConnection conexion = new OracleConnection(cadenaconexion);//abrir la conexion 
                conexion.Open();     // se inicia la conexion 
                OracleDataAdapter adapter = new OracleDataAdapter("select * from fas_listar_detalles_venta(" +id_DetalleV + ")", conexion);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "fas_listar_documentos()");
                return ds;
            }
            catch (Exception ex)
            {
                // Manejar excepciones aquí
                throw new Exception("Error al intentar obtener datos: " + ex.Message);
            }

        }

        [WebMethod]
        public String DetalleVentaGuardar(int DEV_PRODUCTO, int DEV_VENTA, float DEV_CANTIDAD, float DEV_DESCUENTO)
        {
            using (OracleConnection conexion = new OracleConnection())
            {
                try
                {
                    conexion.ConnectionString = conexionString;
                    conexion.Open();
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.CommandText = "pas_crear_detalle_venta";
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Connection = conexion;
                        comando.Parameters.Add(new OracleParameter("p_producto", DEV_PRODUCTO));
                        comando.Parameters.Add(new OracleParameter("p_venta", DEV_VENTA));
                        comando.Parameters.Add(new OracleParameter("p_cantidad", DEV_CANTIDAD));
                        comando.Parameters.Add(new OracleParameter("p_descuento", DEV_DESCUENTO));

                        //comando.Parameters.Add(new OracleParameter("p_nit", pDoc_valor_total));
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
        public DataSet DetalleVentaBuscar(int p_id)
        {
            try
            {
                OracleConnection conexion = new OracleConnection(cadenaconexion);//abrir la conexion 
                conexion.Open();     // se inicia la conexion 
                OracleDataAdapter adapter = new OracleDataAdapter("select * from fas_buscar_detalle_venta(" + p_id + ")", conexion);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "fas_buscar_detalle_venta()");
                return ds;
            }
            catch (Exception ex)
            {
                // Manejar excepciones aquí
                throw new Exception("Error al intentar obtener datos: " + ex.Message);
            }

        }

        [WebMethod]
        public String DetalleVentactualizar(int DetalleVenta, int DEV_PRODUCTO, int DEV_VENTA, float DEV_CANTIDAD, float DEV_DESCUENTO)
        {
            using (OracleConnection conexion = new OracleConnection())
            {
                try
                {
                    conexion.ConnectionString = this.cadenaconexion;
                    conexion.Open();
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.CommandText = "pas_actualizar_detalle_venta";
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Connection = conexion;
                        comando.Parameters.Add(new OracleParameter("p_detalle_venta", DetalleVenta));
                        comando.Parameters.Add(new OracleParameter("p_producto", DEV_PRODUCTO));
                        comando.Parameters.Add(new OracleParameter("p_venta", DEV_VENTA));
                        comando.Parameters.Add(new OracleParameter("p_cantidad", DEV_CANTIDAD));
                        comando.Parameters.Add(new OracleParameter("p_descuento", DEV_DESCUENTO));
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
        public String DetalleVentaEliminar(int p_detalle_venta)
        {

            using (OracleConnection conexion = new OracleConnection())
            {
                try
                {
                    conexion.ConnectionString = conexionString;
                    conexion.Open();
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.CommandText = "pas_eliminar_detalle_venta";
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Connection = conexion;
                        comando.Parameters.Add(new OracleParameter("p_detalle_venta", p_detalle_venta));

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
