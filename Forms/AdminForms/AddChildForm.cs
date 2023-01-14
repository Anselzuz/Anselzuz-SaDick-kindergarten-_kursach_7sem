using SaDick.UsersLogic.AdminLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaDick.Forms
{
    public partial class AddChildForm : Form
    {
        public AddChildForm()
        {
            InitializeComponent();
            adminl = new();
        }

        //Enter
        private void button1_Click(object sender, EventArgs e)
        {
            int groupNum;
            long parentPhonenum;

            //Check right
            if (!int.TryParse(textBox2.Text, out groupNum))
            {
                MessageBox.Show("Wrong group number.");
                return;
            }

            if (!long.TryParse(textBox7.Text, out parentPhonenum))
            {
                MessageBox.Show("Wrong parent phone number.");
                return;
            }

            if (textBox1.Text.Length > 12)
            {
                MessageBox.Show("Birth sertificate can't be more 12 symbols.");
                return;
            }


            MessageBox.Show(adminl.AddChild(textBox1.Text, textBox4.Text, textBox3.Text,
                groupNum, parentPhonenum, textBox6.Text, textBox5.Text));
        }

        private AdminL adminl;
    }
}