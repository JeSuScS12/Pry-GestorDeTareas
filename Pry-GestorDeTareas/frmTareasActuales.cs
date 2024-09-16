﻿using System;
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
    }
}
