﻿using CuaHangPhanMem.DAO;
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
    public partial class frmTimKiemDonHang : Form
    {
        public frmTimKiemDonHang()
        {
            InitializeComponent();
        }

        private void frmTimKiemDonHang_Load(object sender, EventArgs e)
        {
            loadAllListBill();
        }
        private void loadAllListBill()
        {
            dgvDonHang.DataSource = ViewBillDAO.Instance.getAllBillView();
        }
        private void dgvDonHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvDonHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtID.Text = this.dgvDonHang.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtName.Text = this.dgvDonHang.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtSDT.Text = this.dgvDonHang.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtMoney.Text = this.dgvDonHang.Rows[e.RowIndex].Cells[4].Value.ToString();
                LoadRightTable(int.Parse(txtID.Text));
            }
            catch
            {
                MessageBox.Show("Vui lòng chọn hợp lệ");
            }
        }
        public void LoadRightTable(int id)
        {
            dgvCTDH.DataSource = ViewBillInfoDAO.Instance.loadAllBillViewByIDHD(id);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string sdt = txtSDT.Text;
            try
            {
                dgvDonHang.DataSource = ViewBillDAO.Instance.getAllBillViewBySDT(sdt);
            }
            catch
            {
                MessageBox.Show("Tìm không thấy");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            loadAllListBill();
            txtID.Text = "";
            txtMoney.Text = "";
            txtName.Text = "";
            txtSDT.Text = "";
        }
    }
}
