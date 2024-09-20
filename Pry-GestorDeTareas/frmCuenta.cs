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
    public partial class frmCuenta : Form
    {
        public frmCuenta()
        {
            InitializeComponent();
        }

        //Instanciar conexion
        clsConexion obj = new clsConexion();

        private void frmCuenta_Load(object sender, EventArgs e)
        {
            obj.Usuario(txtUsuario, txtPass, txtCargo, txtPerfil);
        }

        private void rbEditar_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEditar.Checked == true) 
            {
                txtUsuario.Enabled = true;
                txtPass.Enabled = true;
                btnModificar.Enabled = true;
            }
            else
            {
                txtUsuario.Enabled = false;
                txtPass.Enabled = false;
                btnModificar.Enabled = false;
            }
        }


        //Arreglar
        private void btnModificar_Click(object sender, EventArgs e)
        {
            string user = txtUsuario.Text.Trim();
            string pass = txtPass.Text.Trim();

            obj.ModificarUsuario(user,pass);
            obj.Usuario(txtUsuario, txtPass, txtCargo, txtPerfil);

        }
        
    }
}
