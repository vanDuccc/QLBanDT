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
using System.Data;
using System.Drawing.Drawing2D;     // set màu

namespace QuanLyBanDienThoai
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        //set màu
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

            private void btnDangnhap_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-K95NLUD6\SQLEXPRESS03;Initial Catalog=DangNhap;Integrated Security=True");
            try
            {
                conn.Open();
                string dn = txtTdn.Text;
                string mk = txtMk.Text;
                string sql = "select *from dangnhap where TenDN = '" +dn+ "' and MatKhau = '" +mk+ "'";
                SqlCommand cmd = new SqlCommand(sql, conn);

                //  Sửa 
                cmd.Parameters.Add(new SqlParameter("TenDN", dn));
                cmd.Parameters.Add(new SqlParameter("MatKhau", mk));
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    SqlDataReader dta = cmd.ExecuteReader();
                    if(dta.Read() == true)
                    {
                        MessageBox.Show("Đăng nhập thàng công");
                    }
                    else
                    {
                        MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!");
                    }
                }

                /* SqlDataReader dta = cmd.ExecuteReader();
                 if(dta.Read() == true)
                 {
                     MessageBox.Show("Đăng nhập thàng công");
                 }
                 else
                 {
                     MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!");
                 }*/

                // Đóng kết nối
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                    conn.Dispose();
                    cmd.Dispose();                    
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Sai tài khoản, mật khẩu!");
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {        
            if (MessageBox.Show("Bạn chắc chắn muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
