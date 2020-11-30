using CuaHangPhanMem.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHangPhanMem.DAO
{
    public class ViewBillInfoDAO
    {
        private static ViewBillInfoDAO instance;
        public static ViewBillInfoDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new ViewBillInfoDAO();
                return instance;
            }
            private set { instance = value; }
        }
        private ViewBillInfoDAO() { }
        public List<ViewBillInfo> loadAllBillViewNonePay(int idkh)
        {
            List<ViewBillInfo> list = new List<ViewBillInfo>();
            string query = "SELECT  TENSP, SL, DONGIA, DONGIA*SL AS'TONGGIA' FROM CHITIETHOADON CT INNER JOIN SANPHAM ON SANPHAM.MASP = CT.MASP INNER JOIN HOADON ON HOADON.MAHD = CT.MAHD INNER JOIN KHACHHANG ON HOADON.MAKH = KHACHHANG.MAKH AND HOADON.STATUS = 0 WHERE KHACHHANG.MAKH = " + idkh;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                ViewBillInfo product = new ViewBillInfo(item);
                list.Add(product);
            }
            return list;
        }
        public List<ViewBillInfo> loadAllBillViewByIDHD(int idHD)
        {
            List<ViewBillInfo> list = new List<ViewBillInfo>();
            string query = "SELECT  TENSP, SL, DONGIA, DONGIA*SL AS'TONGGIA' FROM CHITIETHOADON CT INNER JOIN SANPHAM ON SANPHAM.MASP = CT.MASP INNER JOIN HOADON ON HOADON.MAHD = CT.MAHD INNER JOIN KHACHHANG ON HOADON.MAKH = KHACHHANG.MAKH AND HOADON.STATUS = 1 WHERE HOADON.MAHD = " + idHD;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                ViewBillInfo product = new ViewBillInfo(item);
                list.Add(product);
            }
            return list;
        }
    }
}
