﻿using CuaHangPhanMem.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHangPhanMem.DAO
{
    public class BillDAO
    {
        private static BillDAO instance;
        public static BillDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new BillDAO();
                return instance;
            }
            private set { instance = value; }
        }
        public List<Bill> loadAllBill()
        {
            List<Bill> list = new List<Bill>();
            string query = "SELECT [MAHD],[MAKH],[NVBAN],[TGMUA],[TONGTIEN] FROM [QUANLYBANHANG].[dbo].[HOADON]";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Bill bill = new Bill(item);
                list.Add(bill);
            }
            return list;
        }
        public int loadTotalMoney()
        {
            int money = 0;
            string query = "Select SUM(TONGTIEN) from HOADON";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            money = (int) data.Rows[0][0];
            return money;
           
        }

        public void InsertBill(int id, string name)
        {
            DataProvider.Instance.ExecuteNoneQuery("exec USP_INSERTBILL "+ id+" ,'"+name+"' " );
        }
        public int GetUncheckBillByIDCustomer(int id)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM HOADON WHERE MAKH=" + id + " AND STATUS = 0");
            if(data.Rows.Count > 0)
            {
                Bill bill = new Bill(data.Rows[0]);
                return bill.ID;
            }
            return -1;
        }

        public int getMaxID()
        {
            try
            {
                return (int)DataProvider.Instance.ExecuteScalar("Select max(mahd) from dbo.HOADON ");
            }
            catch
            {
                return 1;
            }
        }
        public bool PaytheBill(int idkh)
        {
            string query = "EXEC PROC_THANHTOAN " + idkh;
            int rs = DataProvider.Instance.ExecuteNoneQuery(query);
            return rs > 0;
        }
        public List<Bill> getAllBillViewByTime(string start, string end)
        {
            List<Bill> list = new List<Bill>();
            string query = "SET DATEFORMAT DMY  SELECT* FROM HOADON WHERE HOADON.TGMUA >= '" + start + "' AND HOADON.TGMUA <= '" + end + "'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Bill bill = new Bill(item);
                list.Add(bill);
            }
            return list;
        }
        public int getMoneyByTime(string start, string end)
        {
            try
            {
                string query = "SET DATEFORMAT DMY  SELECT Sum(TONGTIEN) FROM HOADON WHERE HOADON.TGMUA >= '" + start + "' AND HOADON.TGMUA <= '" + end + "'";
                int money = (int)DataProvider.Instance.ExecuteScalar(query);
                return money;
            }
            catch
            {
                return 0;
            }
        }
    }
}
