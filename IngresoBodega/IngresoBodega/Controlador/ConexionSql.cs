using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using IngresoBodega.Modelos;
using System.Data;

namespace IngresoBodega.Controlador
{
    class ConexionSql
    {
        private const string connectionString = "" +
                "datasource=85.10.205.173;" +
                "port=3306;" +
                "username=adminbodega;" +
                "password=ABodega2020;" +
                "database=bodega2020;" +
                "old guids=true;";

        public Boolean RegistrarInOut(string tipo, string documento)
        {
            string queryRegistro = "INSERT INTO `registro` (`reg_id`, `reg_tipo`, `reg_documento`, `fecha`) VALUES(NULL, '" + tipo + "', '" + documento + "', SYSDATE())";

            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(queryRegistro, databaseConnection);
            commandDatabase.CommandTimeout = 60;

            try
            {
                databaseConnection.Open();
                commandDatabase.ExecuteReader();

                return true;
            }
            catch (Exception e)
            {
                var error = e.Message;
                return false;
            }
        }

        public Boolean validarFuncionario(string documento, string tabla)
        {
            string query = string.Empty;
            if (tabla == "funcionario")
            {
                query = "SELECT * FROM `funcionario` WHERE fun_documento = '" + documento + "'";
            }
            else
            {
                query = "SELECT * FROM `autorizados` WHERE aut_documento = '" + documento + "'";
            }

            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;

            try
            {
                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();

                if (reader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                var error = e.Message;
                return false;
            }
        }

        public Boolean AutorizarIngreso(float documento)
        {
            string query = "SELECT * FROM `autorizados` WHERE aut_documento = '" + documento + "'";

            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;

            try
            {
                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();

                if (reader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                var error = e.Message;
                return false;
            }
        }

        public Boolean Insertar(string documento)
        {
            string query = "INSERT INTO autorizados (aut_id, aut_documento) VALUES (null, '" + documento + "')";

            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;

            try
            {
                databaseConnection.Open();

                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                databaseConnection.Close();

                return true;
            }
            catch (Exception e)
            {
                var error = e.Message;
                return false;
            }
        }

        public List<Funcionarios> Mostrar(string documento)
        {
            List<Funcionarios> listaMostrar = new List<Funcionarios>();
            DataTable datos = new DataTable();

            string query = "SELECT * FROM funcionario WHERE fun_documento = '" + documento + "'";

            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;

            try
            {
                databaseConnection.Open();

                reader = commandDatabase.ExecuteReader();

                datos.Load(reader);

                foreach (DataRow item in datos.Rows)
                {
                    listaMostrar.Add(CargarConsultaFuncionarios(item));
                }

                databaseConnection.Close();
                return listaMostrar;
            }
            catch (Exception ex)
            {
                return listaMostrar;
            }
        }
        private Funcionarios CargarConsultaFuncionarios(DataRow registro)
        {
            Funcionarios Funcionarios = new Funcionarios();
            
            Funcionarios.fun_nombre = registro["fun_nombre"].ToString();
            Funcionarios.fun_segundo_nombre = registro["fun_segundo_nombre"].ToString();
            Funcionarios.fun_apellidos = registro["fun_apellidos"].ToString();
            Funcionarios.fun_documento = float.Parse(registro["fun_documento"].ToString());            
            Funcionarios.fun_img_perfil = registro["fun_img_perfil"].ToString();
            Funcionarios.fun_contacto1 = registro["fun_contacto1"].ToString();
            Funcionarios.fun_contacto2 = registro["fun_contacto2"].ToString();
            Funcionarios.fun_contacto3 = registro["fun_contacto3"].ToString();
            Funcionarios.fun_correo = registro["fun_correo"].ToString();

            return Funcionarios;
        }

    }
}
