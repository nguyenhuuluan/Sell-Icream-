using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace index
{
    public partial class frmManager : Form
    {
        int flag = 0;
        public frmManager()
        {
            InitializeComponent();
            this.Load += frmManager_Load;
            lstMenu.Click += lstMenu_Click;
            lstMenu.ForeColor = Color.Black;
            
        }

        void lstMenu_Click(object sender, EventArgs e)
        {
            Sell_icreamEntities db = new Sell_icreamEntities();
            int id = (int)lstMenu.SelectedRows[0].Cells[0].Value;
            menu Menu = db.menus.Single(st => st.id == id);
            txtMa.Text = Menu.id.ToString();
            txtTen.Text = Menu.Name.Trim();
            txtGia.Text = Menu.Price.ToString();
        }

        void frmManager_Load(object sender, EventArgs e)
        {
            Sell_icreamEntities db = new Sell_icreamEntities();
            lstMenu.DataSource = db.menus.ToList();
            this.lstMenu.Columns["Bill_item"].Visible = false;
            btnSua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = false;
            btnThem.Show();
            btnSua.Show();
            btnXoa.Show();
            btnLuu.Hide();
            btnHuy.Hide();
            groupBox1.Enabled = false;
            label1.Enabled = true;
            groupBox2.Enabled = true;
            lstMenu.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            flag = 1;
            txtGia.Clear();
            txtMa.Clear();
            txtTen.Clear();
            btnLuu.Show();
            btnHuy.Show();
            btnSua.Hide();
            btnXoa.Hide();
            btnThem.Hide();
            groupBox1.Enabled = true;
            txtMa.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            Sell_icreamEntities db = new Sell_icreamEntities();
            int id = (int)lstMenu.SelectedRows[0].Cells[0].Value;
            menu Menu = db.menus.Single(st => st.id == id);
            DialogResult dr = MessageBox.Show("Bạn có chắc  chắn xóa?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    db.menus.Remove(Menu);
                    db.SaveChanges();
                    this.frmManager_Load(null, null);
                    MessageBox.Show("Xóa món ăn thành công!!");

                }
                catch (Exception)
                { MessageBox.Show("Xóa không thành công!!"); }
            }
            else 
            {
                frmManager_Load(null, null);
                return;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            flag = 2;
            txtMa.Enabled = false;
            btnLuu.Show();
            btnHuy.Show();
            btnSua.Hide();
            btnXoa.Hide();
            btnThem.Hide();
            lstMenu.Enabled = false;
            groupBox1.Enabled = true;

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                try
                {
                    Sell_icreamEntities db = new Sell_icreamEntities();
                    menu Menu = new menu();
                    Menu.id = int.Parse(this.txtMa.Text);
                    Menu.Name = this.txtTen.Text;
                    Menu.Price = int.Parse(this.txtGia.Text);
                    db.menus.Add(Menu);
                    db.SaveChanges();
                    MessageBox.Show("Thêm món mới thành công!");
                    this.frmManager_Load(null, null);
                }
                catch (Exception)
                { MessageBox.Show("Thêm món mới không thành công!"); }
            }
            if (flag == 2)
            {
                try
                {
                    Sell_icreamEntities db = new Sell_icreamEntities();
                    int id = (int)lstMenu.SelectedRows[0].Cells[0].Value;
                    menu Menu = db.menus.Single(st => st.id == id);
                    Menu.Name = this.txtTen.Text;
                    Menu.Price = int.Parse(this.txtGia.Text);
                    db.Entry(Menu).State = EntityState.Modified;
                    db.SaveChanges();
                    MessageBox.Show("Cập nhật thông tin món ăn thành công!");
                    this.frmManager_Load(null, null);
                    groupBox1.Enabled = true;
                    lstMenu.Enabled = true;
                }
                catch (Exception)
                {
                    MessageBox.Show("Cập nhật thông tin món ăn không thành công!");
                }
            }
            lstMenu.Enabled = true;
            groupBox2.Enabled = true;
            groupBox1.Enabled = false;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            txtGia.Clear();
            txtMa.Clear();
            txtTen.Clear();
            frmManager_Load(null, null);
        }

        private void lstMenu_SelectionChanged(object sender, EventArgs e)
        {
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnThem.Enabled = true;
        }
    }
}
