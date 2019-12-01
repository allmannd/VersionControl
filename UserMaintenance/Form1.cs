using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserMaintenance.Entities;

namespace UserMaintenance
{
    public partial class Form1 : Form
    {
        BindingList<user> users = new BindingList<user>();
        public Form1()
        {
            InitializeComponent();
            label1.Text = Resource1.FullName;
            button3.Text = Resource1.Delete;
            button2.Text = Resource1.Write;
            button1.Text = Resource1.Add;
            listBox1.DataSource = users;
            listBox1.ValueMember = "ID";
            listBox1.DisplayMember = "FullName";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var u = new user()
            {
                FullName = textBox1.Text,
                
            };
            users.Add(u);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "csv";
            sfd.Filter = "Comma Seperated Values (*.csv)|*.csv";
            sfd.AddExtension = true;
            if (sfd.ShowDialog() != DialogResult.OK) 
            {
                return;
            }
            using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8) )
            {
                foreach (var item in users)
                {
                    sw.Write(item.ID);
                    sw.Write(";");
                    sw.Write(item.FullName);
                    sw.WriteLine();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Guid trash = (Guid)listBox1.SelectedValue;
            var tr = from x in users
                     where x.ID == trash
                     select x;
            users.Remove(tr.FirstOrDefault());
            listBox1.Refresh();

        }
    }
}
