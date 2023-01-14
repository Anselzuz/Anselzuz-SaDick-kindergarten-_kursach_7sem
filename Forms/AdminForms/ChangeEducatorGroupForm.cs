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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SaDick.Forms.AdminForms
{
    public partial class ChangeEducatorGroupForm : Form
    {
        public ChangeEducatorGroupForm()
        {
            InitializeComponent();
            adminL = new();
        }

        //Enter
        private void button1_Click(object sender, EventArgs e)
        {
            int groupNum;
            long phoneNum;

            //Check right
            if (!int.TryParse(textBox1.Text, out groupNum))
            {
                MessageBox.Show("Wrong group number.");
                return;
            }

            if (!long.TryParse(textBox2.Text, out phoneNum))
            {
                MessageBox.Show("Wrong parent phone number.");
                return;
            }

            MessageBox.Show(adminL.ChangeEducatorGroup(phoneNum, groupNum));
        }

        private AdminL adminL;
    }
}
