using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkingWithDB
{
    public partial class Form2 : Form
    {
        SqlConnection sqlConnection;
        public Form2()
        {
            
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "databaseDataSet.Army". При необходимости она может быть перемещена или удалена.
            this.armyTableAdapter.Fill(this.databaseDataSet.Army);

        }


       

   

        private void button1_Click(object sender, EventArgs e)
        {
            {

                comboBox1.Items.Clear();

                string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\WorkingWithDB\WorkingWithDB\Database.mdf;Integrated Security=True;";

                sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                SqlDataReader sqlReader = null;

                SqlCommand command = new SqlCommand("SELECT [Name_country] FROM [Army]", sqlConnection);

                sqlReader = command.ExecuteReader();

                HashSet <string> set = new HashSet<string>();
                
                while (sqlReader.Read())
                {
                    set.Add(sqlReader[0].ToString());


                }

                foreach (string s in set) {
                    comboBox1.Items.Add(s);
                }
                
                sqlConnection.Close();
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "Германия")
            {
                Form Form3 = new Form3();
                Form3.Show();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //if(comboBox1.)
        }

       
    }
}
