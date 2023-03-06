using QuanLyBanDienThoai.Customer;
using QuanLyBanDienThoai.Customer2;
using QuanLyBanDienThoai.Employee;
using QuanLyBanDienThoai.Employee2;
using QuanLyBanDienThoai.LoginChange;
using QuanLyBanDienThoai.NhanVien;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanDienThoai
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FrmLogin());
            //Application.Run(new FrmLoginChange());
            //Application.Run(new FrmCustomer());
            //Application.Run(new FrmEmployee());
            //Application.Run(new FrmNhanVien());
            Application.Run(new FrmCustomer2());
            //Application.Run(new FrmEmployee2());
        }
    }
}
