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
        }
    }
}
