using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace casoPropuesto01
{
    public partial class Form1 : Form
    {

        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["NeptunoDB"].ConnectionString);

        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void listar_pedidos(string nombre ,string apellidos)
        {
            
            using (SqlCommand cmd = new SqlCommand("usp_listar_pedidos_nombre_apellido", cn))
            {
                using (SqlDataAdapter da = new SqlDataAdapter())
                {
                    da.SelectCommand = cmd;
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@nombreApellido", $"{nombre}{apellidos}");
                    using (DataSet df = new DataSet())
                    {
                        da.Fill(df, "Pedidos");
                        dataGridView1.DataSource = df.Tables["Pedidos"];
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nombre = textBox1.Text.ToString().Trim();
            string apellidos = textBox2.Text.ToString().Trim();
            listar_pedidos(nombre,apellidos);
        }
    }
}
