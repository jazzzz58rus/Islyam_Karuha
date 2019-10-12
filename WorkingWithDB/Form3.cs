using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkingWithDB
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            
            PictureBox foto = new PictureBox();
            foto.Size = new System.Drawing.Size(500, 290);
            foto.Location = new System.Drawing.Point(20, 20);
            foto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            foto.Image = Image.FromFile("C:\\WorkingWithDB\\LeopardTTX.jpg");
            Controls.Add(foto);

            PictureBox foto1 = new PictureBox();
            foto1.Size = new System.Drawing.Size(800, 300);
            foto1.Location = new System.Drawing.Point(20, 400);
            foto1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            foto1.Image = Image.FromFile("C:\\WorkingWithDB\\Leopard.jpg");
            Controls.Add(foto1);
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

     

    }
}
