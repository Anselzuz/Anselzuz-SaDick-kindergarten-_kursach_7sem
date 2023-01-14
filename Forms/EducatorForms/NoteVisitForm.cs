using SaDick.EducatorLogic;
using SaDick.Models;
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
    public partial class NoteVisitForm : Form
    {
        public NoteVisitForm(List<shortChild> shChild, DateTime date)
        {
            InitializeComponent();

            if (shChild.Count() > 30)
            {
                MessageBox.Show("Group has over 30 children.");
                Close();
            }
            else if (shChild.Count() < 1)
            {
                MessageBox.Show("Group has less than 1 children.");
                Close();
            }

            //Initialize and paint buttons
            int x = 35;
            int y = 30;
            for (int i = 0; i < shChild.Count(); i++)
            {
                buttons[i] = new Button();
                buttons[i].Font = new System.Drawing.Font("Unispace", 7, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                buttons[i].Location = new System.Drawing.Point(x, y);
                buttons[i].Name = "button" + i;
                buttons[i].Size = new System.Drawing.Size(150, 80);
                buttons[i].TabIndex = i;
                buttons[i].Text = shChild[i].Surname + '\n' + shChild[i].Name + '\n' + shChild[i].BirthSertificateSerNum;
                buttons[i].UseVisualStyleBackColor = true;
                buttons[i].Click += new System.EventHandler(NoteChild);

                if (shChild[i].IsWas)
                    buttons[i].BackColor = System.Drawing.Color.DarkGreen;
                else
                    buttons[i].BackColor = System.Drawing.Color.DarkRed;

                Controls.Add(buttons[i]);

                //Change coords
                x = x + 150 + 3;
                if (x > 780)
                {
                    x = 35;
                    y += 80 + 3;
                }
            }

            this.shChild = shChild;
            this.date = date;
        }

        public void NoteChild(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if (btn.BackColor == System.Drawing.Color.DarkGreen)
            {
                btn.BackColor = System.Drawing.Color.DarkRed;
                shChild[btn.TabIndex] = new shortChild()
                {
                    Surname = shChild[btn.TabIndex].Surname,
                    Name = shChild[btn.TabIndex].Name,
                    BirthSertificateSerNum = shChild[btn.TabIndex].BirthSertificateSerNum,
                    IsWas = false
                };
            }
            else
            {
                btn.BackColor = System.Drawing.Color.DarkGreen;
                shChild[btn.TabIndex] = new shortChild()
                {
                    Surname = shChild[btn.TabIndex].Surname,
                    Name = shChild[btn.TabIndex].Name,
                    BirthSertificateSerNum = shChild[btn.TabIndex].BirthSertificateSerNum,
                    IsWas = true
                };
            }
        }

        //Enter
        private void button1_Click(object sender, EventArgs e)
        {
            EducatorL edLog = new();
            edLog.ChangeVisit(shChild, date);
            MessageBox.Show("Visiting has been changed.");
            Close();
        }

        private const int maxNumButtons = 30;
        private Button[] buttons = new Button[maxNumButtons];
        private List<shortChild> shChild;
        private DateTime date;
    }
}