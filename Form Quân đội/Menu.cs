using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Form_Quân_đội
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        public static DataTable table = new DataTable();

        int rowIndex;

        private void Form1_Load(object sender, EventArgs e)
        {
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Date of birth", typeof(DateTime));
            table.Columns.Add("Rank", typeof(string));
            table.Columns.Add("Unit", typeof(string));
            table.Columns.Add("Score", typeof(int));
            table.Columns.Add("Grade", typeof(string));

            dataGridView1.DataSource = table;

            string[] lines = File.ReadAllLines("soldier.txt");
            string[] words;
            for (int i = 0; i < lines.Length; i++)
            {
                words = lines[i].ToString().Split(',');
                table.Rows.Add(words);
            }
        }
        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                MessageBox.Show(dataGridView1.SelectedColumns.ToString());
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            rowIndex = e.RowIndex;
            if (rowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[rowIndex];
                string id = selectedRow.Cells[0].Value.ToString();
                string name = selectedRow.Cells[1].Value.ToString();
                string dOB = selectedRow.Cells[2].Value.ToString();
                string rank = selectedRow.Cells[3].Value.ToString();
                string unit = selectedRow.Cells[4].Value.ToString();
                int score = Convert.ToInt32(selectedRow.Cells[5].Value.ToString());
                
                string gradeValue;
                if (score >= 0 && score <= 100)
                {
                    if (score >= 90 && score <= 100)
                        gradeValue = "S";
                    else if (score >= 70 && score < 90)
                        gradeValue = "A";
                    else if (score >= 60 && score < 70)
                        gradeValue = "B";
                    else if (score >= 50 && score < 60)
                        gradeValue = "C";
                    else
                        gradeValue = "D";
                }
                else
                {
                    gradeValue = "Exceed Score";
                }
                selectedRow.Cells[6].Value = gradeValue.ToString();

                textBox_id.Text = id;
                textBox_id.ReadOnly = true;
                textBox_name.Text = name;
                textBox_dob.Text = dOB;
                textBox_rank.Text = rank;
                textBox_unit.Text = unit;
                textBox_score.Text = score.ToString();
                textBox_grade.Text = selectedRow.Cells[6].Value.ToString();
                textBox_grade.ReadOnly = true;
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            table.Rows.Add(Convert.ToInt32(textBox_id_add.Text), 
                           textBox_name_add.Text, 
                           textBox_dob_add.Text,
                           textBox_rank_add.Text,
                           textBox_unit_add.Text,
                           textBox_score_add.Text
                          );
            dataGridView1.DataSource = table;
            MessageBox.Show("Staff added.");
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Update row index: {rowIndex}");

            table.Rows[rowIndex].SetField(1, textBox_name.Text);
            table.Rows[rowIndex].SetField(2, textBox_dob.Text);
            table.Rows[rowIndex].SetField(3, textBox_rank.Text);
            table.Rows[rowIndex].SetField(4, textBox_unit.Text);
            table.Rows[rowIndex].SetField(5, textBox_score.Text);
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Deleted row index: {rowIndex}");
            table.Rows[rowIndex].Delete();
        }

        private void btn_write_Click(object sender, EventArgs e)
        {
            TextWriter writer = new StreamWriter("soldier2.txt");
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    writer.Write("\t" + dataGridView1.Rows[i].Cells[j].Value.ToString() + "\t" + "|");
                }
                writer.WriteLine("");
                writer.WriteLine("-------------------------------------------------------------------------------------------------------------------");
            }
            writer.Close();
            MessageBox.Show("Data Written.");
        }
    }
}
