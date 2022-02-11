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
    public partial class MemberForm : Form
    {
        public bool externalFilter = false;
        public long filterId = 0;
        public MemberForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
           
            
        }
        private void showMembers()
        {
            DataTable dt = new DataTable();
            Member mem = new Member();
            if(externalFilter == true)
            {
                dt = mem.getMemberById(filterId);
            } else
            dt = mem.ShowMember();
            dataGridView1.DataSource = dt;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            showMembers();
        }

        private void MemberForm_Load(object sender, EventArgs e)
        {
            showMembers();
        }


        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
           
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

     
        private void deleteMember(string memberId)
        {
            Member mem = new Member();
            
                mem.MemberNo = memberId;
                mem.DeleteMember();
        }

        private void deleteSelection()
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                string memberNo = row.Cells[0].Value.ToString();
                deleteMember(memberNo);
            }
            showMembers();
        }
        private bool editMember(string memberNo, string name, string surname, string phone, string dob)
        {
            Member mem = new Member(name,surname,phone,dob);
            mem.MemberNo = memberNo;
            bool status = mem.EditMember();
            return status;
        }
        private void button3_Click(object sender, EventArgs e)
        {
           // deleteMember();
        }

        public Member getSelectedItem()
        {
            Member mem = new Member();
            mem.MemberNo = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            mem.Name = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            mem.Surname = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            mem.Phone = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            mem.DOB = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            return mem;
        }

        public void UpdateEditPanel(Member mem)
        {
            textBox1.Text = mem.Name;
            textBox2.Text = mem.Surname;
            textBox3.Text = mem.Phone;
            textBox4.Text = mem.DOB;
        }




        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
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
                        dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[1];
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
                Member m = getSelectedItem();
                UpdateEditPanel(m);
            }
        }

        private bool AddMember(string name, string surname, string phone, string dob)
        {
            Member mem = new Member(name, surname, phone, dob);
            bool status = mem.AddMember();
            return status;
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            if (textBox8.Text != string.Empty && textBox7.Text != string.Empty && textBox6.Text != string.Empty && textBox5.Text != string.Empty)
            {
                bool status = AddMember(textBox8.Text, textBox7.Text, textBox6.Text, textBox5.Text);
                if (status == true)
                {
                    MessageBox.Show("Kayıt başarıyla eklendi!");
                    showMembers();
                }
                else
                {
                    MessageBox.Show("Bir hata meydana geldi!");
                }
            }
            else
                MessageBox.Show("Alanlar boş bırakılamaz");
        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deleteSelection();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != string.Empty && textBox2.Text != string.Empty && textBox3.Text != string.Empty && textBox4.Text != string.Empty)
            {
                bool status = editMember(dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text);
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

            showMembers();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void okuduğuKitaplarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DepositForm depositForm = new DepositForm();
            depositForm.externalFilterMode = true;
            depositForm.filterType = DepositForm.FilterType.STUDENT_ID;
            depositForm.filterID = long.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            depositForm.Show();
        }
    }
}
