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
    public partial class ChangeChildGroupForm : Form
    {
        public ChangeChildGroupForm()
        {
            InitializeComponent();
            adminL = new();
        }

        //Enter
        private void button1_Click(object sender, EventArgs e)
        {
            int groupNum;

            //Check right
            if (!int.TryParse(textBox2.Text, out groupNum))
            {
                MessageBox.Show("Wrong group number.");
                return;
            }
            if (textBox1.Text.Length > 12)
            {
                MessageBox.Show("Birth sertificate can't be more 12 symbols.");
                return;
            }

            MessageBox.Show(adminL.ChangeChildGroup(textBox1.Text, groupNum));
        }

        private AdminL adminL;
    }
}