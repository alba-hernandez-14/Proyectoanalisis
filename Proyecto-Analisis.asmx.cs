using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Proyectoanalisis_
{
    /// <summary>
    /// Descripción breve de Proyecto_Analisis
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
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

        [WebMethod]
        public DataSet mostrar()
        {
            try
            {
               OracleConnection conexion = new OracleConnection(cadenaconexion);//abrir la conexion 
               conexion.Open();     // se inicia la conexion 
               OracleDataAdapter adapter = new OracleDataAdapter("select * from clientes", conexion);
               DataSet ds = new DataSet();
               adapter.Fill(ds, "clientes");
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
