using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanDienThoai.Employee2
{
    public partial class FrmEmployee2 : Form
    {
        // dataAcess
        Module.DataAcess dtBase = new Module.DataAcess();

        public FrmEmployee2()
        {
            InitializeComponent();
        }

        // Hiện chi tiết
        void HienChiTiet(bool hien)
        {
            txtManhanvien.Enabled = hien;
            txtDienthoai.Enabled = hien;
            txtTennhanvien.Enabled = hien;
            txtDiachi.Enabled = hien;
            cbGioitinh.Enabled = hien;
            txtChucvu.Enabled = hien;
            txtluong.Enabled = hien;
            txtHesoluon.Enabled = hien;

            btnHuy.Enabled = hien;
            btnLuu.Enabled = hien;
        }

        // Load data
        void loadData()
        {
            dgvNhanvien.DataSource = dtBase.DataSelect("select NHANVIEN.MANV, SDTNV, DIACHINV, HOTENNV, GIOITINH, CHUCVU, HESOLUONG, LUONG from NHANVIEN join CHITIETNV on NHANVIEN.MANV = CHITIETNV.MANV");
        }

        // Load form
        private void FrmEmployee2_Load(object sender, EventArgs e)
        {
            loadData();

            // ẩn sửa, xóa
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            //ẩn groupbox chi  tiết
            HienChiTiet(false);
        }

        // Cell click
        private void dgvNhanvien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Hien thi nut sua
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnThem.Enabled = false;
            //HienChiTiet(false);

            txtManhanvien.Text = dgvNhanvien.CurrentRow.Cells[0].Value.ToString();
            txtDienthoai.Text = dgvNhanvien.CurrentRow.Cells[1].Value.ToString();
            txtDiachi.Text = dgvNhanvien.CurrentRow.Cells[2].Value.ToString();
            txtTennhanvien.Text = dgvNhanvien.CurrentRow.Cells[3].Value.ToString();
            cbGioitinh.Text = dgvNhanvien.CurrentRow.Cells[4].Value.ToString();
            txtChucvu.Text = dgvNhanvien.CurrentRow.Cells[5].Value.ToString();
            txtHesoluon.Text = dgvNhanvien.CurrentRow.Cells[6].Value.ToString();
            txtluong.Text = dgvNhanvien.CurrentRow.Cells[7].Value.ToString();
        }

        // Thêm
        private void XoaTrangChiTiet()
        {
            txtManhanvien.Text = "";
            txtTennhanvien.Text = "";
            txtDiachi.Text = "";
            txtDienthoai.Text = "";
            txtChucvu.Text = "";
            txtHesoluon.Text = "";
            txtluong.Text = "";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            XoaTrangChiTiet();
            // Cấm sửa, xóa
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            //Hiện groupbox
            HienChiTiet(true);
        }

        // Xóa
        private void btnXoa_Click(object sender, EventArgs e)
        {
            //Bật Message Box cảnh báo người sử dụng
            if (MessageBox.Show("Bạn  có  chắc  chắn  xóa  mã  mặt  hàng  " + txtManhanvien.Text + " không ? Nếu  có  ấn  nút  Lưu, không  thì  ấn  nút  Hủy", "Xóa  sản  phẩm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                //Hiện gropbox chi tiết
                HienChiTiet(true);
            }
        }

        // Sửa
        private void btnSua_Click(object sender, EventArgs e)
        {
            //Ản thêm, xóa
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            //Hiện groupbox
            HienChiTiet(true);
        }

        // Lưu
        private void btnLuu_Click(object sender, EventArgs e)
        {
            //Kiểm tra khi chưa điền thông tin
            //Tên 
            if (txtTennhanvien.Text.Trim() == "")
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
            // Chức vụ
            if(txtChucvu.Text.Trim() == "")
            {
                errChitiet.SetError(txtChucvu, "Không thể để trống chức vụ!");
                return;
            }
            else
            {
                errChitiet.Clear();
            }
            // Hệ số lương
            if(txtHesoluon.Text.Trim() == "")
            {
                errChitiet.SetError(txtHesoluon, "Không thể để trống hệ số lương!");
                return;
            }
            // Lương
            if (txtluong.Text.Trim() == "")
            {
                errChitiet.SetError(txtluong, "Không thể để trống lương!");
                return;
            }

            //Thêm
            if (btnThem.Enabled == true)
            {
                if (txtManhanvien.Text.Trim() == "")
                {
                    errChitiet.SetError(txtManhanvien, "Không thể bỏ trống mã nhân viên!");
                    return;
                }
                else
                {
                    DataTable dtNhanvien = dtBase.DataSelect("select * from NHANVIEN join CHITIETNV on NHANVIEN.MANV = CHITIETNV.MANV where NHANVIEN.MANV = '" + txtManhanvien.Text + "'");
                    if (dtNhanvien.Rows.Count > 0)
                    {
                        errChitiet.SetError(txtManhanvien, "Mã nhân viên bị trùng");
                        return;
                    }
                    errChitiet.Clear();
                }
                string sqlInsert = "insert into NHANVIEN values('" + txtManhanvien.Text + "', N'" + txtDienthoai.Text + "', N'" + txtDiachi.Text + "', N'" + txtTennhanvien.Text + "', N'" + cbGioitinh.Text + "') ";
                                              
                dtBase.DataUpdate(sqlInsert);
                sqlInsert = "insert into CHITIETNV values('"+txtManhanvien.Text+"',N'" + txtChucvu.Text + "', '" + txtHesoluon.Text + "', '" + txtluong.Text + "')";
                dtBase.DataUpdate(sqlInsert);
                loadData();
                MessageBox.Show("Đã thêm dữ liệu thành công");
            }

            //Sửa
            if (btnSua.Enabled == true)
            {
                string sqlInsert = " update NHANVIEN set HOTENNV = N'" + txtTennhanvien.Text + "', DIACHINV = N'" + txtDiachi.Text + "', SDTNV = N'" + txtDienthoai.Text + "', GIOITINH = N'" + cbGioitinh.Text + "' where NHANVIEN.MANV = '" + txtManhanvien.Text + "'";
                dtBase.DataUpdate(sqlInsert);
                sqlInsert = "update CHITIETNV set CHUCVU = N'" + txtChucvu.Text + "', HESOLUONG = '"+txtHesoluon.Text+"', LUONG = '"+txtluong.Text+"' where CHITIETNV.MANV = '" + txtManhanvien.Text + "'";
                dtBase.DataUpdate(sqlInsert);
                loadData();
                MessageBox.Show("Bạn đã sửa thành công");
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                txtManhanvien.Enabled = false;
                txtDienthoai.Enabled = false;
                txtTennhanvien.Enabled = false;
                txtDiachi.Enabled = false;
                cbGioitinh.Enabled = false;
                txtChucvu.Enabled = false;
                txtluong.Enabled = false;
                txtHesoluon.Enabled = false;
                cbGioitinh.Enabled = false;
            }

            //Xóa
            if (btnXoa.Enabled == true)
            {
                string sqlInsert = "delete from CHITIETNV where CHITIETNV.MANV = '" + txtManhanvien.Text + "'";   
                dtBase.DataUpdate(sqlInsert);
                sqlInsert = " delete from NHANVIEN where NHANVIEN.MANV = '" + txtManhanvien.Text + "'";
                dtBase.DataUpdate(sqlInsert);
                loadData();
                   MessageBox.Show("Bạn đã xóa thành công");
                    btnSua.Enabled = false;
                    btnXoa.Enabled = false;
                }
            }

            // Tìm kiếm
            private void btnTimkiem_Click(object sender, EventArgs e)
        {
            //Cấm nút Sửa và Xóa
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            // SQL tìm kiếm
            string sqlInsert = "select NHANVIEN.MANV, SDTNV, DIACHINV, HOTENNV, GIOITINH, CHUCVU, HESOLUONG, LUONG from NHANVIEN join CHITIETNV on NHANVIEN.MANV = CHITIETNV.MANV where NHANVIEN.MANV is not null";
            // Tìm theo mã 
            if (txtManhanvien.Text.Trim() != "")
            {
                sqlInsert += " and NHANVIEN.MANV like N'%" + txtManhanvien2.Text + "%'";
            }
            // Tìm tên
            if (txtTennhanvien2.Text.Trim() != "")
            {
                sqlInsert += " and HOTENNV like N'%" + txtTennhanvien2.Text + "%'";
            }
            // Load lên dta
            dgvNhanvien.DataSource = dtBase.DataSelect(sqlInsert);
        }

        // Hủy
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

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                this.Close();
        }
    }
}
