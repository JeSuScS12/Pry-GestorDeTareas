using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pry_GestorDeTareas
{
    internal class clsConexion
    {
        OleDbCommand comando;
        OleDbConnection conectar;
        OleDbDataAdapter adaptador;

        string cadena;

        public clsConexion()
        {
            cadena = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=../../BD/BDPry3.accdb";
        }

        public void Conect()
        {
            try
            {
                conectar = new OleDbConnection(cadena);
                conectar.Open();
            }
            catch (Exception x)
            {
                MessageBox.Show("Error" + x, "😒");
            }
        }

        public bool ValidarUsuario(string user, string password)
        {
            string consulta = $"select count(*) from Usuarios where Nombre = '{user}' and Password = '{password}'"; 
            conectar = new OleDbConnection(cadena);
            comando = new OleDbCommand(consulta, conectar);
            try
            {
                conectar.Open();
                int count = (int)comando.ExecuteScalar();
                return count > 0;
            }
            catch (Exception error)
            {
                MessageBox.Show("Error" + error);
                return false;
            }
        }

        public void UserGlobal(string user, string password)
        {

            string consulta = $"select Usuarios.IdUsuario , Usuarios.Nombre , Cargos.NombreCargo as Cargo , Usuarios.Perfil from Usuarios inner join Cargos on Cargos.IdCargos = Usuarios.Cargo where Nombre = '{user}' and Password = '{password}'";
            conectar = new OleDbConnection(cadena);
            comando = new OleDbCommand(consulta, conectar);

            try
            {
                conectar.Open();
                OleDbDataReader lector = comando.ExecuteReader();
                if (lector.Read())
                {
                    clsUsuario.id = lector.GetInt32(0);
                    clsUsuario.user = lector.GetString(1);
                    clsUsuario.cargo = lector.GetString(2);
                    clsUsuario.perfil = lector.GetString(3);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error" + error);
            }

        }

        //Listar todos los usuarios --> Solo para los Admin
        public void ListaUsuarios(DataGridView tabla)
        {
            conectar = new OleDbConnection(cadena);
            try
            {
                conectar.Open();
                string consulta = $"select Usuarios.IdUsuario as ID , Usuarios.Nombre , Cargos.NombreCargo as Cargo , Usuarios.Perfil from Usuarios inner join Cargos on Cargos.IdCargos = Usuarios.Cargo";

                adaptador = new OleDbDataAdapter(consulta, conectar);
                DataTable dataTable = new DataTable();
                adaptador.Fill(dataTable);
                tabla.DataSource = dataTable;

            }
            catch (Exception error)
            {
                MessageBox.Show("Error" + error);
            }

            tabla.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            tabla.ClearSelection();
        }

        //Listar tareas Actuales
        public void ListarTareas(DataGridView tabla)
        {
            conectar = new OleDbConnection(cadena);
            try
            {
                conectar.Open();
                string consulta = "SELECT Tareas.IdTarea AS Id, Categoria.NombreCat AS Categoria, Usuarios.Nombre, Tareas.Titulo, Tareas.Descripcion, Tareas.Prioridad, Tareas.FechaC, Estado.Estado\r\n" +
                    "FROM ((Tareas \r\n" +
                    "INNER JOIN Categoria ON Tareas.IdCategoria = Categoria.IdCategoria) \r\n" +
                    "INNER JOIN Usuarios ON Tareas.CreadorTarea = Usuarios.IdUsuario)\r\n" +
                    "INNER JOIN Estado ON  Tareas.Estado = Estado.IdEstado ;";

                adaptador = new OleDbDataAdapter(consulta, conectar);
                DataTable dataTable = new DataTable();
                adaptador.Fill(dataTable);
                tabla.DataSource = dataTable;

            }
            catch (Exception error)
            {
                MessageBox.Show("Error" + error);
            }

            tabla.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            
        }

        //LLenar el ComboBox con los Estados
        public void CargarEstados(ComboBox combo)
        {
            conectar = new OleDbConnection(cadena);
            try
            {
                conectar.Open();
                string consulta = "SELECT Estado  FROM Estado";
                comando = new OleDbCommand(consulta, conectar);
                OleDbDataReader reader = comando.ExecuteReader();

                // Limpia los items del ComboBox antes de agregar nuevos datos
                combo.Items.Clear();
                // Agrega los datos al ComboBox
                while (reader.Read())
                {
                    combo.Items.Add(reader["Estado"].ToString());
                }
                reader.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error" + error);
            }
        }

        //Llenar la tabla con estados Especificos
        public void TablaEstado(DataGridView tabla,int item)
        {
            conectar = new OleDbConnection(cadena);
            try
            {
                conectar.Open();
                string consulta = "SELECT Tareas.IdTarea AS Id, Categoria.NombreCat AS Categoria, Usuarios.Nombre, Tareas.Titulo, Tareas.Descripcion, Tareas.Prioridad, Tareas.FechaC, Estado.Estado\r\n" +
                    "FROM ((Tareas \r\n" +
                    "INNER JOIN Categoria ON Tareas.IdCategoria = Categoria.IdCategoria) \r\n" +
                    "INNER JOIN Usuarios ON Tareas.CreadorTarea = Usuarios.IdUsuario)\r\n" +
                    "INNER JOIN Estado ON  Tareas.Estado = Estado.IdEstado " +
                    $"where Tareas.Estado = {item};";

                adaptador = new OleDbDataAdapter(consulta, conectar);
                DataTable dataTable = new DataTable();
                adaptador.Fill(dataTable);
                tabla.DataSource = dataTable;

            }
            catch (Exception error)
            {
                MessageBox.Show("Error" + error);
            }

            tabla.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
        }
    }
}
