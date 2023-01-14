using SaDick.Forms.AdminForms;
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
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
            adminL = new();
        }

        //Child
        //Add child
        private void button1_Click(object sender, EventArgs e)
        {
            AddChildForm addChild = new();
            addChild.Show();
        }

        //Del child
        private void button2_Click(object sender, EventArgs e)
        {
            //Check right
            if (textBox1.Text.Length > 12)
            {
                MessageBox.Show("Birth sertificate can't be more 12 symbols.");
                return;
            }

            MessageBox.Show(adminL.DeleteChild(textBox1.Text));
        }

        //Change child group
        private void button3_Click(object sender, EventArgs e)
        {
            ChangeChildGroupForm changeGroup = new();
            changeGroup.Show();
        }

        //Educator
        //Add educator
        private void button4_Click(object sender, EventArgs e)
        {
            AddEducatorForm addEducator = new();
            addEducator.Show();
        }

        //Del educator
        private void button6_Click(object sender, EventArgs e)
        {
            long phoneNum;

            //Check right
            if (!long.TryParse(textBox2.Text, out phoneNum))
            {
                MessageBox.Show("Wrong educator phone number.");
                return;
            }

            MessageBox.Show(adminL.DeleteEducator(phoneNum));
        }

        //Change educator group
        private void button5_Click(object sender, EventArgs e)
        {
            ChangeEducatorGroupForm changeGroup = new();
            changeGroup.Show();
        }

        //Payment
        //Add 1 month of payment to everyone
        private void button7_Click(object sender, EventArgs e)
        {
            adminL.Add1MonthToAll();
            MessageBox.Show("Success!");
        }

        //Delete 1 month of payment from everyone
        private void button8_Click(object sender, EventArgs e)
        {
            adminL.Delete1MonthFromAll();
            MessageBox.Show("Success!");
        }

        //Check group payment
        private void button9_Click(object sender, EventArgs e)
        {
            int groupNum;

            //Check right
            if (!int.TryParse(textBox3.Text, out groupNum))
            {
                MessageBox.Show("Wrong group number.");
                return;
            }

            MessageBox.Show(adminL.CheckGroupPayment(groupNum));
        }

        //Add 1 month of payment to the group
        private void button10_Click(object sender, EventArgs e)
        {
            int groupNum;

            //Check right
            if (!int.TryParse(textBox4.Text, out groupNum))
            {
                MessageBox.Show("Wrong group number.");
                return;
            }

            MessageBox.Show(adminL.Add1MonthToTheGroup(groupNum));
        }

        //Delete 1 month of payment from the group
        private void button11_Click(object sender, EventArgs e)
        {
            int groupNum;

            //Check right
            if (!int.TryParse(textBox5.Text, out groupNum))
            {
                MessageBox.Show("Wrong group number.");
                return;
            }

            MessageBox.Show(adminL.Delete1MonthFromTheGroup(groupNum));
        }

        //Add 1 month of child payment
        private void button12_Click(object sender, EventArgs e)
        {
            //Check right
            if (textBox1.Text.Length > 12)
            {
                MessageBox.Show("Birth sertificate can't be more 12 symbols.");
                return;
            }

            MessageBox.Show(adminL.Add1MonthToTheChild(textBox7.Text));
        }

        //Delete 1 month child payment
        private void button13_Click(object sender, EventArgs e)
        {
            //Check right
            if (textBox1.Text.Length > 12)
            {
                MessageBox.Show("Birth sertificate can't be more 12 symbols.");
                return;
            }

            MessageBox.Show(adminL.Delete1MonthFromTheChild(textBox6.Text));
        }

        //Others
        //Check the number of children in the group
        private void button14_Click(object sender, EventArgs e)
        {
            int groupNum;

            //Check right
            if (!int.TryParse(textBox8.Text, out groupNum))
            {
                MessageBox.Show("Wrong group number.");
                return;
            }

            MessageBox.Show(adminL.CheckNumChildrenInTheGroup(groupNum));
        }

        //Check educator's pasword
        private void button15_Click(object sender, EventArgs e)
        {
            long phoneNum;

            //Check right
            if (!long.TryParse(textBox9.Text, out phoneNum))
            {
                MessageBox.Show("Wrong educator phone number.");
                return;
            }

            MessageBox.Show(adminL.CHeckEducatorsPasw(phoneNum));
        }

        //Create new group
        private void button16_Click(object sender, EventArgs e)
        {
            int groupNum;

            //Check right
            if (!int.TryParse(textBox10.Text, out groupNum))
            {
                MessageBox.Show("Wrong group number.");
                return;
            }
            MessageBox.Show(adminL.CreateGroup(groupNum));
        }

        private AdminL adminL;
    }
}