using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Графы
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private bool chk(DataGridView dataGridView)
        {

            if (dataGridView.Rows.Count < 2)
                return false;
            for (int i = 0; i < dataGridView.Rows.Count; i++)
                for (int j = 0; j < dataGridView.Rows.Count; j++)
                {
                    if ((dataGridView.Rows[i].Cells[j].Value == null && dataGridView.Rows[j].Cells[i].Value == null) || dataGridView.Rows[i].Cells[j].Value != null && dataGridView.Rows[j].Cells[i].Value != null || (dataGridView.Rows[i].Cells[i].Value != null))
                        continue;
                    else { return false; }




                }
            return true;
        }

        private void save(DataGridView dataGridView)
        {
            saveFileDialog1.Filter = "Text files (*.txt)|*.txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamWriter write = new StreamWriter(saveFileDialog1.FileName);
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView.Columns.Count; j++)
                    {
                        if (dataGridView.Rows[i].Cells[j].Value != null)
                        {
                            write.Write(j + 1 + " ");
                        }
                    }

                    write.Write("0 ");
                }
                write.Close();
            }


        }

        private void cpy(DataGridView dgv, int n)
        {
            int l = dgv.Rows.Count;
            int[,] a = new int[l, l];
            for (int i = 0; i < l; i++)
                for (int j = 0; j < l; j++)
                    if (dgv.Rows[i].Cells[j].Value != null)
                        a[i, j] = 1;
            create(dgv, n);
            for (int i = 0; i < l; i++)
                for (int j = 0; j < l; j++)
                    if (a[i, j] == 1)
                        dgv.Rows[i].Cells[j].Value = 1;
        }
        private void create(DataGridView dataGridView, int n)
        {
            dataGridView.Columns.Clear();
            dataGridView.Rows.Clear();
            dataGridView.Refresh();
            for (int i = 0; i < n; i++)
            {
                dataGridView.Columns.Add(null, (i + 1).ToString());
                dataGridView.Columns[i].Width = 20;
            } if (n > 1)
            {
                dataGridView.Rows.Add(n - 1);
                for (int i = 0; i < n; i++)
                {
                    dataGridView.Rows[i].HeaderCell.Value = (i + 1).ToString();

                }
            }
        }
        private void load(DataGridView dataGridView)
        {

            openFileDialog1.Filter = "Text files (*.txt)|*.txt";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamReader read = new StreamReader(openFileDialog1.FileName);
                    string a = read.ReadToEnd();
                    read.Close();
                    string[] b = a.Split(' ');
                    int n = 0;
                    for (int i = 0; i < b.Length; i++)
                        if (b[i] == "0")
                        {
                            n++;
                        }
                    create(dataGridView, n);


                    int m = 0;
                    for (int i = 0; i < n; )
                    {
                        while (true)
                        {

                            if (int.Parse(b[m]) != 0)
                            {

                                dataGridView.Rows[i].Cells[int.Parse(b[m]) - 1].Value = 1;
                            }
                            else
                            {
                                i++;
                                m++;
                                break;
                            }
                            m++;
                        }
                    }
                    if (!chk(dataGridView))
                        throw new Exception("Граф неверного формата");
                }

                catch (Exception ex)
                {
                    create(dataGridView, 0);
                    MessageBox.Show(ex.Message);

                }
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            load(dataGridView1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            load(dataGridView2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            save(dataGridView1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            save(dataGridView2);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                create(dataGridView1, int.Parse(textBox1.Text));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                create(dataGridView2, int.Parse(textBox2.Text));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '1')
            {
                if (dataGridView1.CurrentCellAddress.X != dataGridView1.CurrentCellAddress.Y)
                {
                    dataGridView1.CurrentCell.Value = 1;
                    dataGridView1.Rows[dataGridView1.CurrentCellAddress.X].Cells[dataGridView1.CurrentCellAddress.Y].Value = 1;
                }
            }
            else
            {
                dataGridView1.CurrentCell.Value = null;
                dataGridView1.Rows[dataGridView1.CurrentCellAddress.X].Cells[dataGridView1.CurrentCellAddress.Y].Value = null;
            }
        }

        private void dataGridView2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '1')
            {
                if (dataGridView1.CurrentCellAddress.X != dataGridView1.CurrentCellAddress.Y)
                {
                    dataGridView2.CurrentCell.Value = 1;
                    dataGridView2.Rows[dataGridView2.CurrentCellAddress.X].Cells[dataGridView2.CurrentCellAddress.Y].Value = 1;
                }

            }
            else
            {
                dataGridView2.CurrentCell.Value = null;
                dataGridView2.Rows[dataGridView2.CurrentCellAddress.X].Cells[dataGridView2.CurrentCellAddress.Y].Value = null;
            }
        }



        private void button7_Click(object sender, EventArgs e)
        {
            int sz = dataGridView1.Rows.Count;
            int[,] l = new int[sz, sz];
            

            int count = 0;

            for (int i = 0; i < sz; i++)
                for (int j = 0; j < sz; j++)
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                    {
                        l[i, j] = 1;
                        count++;
                    }
                    else if (i != j)
                        l[i, j] = 999;
                    else
                    {
                        l[i, j] = 0;
                        
                    }
            
            for (int k = 0; k < sz; k++)
            {
                for (int i = 0; i < sz; i++)
                    for (int j = 0; j < sz; j++)
                    {
                        l[i, j] = Math.Min(l[i, j], l[i, k] + l[k, j]);
                    }
            }
            int[,] resm = new int[count/2, sz];
            MessageBox.Show((count / 2).ToString());
            create(dataGridView2, sz);
            for (int i = 0; i < sz; i++)
            {
                for (int j = 0; j < sz; j++)
                {
                    dataGridView2.Rows[i].Cells[j].Value = l[i, j];
                }
            }
            create(dataGridView3, Math.Max(sz,count/2));
            int f = 0;
          for (int i = 0; i < sz-1; i++)
          {
              for (int j = i; j < sz; j++)
              {
                  if (l[i, j] == 1)
                  {
                      
                      for (int c = 0; c < sz; c++)
                      {

                          if (l[i, c] != 0 && l[j, c] != 0)
                          {
                              dataGridView3.Rows[f].HeaderCell.Value = String.Format("{0}-{1}", i+1, j+1);
                              if (l[i, c] < l[j, c])
                                  resm[f, l[i, c] - 1]++;
                              else
                                  resm[f, l[j, c] - 1]++;
                          }
                      }
                      f++;
                  }
              }
          }
       
          
          for (int i = 0; i < count/2; i++)
          {
              for (int j = 0; j < sz; j++)
              {
                  dataGridView3.Rows[i].Cells[j].Value = resm[i, j];
              }
          }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                string[] a = textBox3.Text.Split(' ');
                int[] b = new int[a.Length];
                int max = 0;
                for (int i = 0; i < a.Length; i++)
                {
                    b[i] = int.Parse(a[i]);
                    if (b[i] > max)
                        max = b[i];
                }
                if (numericUpDown1.Value > max)
                    max = (int)numericUpDown1.Value;
                if (dataGridView1.Rows.Count == 0)
                    create(dataGridView1, max);
                else if (dataGridView1.Rows.Count < max)
                    cpy(dataGridView1, max);
                foreach (int p in b)
                {
                    if ((int)numericUpDown1.Value - 1 != p - 1)
                    {

                        dataGridView1.Rows[(int)numericUpDown1.Value - 1].Cells[p - 1].Value = 1;
                        dataGridView1.Rows[p - 1].Cells[(int)numericUpDown1.Value - 1].Value = 1;
                    }
                    else { MessageBox.Show("Вершина не может быть связана сама с собой"); }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

    }
}
