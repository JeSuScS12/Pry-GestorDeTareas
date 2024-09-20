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
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        //Instancia de conexion
        clsConexion obj = new clsConexion();



        //Controles de Minmizar  y Cerrar
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //Abre los dstintos formularios en un panel dentro del Principal
        private void AbrirFrm(object frmHijo)
        {
            if (this.panelConte.Controls.Count > 0)
            {
                this.panelConte.Controls.RemoveAt(0);
            }

            Form frm = frmHijo as Form;
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            this.panelConte.Controls.Add(frm);
            this.panelConte.Tag = frm;
            frm.Show();
        }

        //Hora
        private void timerHora_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        //Controles  a Cargar al iniciar el frmPrincipal
        private void frmPrincipal_Load(object sender, EventArgs e)
        {

            if(clsUsuario.perfil == "Admin")
            {
                btnCrearPerfil.Visible = true;
                lblUser.Text = clsUsuario.user + " 👑";
            }
            else
            {
                lblUser.Text = clsUsuario.user;
            }
            lblPerfil.Text = clsUsuario.cargo;

            btnCrearTarea_Click(sender, e);
        }

        private void frmPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            lblPerfil.Text = "";
            lblUser.Text = "";
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            clsUsuario.user = "";
            clsUsuario.perfil = "";

            this.Close();
        }

        //Formularios a Abrir

        private void btnCrearTarea_Click(object sender, EventArgs e)
        {
            ColorBtn(panelLeft);
            btnCrearTarea.BackColor = Color.FromArgb(145, 42, 66);
            AbrirFrm(new frmTareasActuales());
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            ColorBtn(panelLeft);
            btnModificar.BackColor = Color.FromArgb(145, 42, 66);
            AbrirFrm(new frmModTareas());
        }

        private void btnCuenta_Click(object sender, EventArgs e)
        {
            ColorBtn(panelLeft);
            btnCuenta.BackColor = Color.FromArgb(145, 42, 66);
            AbrirFrm(new frmCuenta());
        }

        private void btnCrearPerfil_Click(object sender, EventArgs e)
        {
            ColorBtn(panelLeft);
            btnCrearPerfil.BackColor = Color.FromArgb(145, 42, 66);
            AbrirFrm(new frmUsuarios());
        }

        //Control de Btn
        public void ColorBtn(Panel panel)
        {
            foreach (Control btn in panel.Controls)
            {
                if (btn is Button)
                {
                    if(btn.BackColor == Color.FromArgb(145, 42, 66))
                    {
                        btn.BackColor = Color.FromArgb(121, 35, 55);
                    }
                }
            }
                
        }
    }
}
