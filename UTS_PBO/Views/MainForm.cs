using System.Data;
using winform_mvc.App.Context;
using winform_mvc.App.Models;
using winform_mvc.Views;

namespace winform_mvc
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.Load += MainForm_Load;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddTugasForm addMahasiswaForm = new AddTugasForm();

            if (addMahasiswaForm.ShowDialog() == DialogResult.OK)
            {
                LoadDataMahasiswa();
            }

            this.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadDataMahasiswa();
        }

        private void LoadDataMahasiswa()
        {
            try
            {
                dataGridView1.AllowUserToAddRows = false;

                DataTable mahasiswaData = TugasContext.All();
                if (mahasiswaData == null)
                {
                    MessageBox.Show("Error: Gagal mengambil data mahasiswa");
                    return;
                }

                dataGridView1.Columns.Clear();

                DataGridViewTextBoxColumn nomorColumn = new DataGridViewTextBoxColumn();
                nomorColumn.HeaderText = "No";
                nomorColumn.Name = "nomor";
                dataGridView1.Columns.Add(nomorColumn);

                dataGridView1.DataSource = mahasiswaData;

                if (dataGridView1.Columns["id"] != null) dataGridView1.Columns["id"].Visible = false;
                if (dataGridView1.Columns["nama"] != null)
                    dataGridView1.Columns["nama"].HeaderText = "Nama";
                if (dataGridView1.Columns["nim"] != null)
                    dataGridView1.Columns["nim"].HeaderText = "NIM";
                if (dataGridView1.Columns["email"] != null)
                    dataGridView1.Columns["email"].HeaderText = "Email";
                if (dataGridView1.Columns["semester"] != null)
                    dataGridView1.Columns["semester"].HeaderText = "Semester";
                if (dataGridView1.Columns["nama_prodi"] != null)
                    dataGridView1.Columns["nama_prodi"].HeaderText = "Program Studi";
                if (dataGridView1.Columns["id_prodi"] != null)
                    dataGridView1.Columns["id_prodi"].Visible = false;

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Cells["nomor"].Value = (i + 1).ToString();
                }


                DataGridViewButtonColumn updateButtonColumn = new DataGridViewButtonColumn
                {
                    Name = "Update",
                    HeaderText = "Update",
                    Text = "Edit",
                    UseColumnTextForButtonValue = true
                };
                dataGridView1.Columns.Add(updateButtonColumn);

                DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn
                {
                    Name = "Delete",
                    HeaderText = "Delete",
                    Text = "Delete",
                    UseColumnTextForButtonValue = true
                };
                dataGridView1.Columns.Add(deleteButtonColumn);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error dalam LoadDataMahasiswa: {ex.Message}\n{ex.StackTrace}");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == dataGridView1.Columns["Update"].Index)
            {
                try
                {
                    int mahasiswaId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value);

                    DataTable mahasiswaData = TugasContext.getMahasiswaById(mahasiswaId);

                    if (mahasiswaData.Rows.Count > 0)
                    {
                        DataRow row = mahasiswaData.Rows[0];
                        M_Tugas mahasiswa = new M_Tugas
                        {
                            id = (int)row["id"],
                            nama = row["nama"].ToString(),
                            nim = row["nim"].ToString(),
                            email = row["email"].ToString(),
                            semester = (int)row["semester"],
                            id_prodi = (int)row["id_prodi"]
                        };

                        this.Hide();
                        AddTugasForm addMahasiswaForm = new AddTugasForm();
                        addMahasiswaForm.PopulateForm(mahasiswa);
                        if (addMahasiswaForm.ShowDialog() == DialogResult.OK)
                        {
                            LoadDataMahasiswa();
                        }
                        this.Show();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else if (e.ColumnIndex == dataGridView1.Columns["Delete"].Index)
            {
                int mahasiswaId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value);
                TugasContext.DeleteMahasiswa(mahasiswaId);
                LoadDataMahasiswa();
            }
        }
    }
}
