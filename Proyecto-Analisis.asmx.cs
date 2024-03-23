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
    /// Descripción breve de Proyecto_Analisis
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
     [System.Web.Script.Services.ScriptService]
    public class Proyecto_Analisis : System.Web.Services.WebService
    {
        String servidor;
        String usuario;
        String password;
        String cadenaconexion;

        public Proyecto_Analisis()
        {

            servidor = "localhost:1521 / orcl";
            usuario = "PROYECTO";
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
            String respuesta;
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

                        return respuesta = "guardado";
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
            String respuesta;
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

                        return respuesta = "datos de usuario actualizados";
                    }
                }
                catch (Exception error)
                {
                    return respuesta = "error";
                    throw error;
                }
            }

        }
        [WebMethod]//clientes

        public String ClientesEliminar (int cli_cliente)
        {
            String respuesta;
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
                        return respuesta = "datos de usuario eliminados";
                    }
                }
                catch (Exception error)
                {
                    return respuesta = error.Message;//"error al eliminar cliente";
                    throw error;
                }
            }

        }


        //Creditos

        [WebMethod]
        public DataSet CREDITOSMostrar()
        {
            try
            {
                OracleConnection conexion = new OracleConnection(cadenaconexion);//abrir la conexion 
                conexion.Open();     // se inicia la conexion 
                OracleDataAdapter adapter = new OracleDataAdapter("select * from fas_listar_credito()", conexion);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "fas_listar_credito()");
                return ds;
            }
            catch (Exception ex)
            {
                // Manejar excepciones aquí
                throw new Exception("Error al intentar obtener datos: " + ex.Message);
            }
        }

        [WebMethod] //credito 
        public String CREDITOSGuardar(int CLI_CLIENTE, double CRE_CREDITO_DISPONIBLE, double CRE_PLAZO)
        {
            String respuesta;
            using (OracleConnection conexion = new OracleConnection())
            {
                try
                {
                    conexion.ConnectionString = conexionString;
                    conexion.Open();
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.CommandText = "pas_crear_credito";
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Connection = conexion;
                        comando.Parameters.Add(new OracleParameter("p_cliente", CLI_CLIENTE));
                        comando.Parameters.Add(new OracleParameter("p_credito", CRE_CREDITO_DISPONIBLE));
                        comando.Parameters.Add(new OracleParameter("p_plazo", CRE_PLAZO));
                        OracleDataReader read = comando.ExecuteReader();
                        return respuesta = "guardado";
                    }
                }
                catch (Exception error)
                {
                    return respuesta = "error";
                    throw error;
                }
            }
        }

        [WebMethod]//creditos

        public String CREDITOSActualizar(int cre_credito , int cli_cliente, double cre_credito_disponible, double cre_plazo)
        {
            String respuesta;
            using (OracleConnection conexion = new OracleConnection())
            {
                try
                {
                    conexion.ConnectionString = conexionString;
                    conexion.Open();
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.CommandText = "pas_actualizar_credito";
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Connection = conexion;
                        comando.Parameters.Add(new OracleParameter("p_credito", cre_credito));
                        comando.Parameters.Add(new OracleParameter("p_cliente", cli_cliente));
                        comando.Parameters.Add(new OracleParameter("p_credito_disponible", cre_credito_disponible));
                        comando.Parameters.Add(new OracleParameter("p_plazo", cre_plazo));
                        
                        OracleDataReader read = comando.ExecuteReader();
                        return respuesta = "datos de usuario actualizados";
                    }
                }
                catch (Exception error)
                {
                    return respuesta = "error";
                    throw error;
                }
            }

        }
        [WebMethod]//creditos

        public String CREDITOSEliminar(int cre_credito)
        {
            String respuesta;
            using (OracleConnection conexion = new OracleConnection())
            {
                try
                {
                    conexion.ConnectionString = conexionString;
                    conexion.Open();
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.CommandText = "pas_eliminar_credito";
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Connection = conexion;
                        comando.Parameters.Add(new OracleParameter("p_credito", cre_credito));

                        OracleDataReader read = comando.ExecuteReader();
                        return respuesta = "datos de usuario eliminados";
                    }
                }
                catch (Exception error)
                {
                    return respuesta = "error al eliminar cliente";
                    throw error;
                }
            }

        }

        //Empleados

        [WebMethod]
        public DataSet EmpleadosMostrar()
        {
            try
            {
                OracleConnection conexion = new OracleConnection(cadenaconexion);//abrir la conexion 
                conexion.Open();     // se inicia la conexion 
                OracleDataAdapter adapter = new OracleDataAdapter("select * from fas_listar_empleado()", conexion);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "fas_listar_empleado()");
                return ds;
            }
            catch (Exception ex)
            {
                // Manejar excepciones aquí
                throw new Exception("Error al intentar obtener datos: " + ex.Message);
            }
        }


        [WebMethod] //empleados
        public String Empleadosguardar(string EMP_NOMBRE, string EMP_APELLIDO, string EMP_TELEFONO, string EMP_DIRECCION, string EMP_CORREO_ELECTRONICO)
        {
            String respuesta;
            using (OracleConnection conexion = new OracleConnection())
            {
                try
                {
                    conexion.ConnectionString = conexionString;
                    conexion.Open();
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.CommandText = "pas_crear_empleado";
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Connection = conexion;
                        comando.Parameters.Add(new OracleParameter("p_nombre", EMP_NOMBRE));
                        comando.Parameters.Add(new OracleParameter("p_apellido", EMP_APELLIDO));
                        comando.Parameters.Add(new OracleParameter("p_telefono", EMP_TELEFONO));
                        comando.Parameters.Add(new OracleParameter("p_direccion", EMP_DIRECCION));
                        comando.Parameters.Add(new OracleParameter("p_email", EMP_CORREO_ELECTRONICO));
                        OracleDataReader read = comando.ExecuteReader();

                        return respuesta = "guardado";
                    }
                }
                catch (Exception error)
                {
                    return respuesta = "error";
                    throw error;
                }
            }

        }


        [WebMethod] //empleados
        public String EmpleadosActualizar(int EMP_EMPLEADO, string EMP_NOMBRE, string EMP_APELLIDO, string EMP_TELEFONO, string EMP_DIRECCION, string EMP_CORREO_ELECTRONICO)
        {
            String respuesta;
            using (OracleConnection conexion = new OracleConnection())
            {
                try
                {
                    conexion.ConnectionString = conexionString;
                    conexion.Open();
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.CommandText = "pas_actualiza_empleado";
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Connection = conexion;
                        comando.Parameters.Add(new OracleParameter("p_empleado", EMP_EMPLEADO));
                        comando.Parameters.Add(new OracleParameter("p_nombre", EMP_NOMBRE));
                        comando.Parameters.Add(new OracleParameter("p_apellido", EMP_APELLIDO));
                        comando.Parameters.Add(new OracleParameter("p_telefono", EMP_TELEFONO));
                        comando.Parameters.Add(new OracleParameter("p_direccion", EMP_DIRECCION));
                        comando.Parameters.Add(new OracleParameter("p_email", EMP_CORREO_ELECTRONICO));
                        OracleDataReader read = comando.ExecuteReader();

                        return respuesta = "Actualizado";
                    }
                }
                catch (Exception error)
                {
                    return respuesta = "error";
                    throw error;
                }
            }

        }

        [WebMethod] //empleados
        public String EmpleadosEliminar(int EMP_EMPLEADO)
        {
            String respuesta;
            using (OracleConnection conexion = new OracleConnection())
            {
                try
                {
                    conexion.ConnectionString = conexionString;
                    conexion.Open();
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.CommandText = "pas_eliminar_empleado";
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Connection = conexion;
                        comando.Parameters.Add(new OracleParameter("p_empleado", EMP_EMPLEADO));
                        OracleDataReader read = comando.ExecuteReader();

                        return respuesta = "Eliminados";
                    }
                }
                catch (Exception error)
                {
                    return respuesta = "error";
                    throw error;
                }
            }

        }



        //Productos

        [WebMethod]
        public DataSet ProductosMostrar()
        {
            try
            {
                OracleConnection conexion = new OracleConnection(cadenaconexion);//abrir la conexion 
                conexion.Open();     // se inicia la conexion 
                OracleDataAdapter adapter = new OracleDataAdapter("select * from fas_listar_productos()", conexion);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "fas_listar_productos()");
                return ds;
            }
            catch (Exception ex)
            {
                // Manejar excepciones aquí
                throw new Exception("Error al intentar obtener datos: " + ex.Message);
            }
        }


        [WebMethod] //empleados
        public String Productosguardar(string pro_descripcion, double pro_precio, double pro_cantidad)
        {
            String respuesta;
            using (OracleConnection conexion = new OracleConnection())
            {
                try
                {
                    conexion.ConnectionString = conexionString;
                    conexion.Open();
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.CommandText = "pas_crear_producto";
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Connection = conexion;
                        comando.Parameters.Add(new OracleParameter("p_descripcion", pro_descripcion));
                        comando.Parameters.Add(new OracleParameter("p_precio", pro_precio));
                        comando.Parameters.Add(new OracleParameter("p_cantidad", pro_cantidad));
                        OracleDataReader read = comando.ExecuteReader();

                        return respuesta = "guardado";
                    }
                }
                catch (Exception error)
                {
                    return respuesta = "error";
                    throw error;
                }
            }

        }


        [WebMethod] //productos
        public String ProductosActualizar(int pro_producto, string pro_descripcion, double pro_precio, double pro_cantidad)
        {
            String respuesta;
            using (OracleConnection conexion = new OracleConnection())
            {
                try
                {
                    conexion.ConnectionString = conexionString;
                    conexion.Open();
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.CommandText = "pas_actualizar_producto";
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Connection = conexion;
                        comando.Parameters.Add(new OracleParameter("p_id", pro_producto));
                        comando.Parameters.Add(new OracleParameter("p_descripcion", pro_descripcion));
                        comando.Parameters.Add(new OracleParameter("p_precio", pro_precio));
                        comando.Parameters.Add(new OracleParameter("p_cantidad", pro_cantidad));
                        OracleDataReader read = comando.ExecuteReader();

                        return respuesta = "Actualizado";
                    }
                }
                catch (Exception error)
                {
                    return respuesta = "error";
                    throw error;
                }
            }

        }

        [WebMethod] //productos
        public String ProductosEliminar(int pro_producto)
        {
            String respuesta;
            using (OracleConnection conexion = new OracleConnection())
            {
                try
                {
                    conexion.ConnectionString = conexionString;
                    conexion.Open();
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.CommandText = "pas_eliminar_producto";
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Connection = conexion;
                        comando.Parameters.Add(new OracleParameter("p_id", pro_producto));
                        OracleDataReader read = comando.ExecuteReader();

                        return respuesta = "Eliminados";
                    }
                }
                catch (Exception error)
                {
                    return respuesta = "error";
                    throw error;
                }
            }

        }







    }
}
