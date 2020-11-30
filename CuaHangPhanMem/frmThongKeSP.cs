using CuaHangPhanMem.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CuaHangPhanMem
{
    public partial class frmThongKeSP : Form
    {
        public frmThongKeSP()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string start = dptStart.Value.ToString("dd-MM-yyyy");
                string end = dptEnd.Value.ToString("dd-MM-yyyy");

                dataGridView2.DataSource = ViewProductRankingDAO.Instance.loadAllProductRankingByTime(start, end);
            }
            catch
            {
                MessageBox.Show("Thống kê thất bại");
            }
        }

        private void frmThongKeSP_Load(object sender, EventArgs e)
        {
            dataGridView2.DataSource = ViewProductRankingDAO.Instance.loadAllProductRanking();
        }
    }
}
