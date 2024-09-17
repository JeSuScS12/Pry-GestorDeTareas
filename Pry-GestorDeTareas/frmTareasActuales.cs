using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Pry_GestorDeTareas
{
    public partial class frmTareasActuales : Form
    {
        public frmTareasActuales()
        {
            InitializeComponent();
            dgvTabla.ClearSelection();

        }

        //Instanciar conexion   
        clsConexion obj = new clsConexion();

        private void frmTareasActuales_Load(object sender, EventArgs e)
        {
            obj.ListarTareas(dgvTabla);
            obj.CargarEstados(cmbEstados);
            
        }

        private void dgvTabla_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.dgvTabla.Columns[e.ColumnIndex].Name == "Estado")
            {
                string caja = e.Value?.ToString();

                if (caja == "Finalizado")
                {
                    e.CellStyle.ForeColor = Color.Green;
                }
                if (caja == "Sin Iniciar")
                {
                    e.CellStyle.ForeColor = Color.Red;
                }
                if (caja == "En Proceso")
                {
                    e.CellStyle.ForeColor = Color.Blue;
                }
            }
        }

        private void cmbEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            int x = cmbEstados.SelectedIndex;

            obj.TablaEstado(dgvTabla, x+1);
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            cmbEstados.Text = "";
            obj.ListarTareas(dgvTabla);
        }

        private void dgvTabla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvTabla.CurrentRow != null) // Verifica si hay una fila seleccionada
            {
                // Obtener la fila seleccionada actualmente
                DataGridViewRow fila = dgvTabla.CurrentRow;

                // Obtener los valores de las celdas de la fila seleccionada
                int idTarea = Convert.ToInt32(fila.Cells["Id"].Value);
                string cat = fila.Cells["Categoria"].Value.ToString();
                string nombre = fila.Cells["Nombre"].Value.ToString(); 
                string title = fila.Cells["Titulo"].Value.ToString();
                string desc = fila.Cells["Descripcion"].Value.ToString();
                string prio = fila.Cells["Prioridad"].Value.ToString();
                DateTime fecha = Convert.ToDateTime(fila.Cells["FechaC"].Value);
                string estado = fila.Cells["Estado"].Value.ToString();

                //aux = Convert.ToInt32(fila.Cells["Codigo"].Value);

                // Mostrar los valores en la consola o utilizarlos según sea necesario
                lblCategoria.Text = cat;
                lblCreador.Text = nombre;
                lblTitulo.Text = title;
                lblDescripcion.Text = desc;
                lblPrioridad.Text = prio;
                lblFechaC.Text = fecha.ToString("dd MMMM, yyyy");
                lblEstado.Text = estado;
                
            }
        }
    }
}
