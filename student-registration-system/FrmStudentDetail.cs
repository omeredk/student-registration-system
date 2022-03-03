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
    public partial class FrmStudentDetail : Form
    {
        public FrmStudentDetail()
        {
            InitializeComponent();
        }

        public string number;

        SqlConnection baglanti= new SqlConnection(@"Data Source=DESKTOP-CJTBSKD;Initial Catalog=student-registration-system;Integrated Security=True");
        //Data Source=DESKTOP-CJTBSKD;Initial Catalog=student-registration-system;Integrated Security=True

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FrmStudentDetail_Load(object sender, EventArgs e)
        {
            LblStuNo.Text = number;

            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From TBLLesson where StuNo=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", number);
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                LblNameSurname.Text=dr[2].ToString()+ " "+dr[3].ToString();
                LblNote1.Text = dr[4].ToString();
                LblNote2.Text = dr[5].ToString();
                LblNote3.Text = dr[6].ToString();
                LblAvgNote.Text = dr[7].ToString();

                string statusStudent = "";
                if(dr[8].ToString() == "True")
                {
                    statusStudent = "SUCCES";
                }

                else
                {
                    statusStudent = "FAIL";
                }
                LblStuStatus.Text = statusStudent;
            }
            baglanti.Close();
        }

        private void LblStuStatus_Click(object sender, EventArgs e)
        {
        }

        private void LblAvgNote_Click(object sender, EventArgs e)
        {

        }
    }
}
