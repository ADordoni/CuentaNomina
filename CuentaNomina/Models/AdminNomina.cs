using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CuentaNomina.Models
{
    public class AdminNomina
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        /* Fill the "Data Source" field with the server name,
         * fill the "Initial Catalog" field with the data base name,
         * and fill the "Integrated Security" (write "true" if there is no password)*/
        private string cadena = "Data Source= ;Initial Catalog= ;Integrated Security= ";
        private void Conectar()
        {
            conexion = new SqlConnection(cadena);
        }
        public void Alta(Empleado per)
        {
            Conectar();
            comando = new SqlCommand("insert into nomina (dni,nombre,domicilio,fechanac,fechaing,puesto,salario,usuario) values (@dni,@nombre,@domicilio,@fechanac,@fechaing,@puesto,@salario,@usuario)", conexion);
            comando.Parameters.Add("@dni", SqlDbType.VarChar);
            comando.Parameters.Add("@nombre", SqlDbType.VarChar);
            comando.Parameters.Add("@domicilio", SqlDbType.VarChar);
            comando.Parameters.Add("@fechanac", SqlDbType.Date);
            comando.Parameters.Add("@fechaing", SqlDbType.Date);
            comando.Parameters.Add("@puesto", SqlDbType.VarChar);
            comando.Parameters.Add("@salario", SqlDbType.Int);
            comando.Parameters.Add("@usuario", SqlDbType.VarChar);
            comando.Parameters["@dni"].Value = per.dni;
            comando.Parameters["@nombre"].Value = per.nombre;
            comando.Parameters["@domicilio"].Value = per.domicilio;
            comando.Parameters["@fechanac"].Value = per.fechanac;
            comando.Parameters["@fechaing"].Value = per.fechaing;
            comando.Parameters["@puesto"].Value = per.puesto;
            comando.Parameters["@salario"].Value = per.salario;
            comando.Parameters["@usuario"].Value = per.usuario;
            conexion.Open();
            comando.ExecuteNonQuery();
            conexion.Close();
        }
        public Empleado Leer(string dni, string usuario)
        {
            Conectar();
            comando = new SqlCommand("select nombre,domicilio,fechanac,fechaing,puesto,salario from nomina where dni=@dni and usuario=@usuario", conexion);
            comando.Parameters.Add("@dni", SqlDbType.VarChar);
            comando.Parameters["@dni"].Value = dni;
            comando.Parameters.Add("@usuario", SqlDbType.VarChar);
            comando.Parameters["@usuario"].Value = usuario;
            conexion.Open();
            SqlDataReader registro = comando.ExecuteReader();
            Empleado pers = new Empleado();
            if (registro.Read())
            {
                pers.nombre = registro["nombre"].ToString();
                pers.domicilio = registro["domicilio"].ToString();
                pers.fechanac = DateTime.Parse(registro["fechanac"].ToString());
                pers.fechaing = DateTime.Parse(registro["fechaing"].ToString());
                pers.puesto = registro["puesto"].ToString();
                pers.salario = int.Parse(registro["salario"].ToString());
                pers.dni = dni;
                pers.usuario = usuario;
            }
            conexion.Close();
            return pers;
        }
        public List<Empleado> LeerTodo(string usuario)
        {
            Conectar();
            List<Empleado> lista = new List<Empleado>();
            comando = new SqlCommand("select nombre,domicilio,fechanac,fechaing,puesto,salario,dni from nomina where usuario=@usuario", conexion);
            comando.Parameters.Add("@usuario", SqlDbType.VarChar);
            comando.Parameters["@usuario"].Value = usuario;
            conexion.Open();
            SqlDataReader registros = comando.ExecuteReader();
            while (registros.Read())
            {
                Empleado pers = new Empleado()
                {
                    dni = registros["dni"].ToString(),
                    nombre = registros["nombre"].ToString(),
                    domicilio = registros["domicilio"].ToString(),
                    fechanac = DateTime.Parse(registros["fechanac"].ToString()),
                    fechaing = DateTime.Parse(registros["fechaing"].ToString()),
                    puesto = registros["puesto"].ToString(),
                    salario = int.Parse(registros["salario"].ToString()),
                    usuario = usuario
                };
                lista.Add(pers);
            }
            conexion.Close();
            return lista;
        }
        public void Modificar(Empleado per)
        {
            Conectar();
            comando = new SqlCommand("update nomina set nombre=@nombre, domicilio=@domicilio, fechanac=@fechanac, fechaing=@fechaing, puesto=@puesto, salario=@salario where dni=@dni and usuario=@usuario", conexion);
            comando.Parameters.Add("@dni", SqlDbType.VarChar);
            comando.Parameters.Add("@nombre", SqlDbType.VarChar);
            comando.Parameters.Add("@domicilio", SqlDbType.VarChar);
            comando.Parameters.Add("@fechanac", SqlDbType.Date);
            comando.Parameters.Add("@fechaing", SqlDbType.Date);
            comando.Parameters.Add("@puesto", SqlDbType.VarChar);
            comando.Parameters.Add("@salario", SqlDbType.Int);
            comando.Parameters.Add("@usuario", SqlDbType.VarChar);
            comando.Parameters["@dni"].Value = per.dni;
            comando.Parameters["@nombre"].Value = per.nombre;
            comando.Parameters["@domicilio"].Value = per.domicilio;
            comando.Parameters["@fechanac"].Value = per.fechanac;
            comando.Parameters["@fechaing"].Value = per.fechaing;
            comando.Parameters["@puesto"].Value = per.puesto;
            comando.Parameters["@salario"].Value = per.salario;
            comando.Parameters["@usuario"].Value = per.usuario;
            conexion.Open();
            comando.ExecuteNonQuery();
            conexion.Close();
        }
        public void Borrar(string usuario, string dni)
        {
            Conectar();
            comando = new SqlCommand("delete from nomina where dni = @dni and usuario=@usuario", conexion);
            comando.Parameters.Add("@usuario", SqlDbType.VarChar);
            comando.Parameters["@usuario"].Value = usuario;
            comando.Parameters.Add("@dni", SqlDbType.VarChar);
            comando.Parameters["@dni"].Value = dni;
            conexion.Open();
            comando.ExecuteNonQuery();
            conexion.Close();
        }
    }
}
