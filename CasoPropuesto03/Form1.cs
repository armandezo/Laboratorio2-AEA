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

namespace CasoPropuesto03
{
    public partial class Form1 : Form
    {
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["NeptunoDB"].ConnectionString);


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ListarAnios();
        }

        private void ListarAnios()
        {
            using (SqlCommand cmd = new SqlCommand("usp_Lista_anios", cn))
            {
                using (SqlDataAdapter da = new SqlDataAdapter())
                {
                    da.SelectCommand = cmd;
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    using (DataSet df = new DataSet())
                    {
                        da.Fill(df, "ListaAnios");
                        comboBox1.DataSource = df.Tables["ListaAnios"];
                        comboBox1.DisplayMember = "Anios";
                        comboBox1.ValueMember = "Anios";
                    }
                }
            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            using (SqlCommand cmd = new SqlCommand("usp_nombre_meses", cn))
            {
                using (SqlDataAdapter da = new SqlDataAdapter())
                {
                    da.SelectCommand = cmd;
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@anio", int.Parse(comboBox1.SelectedValue.ToString()));
                    using (DataSet df = new DataSet())
                    {
                        da.Fill(df, "listameses");
                        comboBox2.DataSource = df.Tables["listameses"];
                        comboBox2.DisplayMember = "meses";
                        comboBox2.ValueMember = "idMeses";
                    }
                }
            }
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_filtrar_pedidos_año_mes", cn))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        da.SelectCommand = cmd;
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@anio", comboBox1.SelectedValue.ToString());
                        da.SelectCommand.Parameters.AddWithValue("@mes", comboBox2.SelectedValue.ToString());
                        using (DataSet df = new DataSet())
                        {
                            da.Fill(df, "resultados");
                            dataGridView1.DataSource = df.Tables["resultados"];
                        }
                    }
                }
            }
            catch (Exception)
            {
                dataGridView1.DataSource = null;
            }
            
        }
    }
}
