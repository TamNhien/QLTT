using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLTT
{
    public partial class Form1 : Form
    {
        private readonly SqlConnection con = new SqlConnection(@"Data Source=CANHTHIEN\TAMNHIEN;Initial Catalog=QUANLITHONGTIN;Integrated Security=True;Encrypt=False");

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            maTT.Clear();
            hoTen.Clear();
            namSinh.Clear();
            queQuan.Clear();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ExecuteQuery($"INSERT INTO QUANLITHONGTIN(MATT, HOTEN, NAMSINH, QUEQUAN) VALUES (N'{maTT.Text}', N'{hoTen.Text}', N'{namSinh.Text}', N'{queQuan.Text}')");
            LoadData();
        }

        private void btnFix_Click(object sender, EventArgs e)
        {
            ExecuteQuery($"UPDATE QUANLITHONGTIN SET HOTEN = N'{hoTen.Text}', NAMSINH =  N'{namSinh.Text}', QUEQUAN = N'{queQuan.Text}' WHERE MATT = N'{maTT.Text}'");
            LoadData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ExecuteQuery($"DELETE FROM QUANLITHONGTIN WHERE MATT = N'{maTT.Text}'");
            LoadData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData($"SELECT * FROM QUANLITHONGTIN WHERE MATT = N'{tentimkiem.Text}'");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            maTT.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            hoTen.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            namSinh.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            queQuan.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void LoadData(string query = "SELECT * FROM QUANLITHONGTIN")
        {
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void ExecuteQuery(string query)
        {
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}