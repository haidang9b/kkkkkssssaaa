using CuaHangPhanMem.DAO;
using CuaHangPhanMem.DTO;
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
    public partial class frmBanHang : Form
    {
        public string nameStaff;
        public frmBanHang( string name)
        {
            InitializeComponent();
            nameStaff = name;
        }

        private void frmBanHang_Load(object sender, EventArgs e)
        {
            loadCatagoryList();
            LoadKH();
            loadDataDonhang(0);

        }
        public void loadDataDonhang(int id)
        {
            dgvBillInfo.DataSource = ViewBillInfoDAO.Instance.loadAllBillViewNonePay(id);
            int totalPrice = 0;
            foreach(ViewBillInfo item in ViewBillInfoDAO.Instance.loadAllBillViewNonePay(id))
            {
                totalPrice += item.TotalMoney;
            }
            txtTotal.Text = totalPrice.ToString();
        }
        private void loadCatagoryList()
        {
            List<ProductType> listCata = ProductTypeDAO.Instance.LoadAllProductType();
            comboBox1.DataSource = listCata;
            comboBox1.DisplayMember = "name";
        }
        private void loadProductListByCatagoryID(int id)
        {
            List<Product> listProduct = ProductDAO.Instance.LoadAllProductByCatagoryID(id);
            comboBox2.DataSource = listProduct;
            comboBox2.DisplayMember = "name";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 1;
            ComboBox cb = sender as ComboBox;
            if(comboBox1.SelectedItem == null)
            {
                return;
            }
            ProductType selected = comboBox1.SelectedItem as ProductType;
            id = selected.Id;
            loadProductListByCatagoryID(id);
        }

        private void btnAddKH_Click(object sender, EventArgs e)
        {
            String name = txtName.Text;
            String sdt = txtSDT.Text;
            String dc = txtDC.Text;

            if (name.Length > 0 || sdt.Length > 0 || dc.Length > 0)
            {
                try
                {
                    if (CustomerDAO.Instance.InsertCustomer(name, sdt, dc))
                    {
                        MessageBox.Show("Thêm khách hàng thành công !!");
                    }
                    else
                    {
                        MessageBox.Show("Thêm khách hàng mềm thất bại!!");
                    }
                }
                catch
                {
                    MessageBox.Show("Thêm khách hàng thất bại!!");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập thông tin");
            }
            LoadKH();
        }

        private void LoadKH()
        {
            dgvKhachHang.DataSource = CustomerDAO.Instance.LoadAllCustomer();
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtName.Text = this.dgvKhachHang.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtID.Text = this.dgvKhachHang.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtDC.Text = this.dgvKhachHang.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtSDT.Text = this.dgvKhachHang.Rows[e.RowIndex].Cells[2].Value.ToString();
                loadDataDonhang(int.Parse(txtID.Text));
            }
            catch
            {
                MessageBox.Show("Vui lòng chọn dòng hợp lệ");
            }
        }
        private void clearTextPhanMem()
        {
            comboBox1.SelectedIndex = 1;
            numericUpDown1.Value = 0;
            loadCatagoryList();
            
        }
        private void clearTextKH()
        {
            txtDC.Text = "";
            txtID.Text = "";
            txtTotal.Text = "";
            txtName.Text = "";
        }
        private void iconButton1_Click(object sender, EventArgs e)
        {
            if(txtID.Text.Length < 1)
            {
                MessageBox.Show("Vui lòng chọn khách hàng");
            }
            else if((int)numericUpDown1.Value == 0)
            {
                MessageBox.Show("Vui lòng chọn lại số lượng");
            }
            else
            {
                try
                {
                    int idKH = int.Parse(txtID.Text);
                    int idBill = BillDAO.Instance.GetUncheckBillByIDCustomer(idKH);
                    int idProduct = (comboBox2.SelectedItem as Product).ID;
                    int count = (int)numericUpDown1.Value;
                    //create new bill
                    if (idBill == -1)
                    {
                        BillDAO.Instance.InsertBill(idKH, nameStaff);
                        //int idmax = BillDAO.Instance.getMaxID();
                        BillInfoDAO.Instance.InsertBillInfo(BillDAO.Instance.getMaxID(), idProduct, count);
                    }
                    else
                    {
                        BillInfoDAO.Instance.InsertBillInfo(idBill, idProduct, count);
                    }
                    loadDataDonhang(idKH);
                }
                catch
                {
                    MessageBox.Show("Vui lòng chọn hợp lệ");
                }
            }
        }

        private void btnSearchKH_Click(object sender, EventArgs e)
        {
            int sdt = int.Parse(txtSDT.Text);
            try
            {
                dgvKhachHang.DataSource = CustomerDAO.Instance.SearchCustomerByPhone(sdt);
            }
            catch
            {
                MessageBox.Show("tìm không ra");
            }
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            int idkh = int.Parse(txtID.Text);
            if(idkh!=0)
            {
                try
                {
                    bool rs = BillDAO.Instance.PaytheBill(idkh);
                    if (rs) {
                        MessageBox.Show("Thanh toán thành công. Khách hàng " +txtName.Text +" với đơn hàng trị giá "+txtTotal.Text);
                    }
                    else
                    {
                        MessageBox.Show("Thanh toán thất bại");
                    }
                }
                catch
                {
                    MessageBox.Show("Thanh toán thất bại");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn đơn hàng của khách hàng cần thanh toán");
            }
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {

        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            try
            {
                int money = int.Parse(txtTotal.Text);
                int dis = (int)nudDiscount.Value;
                float moneyDis = money * dis / 100;
                txtTotal.Text = (money - moneyDis).ToString();
            }
            catch
            {
                MessageBox.Show("Giảm giá thất bại");
            }
        }
    }
}
