using Kutuphane_otomasyon.EntityLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
namespace Kutuphane_otomasyon
{
    public partial class DepositForm : Form
    {
        List<ListViewItem> masterstudentlist = new List<ListViewItem>();
        List<ListViewItem> masterbooklist = new List<ListViewItem>();
        public bool externalFilterMode = false;
        public long filterID = 0;
        public enum FilterType
        {
            STUDENT_ID,
            BOOK_ID
        }
        public FilterType filterType;

        public DepositForm()
        {
            InitializeComponent();
        }

        public void showStudents()
        {
            Member member = new Member();
            DataTable dt = member.ShowMember();
            masterstudentlist.Clear();
            listView1.Columns.Add("Öğrenci No");
            listView1.Columns.Add("Ad");
            listView1.Columns.Add("Soyad");
            listView1.View = View.Details;

           

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                ListViewItem listitem = new ListViewItem(dr["MemberNumber"].ToString());
                listitem.SubItems.Add(dr["Name"].ToString());
                listitem.SubItems.Add(dr["Surname"].ToString());
                masterstudentlist.Add(listitem);
            }
            DisplayStudentItemsOnUpdate();
          
        }

        public void showBooks()
        {
            Book book = new Book();
            DataTable dt = book.ShowBooks();
            masterbooklist.Clear();
            masterbooklist.Clear();

            listView3.Columns.Add("Kitap No");
            listView3.Columns.Add("Kitap Adı");
            listView3.Columns.Add("Yazarı");
            listView3.View = View.Details;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                ListViewItem listitem = new ListViewItem(dr["BookNo"].ToString());
                listitem.SubItems.Add(dr["BookName"].ToString());
                listitem.SubItems.Add(dr["Author"].ToString());
                masterbooklist.Add(listitem);

            }

            DisplayBookItemsOnUpdate();
           
        }


        public void EditCellStyle()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                double twoDaysLeft = (DateTime.Today - Convert.ToDateTime(row.Cells[7].Value)).TotalDays;
                if (row.Cells[9].Value.ToString().ToUpper() == "GEÇ")
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }
               else if (row.Cells[9].Value.ToString().ToUpper().Contains("TESLİM"))
                {
                    row.DefaultCellStyle.BackColor = Color.Green;
                }

               else if (twoDaysLeft == -2 || twoDaysLeft == -1 && !row.Cells[9].Value.ToString().ToUpper().Contains("TESLİM"))
                {
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                }
            }
        }
        public void showDeposits()
        {
            Deposit deposit = new Deposit();
            DataTable dt;
            if (externalFilterMode == true)
            {
                switch(filterType)
                {
                    case FilterType.STUDENT_ID:
                        dt = deposit.getDepositByStudentId(filterID);
                        break;
                    case FilterType.BOOK_ID:
                        dt = deposit.getDepositByBookId(filterID);
                        break;
                    default:
                        dt = deposit.getDeposits();
                        break;
                }
            }
                
            else
               dt = deposit.getDeposits();
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            EditCellStyle();

           
        }
        private void DepositForm_Load(object sender, EventArgs e)
        {
            showDeposits();
            showStudents();
            showBooks();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SearchList(List<ListViewItem> lv, ListView view, string text)
        {
            view.Items.Clear();
            foreach (ListViewItem item in lv.Where(lvi => lvi.Text == text))
            {
                view.Items.Add(item);
            }
        }
        private void DisplayStudentItemsOnUpdate()
        {
            listView1.Items.Clear();

            // This filters and adds your filtered items to listView1
            foreach (ListViewItem item in masterstudentlist.Where(lvi => lvi.Text.ToLower().Contains(textBox1.Text.ToLower().Trim()) || lvi.SubItems[1].Text.ToLower().Contains(textBox1.Text.ToLower().Trim())))
            {
                listView1.Items.Add(item);
            }
        }

      


        private void DisplayBookItemsOnUpdate()
        {
            listView3.Items.Clear();

            // This filters and adds your filtered items to listView1
            foreach (ListViewItem item in masterbooklist.Where(lvi => lvi.Text.ToLower().Contains(textBox3.Text.ToLower().Trim()) || lvi.SubItems[1].Text.ToLower().Contains(textBox3.Text.ToLower().Trim()) || lvi.SubItems[2].Text.ToLower().Contains(textBox3.Text.ToLower().Trim())))
            {
                listView3.Items.Add(item);
            }
        }

    
        private int SearchInListView(List<ListViewItem> lv, string searchText)
        {
           foreach(ListViewItem item in lv.Where(lvi => lvi.Text == searchText))
            {
                return item.Index;
            }

            return 0;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DisplayStudentItemsOnUpdate();

        }

        private bool AddDeposit(string _memberno, string _bookno, DateTime _depositdate, DateTime _withdrawdate, decimal _penalty, string _status)
        {
            Deposit deposit = new Deposit(_memberno, _bookno, _depositdate, _withdrawdate, _penalty, _status);
            return deposit.AddDeposit();
        }
        private bool EditDeposit(string _depositid,string _memberno, string _bookno, DateTime _depositdate, DateTime _withdrawdate,  decimal _penalty, string _status)
        {
            Deposit deposit = new Deposit(_depositid, _memberno, _bookno, _depositdate, _withdrawdate, _penalty, _status);
            return deposit.UpdateDeposit();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1 && dateTimePicker1.Value != null && dateTimePicker2.Value != null)
            {
                EditDeposit(dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), listView1.SelectedItems[0].Text, listView3.SelectedItems[0].Text, dateTimePicker1.Value, dateTimePicker2.Value, Convert.ToDecimal(dataGridView1.SelectedRows[0].Cells[8].Value), dataGridView1.SelectedRows[0].Cells[9].Value.ToString());
                showDeposits();
            }else
            {
                MessageBox.Show("Alanlar Boş Olamaz!");
            }
        }



        
        private void deleteDeposit(string depositId)
        {
            Deposit deposit = new Deposit();

            deposit.ID = depositId;
            deposit.DeleteDeposit();
        }

        private void deleteSelection()
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                string memberNo = row.Cells[0].Value.ToString();
                deleteDeposit(memberNo);
            }
            showDeposits();
        }

        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 1)
            {
                if (e.Button == MouseButtons.Right)
                {
                    try
                    {
                        dataGridView1.Rows[e.RowIndex].Selected = true;
                        dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[0];
                        contextMenuStrip1.Items[1].Visible = true;
                        contextMenuStrip1.Items[2].Visible = true;
                        contextMenuStrip1.Items[1].Text = "Sil";


                    }
                    catch (Exception)
                    {
                        contextMenuStrip1.Items[1].Visible = false;
                        contextMenuStrip1.Items[2].Visible = false;

                    }
                    finally
                    {
                        this.contextMenuStrip1.Show(this.dataGridView1, e.Location);
                        contextMenuStrip1.Show(Cursor.Position);
                    }

                }
            }
            else if (dataGridView1.SelectedRows.Count >= 1)
            {
                if (e.Button == MouseButtons.Right)
                {
                    try
                    {
                        contextMenuStrip1.Items[1].Text = "Seçili Üyeleri Sil";
                        contextMenuStrip1.Items[1].Visible = true;
                        contextMenuStrip1.Items[2].Visible = false;



                    }
                    catch (Exception)
                    {
                        contextMenuStrip1.Items[1].Visible = false;

                    }
                    finally
                    {
                        this.contextMenuStrip1.Show(this.dataGridView1, e.Location);
                        contextMenuStrip1.Show(Cursor.Position);
                    }
                }
            }
        }

        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            DisplayBookItemsOnUpdate();
        }

        public Deposit getSelectedItem()
        {
            Deposit deposit = new Deposit();
            deposit.ID = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            deposit.MemberNo = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            deposit.BookNo = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            deposit.DepositDate = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells[6].Value.ToString().Trim());
            deposit.WithdrawDate = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells[7].Value.ToString().Trim());
            deposit.Penalty = Convert.ToDecimal(dataGridView1.SelectedRows[0].Cells[8].Value);
            deposit.Status = dataGridView1.SelectedRows[0].Cells[9].Value.ToString();
            return deposit;
        }

        public void UpdateEditPanel(Deposit d)
        {
            int student = SearchInListView(masterstudentlist, d.MemberNo.ToString());
            int book = SearchInListView(masterbooklist, d.BookNo.ToString());
            SearchList(masterstudentlist, listView1, d.MemberNo.ToString());
            SearchList(masterbooklist, listView3, d.BookNo.ToString());
            dateTimePicker1.Value = d.DepositDate;
            dateTimePicker2.Value = d.WithdrawDate;
            foreach(ListViewItem item in listView1.Items)
            {
                item.Selected = true;
                item.Focused = true;
            }
            foreach (ListViewItem item in listView3.Items)
            {
                item.Selected = true;
                item.Focused = true;
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 1)
            {
                panel4.Enabled = false;
            }
            else
            {
                panel4.Enabled = true;
                Deposit d = getSelectedItem();
                UpdateEditPanel(d);
            }
        }

        private void dataGridView1_ColumnSortModeChanged(object sender, DataGridViewColumnEventArgs e)
        {
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            EditCellStyle();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1 && dateTimePicker1.Value != null && dateTimePicker2.Value != null)
            {
                AddDeposit(listView1.SelectedItems[0].Text, listView3.SelectedItems[0].Text, dateTimePicker1.Value, dateTimePicker2.Value, Convert.ToDecimal(dataGridView1.SelectedRows[0].Cells[8].Value),"EMANETTE");
                showDeposits();
            }
            else
            {
                MessageBox.Show("Alanlar Boş Olamaz!");
            }
        }

        private void yenileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showDeposits();

        }

        private bool closeProcess()
        {

            string status = dataGridView1.SelectedRows[0].Cells[9].Value.ToString();
            


                    if (!status.ToUpper().Contains("GEÇ"))
                    {
                        status = "ZAMANINDA TESLİM";
                    }
                    else
                    {
                        DialogResult Result = MessageBox.Show("Öğrenci kitabı geç teslim ettiği için ücret cezası uygulanmalıdır, Ücret cezası uygulandı mı ?", "DİKKAT", MessageBoxButtons.YesNo);
                        if (Result == DialogResult.Yes)
                            status = "GEÇ TESLİM";
                    }

                    return EditDeposit(dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), dataGridView1.SelectedRows[0].Cells[2].Value.ToString(), dataGridView1.SelectedRows[0].Cells[1].Value.ToString(), Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells[6].Value), Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells[7].Value), Convert.ToDecimal(dataGridView1.SelectedRows[0].Cells[8].Value), status);
            
        }

        private void süreçKapatToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            closeProcess();
            showDeposits();
        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deleteSelection();
        }

        private void button3_Click(object sender, EventArgs e)
        {
           DepositGraph depositGraph = new DepositGraph();
            depositGraph.Show();
        }

        private void kitabıGörüntüleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BookForm bookForm = new BookForm();
            bookForm.externalFilter = true;
            bookForm.filterId = long.Parse(dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
            bookForm.Show();
        }

        private void öğrenciyiGörüntüleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MemberForm memberForm = new MemberForm();
            memberForm.externalFilter = true;
            memberForm.filterId = long.Parse(dataGridView1.SelectedRows[0].Cells[2].Value.ToString());
            memberForm.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = dataGridView1.DataSource;
            if (comboBox1.SelectedIndex == 0)
            {
                bs.Filter = "Status like '%" + "" + "%'";

            }
            else if (comboBox1.SelectedIndex == 1)
            {
                bs.Filter = "Status like '%" + "emanette" + "%'";

            }
            else if (comboBox1.SelectedIndex == 2)
            {
               bs.Filter = "Status = 'geç'";

            }
            else if (comboBox1.SelectedIndex == 3)
            {
                bs.Filter = "Status like '%" + "teslim" + "%'";

            }

            dataGridView1.DataSource = bs;

        }
    }
}
