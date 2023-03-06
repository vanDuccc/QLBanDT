﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanDienThoai.Customer
{
    public partial class FrmCustomer : Form
    {
        Module.DataAcess dtBase = new Module.DataAcess();

        public FrmCustomer()
        {
            InitializeComponent();         
        }
        // ------Hiện chi tiết
        void HienChiTiet(bool hien)
        {
            txtMakhachhang.Enabled = hien;
            txtTenkhachhang.Enabled = hien;
            txtDiachi.Enabled = hien;
            txtDienthoai.Enabled = hien;

            btnHuy.Enabled = hien;
            btnLuu.Enabled = hien;
        }

        // Load form
        private void FrmCustomer_Load(object sender, EventArgs e)
        {
            loadData();
            // ẩn sửa, xóa
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            //ẩn groupbox chi  tiết
            HienChiTiet(false);
            dgvKhachhang.Columns[0].HeaderText = "Mã khách hàng";
            dgvKhachhang.Columns[1].HeaderText = "Tên khách hàng";
            dgvKhachhang.Columns[2].HeaderText = "Địa chỉ";
            dgvKhachhang.Columns[3].HeaderText = "Điện thoại";
            dgvKhachhang.Columns[1].Width = 200;
            dgvKhachhang.Columns[0].Width = 130;
        }
        // ----Load Data 
        void loadData()
        {
            dgvKhachhang.DataSource = dtBase.DataSelect("select * from KHACHHANG");
        }

        // Cell click
        private void dgvKhachhang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Hien thi nut sua
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnThem.Enabled = false;

            txtMakhachhang.Text = dgvKhachhang.CurrentRow.Cells[0].Value.ToString();
            txtTenkhachhang.Text = dgvKhachhang.CurrentRow.Cells[1].Value.ToString();
            txtDiachi.Text = dgvKhachhang.CurrentRow.Cells[2].Value.ToString();
            txtDienthoai.Text = dgvKhachhang.CurrentRow.Cells[3].Value.ToString();
        }
        // Thêm 
        private void XoaTrangChiTiet()
        {
            txtMakhachhang.Text = "";
            txtTenkhachhang.Text = "";
            txtDiachi.Text = "";
            txtDienthoai.Text = "";
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            //Cập nhật tiêu đề
            lblTieude.Text = "THÊM KHÁCH HÀNG";
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
            if (MessageBox.Show("Bạn  có  chắc  chắn  xóa  mã  mặt  hàng  " + txtMakhachhang.Text + " không ? Nếu  có  ấn  nút  Lưu, không  thì  ấn  nút  Hủy", "Xóa  sản  phẩm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                lblTieude.Text = "XÓA KHÁCH HÀNG";
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                //Hiện gropbox chi tiết
                HienChiTiet(true);
            }           
        }

        // Sửa
        private void btnSua_Click(object sender, EventArgs e)
        {
            //Câp nhật tiêu đề
            lblTieude.Text = "CẬP NHẬT KHÁCH HÀNG";
            //Ản thêm, xóa
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            //Hiện groupbox
            HienChiTiet(true);          
        }


        void resetData()
        {
            txtMakhachhang.Text = "";
            txtTenkhachhang.Text = "";
            txtDiachi.Text = "";
            txtDienthoai.Text = "";
            txtMakhachhang.Focus();
        }


        //LƯU
        private void btnLuu_Click_2(object sender, EventArgs e)
        {
            // ktra tên
            if(txtTenkhachhang.Text.Trim() == "")
            {
                errChitiet.SetError(txtTenkhachhang, "Tên khách hàng không được bỏ trống!");
                return;
            }
            else
            {
                errChitiet.Clear();
            }
            //kiểm tra địa chỉ
            if (txtDiachi.Text.Trim() == "")
            {
                errChitiet.SetError(txtTenkhachhang, "Địa chỉ không được bỏ trống!");
                return;
            }
            else
            {
                errChitiet.Clear();
            }
            //ktra điện thoại
            if (txtDienthoai.Text.Trim() == "")
            {
                errChitiet.SetError(txtTenkhachhang, "Số điện thoại không được bỏ trống!");
                return;
            }
            else
            {
                errChitiet.Clear();
            }

            //THÊM
            if(btnThem.Enabled == true)
            {
                if(txtMakhachhang.Text.Trim() == "")
                {
                    errChitiet.SetError(txtMakhachhang, "Mã khách hàng không thể bỏ trống!");
                    return;
                }
                else
                {
                    DataTable dtKhachhang = dtBase.DataSelect("select * from KHACHHANG where MAKH = '"+txtMakhachhang.Text+"'");
                    if(dtKhachhang.Rows.Count > 0)
                    {
                        errChitiet.SetError(txtMakhachhang, "Mã khách hàng bị trùng!");
                    return;
                    }
                    errChitiet.Clear();

                string sqlInsert = " insert into KHACHHANG values('" + txtMakhachhang.Text + "', N'" + txtTenkhachhang.Text + "', N'" + txtDiachi.Text + "', '" + txtDienthoai.Text + "')";
                dtBase.DataUpdate(sqlInsert);
                loadData();
                MessageBox.Show("Đã thêm dữ liệu thành công");
                }
            }

            //SỬA
            if(btnSua.Enabled == true)
            {
                string sqlInsert = " update KHACHHANG set HOTENKH = N'"+txtTenkhachhang.Text+"', DIACHIKH = N'"+txtDiachi.Text+"', SDTKH = '"+txtDienthoai.Text+"' where MAKH = '"+txtMakhachhang.Text+"'";
                dtBase.DataUpdate(sqlInsert);         
                loadData();
                MessageBox.Show("Bạn đã sửa thành công");
            }

            //XÓA
            if(btnXoa.Enabled == true)
            {
                string sqlInsert = " delete from KHACHHANG where MAKH = '"+txtMakhachhang.Text+"'";
                dtBase.DataUpdate(sqlInsert);
                loadData();
                MessageBox.Show("Bạn đã xóa thành công");
            }
        }

        // HỦY
        //Hủy
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

        private void btnLuu_Click_1(object sender, EventArgs e)
        {

        }



        // ----- lại ---

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            //Cập nhật tiêu đề
            lblTieude.Text = "THÊM KHÁCH HÀNG";
            XoaTrangChiTiet();
            // Cấm sửa, xóa
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            //Hiện groupbox
            HienChiTiet(true);
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            //Bật Message Box cảnh báo người sử dụng
            if (MessageBox.Show("Bạn  có  chắc  chắn  xóa  mã  mặt  hàng  " + txtMakhachhang.Text + " không ? Nếu  có  ấn  nút  Lưu, không  thì  ấn  nút  Hủy", "Xóa  sản  phẩm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                lblTieude.Text = "XÓA KHÁCH HÀNG";
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                //Hiện gropbox chi tiết
                HienChiTiet(true);
            }
        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            //Câp nhật tiêu đề
            lblTieude.Text = "CẬP NHẬT KHÁCH HÀNG";
            //Ản thêm, xóa
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            //Hiện groupbox
            HienChiTiet(true);
        }

        

        private void btnHuy_Click_1(object sender, EventArgs e)
        {

        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {

        }
    }
}
