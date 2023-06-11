using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PWdB_lab8
{
    public partial class Form2 : Form
    {
        private List<Tuple<int, int>> selectedCells;
        private readonly Form1 form1;
        private bool gameOver;
        private int timeLeft, x, y;

        public Form2(Form1 form1)
        {
            InitializeComponent();
            this.form1 = form1;
            y = int.Parse(this.form1.textBox2.Text) * 2;
            dataGridView1.ReadOnly = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(gameOver)
            {
                return;
            }

            if (y == 0)
            {
                timer1.Stop();
                MessageBox.Show("Wygrana! Super:)");
                gameOver = true;
                return;
            }

            DataGridView dgv = (DataGridView)sender;
            if (e.RowIndex >= 0 && e.RowIndex < dgv.RowCount && e.ColumnIndex >= 0 && e.ColumnIndex < dgv.ColumnCount)
            {
                Tuple<int, int> cell = Tuple.Create(e.RowIndex, e.ColumnIndex);
                if (selectedCells.Contains(cell))
                {
                    dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Red;
                    dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionBackColor = Color.Red;
                    y--;
                }
                else
                {
                    dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Green;
                    dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionBackColor = Color.Green;
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timeLeft--;
            if (timeLeft == 0)
            {
                timer1.Stop();
                MessageBox.Show("Przegrana! :(");
                gameOver = true;
                return;
            }

            if (y == 0)
            {
                timer1.Stop();
                MessageBox.Show("Wygrana! Super:)");
                gameOver = true;
                return;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Random random = new Random();

            timeLeft = int.Parse(form1.textBox2.Text) * 3;
            timer1.Interval = 1000;
            timer1.Tick += Timer_Tick;
            timer1.Start();
            gameOver = false;

            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ColumnHeadersVisible = false;

            if(int.TryParse(form1.textBox1.Text, out x))
            {
                dataGridView1.RowCount = x;
                dataGridView1.ColumnCount = x;

                for (int i = 0; i < x; i++)
                {
                    dataGridView1.Columns[i].Width = 50;
                    dataGridView1.Rows[i].Height = 50;
                }
                dataGridView1.Size = new Size(50 * x + 5, 50 * x + 5);
                dataGridView1.Location = new Point(10, 10);

            }

            selectedCells = new List<Tuple<int, int>>();

            int cellsToSelect = int.Parse(form1.textBox2.Text);
            while(selectedCells.Count < cellsToSelect)
            {
                int row = random.Next(x);
                int column = random.Next(x);
                if (!selectedCells.Contains(Tuple.Create(row, column)))
                {
                    selectedCells.Add(Tuple.Create(row, column));
                }
            }

            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.ClearSelection();
            this.Controls.Add(dataGridView1);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
