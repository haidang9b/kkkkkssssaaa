using CuaHangPhanMem.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHangPhanMem.DAO
{
    public class ViewBillDAO
    {
        private static ViewBillDAO instance;
        public static ViewBillDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new ViewBillDAO();
                return instance;
            }
            set => instance = value;
        }

        public List<ViewBill> getAllBillView()
        {
            List<ViewBill> list = new List<ViewBill>();
            string query = "SELECT * FROM BILLVIEW";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                ViewBill product = new ViewBill(item);
                list.Add(product);
            }
            return list;
        }
        public List<ViewBill> getAllBillViewBySDT(string sdt)
        {
            List<ViewBill> list = new List<ViewBill>();
            string query = "SELECT * FROM BILLVIEW WHERE SDTKH LIKE '%"+sdt+"%'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                ViewBill product = new ViewBill(item);
                list.Add(product);
            }
            return list;
        }

        
    }
}
