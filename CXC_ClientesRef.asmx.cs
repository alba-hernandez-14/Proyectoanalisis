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
    /// Descripción breve de CXC_ClientesRef
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class CXC_ClientesRef : System.Web.Services.WebService
    {
        String servidor;
        String usuario;
        String password;
        String cadenaconexion;

        public CXC_ClientesRef()
        {

            servidor = "localhost:1521 / orcl";
            usuario = "cliente";
            password = "123";
            cadenaconexion = "Data Source=" + servidor + ";User Id=" + usuario + "; Password=" + password + "; ";
        }

        public string conexionString = ConfigurationManager.ConnectionStrings["BaseDatos"].ConnectionString;


        [WebMethod]
        public DataSet ClientesREFMostrar()
        {
            try
            {
                OracleConnection conexion = new OracleConnection(cadenaconexion);//abrir la conexion 
                conexion.Open();     // se inicia la conexion 
                OracleDataAdapter adapter = new OracleDataAdapter("select * from FAS_LISTAR_REFERENCIAS_CLIENTE()", conexion);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "FAS_LISTAR_REFERENCIAS_CLIENTE()");
                return ds;
            }
            catch (Exception ex)
            {
                // Manejar excepciones aquí
                throw new Exception("Error al intentar obtener datos: " + ex.Message);
            }
        }

        [WebMethod]
        public DataSet ClientesREFBuscar(int id_buscar)
        {
            try
            {
                OracleConnection conexion = new OracleConnection(cadenaconexion);//abrir la conexion 
                conexion.Open();     // se inicia la conexion 
                OracleDataAdapter adapter = new OracleDataAdapter("select * from FAS_BUSCAR_REFERENCIA_CLIENTE_POR_ID(" + id_buscar + ")", conexion);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "FAS_BUSCAR_REFERENCIA_CLIENTE_POR_ID()");
                return ds;
            }
            catch (Exception ex)
            {
                // Manejar excepciones aquí
                throw new Exception("Error al intentar obtener datos: " + ex.Message);
            }

        }

        [WebMethod]
        public String ClientesREFguardar(int P_CLIENTE_ID, String CRF_NOMBRE_REF, String CRF_TELEFONO1, String CRF_TELEFONO2, String CRF_TELEFONO3, String CRF_CORREO_ELECTRONICO)
        {
            using (OracleConnection conexion = new OracleConnection())
            {
                try
                {
                    conexion.ConnectionString = conexionString;
                    conexion.Open();
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.CommandText = "PAS_CREAR_REFERENCIA_CLIENTE";
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Connection = conexion;
                        comando.Parameters.Add(new OracleParameter("P_CLIENTE_ID", P_CLIENTE_ID));
                        comando.Parameters.Add(new OracleParameter("P_NOMBRE_REF", CRF_NOMBRE_REF));
                        comando.Parameters.Add(new OracleParameter("P_TELEFONO1", CRF_TELEFONO1));
                        comando.Parameters.Add(new OracleParameter("P_TELEFONO2", CRF_TELEFONO2));
                        comando.Parameters.Add(new OracleParameter("P_TELEFONO3", CRF_TELEFONO3));
                        comando.Parameters.Add(new OracleParameter("P_CORREO", CRF_CORREO_ELECTRONICO));
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
        public String ClientesREFactualizar(int P_REFERENCIA_ID, int P_CLIENTE_ID, String CRF_NOMBRE_REF, String CRF_TELEFONO1, String CRF_TELEFONO2, String CRF_TELEFONO3, String CRF_CORREO_ELECTRONICO)
        {
            using (OracleConnection conexion = new OracleConnection())
            {
                try
                {
                    conexion.ConnectionString = this.cadenaconexion;
                    conexion.Open();
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.CommandText = "PAS_ACTUALIZAR_REFERENCIA_CLIENTE";
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Connection = conexion;
                        comando.Parameters.Add(new OracleParameter("P_REFERENCIA_ID", P_REFERENCIA_ID));
                        comando.Parameters.Add(new OracleParameter("P_CLIENTE_ID", P_CLIENTE_ID));
                        comando.Parameters.Add(new OracleParameter("P_NOMBRE_REF", CRF_NOMBRE_REF));
                        comando.Parameters.Add(new OracleParameter("P_TELEFONO1", CRF_TELEFONO1));
                        comando.Parameters.Add(new OracleParameter("P_TELEFONO2", CRF_TELEFONO2));
                        comando.Parameters.Add(new OracleParameter("P_TELEFONO3", CRF_TELEFONO3));
                        comando.Parameters.Add(new OracleParameter("P_CORREO", CRF_CORREO_ELECTRONICO));
                        OracleDataReader read = comando.ExecuteReader();

                        return "datos actualizados";
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

        public String ClientesREFEliminar(int P_REFERENCIA_ID)
        {

            using (OracleConnection conexion = new OracleConnection())
            {
                try
                {
                    conexion.ConnectionString = conexionString;
                    conexion.Open();
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.CommandText = "PAS_ELIMINAR_REFERENCIA_CLIENTE";
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Connection = conexion;
                        comando.Parameters.Add(new OracleParameter("P_REFERENCIA_ID", P_REFERENCIA_ID));

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
