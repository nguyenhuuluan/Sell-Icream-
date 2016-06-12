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
    public partial class index : Form
    {
        public index()
        {
            InitializeComponent();
            btnManager.Click += btnManager_Click;
            btnOrder.Click += btnOrder_Click;
            label2.BackColor = Color.Transparent;
        }

        void btnOrder_Click(object sender, EventArgs e)
        {
            frmOrderBill frm = new frmOrderBill();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        void btnManager_Click(object sender, EventArgs e)
        {
            frmManager frm = new frmManager();
            frmLogin frm2 = new frmLogin();
            frm2.ShowDialog();
            this.Hide();
            this.Show();
        }
    }
}
