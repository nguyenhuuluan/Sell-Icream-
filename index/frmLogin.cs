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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            frmManager frm = new frmManager();
            if (this.txtMk.Text == "12345")
            {
                this.Hide();
                frm.ShowDialog();
            }
            else 
            {
                MessageBox.Show("Sai mật khẩu!");
                this.txtMk.Text = "";
            }
        }
        private void txtMk_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOk_Click(this, new EventArgs());
            }
        }
    }
}
