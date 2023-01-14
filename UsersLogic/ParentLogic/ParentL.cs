using Microsoft.EntityFrameworkCore;
using SaDick.Context;
using SaDick.Forms;
using SaDick.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaDick.UsersLogic.ParentLogic
{
    class ParentL : IParentL
    {
        public string CheckPayment()
        {
            using (SadickContext ctx = new())
            {
                string result = "";
                //Search for parent's children
                var children = ctx.Children.AsNoTracking().Where(child => child.ParentPhoneNum == UserInformation.phoneNum).ToList();

                foreach (var child in children)
                {
                    result += "Child's surname: " + child.Surname + '\n'
                        + "Child's name: " + child.Name + '\n'
                        + "Child's birth sertificate number: " + child.BirthSertificateSerNum + '\n'
                        + "Due months of payment: " + ctx.Payments.AsNoTracking().Where(payment => payment.BirthSertificateSerNum == child.BirthSertificateSerNum)
                        .First().MonthNotPayed + "\n\n";
                }

                return result;
            }
        }

        public string CheckVisitingFor30DayFromACertainDate(DateTime date)
        {
            string result = "";

            using (SadickContext ctx = new())
            {
                //Search for parent's children
                var children = ctx.Children.AsNoTracking().Where(child => child.ParentPhoneNum == UserInformation.phoneNum).ToList();

                foreach (var child in children)
                {
                    result += "Start from this date " + date.ToShortDateString() + " your child\n"
                        + child.Surname + " " + child.Name + " " + child.BirthSertificateSerNum + " was in kindergarten ";
                    int count = 0;
                    DateTime dateBuff = date;

                    for (int i = 0; i < 30 && !(dateBuff.Year >= DateTime.Now.Year && dateBuff.Month >= DateTime.Now.Month && dateBuff.Day >= DateTime.Now.Day); i++)
                    {
                        if (ctx.Visitings.AsNoTracking().Where(visit => visit.Data == dateBuff && visit.BirthSertificateSerNum == child.BirthSertificateSerNum).Count() == 1)
                            count++;
                        dateBuff = dateBuff.Add(new TimeSpan(864000000000));
                    }
                    result += count + " times.";
                }
            }

            return result;
        }

        public string CheckVisitingOnASpecificDate(DateTime date)
        {
            using (SadickContext ctx = new())
            {
                string result = "";

                //Search for parent's children
                var children = ctx.Children.AsNoTracking().Where(child => child.ParentPhoneNum == UserInformation.phoneNum).ToList();

                foreach (var child in children)
                {
                    result += "Child's surname: " + child.Surname + '\n'
                            + "Child's name: " + child.Name + '\n'
                            + "Child's birth sertificate number: " + child.BirthSertificateSerNum + '\n'
                            + "In " + date.ToShortDateString() + " your child was ";

                    var dates = ctx.Visitings.AsNoTracking().Where(visit => visit.Data == date && visit.BirthSertificateSerNum == child.BirthSertificateSerNum);
                    if (dates.Count() == 0)
                        result += "not " + "in kindergarten.\n\n";
                    else
                        result += "in kindergarten.\n\n";
                }

                return result;
            }
        }
    }
}