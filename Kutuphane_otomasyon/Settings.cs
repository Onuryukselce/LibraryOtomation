using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kutuphane_otomasyon.Util;

namespace Kutuphane_otomasyon
{
    public partial class Settings : Form
    {
        INI SettingsINI = new INI("Settings.ini");
        public bool save = true;
        public Settings()
        {
            InitializeComponent();
        }

        public void getDatabasePath()
        {
            try
            {
                textBox1.Text = SettingsINI.Read("DatabasePath");
            }
            catch (Exception)
            { MessageBox.Show("Ayar dosyasına erişilirken istenmeyen bir hata meydana geldi!"); }
        }

        public void getDefaultSettings()
        {
            try
            {
                string exePath = System.Reflection.Assembly.GetEntryAssembly().Location;
                string workPath = System.IO.Path.GetDirectoryName(exePath);
                string databasePath = System.IO.Path.Combine(workPath, "Library.mdb");
                textBox1.Text = databasePath;
                save = false;

            }
            catch (Exception)
            { MessageBox.Show("Ayar dosyasına erişilirken istenmeyen bir hata meydana geldi!"); }
        }
        public void saveSettings()
        {
            try
            {
                SettingsINI.Write("DatabasePath", textBox1.Text);
                save = true;
                MessageBox.Show("Ayarlar başarıyla kaydedildi! Program yeniden başlatılacaktır");
                Application.Restart();
                Environment.Exit(0);
                

            }
            catch (Exception)
            { MessageBox.Show("Ayar dosyasına yazılırken istenmeyen bir hata meydana geldi!"); }
        }
        private void Settings_Load(object sender, EventArgs e)
        {
            getDatabasePath();
        }

        private void Settings_Leave(object sender, EventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "d:\\";
                openFileDialog.Filter = "mdb files (*.mdb)|*.mdb"; //just mdb files will open
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;


                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    textBox1.Text = openFileDialog.FileName;
                    save = false;
                }
            }
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (save == false)
            {
                DialogResult saved = MessageBox.Show("Ayarları kaydetmediniz, kaydedilmemiş ayarlar kaybolacaktır! Şimdi kaydetmek ister misiniz ?","Dikkat",MessageBoxButtons.YesNo);
                if(saved == DialogResult.Yes)
                {
                    saveSettings();
                    this.Close();
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            getDefaultSettings();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveSettings();
        }
    }
}
