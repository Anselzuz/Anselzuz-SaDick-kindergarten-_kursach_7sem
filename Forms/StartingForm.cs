using SaDick.Context;
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
    public partial class StartingForm : Form
    {
        public StartingForm()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (!long.TryParse(textBox1.Text, out UserInformation.phoneNum))
            {
                MessageBox.Show("Wrong login entered.");
                return;
            }

            using (SadickContext ctx = new())
            {
                string name = "";
                long phoneNumBuff = 0;

                //Authorization if parent
                var phones = ctx.LoginParents.Where(login => login.ParentPhoneNum == UserInformation.phoneNum && login.Password == textBox2.Text).Select(login => login.ParentPhoneNum).AsEnumerable();
                foreach (var el in phones)
                    phoneNumBuff = el;

                //If user is parent
                if (phoneNumBuff != 0)
                {
                    UserInformation.phoneNum = phoneNumBuff;
                    IEnumerable<string?> names = ctx.Parents.Where(p => p.ParentPhoneNum == phoneNumBuff).Select(p => p.Name).AsEnumerable();
                    foreach (var el in names)
                        name = el;
                    MessageBox.Show("Welcome parent\n" + name);

                    //Creating parents form
                    UserInformation.role = 2;
                    Close();
                    return;
                }

                //Authorization if educator
                phones = ctx.LoginEducators.Where(login => login.EducatorPhoneNum == UserInformation.phoneNum && login.Password == textBox2.Text).Select(login => login.EducatorPhoneNum).AsEnumerable();
                foreach (var el in phones)
                    phoneNumBuff = el;

                //If user is educator
                if (phoneNumBuff != 0)
                {
                    UserInformation.phoneNum = phoneNumBuff;
                    IEnumerable<string?> names = ctx.Educators.Where(p => p.EducatorPhoneNum == phoneNumBuff).Select(p => p.Name).AsEnumerable();
                    foreach (var el in names)
                        name = el;
                    MessageBox.Show("Welcome educator\n" + name);

                    //Creating edicators form
                    UserInformation.role = 1;
                    Close();
                    return;
                }

                //Authorization if admin
                phones = ctx.LoginAdmins.Where(login => login.AdminPhoneNum == UserInformation.phoneNum && login.Password == textBox2.Text).Select(login => login.AdminPhoneNum).AsEnumerable();
                foreach (var el in phones)
                    phoneNumBuff = el;

                //If user is admin
                if (phoneNumBuff != 0)
                {
                    UserInformation.phoneNum = phoneNumBuff;
                    IEnumerable<string?> names = ctx.Administrators.Where(p => p.AdminPhoneNum == phoneNumBuff).Select(p => p.Name).AsEnumerable();
                    foreach (var el in names)
                        name = el;
                    MessageBox.Show("Welcome admin\n" + name);

                    //Creating admins form
                    UserInformation.role = 0;
                    Close();
                    return;
                }

                //If all failed
                MessageBox.Show("Failed authorization.");
            }
        }
    }
}