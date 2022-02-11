using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kutuphane_otomasyon
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DepositForm df = new DepositForm();
            df.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            BookForm bookForm = new BookForm();
            bookForm.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            MemberForm memberForm = new MemberForm();
            memberForm.Show();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            DepositForm depositForm = new DepositForm();
            depositForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Settings st = new Settings();
            st.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
