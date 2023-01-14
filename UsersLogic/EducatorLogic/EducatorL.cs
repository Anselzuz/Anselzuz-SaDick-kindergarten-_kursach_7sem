using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SaDick.Context;
using SaDick.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaDick.EducatorLogic
{
    public struct shortChild
    {
        public string Name;
        public string Surname;
        public string BirthSertificateSerNum;
        public bool IsWas;
    }

    public class EducatorL : IEducatorL
    {
        public string CheckParentPasw(long phoneNum)
        {
            using (SadickContext ctx = new())
            {
                string result = "";

                var resultBuff = ctx.LoginParents.AsNoTracking().Where(parent => parent.ParentPhoneNum == phoneNum);
                if (resultBuff.Count() == 0)
                    return result;
                else
                {
                    result = resultBuff.First().Password;
                    return result;
                }
            }
        }

        public string CheckPayment()
        {
            using (SadickContext ctx = new())
            {
                string result = "";

                //Find group number
                int? groupNum = ctx.Educators.AsNoTracking().Where(educator => educator.EducatorPhoneNum == UserInformation.phoneNum).First().GroupNum;
                var children = ctx.Children.AsNoTracking().Where(child => child.GroupNum == groupNum).ToList();

                //Check payment
                foreach (var child in children)
                {
                    result += "Surname: " + child.Surname + ", name: " + child.Name + ", num: " + child.BirthSertificateSerNum +
                        ", months not paid: " + ctx.Payments.AsNoTracking().Where(payment => payment.BirthSertificateSerNum == child.BirthSertificateSerNum).First().MonthNotPayed + '\0';
                }

                return result;
            }
        }

        public List<shortChild> NoteTheVisit(DateTime date)
        {
            using (SadickContext ctx = new())
            {
                List<shortChild> shortChildren = new List<shortChild>();

                //Find group number
                int? groupNum = ctx.Educators.AsNoTracking().Where(educator => educator.EducatorPhoneNum == UserInformation.phoneNum).First().GroupNum;
                var children = ctx.Children.AsNoTracking().Where(child => child.GroupNum == groupNum).ToList();

                foreach (var child in children)
                {
                    bool IsWas = false;
                    var visiting = ctx.Visitings.AsNoTracking().Where(visit => visit.BirthSertificateSerNum == child.BirthSertificateSerNum && visit.Data == date);
                    if (visiting.Count() != 0)
                        IsWas = true;

                    shortChildren.Add(new shortChild
                    {
                        Name = child.Name,
                        Surname = child.Surname,
                        BirthSertificateSerNum = child.BirthSertificateSerNum,
                        IsWas = IsWas
                    });
                }

                return shortChildren;
            }
        }

        public void ChangeVisit(List<shortChild> shtChild, DateTime date)
        {
            using (SadickContext ctx = new())
            {
                using (SadickContext ctx2 = new())
                {
                    foreach (var el in shtChild)
                    {
                        if (ctx.Visitings.AsNoTracking().Where(visit => visit.BirthSertificateSerNum == el.BirthSertificateSerNum
                        && visit.Data == date).Count() == 0)
                        {
                            if (el.IsWas)
                            {
                                int? id;
                                if (ctx.Visitings.AsNoTracking().IsNullOrEmpty())
                                    id = 0;
                                else
                                    id = ctx.Visitings.AsNoTracking().ToList().Last().Id + 1;

                                ctx2.Visitings.Add(new Visiting() { Id = id, BirthSertificateSerNum = el.BirthSertificateSerNum, Data = date });
                            }
                        }
                        else
                        {
                            if (!el.IsWas)
                            {
                                int? id = ctx.Visitings.AsNoTracking().Where(visit => visit.BirthSertificateSerNum == el.BirthSertificateSerNum
                                && visit.Data == date).First().Id;
                                Visiting visit = new Visiting() { Id = id };
                                ctx2.Visitings.Remove(visit);
                            }
                        }
                    }
                    ctx2.SaveChanges();
                }
            }
        }
    }
}