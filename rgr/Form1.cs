using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TPoint;
using _storage;

namespace rgr
{
   
    public partial class Form1 : Form
    {
        int n = 1;
        MyStorage storage = new MyStorage(0);
        MyStorage storage2 = new MyStorage(0);
        int versh = -1;
        int toversh;
        int x, y;
        int k, l;
        public Form1()
        {
            InitializeComponent();
            dataGridView1.AllowUserToAddRows = false;
        }
        bool mas(bool[] a)
        {
            for (int i = 0; i < a.Length; i++)
                if (!a[i])
                    return false;
            return true;
        }
        int[] Deikstra(int[,] matrix, int st, int N)

        {
            bool[] a = new bool[N];
            int[] b = new int[N];
            int[] c = new int[N];
            for (int i = 0; i < N; i++)
            {
                a[i] = false;
                c[i] = st;
                b[i] = matrix[st, i];
            }
            a[st] = true;
            c[st] = 0;
            int j = st;
            while (!mas(a))
            {
                int min = 1000000;
                for (int i = 0; i < N; i++)
                {
                    if ((!a[i]) && ((b[i] < min) || (b[i] == min)))
                    {
                        min = b[i];
                        j = i;
                    }
                }

                for (int i = 0; i < N; i++)
                {
                    if (!a[i] && (b[i] > min + matrix[i, j]))
                    {
                        b[i] = min + matrix[i, j];
                        c[i] = j;
                    }
                }
                a[j] = true;
            }
            return b;
        }
        private void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (radioButton1.Checked)
            {
                Pen q = new Pen(Color.Black);
                CCircle RR = new CCircle(e.X, e.Y, 10, q);
                storage.Add(RR);
                Label lb = new Label();
                lb.Location = new System.Drawing.Point(e.X-18, e.Y-18);
                this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                lb.BackColor = Color.Transparent;
                lb.Name = "label1";
                lb.Size = new System.Drawing.Size(10, 12);
                lb.Text = n.ToString();
                panel1.Controls.Add(lb);
                dataGridView1.RowCount = n + 1;
                dataGridView1.ColumnCount = n + 1;
                dataGridView1[0, 0].Value = 0;
                for (int i = 1; i < dataGridView1.Rows.Count; ++i)
                    dataGridView1[0, i].Value = i;
                for (int i = 1; i < dataGridView1.Rows.Count; ++i)
                    dataGridView1[i, 0].Value = i;
                n++;
                panel1.Invalidate();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < storage.getCount(); i++)
            {
                storage.GetObject(i).draw(e.Graphics);
            }
            for (int i = 0; i < storage2.getCount(); i++)
            {
                storage2.GetObject(i).draw(e.Graphics);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = string.Empty;
            int start;
            int Npf;
            int[] b = new int[dataGridView1.RowCount - 1];
            if (int.TryParse(textBox2.Text, out start) && (start > 0) && (start < dataGridView1.RowCount)&& int.TryParse(textBox3.Text, out Npf))
            {
                int N = dataGridView1.RowCount - 1;
                int[,] matrix = new int[N, N];
                for (int i = 0; i < N; i++)
                    for (int j = 0; j < N; j++)
                    {
                        if (this.dataGridView1.Rows[i + 1].Cells[j + 1].Value == null)
                            matrix[i, j] = 1000000;
                        else
                            matrix[i, j] = Convert.ToInt32(dataGridView1.Rows[i + 1].Cells[j + 1].Value);
                    }

               b= Deikstra(matrix, start - 1, N);
                for (int i = 0; i < b.Length; i++)
                {
                    if (b[i] > Npf && (b[i]!= 1000000))
                    {
                        richTextBox1.Text += (i + 1).ToString()+" ";
                    }
                }
                dataGridView1.Controls.Clear();
                dataGridView1.RowCount = N + 1;
                dataGridView1.ColumnCount = 2;
                dataGridView1[0, 0].Value = 0;
                for (int i = 1; i < dataGridView1.Rows.Count; ++i)
                    dataGridView1[0, i].Value = i;
                    dataGridView1[1, 0].Value = start;
                for (int i = 0; i < b.Length; i++)
                {
                    dataGridView1[1, i+1].Value = b[i];
                }

            }
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (radioButton2.Checked)
            {
                if (versh == -1)
                {
                    for (int i = 0; i < storage.getCount(); i++)
                    {
                        if (storage.GetObject(i).finder(e.X, e.Y))

                        {
                            versh = i;
                            break;
                        }
                        panel1.Invalidate();
                    }
                }
                else
                {
                    toversh = -1;
                    for (int i = 0; i < storage.getCount(); i++)
                    {
                        if (storage.GetObject(i).finder(e.X, e.Y))

                        { 
                            toversh = i;
                            break;
                        }
                       
                    }

                    if ((toversh != -1) && (versh != toversh))
                    {
                        Graphics g = panel1.CreateGraphics(); ;
                        Pen pen1 = new Pen(Color.Red);
                        float x1, x2, y1, y2;
                        x1 = storage.GetObject(versh).x;
                        y1 = storage.GetObject(versh).y;
                        x2 = storage.GetObject(toversh).x;
                        y2 = storage.GetObject(toversh).y;
                        x = Convert.ToInt32(x1+(x2 - x1)/2);
                        y = Convert.ToInt32(y1+(y2 - y1) / 2);
                        Section line = new Section(x1, y1, x2, y2,pen1);
                        storage2.Add(line);
                        label1.Visible = true;
                        textBox1.Visible = true;
                        button1.Visible = true;
                        MessageBox.Show("Введите расстояние");
                        k = versh;
                        l = toversh;
                    }
                    panel1.Invalidate();
                    versh = -1;
                }
        }
    }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text != "") || (textBox1.Text != " "))
            {
                Label lb = new Label();
                lb.Location = new System.Drawing.Point(x,y);
                this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                lb.BackColor = Color.Transparent;
                lb.Name = "label3";
                lb.Size = new System.Drawing.Size(30, 12);
                lb.Text = textBox1.Text;
                dataGridView1[k+1, l+1].Value = textBox1.Text;
                dataGridView1[l + 1, k + 1].Value = textBox1.Text;
                panel1.Controls.Add(lb);
                label1.Visible = false;
                textBox1.Visible = false;
                button1.Visible = false;
                textBox1.Text = "";
            }
        }
    }
}
