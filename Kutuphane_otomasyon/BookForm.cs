using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kutuphane_otomasyon.EntityLayer;
namespace Kutuphane_otomasyon
{
    public partial class BookForm : Form
    {
        public bool externalFilter = false;
        public long filterId = 0;
        public BookForm()
        {
            InitializeComponent();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void BookForm_Load(object sender, EventArgs e)
        {
            showBooks();
        }

        private void showBooks()
        {
            DataTable dt = new DataTable();
            Book b = new Book();
            if(externalFilter == true)
            {
                dt = b.getBookById(filterId);
            } else
            dt = b.ShowBooks();
            dataGridView1.DataSource = dt;
        }

        private bool EditBook(string BookNo,string ISBN, string BookName, string Author, string Issue, string Released )
        {
            Book book = new Book(ISBN, BookName, Author, Issue, Released);
            book.BookNo = BookNo;
            bool status = book.UpdateBook();
            return status;
        }

        private bool DeleteBook(string bookNo)
        {
            Book book = new Book();

                book.BookNo = bookNo;
                bool status = book.DeleteBook();

            return status;

        }
        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            showBooks();
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != string.Empty && textBox2.Text != string.Empty && textBox3.Text != string.Empty && textBox4.Text != string.Empty && textBox5.Text != string.Empty)
            {
               bool status = EditBook(dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text);
                if (status == true)
                {
                    MessageBox.Show("Kayıt başarıyla güncellendi!");
                }
                else
                {
                    MessageBox.Show("Bir hata oluştu!");
                }

            }
            else
                MessageBox.Show("Alanlar boş bırakılamaz");

            showBooks();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           // DeleteBook(textBox5.Text);
            showBooks();
        }

        private void DeleteSelection()
        {
            foreach(DataGridViewRow row in dataGridView1.SelectedRows)
            {
                string bookNo = row.Cells[0].Value.ToString();
                DeleteBook(bookNo);
            }
            showBooks();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public Book getSelectedItem()
        {
            string BookNo = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            string ISBN = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            string BookName = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            string Author = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            string Issue = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            string ReleaseDate = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            Book newbook = new Book(BookNo, ISBN, BookName, Author, Issue, ReleaseDate);
            return newbook;
        }

        public void UpdateEditPanel(Book b)
        {
            textBox1.Text = b.ISBN;
            textBox2.Text = b.BookName;
            textBox3.Text = b.Author;
            textBox4.Text = b.Issue;
            textBox5.Text = b.ReleaseDate;
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count != 1)
            {
                panel4.Enabled = false;
            } else
            {
                panel4.Enabled = true;
                Book b = getSelectedItem();
                UpdateEditPanel(b);
            }
        }

        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(dataGridView1.SelectedRows.Count <= 1)
            {
                if(e.Button == MouseButtons.Right)
                {
                    try
                    {
                        dataGridView1.Rows[e.RowIndex].Selected = true;
                        dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[1];
                        contextMenuStrip1.Items[1].Visible =true;
                        contextMenuStrip1.Items[2].Visible = true;
                        contextMenuStrip1.Items[1].Text = "Sil";


                    }
                    catch (Exception)
                    {
                        contextMenuStrip1.Items[2].Visible = false;
                        contextMenuStrip1.Items[1].Visible = false;
                    }
                    finally
                    {
                        this.contextMenuStrip1.Show(this.dataGridView1, e.Location);
                        contextMenuStrip1.Show(Cursor.Position);
                    }

                }
            }
            else if(dataGridView1.SelectedRows.Count >= 1)
            {
                if(e.Button == MouseButtons.Right)
                {
                    try
                    {
                        contextMenuStrip1.Items[1].Text = "Seçili Kitapları Sil";
                        contextMenuStrip1.Items[1].Visible = true;
                        contextMenuStrip1.Items[2].Visible = false;


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
        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteSelection();
        }

        private bool AddBook(string isbn, string bookname, string author, string issue, string released) // Kitap ekleme fonksiyonu
        {
            Book book = new Book(isbn, bookname, author, issue, released);
            bool status = book.AddBook();
            return status;
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            if (textBox10.Text != string.Empty && textBox9.Text != string.Empty && textBox8.Text != string.Empty && textBox7.Text != string.Empty && textBox6.Text != string.Empty)
            {
                bool status = AddBook(textBox10.Text, textBox9.Text, textBox8.Text, textBox7.Text, textBox6.Text);
                if (status == true)
                {
                    MessageBox.Show("Kayıt başarıyla eklendi!");
                    showBooks();
                }
                   
                else
                {
                    MessageBox.Show("Bir hata meydana geldi!");
                }
            }
            else
                MessageBox.Show("Alanlar boş bırakılamaz");
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void yenileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showBooks();
        }

        private void kitabıAlanÖğrencilerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DepositForm depositForm = new DepositForm();
            depositForm.externalFilterMode = true;
            depositForm.filterType = DepositForm.FilterType.BOOK_ID;
            depositForm.filterID = long.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            depositForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = dataGridView1.DataSource;
            if (comboBox1.SelectedIndex == 0)
            {
                if (textBox11.Text != "")
                    bs.Filter = "BookNo = " + textBox11.Text;
                else
                    bs.Filter = "BookName like '%" + textBox11.Text + "%'";
            }
            else if(comboBox1.SelectedIndex == 1)
            {
                bs.Filter = "ISBN like '%" + textBox11.Text + "%'";
            }
            else if(comboBox1.SelectedIndex == 2)
            {
                bs.Filter = "Author like '%" + textBox11.Text + "%'";
            }
            else if(comboBox1.SelectedIndex == 3)
            {
                bs.Filter = "BookName like '%" + textBox11.Text + "%'";
            }

            dataGridView1.DataSource = bs;
        }
    }
}
