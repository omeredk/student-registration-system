using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace student_registration_system
{
    public partial class FrmTeacherDetail : Form
    {
        public FrmTeacherDetail()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-CJTBSKD;Initial Catalog=student-registration-system;Integrated Security=True");

        private void TeacherDetail_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the '_student_registration_systemDataSet.TBLLesson' table. You can move, or remove it, as needed.
            this.tBLLessonTableAdapter.Fill(this._student_registration_systemDataSet.TBLLesson);

            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select Count(Status) From TBLLesson where Status='True' Group By Status", baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblSuccesful.Text = dr[0].ToString();
            }
            dr.Close();

            SqlCommand komut2 = new SqlCommand("Select Count(Status) From TBLLesson where Status='False' Group By Status", baglanti);
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                LblUnsuccesful.Text = dr2[0].ToString();
            }

            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TblLesson (StuNo,StuName,StuSurname) values (@P1, @P2, @P3)", baglanti);
            komut.Parameters.AddWithValue("@P1", MskNumber.Text);
            komut.Parameters.AddWithValue("@P2", TxtName.Text);
            komut.Parameters.AddWithValue("@P3", TxtSurname.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Student added");
            this.tBLLessonTableAdapter.Fill(this._student_registration_systemDataSet.TBLLesson);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            MskNumber.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtName.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            TxtSurname.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            TxtExam1.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            TxtExam2.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            TxtExam3.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            decimal avgExam, e1, e2, e3;
            string durum;
            e1 = Convert.ToDecimal(TxtExam1.Text);
            e2 = Convert.ToDecimal(TxtExam2.Text);
            e3 = Convert.ToDecimal(TxtExam3.Text);

            avgExam = (e1 + e2 + e3) / 3;
            LblAverage.Text = avgExam.ToString();

            if(avgExam >= 50)
            {
                durum = "True";
            }
            else
            {
                durum = "False";
            }

            baglanti.Open();
            SqlCommand komut = new SqlCommand("update TBLLesson set StuExam1 = @P1, StuExam2 = @P2, StuExam3 = @P3, Avg = @P4, Status = @P5 WHERE StuNo=@P6", baglanti);
            komut.Parameters.AddWithValue("@P1", TxtExam1.Text);
            komut.Parameters.AddWithValue("@P2", TxtExam2.Text);
            komut.Parameters.AddWithValue("@P3", TxtExam3.Text);
            komut.Parameters.AddWithValue("@P4", decimal.Parse(LblAverage.Text));
            komut.Parameters.AddWithValue("@P5", durum);
            komut.Parameters.AddWithValue("@P6", MskNumber.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Student notes updated");
            this.tBLLessonTableAdapter.Fill(this._student_registration_systemDataSet.TBLLesson);
        }
    }
}
