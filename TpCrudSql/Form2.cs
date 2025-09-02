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
    public partial class Form2 : Form
    {
     
        public Form2()
        {
            InitializeComponent();
            
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            AutosDb oAutoDb = new AutosDb();

            try 
            {
                oAutoDb.AgregarAuto(txtName.Text, int.Parse(txtEdad.Text)); 
                MessageBox.Show("Auto agregado con éxito");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un erro en agregar el auto" + ex.Message); 
            }
        }

 
    }
}
