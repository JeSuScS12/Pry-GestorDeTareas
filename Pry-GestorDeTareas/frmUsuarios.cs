using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pry_GestorDeTareas
{
    public partial class frmUsuarios : Form
    {
        public frmUsuarios()
        {
            InitializeComponent();
        }

        //Instanciar conexion
        clsConexion obj = new clsConexion();

        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            obj.ListaUsuarios(dgvTabla);
            obj.ListarSolicitudes(dgvTabla2);
            obj.CargarCargos(cmbCargo);
            obj.CargarCargos(cmbCargoC);
        }

        private void rbEditar_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEditar.Checked) boxEditar.Enabled = true;
            else boxEditar.Enabled = false;
        }

        private void rbCrear_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCrear.Checked) boxCrear.Enabled = true;
            else boxCrear.Enabled = false;
        }

        private void dgvTabla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvTabla.CurrentRow != null) // Verifica si hay una fila seleccionada
            {
                // Obtener la fila seleccionada actualmente
                DataGridViewRow fila = dgvTabla.CurrentRow;

                // Obtener los valores de las celdas de la fila seleccionada
                int id = Convert.ToInt32(fila.Cells["ID"].Value);
                string nombre = fila.Cells["Nombre"].Value.ToString();
                string pass = fila.Cells["Password"].Value.ToString();
                string cargo = fila.Cells["Cargo"].Value.ToString();
                string perfil = fila.Cells["Perfil"].Value.ToString();

                // Mostrar los valores en la consola o utilizarlos según sea necesario
                txtPerfil.Text = perfil;
                txtPass.Text = pass;
                txtUsuario.Text = nombre;
                cmbCargo.Text = cargo;

                idM = id;
            }
        }

        //ID's
        int idM = 0;
        int idC = 0;


        private void dgvTabla2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvTabla2.CurrentRow != null)
            {
                // Obtener la fila seleccionada actualmente
                DataGridViewRow fila = dgvTabla2.CurrentRow;

                // Obtener los valores de las celdas de la fila seleccionada
                int id = Convert.ToInt32(fila.Cells["ID"].Value.ToString());
                string nombre = fila.Cells["Usuario"].Value.ToString();
                string pass = fila.Cells["Contraseña"].Value.ToString();

                // Mostrar los valores en la consola o utilizarlos según sea necesario
                txtPassC.Text = pass;
                txtUsuarioC.Text = nombre;

                idC = id;
            }
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            string user = txtUsuarioC.Text;
            string pass = txtPassC.Text;

            string perfil = txtPerfilC.Text;
            int cargo = cmbCargoC.SelectedIndex;

            obj.CrearUsuario(user, pass, cargo + 1, perfil);
            obj.BorrarSoli(idC);
            obj.ListarSolicitudes(dgvTabla2);
            obj.ListaUsuarios(dgvTabla);

            idC = 0;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            string user = txtUsuario.Text;
            string pass = txtPass.Text;

            string perfil = txtPerfil.Text;
            int cargo = cmbCargo.SelectedIndex;

            obj.ModificarUser(idM, user, pass, cargo+1, perfil);
            obj.ListaUsuarios(dgvTabla);

            idM = 0;
        }
    }
}
