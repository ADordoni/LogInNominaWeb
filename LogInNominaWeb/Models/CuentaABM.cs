using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace LogInNominaWeb.Models
{
    public class CuentaABM
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private string cadena = "Data Source=DESKTOP-KCVTS1S;Initial Catalog=db1;Integrated Security= True";
        private void Conectar()
        {
            conexion = new SqlConnection(cadena);
        }
        public void Alta (Cuenta user)
        {
            Conectar();
            comando = new SqlCommand("insert into usuarios (nombre,clave) values (@nombre,@clave)", conexion);
            comando.Parameters.Add("@nombre", SqlDbType.VarChar);
            comando.Parameters.Add("@clave", SqlDbType.VarChar);
            comando.Parameters["@nombre"].Value = user.nombre;
            comando.Parameters["@clave"].Value = user.clave;
            conexion.Open();
            comando.ExecuteNonQuery();
            conexion.Close();
        }
        public Cuenta Leer (string nombre)
        {
            Conectar();
            comando = new SqlCommand("select nombre,clave from usuarios where nombre=@nombre", conexion);
            comando.Parameters.Add("@nombre", SqlDbType.VarChar);
            comando.Parameters["@nombre"].Value = nombre;
            conexion.Open();
            SqlDataReader registro = comando.ExecuteReader();
            Cuenta user = new Cuenta();
            if (registro.Read())
            {
                user.nombre = registro["nombre"].ToString();
                user.clave = registro["clave"].ToString();
                conexion.Close();
                return user;
            }
            else
            {
                return null;
            }
        }
        public void Modificar (Cuenta user)
        {
            Conectar();
            comando = new SqlCommand("update usuarios set clave=@clave where nombre=@nombre", conexion);
            comando.Parameters.Add("@nombre", SqlDbType.VarChar);
            comando.Parameters.Add("@clave", SqlDbType.VarChar);
            comando.Parameters["@nombre"].Value = user.nombre;
            comando.Parameters["@clave"].Value = user.clave;
            conexion.Open ();
            comando.ExecuteNonQuery ();
            conexion.Close ();
        }
    }
}
