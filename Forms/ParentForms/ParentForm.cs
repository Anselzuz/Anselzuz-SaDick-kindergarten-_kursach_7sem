using SaDick.UsersLogic.ParentLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SaDick.Forms
{
    public partial class ParentForm : Form
    {
        public ParentForm()
        {
            InitializeComponent();
            pLog = new();
        }

        //Check payment
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(pLog.CheckPayment());
        }

        //Check visiting on a specific date
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show(pLog.CheckVisitingOnASpecificDate(Convert.ToDateTime(textBox1.Text)));
            }
            catch (FormatException ex)
            {
                MessageBox.Show("You entered the wrong date.");
            }
        }   

        //Check visiting for 30 day from a certain date
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show(pLog.CheckVisitingFor30DayFromACertainDate(Convert.ToDateTime(textBox1.Text)));
            }
            catch (FormatException ex)
            {
                MessageBox.Show("You entered the wrong date.");
            }
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private ParentL pLog;
    }
}