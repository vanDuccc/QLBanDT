using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanDienThoai.Employee
{
    public partial class FrmEmployee : Form
    {
        Module.DataAcess dtBase = new Module.DataAcess();
        public FrmEmployee()
        {
            InitializeComponent();
        }
         void HienChiTiet(bool hien)
        {
            txtManhanvien.Enabled = hien;
            txtDienthoai.Enabled = hien;
            txtTennhanvien.Enabled = hien;
            txtDiachi.Enabled = hien;
            cbGioitinh.Enabled = hien;

            btnHuy.Enabled = hien;
            btnLuu.Enabled = hien;
        }

        // Load form
        private void FrmEmployee_Load(object sender, EventArgs e)
        {
            loadData();

            // ẩn sửa, xóa
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            //ẩn groupbox chi  tiết
            HienChiTiet(false);
        }
        void loadData()
        {
            dgvNhanvien.DataSource = dtBase.DataSelect("select * from NHANVIEN");
        }
        void resetData()
        {
            txtManhanvien.Text = "";
            txtTennhanvien.Text = "";
            txtDiachi.Text = "";
            txtDienthoai.Text = "";
            txtManhanvien.Focus();
        }
        //cell click
        private void dgvNhanvien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Hien thi nut sua
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnThem.Enabled = false;
            HienChiTiet(false);

            txtManhanvien.Text = dgvNhanvien.CurrentRow.Cells[0].Value.ToString();
            txtDienthoai.Text = dgvNhanvien.CurrentRow.Cells[1].Value.ToString();
            txtDiachi.Text = dgvNhanvien.CurrentRow.Cells[2].Value.ToString();
            txtTennhanvien.Text = dgvNhanvien.CurrentRow.Cells[3].Value.ToString();
            cbGioitinh.Text = dgvNhanvien.CurrentRow.Cells[4].Value.ToString();
        }

        //---THÊM
        private void XoaTrangChiTiet()
        {
            txtManhanvien.Text = "";
            txtTennhanvien.Text = "";
            txtDiachi.Text = "";
            txtDienthoai.Text = "";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            //Cập nhật tiêu đề
            lblTieude.Text = "THÊM NHÂN VIÊN";
            XoaTrangChiTiet();
            // Cấm sửa, xóa
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            //Hiện groupbox
            HienChiTiet(true);
        }

        //Xóa
        private void btnXoa_Click(object sender, EventArgs e)
        {
            //Bật Message Box cảnh báo người sử dụng
            if (MessageBox.Show("Bạn  có  chắc  chắn  xóa  mã  mặt  hàng  " + txtManhanvien.Text + " không ? Nếu  có  ấn  nút  Lưu, không  thì  ấn  nút  Hủy", "Xóa  sản  phẩm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                lblTieude.Text = "XÓA NHÂN VIÊN";
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                //Hiện gropbox chi tiết
                HienChiTiet(true);
            }
        }

        //Sửa
        private void btnSua_Click(object sender, EventArgs e)
        {
            //Câp nhật tiêu đề
            lblTieude.Text = "CẬP NHẬT NHÂN VIÊN";
            //Ản thêm, xóa
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            //Hiện groupbox
            HienChiTiet(true);
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            //Kiểm tra khi chưa điền thông tin
            //Tên 
            if(txtTennhanvien.Text.Trim() == "")
            {
                errChitiet.SetError(txtTennhanvien, "Không thể để trống tên nhân viên!");
                return;
            }
            else
            {
                errChitiet.Clear();
            }
            // Địa chỉ
            if (txtDiachi.Text.Trim() == "")
            {
                errChitiet.SetError(txtDiachi, "Không thể để trống địa chỉ nhân viên!");
                return;
            }
            else
            {
                errChitiet.Clear();
            }
            //điện thoại
            if (txtDienthoai.Text.Trim() == "")
            {
                errChitiet.SetError(txtDienthoai, "Không thể để trống số điện thoại nhân viên!");
                return;
            }
            else
            {
                errChitiet.Clear();
            }

            //Thêm
            if (btnThem.Enabled == true)
            {
                if(txtManhanvien.Text.Trim() == "")
                {
                    errChitiet.SetError(txtManhanvien, "Không thể bỏ trống mã nhân viên!");
                    return;
                }
                else
                {
                    DataTable dtNhanvien = dtBase.DataSelect("select * from NHANVIEN where MANV = '" + txtManhanvien.Text + "'");
                    if (dtNhanvien.Rows.Count > 0)
                    {
                        errChitiet.SetError(txtManhanvien, "Mã nhân viên bị trùng");
                        return;
                    }
                    errChitiet.Clear();                   
                }
                string sqlInsert = " insert into NHANVIEN values('" + txtManhanvien.Text + "', '" + txtDienthoai.Text + "', N'" + txtDiachi.Text + "', N'" + txtTennhanvien.Text + "', N'" + cbGioitinh.Text + "')";
                dtBase.DataUpdate(sqlInsert);
                loadData();
                MessageBox.Show("Đã thêm dữ liệu thành công");
            }

            //Sửa
            if(btnSua.Enabled == true)
            {
                string sqlInsert = " update NHANVIEN set HOTENNV = N'" + txtTennhanvien.Text + "', DIACHINV = N'" + txtDiachi.Text + "', SDTNV = '" + txtDienthoai.Text + "', GIOITINH = '" + cbGioitinh.Text + "' where MANV = '" + txtManhanvien.Text + "'";
                dtBase.DataUpdate(sqlInsert);
                loadData();
                MessageBox.Show("Bạn đã sửa thành công");
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
            }

            //Xóa
            if(btnHuy.Enabled == true)
            {
              string sqlInsert = " delete from NHANVIEN where MANV = '" + txtManhanvien.Text + "'";
               dtBase.DataUpdate(sqlInsert);
               loadData();
               MessageBox.Show("Bạn đã xóa thành công");
               btnSua.Enabled = false;
               btnXoa.Enabled = false;
            }


        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            //thiết lập như ban đầu
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnThem.Enabled = true;
            // Xóa trang chi tiết
            XoaTrangChiTiet();
            // Hiện groupbox
            HienChiTiet(false);
        }
    }
}
