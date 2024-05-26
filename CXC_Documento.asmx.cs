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
using System.Diagnostics;

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
            usuario = "cliente";
            password = "123";
            cadenaconexion = "Data Source=" + servidor + ";User Id=" + usuario + "; Password=" + password + "; ";
        }
        public string conexionString = ConfigurationManager.ConnectionStrings["BaseDatos"].ConnectionString;

        [WebMethod]
        public DataSet DocumentosMostrar()
        {
            try
            {
                OracleConnection conexion = new OracleConnection(cadenaconexion);//abrir la conexion 
                conexion.Open();     // se inicia la conexion 
                OracleDataAdapter adapter = new OracleDataAdapter("select * from fas_listar_documentos() ", conexion);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "fas_listar_documentos()");

                //OracleDataAdapter adapterc = new OracleDataAdapter("select * from fas_listar_clientes()", conexion);
                //adapterc.Fill(ds,"fas_listar_clientes()");

                return ds;
            }
            catch (Exception ex)
            {
                // Manejar excepciones aquí
                throw new Exception("Error al intentar obtener datos: " + ex.Message);
            }

        }

        [WebMethod]
        public DataSet DocumentoBuscar(int p_documento)
        {
            try
            {
                OracleConnection conexion = new OracleConnection(cadenaconexion);//abrir la conexion 
                conexion.Open();     // se inicia la conexion 
                OracleDataAdapter adapter = new OracleDataAdapter("select * from fas_buscar_documentos(" + p_documento + ")", conexion);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "fas_buscar_documentos()");
                return ds;
            }
            catch (Exception ex)
            {
                // Manejar excepciones aquí
                throw new Exception("Error al intentar obtener datos: " + ex.Message);
            }

        }


        [WebMethod]
        public String Documentoguardar(long pDoc_venta, String pDoc_tipo_documento, String pDoc_valor, String p_serie, String pDoc_no_documento, String pDoc_no_emision, long pDoc_documento_asociado, int pDoc_valor_total)
        {
            using (OracleConnection conexion = new OracleConnection())
            {
                try
                {
                    conexion.ConnectionString = conexionString;
                    conexion.Open();
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.CommandText = "pas_crear_documento";
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Connection = conexion;
                        comando.Parameters.Add(new OracleParameter("p_venta", pDoc_venta));
                        comando.Parameters.Add(new OracleParameter("p_tipo_documento", pDoc_tipo_documento));
                        comando.Parameters.Add(new OracleParameter("v_total", pDoc_valor));
                        comando.Parameters.Add(new OracleParameter("p_serie", p_serie));
                        comando.Parameters.Add(new OracleParameter("p_no_documento", pDoc_no_documento));
                        comando.Parameters.Add(new OracleParameter("p_no_emision", pDoc_no_emision));
                        comando.Parameters.Add(new OracleParameter("p_documento_asociado", pDoc_documento_asociado));
                        comando.Parameters.Add(new OracleParameter("valortotal", pDoc_valor_total));
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
        public String Documentoactualizar(int p_documento, String pDoc_venta, String pDoc_tipo_documento, String pDoc_valor, String p_serie, String pDoc_no_documento, String pDoc_no_emision, String pDoc_documento_asociado)
        {
            using (OracleConnection conexion = new OracleConnection())
            {
                try
                {
                    conexion.ConnectionString = this.cadenaconexion;
                    conexion.Open();
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.CommandText = "pas_actualizar_documento";
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Connection = conexion;
                        comando.Parameters.Add(new OracleParameter("p_documento", p_documento));
                        comando.Parameters.Add(new OracleParameter("p_venta", pDoc_venta));
                        comando.Parameters.Add(new OracleParameter("p_tipo_documento", pDoc_tipo_documento));
                        comando.Parameters.Add(new OracleParameter("p_valor", pDoc_valor));
                        comando.Parameters.Add(new OracleParameter("p_no_documento", pDoc_no_documento));
                        comando.Parameters.Add(new OracleParameter("p_serie", p_serie));
                        comando.Parameters.Add(new OracleParameter("p_no_emision", pDoc_no_emision));
                        comando.Parameters.Add(new OracleParameter("p_documento_asociado", pDoc_documento_asociado));
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
        public String DocumentoEliminar(int p_documento)
        {

            using (OracleConnection conexion = new OracleConnection())
            {
                try
                {
                    conexion.ConnectionString = conexionString;
                    conexion.Open();
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.CommandText = "pas_eliminar_documento";
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Connection = conexion;
                        comando.Parameters.Add(new OracleParameter("p_documento", p_documento));

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

        [WebMethod]
        public String Documento_asignar_emision_documento(int p_documento)
        {
            using (OracleConnection conexion = new OracleConnection())
            {
                try
                {
                    conexion.ConnectionString = conexionString;
                    conexion.Open();
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.CommandText = "pas_asignar_emision_documento";
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Connection = conexion;
                        comando.Parameters.Add(new OracleParameter("p_documento", p_documento));

                        int numerodocumento = Numerodocumento();
                        int serie = NumeroSerie();
                        Guid uuid = Guid.NewGuid();

                        comando.Parameters.Add(new OracleParameter("p_no_documento", numerodocumento.ToString()));
                        comando.Parameters.Add(new OracleParameter("p_serie", serie.ToString()));
                        comando.Parameters.Add(new OracleParameter("p_no_emision", uuid.ToString()));


                        OracleDataReader read = comando.ExecuteReader();
                        return "emision asignada correctamento";
                    }
                }
                catch (Exception error)
                {
                    return "error, no se pudo asignar correctamente, verificar que el documento exista...";
                    throw error;
                }
            }
        }


        private int NumeroSerie()
        {
            Random nserie = new Random();
            int numeroAleatorio = nserie.Next(1000, 10000);
            return numeroAleatorio;
        }
        private int Numerodocumento()
        {
            Random nserie = new Random();
            int numeroAleatorio = nserie.Next(10000, 99999);
            return numeroAleatorio;
        }


        [WebMethod]
        public String Documento_asignar_anulacion_documento(int p_documento)
        {

            using (OracleConnection conexion = new OracleConnection())
            {
                try
                {
                    conexion.ConnectionString = conexionString;
                    conexion.Open();
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.CommandText = "pas_asignar_anulacion_documento";
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Connection = conexion;
                        comando.Parameters.Add(new OracleParameter("p_documento", p_documento));

                        OracleDataReader read = comando.ExecuteReader();
                        return "emision asignada correctamento";
                    }
                }
                catch (Exception error)
                {
                    return "error, no se pudo asignar correctamente...";
                    throw error;
                }
            }
        }


        [WebMethod]
        public DataSet Encabezado_documento(int iddocumento)
        {
            try
            {
                OracleConnection conexion = new OracleConnection(cadenaconexion);//abrir la conexion 
                conexion.Open();     // se inicia la conexion 
                OracleDataAdapter adapter = new OracleDataAdapter("select * from fun_encabezado_documento(" + iddocumento + ")", conexion);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "fun_encabezado_documento()");
                return ds;
            }
            catch (Exception ex)
            {
                // Manejar excepciones aquí
                throw new Exception("Error al intentar obtener datos: " + ex.Message);
            }
        }

        [WebMethod]
        public DataSet detalle_documento(int iddocumento)
        {
            try
            {
                OracleConnection conexion = new OracleConnection(cadenaconexion);//abrir la conexion 
                conexion.Open();     // se inicia la conexion 
                OracleDataAdapter adapter = new OracleDataAdapter("select * from fun_detalle_documento(" + iddocumento + ")", conexion);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "fun_detalle_documento()");
                return ds;
            }
            catch (Exception ex)
            {
                // Manejar excepciones aquí
                throw new Exception("Error al intentar obtener datos: " + ex.Message);
            }
        }

    }
}
