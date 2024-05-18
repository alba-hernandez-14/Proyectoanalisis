﻿using System;
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
    /// Descripción breve de CXC_Reportes
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class CXC_Reportes : System.Web.Services.WebService
    {
        String servidor;
        String usuario;
        String password;
        String cadenaconexion;

        public CXC_Reportes()
        {

            servidor = "localhost:1521 / orcl";
            usuario = "PROYECTOANALISIS";
            password = "1234";
            cadenaconexion = "Data Source=" + servidor + ";User Id=" + usuario + "; Password=" + password + "; ";
        }

        public string conexionString = ConfigurationManager.ConnectionStrings["BaseDatos"].ConnectionString;


        [WebMethod]
        public DataSet Reporte_clientes_frecuentes_frecuencia()
        {
            try
            {
                OracleConnection conexion = new OracleConnection(cadenaconexion);//abrir la conexion 
                conexion.Open();     // se inicia la conexion 
                OracleDataAdapter adapter = new OracleDataAdapter("select * from fun_reporte_clientes_frecuentes_frecuencia_cxc() ", conexion);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "fun_reporte_clientes_frecuentes_frecuencia_cxc()");

                return ds;
            }
            catch (Exception ex)
            {
                // Manejar excepciones aquí
                throw new Exception("Error al intentar obtener datos: " + ex.Message);
            }

        }

        [WebMethod]
        public DataSet Reporte_clientes_frecuentes_cantidad_producto()
        {
            try
            {
                OracleConnection conexion = new OracleConnection(cadenaconexion);//abrir la conexion 
                conexion.Open();     // se inicia la conexion 
                OracleDataAdapter adapter = new OracleDataAdapter("select * from fun_reporte_clientes_frecuentes_cantidad_prod_cxc() ", conexion);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "fun_reporte_clientes_frecuentes_cantidad_prod_cxc()");

                return ds;
            }
            catch (Exception ex)
            {
                // Manejar excepciones aquí
                throw new Exception("Error al intentar obtener datos: " + ex.Message);
            }

        }

        [WebMethod]
        public DataSet Reporte_clientes_frecuentes_compra_total()
        {
            try
            {
                OracleConnection conexion = new OracleConnection(cadenaconexion);//abrir la conexion 
                conexion.Open();     // se inicia la conexion 
                OracleDataAdapter adapter = new OracleDataAdapter("select * from fun_reporte_clientes_frecuentes_compra_total_cxc() ", conexion);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "fun_reporte_clientes_frecuentes_compra_total_cxc()");

                return ds;
            }
            catch (Exception ex)
            {
                // Manejar excepciones aquí
                throw new Exception("Error al intentar obtener datos: " + ex.Message);
            }

        }

        [WebMethod]
        public DataSet Reporte_clientes_sin_compra_3_meses_cxc()
        {
            try
            {
                OracleConnection conexion = new OracleConnection(cadenaconexion);//abrir la conexion 
                conexion.Open();     // se inicia la conexion 
                OracleDataAdapter adapter = new OracleDataAdapter("select * from fun_clientes_sin_compra_3_meses_cxc() ", conexion);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "fun_clientes_sin_compra_3_meses_cxc()");

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