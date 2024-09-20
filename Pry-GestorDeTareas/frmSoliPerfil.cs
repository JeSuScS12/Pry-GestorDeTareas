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
    public partial class frmSoliPerfil : Form
    {
        public frmSoliPerfil()
        {
            InitializeComponent();
        }


        //Instanciar conexion   
        clsConexion obj = new clsConexion();

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSolicitar_Click(object sender, EventArgs e)
        {
            string user = txtUsuario.Text;
            string pass = txtPass.Text;

            obj.Solicitud(user, pass);
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMostrar.Checked)
            {
                if (txtPass.PasswordChar == '●') txtPass.PasswordChar = '\0';
            }
            else txtPass.PasswordChar = '●';
        }
    }
}
