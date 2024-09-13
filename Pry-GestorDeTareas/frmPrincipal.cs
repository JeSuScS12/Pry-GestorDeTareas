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
            AbrirFrm(new frmIngreso());
            obj.Conect();
        }

        //Instancia de conexion
        clsConexion obj = new clsConexion();


        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


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

        private void tbnInicio_Click(object sender, EventArgs e)
        {
            AbrirFrm(new frmIngreso());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AbrirFrm(new frmTareasActuales());
        }
    }
}
