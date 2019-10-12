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
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection;

        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }
        private void AutoSizeRowsMode(Object sender, EventArgs es)
        {
            this.dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.DefaultCellStyle.Font = new Font("Tahoma", 12);
            this.dataGridView1.PerformLayout();

        }



        static Image ScaleImage(Image source, int width, int height)
        {

            Image dest = new Bitmap(width, height);
            using (Graphics gr = Graphics.FromImage(dest))
            {
                gr.FillRectangle(Brushes.White, 0, 0, width, height);  // Очищаем экран
                gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                float srcwidth = source.Width;
                float srcheight = source.Height;
                float dstwidth = width;
                float dstheight = height;

                if (srcwidth <= dstwidth && srcheight <= dstheight)  // Исходное изображение меньше целевого
                {
                    int left = (width - source.Width) / 2;
                    int top = (height - source.Height) / 2;
                    gr.DrawImage(source, left, top, source.Width, source.Height);
                }
                else if (srcwidth / srcheight > dstwidth / dstheight)  // Пропорции исходного изображения более широкие
                {
                    float cy = srcheight / srcwidth * dstwidth;
                    float top = ((float)dstheight - cy) / 2.0f;
                    if (top < 1.0f) top = 0;
                    gr.DrawImage(source, 0, top, dstwidth, cy);
                }
                else  // Пропорции исходного изображения более узкие
                {
                    float cx = srcwidth / srcheight * dstheight;
                    float left = ((float)dstwidth - cx) / 2.0f;
                    if (left < 1.0f) left = 0;
                    gr.DrawImage(source, left, 0, cx, dstheight);
                }

                return dest;
            }
        }














        private async void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "databaseDataSet.Army". При необходимости она может быть перемещена или удалена.
            AutoSizeRowsMode(sender,e);
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\WorkingWithDB\WorkingWithDB\Database.mdf;Integrated Security=True";
 


            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM [Army]", sqlConnection);

            //dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.ColumnHeadersDefaultCellStyle.Font.FontFamily, 12f, FontStyle.Bold | FontStyle.Italic); //жирный курсив размера 16

            List<string[]> data = new List<string[]>();
            //textBox17.ReadOnly = true;
            dataGridView1.ReadOnly = true;
            List<string> im1 = new List<string>();//изначальный
            List<string> im_oborona = new List<string>();
            List<string> im_nastuplenie = new List<string>();
            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    data.Add(new string[8]);

                    data[data.Count - 1][0] = sqlReader[0].ToString();
                    data[data.Count - 1][1] = sqlReader[1].ToString();
                    data[data.Count - 1][2] = sqlReader[2].ToString();
                    data[data.Count - 1][3] = sqlReader[3].ToString(); //легкая техника
                    data[data.Count - 1][4] = sqlReader[4].ToString();
                    data[data.Count - 1][5] = sqlReader[5].ToString();
                    im_oborona.Add(sqlReader[6].ToString());
                    im_nastuplenie.Add(sqlReader[7].ToString());
                    //data[data.Count - 1][6] = sqlReader[6].ToString();
                    //data[data.Count - 1][7] = sqlReader[7].ToString();
                    im1.Add(sqlReader[8].ToString());

                    //dataGridView1.CellDoubleClick += async (object sender_, DataGridViewCellEventArgs e_) =>
                    //{
                    //    MessageBox.Show(data[data.Count - 1][3] = sqlReader[3].ToString(), "Info about etwetw", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //};
                    //listBox1.Items.Add(Convert.ToString(sqlReader["Id"]) + "   " + Convert.ToString(sqlReader["Name_country"]) + "            " + Convert.ToString(sqlReader["Name_vid"]) + "            " + Convert.ToString(sqlReader["Batalon"]) + "            " + Convert.ToString(sqlReader["Rota"]) + "            " + Convert.ToString(sqlReader["automat"]) + "            " + Convert.ToString(sqlReader["pulemet"]) + "            " + Convert.ToString(sqlReader["technika"]));
                }
                foreach (string[] s in data)
                {
                    dataGridView1.Rows.Add(s);
                   
                }

                for (int i = 0; i < im1.Count; i++)
                {
                    string str = im1[i].Replace(@"\", @"\\");
                    Image image = Image.FromFile(str);
                    Image last_image = ScaleImage(image, 270, 190);
                    // Column8.Image = image;
                    dataGridView1.Rows[i].Cells[8].Value = last_image;
                    // dataGridView1.Rws.Add(s);
                }
                for (int i = 0; i < im_oborona.Count; i++)//выводим оборону
                {
                    string str = im_oborona[i].Replace(@"\", @"\\");
                    Image image = Image.FromFile(str);
                    Image last_image = ScaleImage(image, 270, 190);
                    // Column8.Image = image;
                    dataGridView1.Rows[i].Cells[6].Value = last_image;
                    // dataGridView1.Rws.Add(s);
                }
                for (int i = 0; i < im_nastuplenie.Count; i++)
                {
                    string str = im_nastuplenie[i].Replace(@"\", @"\\");
                    Image image = Image.FromFile(str);
                    Image last_image = ScaleImage(image, 258, 190);
                    // Column8.Image = image;
                    dataGridView1.Rows[i].Cells[7].Value = last_image;
                    
                    // dataGridView1.Rws.Add(s);
                }
               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (label7.Visible)
                label7.Visible = false;

            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text) &&
                !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text))
            {
                SqlCommand command = new SqlCommand("INSERT INTO [Army] (Name_country, Name_vid,Rota,Batalon,automat,technika,pulemet,sostav) VALUES (@Name_country, @Name_vid,@Rota,@Batalon,@automat,@technika,@pulemet,@osnova)", sqlConnection);

                command.Parameters.AddWithValue("Name_country", textBox1.Text);

                command.Parameters.AddWithValue("Name_vid", textBox2.Text);

                command.Parameters.AddWithValue("Rota",textBox8.Text);//тяжи

                command.Parameters.AddWithValue("Batalon", textBox9.Text);//лт

                command.Parameters.AddWithValue("automat", textBox10.Text);//авто

                command.Parameters.AddWithValue("technika", путь_до_обороны.Text);//в обороне

                command.Parameters.AddWithValue("pulemet", путь_до_наступления.Text);//в наступлении

                command.Parameters.AddWithValue("osnova", для_пути.Text);//раньше был где котик

                textBox1.Clear();
                textBox2.Clear();
                textBox9.Clear();
                textBox8.Clear();
                textBox10.Clear();
                путь_до_обороны.Clear();
                путь_до_наступления.Clear();
                для_пути.Clear();

                MessageBox.Show("Готово.Обновите БД."," ", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                await command.ExecuteNonQueryAsync();
            }
            else
            {
                label7.Visible = true;

                label7.Text = "Поля  должны быть заполнены!";
            }
        }

        private async void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

            SqlDataReader sqlReader = null;
            

            SqlCommand command = new SqlCommand("SELECT * FROM [Army]", sqlConnection);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                List<string[]> data = new List<string[]>();

                List<string> im = new List<string>();
                List<string> im_oborona = new List<string>();
                List<string> im_nastuplenie = new List<string>();

                while (await sqlReader.ReadAsync())
                {
                    data.Add(new string[8]);
                   

                    data[data.Count - 1][0] = sqlReader[0].ToString();
                    data[data.Count - 1][1] = sqlReader[1].ToString();
                    data[data.Count - 1][2] = sqlReader[2].ToString();
                    data[data.Count - 1][3] = sqlReader[3].ToString();
                    data[data.Count - 1][4] = sqlReader[4].ToString();
                    data[data.Count - 1][5] = sqlReader[5].ToString();

                    im_oborona.Add(sqlReader[6].ToString());
                    im_nastuplenie.Add(sqlReader[7].ToString());
                    im.Add(sqlReader[8].ToString());
                    
                   

                    // listBox1.Items.Add(Convert.ToString(sqlReader["Id"]) + "   " + Convert.ToString(sqlReader["Name_country"]) + "            " + Convert.ToString(sqlReader["Name_vid"]) + "            " + Convert.ToString(sqlReader["Batalon"]) + "            " + Convert.ToString(sqlReader["Rota"]) + "            " + Convert.ToString(sqlReader["automat"]) + "            " + Convert.ToString(sqlReader["pulemet"]) + "            " + Convert.ToString(sqlReader["technika"]));
                }

                dataGridView1.Rows.Clear();


                foreach (string[] s in data)
                {
                    dataGridView1.Rows.Add(s);
     
                }



                for(int i=0;i<im.Count; i++)//выводим котика
                {
                    string str = im[i].Replace(@"\", @"\\");
                    Image image = Image.FromFile(str);
                    Image last_image = ScaleImage(image, 270, 190);
                    // Column8.Image = image;
                    dataGridView1.Rows[i].Cells[8].Value = last_image;
                    // dataGridView1.Rws.Add(s);
                }
                for (int i = 0; i < im_oborona.Count; i++)//выводим оборону
                {
                    string str = im_oborona[i].Replace(@"\", @"\\");
                    Image image = Image.FromFile(str);
                    Image last_image = ScaleImage(image, 270, 190);
                    // Column8.Image = image;
                    dataGridView1.Rows[i].Cells[6].Value = last_image;
                    // dataGridView1.Rws.Add(s);
                }
                for (int i = 0; i < im_nastuplenie.Count; i++)
                {
                    string str = im_nastuplenie[i].Replace(@"\", @"\\");
                    Image image = Image.FromFile(str);
                    Image last_image = ScaleImage(image, 258, 190);
                    // Column8.Image = image;
                    dataGridView1.Rows[i].Cells[7].Value = last_image;
                    // dataGridView1.Rws.Add(s);
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            //if (label8.Visible)
            //    label8.Visible = false;

            if (!string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox3.Text) &&
                !string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text) &&
                !string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox5.Text))
            {
                SqlCommand command = new SqlCommand("UPDATE [Army] SET [Name_country]=@Name_country, [Name_vid]=@Name_vid, [Batalon]=@Batalon, [Rota]=@Rota, [automat]=@automat, [pulemet]=@pulemet,[technika]=@technika WHERE [Id]=@Id", sqlConnection);

                command.Parameters.AddWithValue("Id", textBox5.Text);
                command.Parameters.AddWithValue("Name_country", textBox4.Text);
                command.Parameters.AddWithValue("Name_vid", textBox3.Text);
                command.Parameters.AddWithValue("Batalon", textBox13.Text);//тяжи
                command.Parameters.AddWithValue("Rota", textBox14.Text);//лт
                command.Parameters.AddWithValue("automat", textBox12.Text);//авто
                command.Parameters.AddWithValue("pulemet", textBox15.Text);//оборона
                command.Parameters.AddWithValue("technika", textBox16.Text);//наступление
                //command.Parameters.AddWithValue("sostav", textBox15.Text);

                await command.ExecuteNonQueryAsync();

                textBox5.Clear();
                textBox4.Clear();
                textBox3.Clear();
                textBox14.Clear();
                textBox13.Clear();
                textBox12.Clear();
                textBox16.Clear();
                textBox15.Clear();

            }
 
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            //if (label8.Visible)
            //    label8.Visible = false;

            if (!string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox6.Text))
            {
                SqlCommand command = new SqlCommand("DELETE FROM [Army] WHERE [Id]=@Id", sqlConnection);

                command.Parameters.AddWithValue("Id", textBox6.Text);

                await command.ExecuteNonQueryAsync();
            }
            //else
            //{
            //    label8.Visible = true;

            //    label8.Text = "Id должнен быть заполнен!";
            //}
        }

      

        private void поискToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
             
                Form2 FORM2 = new Form2();
                FORM2.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void инструментыToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void путь_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.ShowDialog();
            string fileName = openFileDialog1.FileName;
            для_пути.Clear();
            для_пути.Text =fileName;
        }

        private void для_пути_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox18_TextChanged(object sender, EventArgs e)//путь до обороны
        {

        }

        private void путь_до_наступления_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog2 = new OpenFileDialog();
            openFileDialog2.ShowDialog();
            string fileName = openFileDialog2.FileName;
            путь_до_обороны.Clear();
            путь_до_обороны.Text = fileName;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog3 = new OpenFileDialog();
            openFileDialog3.ShowDialog();
            string fileName = openFileDialog3.FileName;
            путь_до_наступления.Clear();
            путь_до_наступления.Text = fileName;
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
