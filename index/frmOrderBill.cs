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
    public partial class frmOrderBill : Form
    {
        private int ID;
        private int ID2;
        private menu mn;
        int flag = 0;
        public frmOrderBill()
        {
            InitializeComponent();
            this.Load += frmOrderBill_Load;
            lstItem.Click += lstItem_Click;
            cboName.SelectedIndexChanged += cboName_SelectedIndexChanged;
            cboTable.SelectedIndex = 0;
            groupBox1.BackColor = Color.Transparent;
            groupBox2.BackColor = Color.Transparent;
            groupBox3.BackColor = Color.Transparent;
            groupBox4.BackColor = Color.Transparent;
        }

        void cboName_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboName.DisplayMember = "Name";
            cboName.ValueMember = "id";
            mn = (menu)cboName.SelectedItem;
            txtDongia.Text = mn.Price.ToString();
        }

        void lstItem_Click(object sender, EventArgs e)
        {
            try
            {
                Sell_icreamEntities db = new Sell_icreamEntities();
                string mon = (string)lstItem.SelectedRows[0].Cells[0].Value;
                string soluong = (string)lstItem.SelectedRows[0].Cells[2].Value;
                ID2 = (int)lstItem.SelectedRows[0].Cells[4].Value;
                cboName.Text = mon;
                SL.Value = int.Parse(soluong);
            }
            catch (Exception) { }
            
        }

        void frmOrderBill_Load(object sender, EventArgs e)
        {
            this.LoadBill();
        }

        private void btnThemHD_Click(object sender, EventArgs e)
        {
            try
            {
                Sell_icreamEntities db = new Sell_icreamEntities();
                Bill b = new Bill();
                b.Table = "Ban " + cboTable.Text;
                b.Date = DateTime.Now;
                db.Bills.Add(b);
                db.SaveChanges();
                MessageBox.Show("Thêm hóa đơn mới thành công!");
                this.LoadBill();
            }
            catch (Exception) { }
        }
        private bool OrderEx = true;
        private int tmpCheck;
        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            try
            {
                int id = (int)lstBill.SelectedRows[0].Cells[0].Value;
                this.LoadItem(id);
                btnThemHD.Enabled = false;
                btnXoaHD.Enabled = false;
                groupBox3.Enabled = true;
                cboTable.Enabled = false;
                if (OrderEx)
                {
                    if (tmpCheck == id)
                    {
                        cboTable.Enabled = true;
                        btnThemHD.Enabled = true;
                        this.groupBox3.Hide();
                        OrderEx = false;
                        this.Size = new Size(511, 481);
                        this.LoadBill();
                        btnXoaHD.Enabled = true;
                    }
                    else
                    {
                        this.groupBox3.Show();
                        this.Size = new Size(1052, 481);
                        tmpCheck = id;
                        OrderEx = true;
                    }
                }
                else
                {
                    this.groupBox3.Show();
                    this.Size = new Size(1052, 481);
                    tmpCheck = id;
                    OrderEx = true;
                }
            }
            catch (Exception) { }
        }

        private void btnXoaHD_Click(object sender, EventArgs e)
        {
            Sell_icreamEntities db = new Sell_icreamEntities();
            int id = (int)lstBill.SelectedRows[0].Cells[0].Value;
            Bill bill = db.Bills.Single(st => st.id == id);
            DialogResult dr = MessageBox.Show("Bạn có chắc  chắn Xóa?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    db.Bills.Remove(bill);
                    db.SaveChanges();
                    MessageBox.Show("Xóa thành công!");
                    this.LoadItem(ID);
                }
                catch (Exception)
                { MessageBox.Show("Xóa không thành công! "); }
            }
            else
            { return; }
            this.LoadBill();
        }
        private void LoadBill()
        {
            this.groupBox3.Hide();
            this.Size = new Size(511, 481);
            groupBox3.Enabled = false;
            Sell_icreamEntities db = new Sell_icreamEntities();
            List<Bill> list = db.Bills.ToList();
            lstBill.DataSource = list.Select(b => new { id = b.id, Date = b.Date, Table = b.Table, TongTien = b.Bill_Item.Sum(i => i.Price * int.Parse(i.Quality)) }).ToList();
            btnXoaHD.Enabled = true;
        }
        private void LoadItem(int id)
        {
            lstItem.Enabled = true;
            ID = id;
            Sell_icreamEntities db = new Sell_icreamEntities();
            cboName.DataSource = db.menus.ToList();
            cboName.DisplayMember = "Name";
            cboName.ValueMember = "id";
            List<Bill_Item> list = db.Bill_Item.Where(i => i.C_Bill_id == id).ToList();
            lstItem.DataSource = list.Select(i =>
                new { Ten = i.menu.Name, Gia = i.menu.Price, SoLuong = i.Quality, ThanhTien = int.Parse(i.Quality) * i.Price, Mã = i.id }).ToList();
            lstItem.Columns[4].Visible = false;
            cboName.Enabled = false;
            SL.Enabled = false;
            btnHuy.Hide();
            btnOk.Hide();
            btnXoa.Enabled = true;
            btnThemSP.Enabled = true;
            btnSua.Enabled = true;
        }

        private void btnThemSP_Click(object sender, EventArgs e)
        {
            lstItem.Enabled = false;
            SL.Value = 1;
            flag = 1;
            btnHuy.Show();
            btnOk.Show();
            btnThemSP.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            cboName.Enabled = true;
            SL.Enabled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            flag = 2;
            btnHuy.Show();
            btnOk.Show();
            btnThemSP.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            cboName.Enabled = true;
            SL.Enabled = true;
            lstItem.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            Sell_icreamEntities db = new Sell_icreamEntities();
            try
            {
                Bill_Item bit = db.Bill_Item.Single(st => st.id == ID2);
                DialogResult dr = MessageBox.Show("Bạn có chắc  chắn Xóa?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        db.Bill_Item.Remove(bit);
                        db.SaveChanges();
                        MessageBox.Show("Xóa thành công!");
                        this.LoadItem(ID);
                    }
                    catch (Exception)
                    { MessageBox.Show("Xóa không thành công! "); }
                }
                else { return; }
            }
            catch (Exception)
            {
                MessageBox.Show("Vui lòng chọn lại món cần xóa!");
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //Add new item
            if (flag == 1)
            {
                Sell_icreamEntities db = new Sell_icreamEntities();
                menu Menu = (menu)cboName.SelectedItem;
                string stmp = cboName.Text;
                try
                {
                    Bill_Item tmp = db.Bill_Item.Single(st => st.C_Bill_id == ID && st.Item_id == Menu.id);
                    int dem = int.Parse(tmp.Quality) + (int)SL.Value;
                    tmp.Quality = dem.ToString();
                    db.Entry(tmp).State = EntityState.Modified;
                    db.SaveChanges();
                    db.SaveChanges();
                    LoadItem(ID);
                }
                catch (Exception)
                {
                    Bill_Item item = new Bill_Item();
                    item.C_Bill_id = ID;
                    item.Item_id = Menu.id;
                    item.Quality = SL.Value.ToString();
                    item.Price = Menu.Price;
                    db.Bill_Item.Add(item);
                    db.SaveChanges();
                }
                LoadItem(ID);
                flag = 0;
                btnThemSP.Enabled = true;
                btnXoa.Enabled = true;
                btnSua.Enabled = true;
            }
            //Edit 1 Item in Bill
            if (flag == 2)
            {
                Sell_icreamEntities db = new Sell_icreamEntities();
                try
                {
                    Bill_Item bit = db.Bill_Item.Single(st => st.id == ID2);
                    bit.Quality = SL.Value.ToString();
                    DialogResult dr = MessageBox.Show("Bạn có chắc  chắn chỉnh sửa?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            db.Entry(bit).State = EntityState.Modified;
                            db.SaveChanges();
                            MessageBox.Show("Cập nhật thành công!");
                            this.LoadItem(ID);
                        }
                        catch (Exception)
                        { MessageBox.Show("Chỉnh sửa không thành công! "); }
                        flag = 0;
                        btnThemSP.Enabled = true;
                        btnXoa.Enabled = true;
                        btnSua.Enabled = true;
                        btnHuy.Hide();
                    }
                    else
                    { return; }
                }
                catch (Exception)
                { MessageBox.Show("Vui lòng chọn lại món cần chỉnh sửa "); }
                
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            LoadItem(ID);
        }

        private void lstBill_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                btnChiTiet_Click(null, null);
            }catch(Exception){}
        }
    }
}
