using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TpCrudSql
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void DtaInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Refresh();
        }

        private void Refresh() 
        {
           AutosDb oAutoDb = new AutosDb();
            DtaInfo.DataSource = oAutoDb.Get();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        private void BtnNuevoAuto_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2(); 
            frm.ShowDialog();
            Refresh();
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (DtaInfo.CurrentRow == null)
            {
                MessageBox.Show("Seleccioná un auto de la lista.");
                return;
            }

            var seleccionado = (Auto)DtaInfo.CurrentRow.DataBoundItem;

            var confirma = MessageBox.Show(
                $"¿Seguro que querés eliminar '{seleccionado.Name}' (ID {seleccionado.Id})?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirma != DialogResult.Yes) return;

            try
            {
                new AutosDb().EliminarAuto(seleccionado.Id);
                Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message);
            }
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }
    }
}
