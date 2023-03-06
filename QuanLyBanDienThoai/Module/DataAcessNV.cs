using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyBanDienThoai.Module
{
    class DataAcessNV
    {
        string ConnectString = @"Data Source=LAPTOP-K95NLUD6\SQLEXPRESS03;Initial Catalog=HA;Integrated Security=True";

        SqlConnection sqlConnect = null;

        // PT mở kết nối
        void OpenConnect()
        {
            sqlConnect = new SqlConnection(ConnectString);
            if (sqlConnect.State != ConnectionState.Open)
            {
                sqlConnect.Open();
            }
        }

        //PT đóng kết nối
        void CloseConnect()
        {
            if (sqlConnect.State != ConnectionState.Closed)
            {
                sqlConnect.Close();
                sqlConnect.Dispose();
            }
        }

        // Phương thức Select trả về DataTable
        public DataTable DataSelect(string sqlSelect)
        {
            DataTable dtResult = new DataTable();
            OpenConnect();
            SqlDataAdapter sqlData = new SqlDataAdapter(sqlSelect, sqlConnect);
            sqlData.Fill(dtResult);
            CloseConnect();
            sqlData.Dispose();
            return dtResult;
        }

        //Phương thức thay đổi dữ liệu: insert, delete, update
        public void DataUpdate(String sql)
        {
            OpenConnect();
            SqlCommand sqlComm = new SqlCommand();
            sqlComm.Connection = sqlConnect;
            sqlComm.CommandText = sql;
            sqlComm.ExecuteNonQuery();
            CloseConnect();
            sqlComm.Dispose();
        }
    }
}
