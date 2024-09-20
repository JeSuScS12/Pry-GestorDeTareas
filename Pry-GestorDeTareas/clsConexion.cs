using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

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
                string consulta = $"select Usuarios.IdUsuario as ID , Usuarios.Nombre ,Usuarios.Password, Cargos.NombreCargo as Cargo , Usuarios.Perfil from Usuarios inner join Cargos on Cargos.IdCargos = Usuarios.Cargo";

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
        public void TablaEstado(DataGridView tabla, int item)
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

        public void Solicitud(string user, string pass)
        {
            string consulta = $"insert into Solicitudes (Usuario, Contraseña) values('{user}','{pass}')";
            conectar = new OleDbConnection(cadena);
            comando = new OleDbCommand(consulta, conectar);
            try
            {
                conectar.Open();
                int result = comando.ExecuteNonQuery();
                // Comprobar si se Agrego
                if (result > 0)
                {
                    MessageBox.Show("¡Se realizo la solicitud con exito con éxito!", "Notificacion");
                }
                else
                {
                    MessageBox.Show("No se pude realizar la solicitud.", "Notificacion");
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error" + error);
            }
        }

        public void ListarSolicitudes(DataGridView tabla)
        {
            conectar = new OleDbConnection(cadena);
            try
            {
                conectar.Open();
                string consulta = $"select IdSoli as ID, Usuario,Contraseña from Solicitudes";
                adaptador = new OleDbDataAdapter(consulta, conectar);
                DataTable dataTable = new DataTable();
                adaptador.Fill(dataTable);
                tabla.DataSource = dataTable;

            }
            catch (Exception error)
            {
                MessageBox.Show("Error" + error);
            }
        }

        //Listar cargos en un CMB
        public void CargarCargos(ComboBox combo)
        {
            conectar = new OleDbConnection(cadena);
            try
            {
                conectar.Open();
                string consulta = "SELECT NombreCargo  FROM Cargos";
                comando = new OleDbCommand(consulta, conectar);
                OleDbDataReader reader = comando.ExecuteReader();

                // Limpia los items del ComboBox antes de agregar nuevos datos
                combo.Items.Clear();
                // Agrega los datos al ComboBox
                while (reader.Read())
                {
                    combo.Items.Add(reader["NombreCargo"].ToString());
                }
                reader.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error" + error);
            }
        }


        //Arreglarlo ---------------------------------------------------------------
        public void CrearUsuario(string user, string pass , int cargo, string perfil)
        {
            string consulta = $"insert into Usuarios (Nombre, Password, Cargo,Perfil) values('{user}','{pass}',{cargo},'{perfil}')";
            conectar = new OleDbConnection(cadena);
            comando = new OleDbCommand(consulta, conectar);
            try
            {
                conectar.Open();
                int result = comando.ExecuteNonQuery();
                // Comprobar si se Agrego
                if (result > 0)
                {
                    MessageBox.Show("¡Se creo al Usuario con éxito!", "Notificacion");
                }
                else
                {
                    MessageBox.Show("No se pudo crear el Usuario.", "Notificacion");
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error" + error);
            }
        }
        
        public void BorrarSoli(int id)
        {
            string consulta = $"delete Solicitudes where IdSoli = {id}";
            conectar = new OleDbConnection(cadena);
            try
            {
                conectar.Open();
                comando = new OleDbCommand(consulta, conectar);
                int result = comando.ExecuteNonQuery();
                if(result > 0)
                {
                    Console.WriteLine("CAmbio");
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error" + error);
            }
        }

        public void ModificarUser(int id,string user , string pass ,int cargo , string perfil)
        {
            string consulta = $"update Usuarios set Nombre ='{user}', Password='{pass}', Cargo={cargo}, Perfil='{perfil}' where IdUsuario = {id}";
            conectar = new OleDbConnection(cadena);
            comando = new OleDbCommand(consulta, conectar);
            try
            {
                conectar.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error" + error);
            }
        }
        //---------------------------------------------------------------

        public void Usuario(TextBox user, TextBox pass, TextBox cargo, TextBox perfil)
        {
            string consulta = $"select Usuarios.Nombre ,Usuarios.Password, Cargos.NombreCargo as Cargo , Usuarios.Perfil from Usuarios inner join Cargos on Cargos.IdCargos = Usuarios.Cargo where IdUsuario = {clsUsuario.id}";
            conectar = new OleDbConnection(cadena);
            comando = new OleDbCommand(consulta, conectar);

            try
            {
                conectar.Open();
                OleDbDataReader lector = comando.ExecuteReader();
                if (lector.Read())
                {
                    user.Text = lector.GetString(0);
                    pass.Text = lector.GetString(1);
                    cargo.Text = lector.GetString(2);
                    perfil.Text = lector.GetString(3);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error" + error);
            }
        }

        //Arreglar
        public void ModificarUsuario(string user, string pass)
        {
            string consulta = $"update Usuarios set Nombre ='{user}', Password='{pass}' where IdUsuario = {clsUsuario.id}";
            conectar = new OleDbConnection(cadena);
            comando = new OleDbCommand(consulta, conectar);
            try
            {
                conectar.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error" + error);
            }
        }
        //---------------------------------------------------------------

    }
}
