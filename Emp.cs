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

namespace PetShop
{
    public partial class Employees : Form
    {
        public Employees()
        {
            InitializeComponent();
            DisplayEmployee();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {
           

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
           
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Customers CusObj = new Customers();
            CusObj.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {


        }

        private void label1_Click(object sender, EventArgs e)
        {
            Products ProObj = new Products();
            ProObj.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void EmpNameTb_TextChanged(object sender, EventArgs e)
        {

        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-1D77MCM;Initial Catalog=PetShopDb;Persist Security Info=True;User ID=sa;Password=123");
        private void DisplayEmployee()
        {
            try
            {
                conn.Open();
                string sql = "select * from EmployeeTbl";
                SqlDataAdapter Sda = new SqlDataAdapter(sql, conn);
                SqlCommandBuilder CmdBuilder = new SqlCommandBuilder(Sda);
                var Ds = new DataSet();
                Sda.Fill(Ds);
                EmpGYV.DataSource = Ds.Tables[0];
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void ClearTb()
        {
            EmpNameTb.Text = ""; 
            EmpAddTb.Text = ""; 
            EmpPhoneTb.Text = "";
            EmpPasswordTb.Text = "";
        }
        private void EmpSaveBtn_Click(object sender, EventArgs e)
        {
            if (EmpNameTb.Text == "" || EmpAddTb.Text == "" || EmpPhoneTb.Text == "" || EmpDOBTb.Text == "" || EmpPasswordTb.Text == "" )
            {
                MessageBox.Show("Misssing Information");
            }
            else
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("insert into EmployeeTbl(EmpName,EmpAddress,EmpDOB,EmpPhone,EmpPass) values(@EN,@EA,@ED,@EP,@EPa)",conn);
                    cmd.Parameters.AddWithValue("@EN", EmpNameTb.Text);
                    cmd.Parameters.AddWithValue("@EA", EmpAddTb.Text);
                    cmd.Parameters.AddWithValue("@ED", EmpDOBTb.Value.Date);
                    cmd.Parameters.AddWithValue("@EP", EmpPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@EPa", EmpPasswordTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Empolyee Added");
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
                finally
                {
                    conn.Close();
                    DisplayEmployee();
                   
                    ClearTb();
                }

            }

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }


        int key = 0;
        private void EmpGYV_CellContentClick(object sender, DataGridViewCellEventArgs e)
            
        {
            EmpNameTb.Text = EmpGYV.SelectedRows[0].Cells[1].Value.ToString();
            EmpPhoneTb.Text = EmpGYV.SelectedRows[0].Cells[2].Value.ToString();
            EmpAddTb.Text = EmpGYV.SelectedRows[0].Cells[3].Value.ToString();
            EmpPasswordTb.Text = EmpGYV.SelectedRows[0].Cells[4].Value.ToString();
            EmpDOBTb.Text = EmpGYV.SelectedRows[0].Cells[5].Value.ToString();
            

            if (EmpNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(EmpGYV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void EmpEditBtn_Click(object sender, EventArgs e)
        {
            if (EmpNameTb.Text == "" || EmpAddTb.Text == "" || EmpPhoneTb.Text == "" || EmpDOBTb.Text == "" || EmpPasswordTb.Text == "")
            {
                MessageBox.Show("Misssing Information");
            }
            else
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("update EmployeeTbl set (EmpName=@EN,EmpAddress=@EA,EmpDOB=@ED,EmpPhone=@EP,EmpPass=@EPa where EmpNum=@Ekey", conn);
                    cmd.Parameters.AddWithValue("@EN", EmpNameTb.Text);
                    cmd.Parameters.AddWithValue("@EA", EmpAddTb.Text);
                    cmd.Parameters.AddWithValue("@ED", EmpDOBTb.Value.Date);
                    cmd.Parameters.AddWithValue("@EP", EmpPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@EPa", EmpPasswordTb.Text);
                    cmd.Parameters.AddWithValue("@Ekey",key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Empolyee Update!!!");
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
                finally
                {
                    conn.Close();
                    DisplayEmployee();
                    ClearTb();
                }

            }
        }

        private void EmpPhoneTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void EmpDeleteBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select An Employee");
            }
            else
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("delete from EmployeeTbl where EmpNum = @Empkey", conn);
                    cmd.Parameters.AddWithValue("@Empkey", key);
                   
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Empolyee Deleted!!!");
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
                finally
                {
                    conn.Close();
                    DisplayEmployee();
                    ClearTb();                                                                                                                                                                                                                                  
                }

            }
        }
    }
}
