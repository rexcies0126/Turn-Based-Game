using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TurnBaseGame
{
    public partial class Form1 : Form
    {

        int speed = 2;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Up:
                case Keys.W:
                    panel1.Top -= speed;
                    break;

                case Keys.Down:
                case Keys.S:
                    panel1.Top += speed;
                    break;

                case Keys.Left:
                case Keys.A:
                    panel1.Left -= speed;
                    break;

                case Keys.Right:
                case Keys.D:
                
                    panel1.Left += speed;
                    break;
            }

            Colllision();
        }

        private void Colllision()
        {
            if (panel1.Bounds.IntersectsWith(panel2.Bounds))
            {
                Form2 form =  new Form2();
                form.Show();
                this.Hide();

            }
        }
    }
}
