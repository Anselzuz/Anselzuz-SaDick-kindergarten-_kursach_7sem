using Microsoft.VisualBasic.Logging;
using SaDick.EducatorLogic;
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
    public partial class EducatorForm : Form
    {
        public EducatorForm()
        {
            InitializeComponent();
            educatLog = new();
        }

        //Note the visit
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var shortChild = educatLog.NoteTheVisit(Convert.ToDateTime(textBox2.Text));

                NoteVisitForm nVForm = new (shortChild, Convert.ToDateTime(textBox2.Text));
                nVForm.Show();
            }
            catch (FormatException ex)
            {
                MessageBox.Show("You entered the wrong date.");
            }
        }

        //Check payment
        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(educatLog.CheckPayment());
        }

        //See pasw
        private void button3_Click(object sender, EventArgs e)
        {
            long phoneNum = 0;

            if (!long.TryParse(textBox1.Text, out phoneNum))
            {
                MessageBox.Show("Wrong phone number entered.");
                return;
            }

            string pasw = educatLog.CheckParentPasw(phoneNum);
            if (pasw == "")
            {
                MessageBox.Show("Wrong phone number entered.");
                return;
            }
            else
                MessageBox.Show("Password: " + pasw);
        }

        private EducatorL educatLog;
    }
}