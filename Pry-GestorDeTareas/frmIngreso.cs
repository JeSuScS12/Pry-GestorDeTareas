using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Pry_GestorDeTareas
{
    public partial class frmIngreso : Form
    {
        public frmIngreso()
        {
            InitializeComponent();
            
        }

        //Instanciar
        clsConexion obj = new clsConexion();

        private void frm_close(object sender, FormClosingEventArgs e)
        {
            txtUsuario.Text = "";
            txtPassword.Text = "";
            this.Show();
            txtUsuario.Focus();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string user = txtUsuario.Text;
            string password = txtPassword.Text;

            if (txtUsuario.Text != "")
            {
                if (txtPassword.Text != "")
                {
                    lblError.Text = "";
                    lblError.Visible = false;

                    if (obj.ValidarUsuario(user, password))
                    {
                        obj.UserGlobal(user, password);

                        frmPrincipal frm = new frmPrincipal();

                        frm.Show();
                        this.Hide();

                        frm.FormClosing += frm_close;
                    }
                    else lblError.Text = "El usuario no existe";
                }
                else lblError.Text = "Ingrese Contraseña";
                lblError.Visible = true;
            }
            else lblError.Text = "Ingrese Usuario";
            lblError.Visible = true;
        }

        private void lblCrearPerfil_Click(object sender, EventArgs e)
        {
            frmSoliPerfil frm = new frmSoliPerfil();
            frm.ShowDialog();
        }

        private void chkPass_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPass.Checked)
            {
                if (txtPassword.PasswordChar == '●') txtPassword.PasswordChar = '\0';
            }
            else txtPassword.PasswordChar = '●';
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
