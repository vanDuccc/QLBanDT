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
using System.Drawing.Drawing2D;     // thư viện để set màu 

namespace QuanLyBanDienThoai.LoginChange
{
    public partial class FrmLoginChange : Form
    {
        public FrmLoginChange()
        {
            InitializeComponent();
            SetStyle(ControlStyles.ResizeRedraw, true);     // set màu
        }
        // set màu background
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            Rectangle rc = ClientRectangle;
            if (rc.IsEmpty)
                return;
            if (rc.Width == 0 || rc.Height == 0)
                return;
            using (LinearGradientBrush brush = new LinearGradientBrush(rc, Color.White, Color.FromArgb(196, 232, 250), 90F))
            {
                e.Graphics.FillRectangle(brush, rc);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-K95NLUD6\SQLEXPRESS03;Initial Catalog=DangNhap;Integrated Security=True");
        private void btnCapnhat_Click(object sender, EventArgs e)
        {

            /*try
            {
                conn.Open();
                string sql = "select count (*) from dangnhap where TenDN = '"+txtTenDN.Text+"' and MatKhau = '"+txtMkc.Text+"'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dta = cmd.ExecuteReader();
                if(dta.Read() == true)
                {
                    if(txtMkm.Text == txtNlmk.Text)
                    {
                        string sql2 = "update dangnhap set MatKhau = '"+txtMkm.Text+"' where TenDN = '" + txtTenDN.Text + "' and MatKhau = '" + txtMkc.Text + "'";
                        SqlCommand cmd2 = new SqlCommand(sql2, conn);
                        // SqlDataAdapter sda = new SqlDataAdapter(cmd2);
                        // DataTable dt = new DataTable();
                         //sda.Fill(dt);
                        
                        MessageBox.Show("Đổi mật khẩu thành công");
                    }
                    else
                    {
                        MessageBox.Show("Nhập lại mật khẩu chưa đúng!");
                    }
                }
                else
                {
                    MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi kết nối!");
            }
            */

            SqlDataAdapter da = new SqlDataAdapter("select count (*) from dangnhap where TenDN = '" + txtTenDN.Text + "' and MatKhau = '" + txtMkc.Text + "'",conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            errorProvider1.Clear();     //hiện thông báo lỗi ngay bên cạnh
            if(dt.Rows[0][0].ToString() == "1")
            {
                if(txtMkm.Text == txtNlmk.Text)
                {
                    SqlDataAdapter da1 = new SqlDataAdapter("update dangnhap set MatKhau = '" + txtMkm.Text + "' where TenDN = '" + txtTenDN.Text + "' and MatKhau = '" + txtMkc.Text + "' ", conn);
                    DataTable dt1 = new DataTable();    // refresh là thay đổi data trong sql 
                    da1.Fill(dt1);                      // refresh là thay đổi data trong sql 
                    MessageBox.Show("Đổi mật khẩu thành công ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    errorProvider1.SetError(txtMkm, "Bạn chưa điền mật khẩu!");
                    errorProvider1.SetError(txtNlmk, "Mật khẩu nhập lại chưa đúng!");
                }            
            }
            else
            {
                errorProvider1.SetError(txtTenDN, "Tên người dùng chưa đúng!");
                errorProvider1.SetError(txtMkc, "Mật khẩu cũ ko đúng!");
            }

            // Đóng kết nối
            if(conn.State != ConnectionState.Closed)
            {
                conn.Close();
                conn.Dispose();            
            }
        }
    }
}
