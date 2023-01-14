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

namespace SaDick.Forms.AdminForms
{
    public partial class AddEducatorForm : Form
    {
        public AddEducatorForm()
        {
            InitializeComponent();
            adminL = new();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            long phoneNum;
            int groupNum;

            //Check right
            if (!int.TryParse(textBox3.Text, out groupNum))
            {
                MessageBox.Show("Wrong group number.");
                return;
            }

            if (!long.TryParse(textBox2.Text, out phoneNum))
            {
                MessageBox.Show("Wrong parent phone number.");
                return;
            }

            MessageBox.Show(adminL.AddEducator(phoneNum, groupNum, textBox1.Text, textBox4.Text));
        }

        private AdminL adminL;
    }
}