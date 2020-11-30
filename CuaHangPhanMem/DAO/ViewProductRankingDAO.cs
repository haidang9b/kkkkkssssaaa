using CuaHangPhanMem.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHangPhanMem.DAO
{
    public class ViewProductRankingDAO
    {
        private static ViewProductRankingDAO instance;
        public static ViewProductRankingDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new ViewProductRankingDAO();
                return instance;
            }
            set => instance = value;
        }
        public List<ViewProductRanking> loadAllProductRanking()
        {
            List<ViewProductRanking> list = new List<ViewProductRanking>();
            string query = "SELECT SANPHAM.MASP, SANPHAM.TENSP,SOLUONG, SANPHAM.DONGIA, SOLUONG*SANPHAM.DONGIA AS'TONGTIEN' FROM SANPHAM INNER JOIN (SELECT SP.MASP, SUM(CT.SL) as 'SOLUONG' FROM SANPHAM SP INNER JOIN CHITIETHOADON CT ON SP.MASP = CT.MASP INNER JOIN HOADON HD ON HD.MAHD = CT.MAHD GROUP BY  SP.MASP) AS X ON X.MASP = SANPHAM.MASP ORDER BY SOLUONG DESC";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                ViewProductRanking product = new ViewProductRanking(item);
                list.Add(product);
            }
            return list;
        }
        public List<ViewProductRanking> loadAllProductRankingByTime(string start, string end)
        {
            List<ViewProductRanking> list = new List<ViewProductRanking>();
            string query = "SELECT SANPHAM.MASP, SANPHAM.TENSP,SOLUONG, SANPHAM.DONGIA, SOLUONG*SANPHAM.DONGIA AS'TONGTIEN' FROM SANPHAM INNER JOIN (SELECT SP.MASP, SUM(CT.SL) as 'SOLUONG' FROM SANPHAM SP INNER JOIN CHITIETHOADON CT ON SP.MASP = CT.MASP INNER JOIN HOADON HD ON HD.MAHD = CT.MAHD WHERE HD.TGMUA >='" + start + "' AND HD.TGMUA <= '" + end + "' GROUP BY  SP.MASP) AS X ON X.MASP = SANPHAM.MASP ORDER BY SOLUONG DESC";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                ViewProductRanking product = new ViewProductRanking(item);
                list.Add(product);
            }
            return list;
        }
    }
}
