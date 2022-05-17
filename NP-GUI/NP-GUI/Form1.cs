using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Timers;

namespace NP_GUI
{
    public partial class Form1 : Form
    {
        Point ZeroPos, spare;
        List<int> zerosX, zerosY;
        string start;
        public Form1() { InitializeComponent(); }
        public Form1(String start, List<int> zerosX, List<int> zerosY)
        {
            InitializeComponent();
            ZeroPos.X = 180;
            ZeroPos.Y = 180;
            this.zerosX = zerosY;
            this.zerosY = zerosX;
            this.start = start;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (Button node in panel1.Controls)
                node.Enabled = true;
            if (start[8] != '0')
            {
                Button[] btns = {button4,button5,button6,button7
                    ,button8,button9,button10,button11};
                List<Point> locs = new List<Point>();
                Point p = new Point();
                p.X = 0;
                p.Y = 0;
                locs.Add(p);
                p.X = 90;
                p.Y = 0;
                locs.Add(p);
                p.X = 180;
                p.Y = 0;
                locs.Add(p);
                p.X = 0;
                p.Y = 90;
                locs.Add(p);
                p.X = 90;
                p.Y = 90;
                locs.Add(p);
                p.X = 180;
                p.Y = 90;
                locs.Add(p);
                p.X = 0;
                p.Y = 180;
                locs.Add(p);
                p.X = 90;
                p.Y = 180;
                locs.Add(p);
                for (int i = 0; i < btns.Length; i++)
                    btns[i].Location = locs[i];
                int zeroIndex = start.IndexOf('0');
                Point swap = btns[zeroIndex].Location;
                btns[zeroIndex].Location = ZeroPos;
                ZeroPos = swap;
                btns[zeroIndex].Text = Char.ToString(start[8]);
                for (int i = 0; i < start.Length - 1; ++i)
                {
                    if (start[i] == '0') continue;
                    btns[i].Text = Char.ToString(start[i]);
                }
            }
            else
            {
                button4.Text = Char.ToString(start[0]);
                button5.Text = Char.ToString(start[1]);
                button6.Text = Char.ToString(start[2]);
                button7.Text = Char.ToString(start[3]);
                button8.Text = Char.ToString(start[4]);
                button9.Text = Char.ToString(start[5]);
                button10.Text = Char.ToString(start[6]);
                button11.Text = Char.ToString(start[7]);
            }
        }


        private async void Solvebutton_Click(object sender, EventArgs e)
        {
            Button[] btns = {button4,button5,button6,button7
                    ,button8,button9,button10,button11};
            string s = "Step ";
            this.progressBar1.Minimum = 0;
            this.progressBar1.Maximum = zerosX.Count;
            this.progressBar1.Increment(1);
            for (int i = 1; i < zerosX.Count; ++i)
            {
                textBox1.Text = s + i.ToString();
                this.progressBar1.Increment(1);
                spare.X = zerosX[i] * 90;
                spare.Y = zerosY[i] * 90;
                foreach (Button btn in btns)
                {
                    if (btn.Location == spare)
                    {
                        Point swap = btn.Location;
                        btn.Location = ZeroPos;
                        ZeroPos = swap;
                    }
                }
                await Task.Delay(300);
            }
            MessageBox.Show("SOLVED!!");
            this.Close();
        }
    }
}
