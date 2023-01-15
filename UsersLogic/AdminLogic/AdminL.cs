using Microsoft.EntityFrameworkCore;
using SaDick.Context;
using SaDick.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaDick.UsersLogic.AdminLogic
{
    class AdminL : IAdminL
    {
        public string AddChild(string birthSertificate, string nameChild, string surnameChild, int groupNum,
            long parentPhoneNum, string nameParent, string surnameParent)
        {
            string result = "";
            using (SadickContext ctx = new())
            {
                using (SadickContext ctx2 = new())
                {
                    //Does child in system?
                    if (ctx.Children.AsNoTracking().Where(child => child.BirthSertificateSerNum == birthSertificate).Count() != 0)
                    {
                        result = "Such a child is already in system.";
                        return result;
                    }

                    //Check num of children in the group (cant be more 30)
                    if (ctx.Children.AsNoTracking().Where(children => children.GroupNum == groupNum).Count() > 29)
                    {
                        result = "Number of children cant be more 30.";
                        return result;
                    }

                    //Does such a group exist?
                    if (ctx.ChildGroups.AsNoTracking().Where(group => group.GroupNum == groupNum).Count() == 0)
                    {
                        result = "There is no such group in the system.";
                        return result;
                    }
                    else
                    {
                        //Add 1 to children count in group
                        var groups = ctx2.ChildGroups.Where(group => group.GroupNum == groupNum);
                        foreach (var group in groups)
                            group.NumOfChild++;
                    }

                    //Add child
                    ctx2.Children.Add(new()
                    {
                        BirthSertificateSerNum = birthSertificate,
                        GroupNum = groupNum,
                        Name = nameChild,
                        Surname = surnameChild,
                        ParentPhoneNum = parentPhoneNum
                    });

                    //Add child to the payment
                    ctx2.Payments.Add(new() { BirthSertificateSerNum = birthSertificate, MonthNotPayed = 0 });

                    //Does parent in system?
                    if (ctx.Parents.AsNoTracking().Where(parent => parent.ParentPhoneNum == parentPhoneNum).Count() == 0)
                    {
                        ctx2.Parents.Add(new() { ParentPhoneNum = parentPhoneNum, Surname = surnameParent, Name = nameParent });
                        ctx2.LoginParents.Add(new() { ParentPhoneNum = parentPhoneNum, Password = GeneratePassword(12) });
                    }

                    ctx2.SaveChanges();
                    result = "Success!";
                }
            }

            return result;
        }

        public string DeleteChild(string birthSertificate)
        {
            string result = "";
            using (SadickContext ctx = new())
            {
                using (SadickContext ctx2 = new())
                {
                    //Find child
                    if (ctx.Children.Where(child => child.BirthSertificateSerNum == birthSertificate).Count() == 0)
                    {
                        result = "There is no such child in the system.";
                        return result;
                    }

                    //Remove parent if it only has 1 child
                    long? parentPhoneNum = ctx.Children.Where(child => child.BirthSertificateSerNum == birthSertificate)
                        .ToList().First().ParentPhoneNum;
                    if (ctx.Children.AsNoTracking().Where(child => child.ParentPhoneNum == parentPhoneNum).Count() == 1)
                    {
                        ctx2.LoginParents.Remove(new() { ParentPhoneNum = (long)parentPhoneNum });
                        ctx2.Parents.Remove(new() { ParentPhoneNum = (long)parentPhoneNum });
                    }

                   //Remove payment
                    ctx2.Payments.Remove(new() { BirthSertificateSerNum = birthSertificate });

                    //Increment from group.childCount
                    int? childGroup = ctx.Children.Where(child => child.BirthSertificateSerNum == birthSertificate).ToList().First().GroupNum;
                    var childGroups = ctx2.ChildGroups.Where(group => group.GroupNum == childGroup);
                    foreach (var group in childGroups)
                        group.NumOfChild--;

                    //Delete visitings
                    var visitings = ctx.Visitings.Where(visit => visit.BirthSertificateSerNum == birthSertificate);
                    foreach (var visit in visitings)
                        ctx2.Visitings.Remove(new() { Id = visit.Id });

                    //Delete child
                    ctx2.Children.Remove(new() { BirthSertificateSerNum = birthSertificate });

                    ctx2.SaveChanges();
                    result = "Success";
                }
            }

            return result;
        }

        public string ChangeChildGroup(string birthSertificate, int groupNum)
        {
            string result = "";

            using (SadickContext ctx = new())
            {
                using (SadickContext ctx2 = new())
                {
                    //Check group num
                    if (ctx.ChildGroups.AsNoTracking().Where(group => group.GroupNum == groupNum).Count() == 0)
                    {
                        result = "There are no groups with this number.";
                        return result;
                    }

                    //Check child
                    var children = ctx.Children.Where(child => child.BirthSertificateSerNum == birthSertificate);
                    if (children.Count() == 0)
                    {
                        result = "There are no child with this birth sertificate number.";
                        return result;
                    }
                    else
                    {
                        foreach (var child in children)
                        {
                            int? oldGroupNum = child.GroupNum;
                            if (child.GroupNum == groupNum)
                            {
                                result = "Child already has a such group number.";
                                return result;
                            }
                            else
                                child.GroupNum = groupNum;

                            var childGroups = ctx2.ChildGroups.Where(group => group.GroupNum == oldGroupNum);
                            foreach (var group in childGroups)
                                group.NumOfChild--;
                            childGroups = ctx2.ChildGroups.Where(group => group.GroupNum == groupNum);
                            foreach (var group in childGroups)
                                group.NumOfChild++;
                        }

                        ctx.SaveChanges();
                        ctx2.SaveChanges();
                        result = "Success!";
                    }
                }
            }

            return result;
        }

        public string AddEducator(long phoneNum, int groupNum, string surname, string name)
        {
            string result = "";

            using (SadickContext ctx = new())
            {
                //Check educators
                if(ctx.Educators.AsNoTracking().Where(educator => educator.EducatorPhoneNum == phoneNum).Count() != 0)
                {
                    result = "There is already a educator with the same phone number.";
                    return result;
                }

                //Check groups
                if (ctx.ChildGroups.AsNoTracking().Where(group => group.GroupNum == groupNum).Count() == 0)
                {
                    result = "There are no groups with such group number.";
                    return result;
                }

                //Add educator
                ctx.Educators.Add(new Educator() { EducatorPhoneNum = phoneNum, GroupNum = groupNum,
                    Name = name, Surname = surname});

                //Add educator login
                ctx.LoginEducators.Add(new LoginEducator() { EducatorPhoneNum = phoneNum, Password = GeneratePassword(12) });

                ctx.SaveChanges();
                result = "Success!";
            }

            return result;
        }

        public string DeleteEducator(long phoneNum)
        {
            string result = "";

            using (SadickContext ctx = new())
            {
                //Check educator
                if (ctx.Educators.AsNoTracking().Where(educator => educator.EducatorPhoneNum == phoneNum).Count() == 0)
                {
                    result = "There are no educators with such phone number.";
                    return result;
                }

                //Delete educator
                ctx.Educators.Remove(new Educator() { EducatorPhoneNum = phoneNum});

                //Delete login educator
                ctx.LoginEducators.Remove(new LoginEducator() { EducatorPhoneNum = phoneNum });

                ctx.SaveChanges();
                result = "Success";

            }

            return result;
        }

        public string ChangeEducatorGroup(long phoneNum, int groupNum)
        {
            string result = "";

            using (SadickContext ctx = new())
            {
                //Check educator
                if (ctx.Educators.AsNoTracking().Where(educator => educator.EducatorPhoneNum == phoneNum).Count() == 0)
                {
                    result = "There are no educators with such phone number.";
                    return result;
                }

                //Check group
                if (ctx.ChildGroups.AsNoTracking().Where(group => group.GroupNum == groupNum).Count() == 0)
                {
                    result = "There are no groups with such group number.";
                    return result;
                }

                //Change group
                var educators = ctx.Educators.Where(educator => educator.EducatorPhoneNum == phoneNum);
                foreach (var educator in educators)
                {
                    if (educator.GroupNum == groupNum)
                    {
                        result = "Educator already has a such group number.";
                        return result;
                    }
                    else
                    {
                        educator.GroupNum = groupNum;
                    }
                }

                ctx.SaveChanges();
                result = "Success";
            }

            return result;
        }

        public void Add1MonthToAll()
        {
            using (SadickContext ctx = new())
            {
                var payments = ctx.Payments;
                foreach (var payment in payments)
                    payment.MonthNotPayed++;
                ctx.SaveChanges();
            }
        }

        public void Delete1MonthFromAll()
        {
            using (SadickContext ctx = new())
            {
                var payments = ctx.Payments;
                foreach (var payment in payments)
                    payment.MonthNotPayed--;
                ctx.SaveChanges();
            }
        }

        public string CheckGroupPayment(int groupNum)
        {
            string result = "";

            using (SadickContext ctx = new())
            {
                using (SadickContext ctx2 = new())
                {
                    //Check group
                    if (ctx.ChildGroups.AsNoTracking().Where(group => group.GroupNum == groupNum).Count() == 0)
                    {
                        result = "There are no groups with such group number.";
                        return result;
                    }

                    var children = ctx.Children.AsNoTracking().Where(child => child.GroupNum == groupNum);

                    foreach (var child in children)
                    {
                        result += child.Surname + ", " + child.Name + ", " + child.BirthSertificateSerNum + ", month not payed: "
                            + ctx2.Payments.Where(payment => payment.BirthSertificateSerNum == child.BirthSertificateSerNum).First().MonthNotPayed + '\n';
                    }
                }
            }

            return result;
        }

        public string Add1MonthToTheGroup(int groupNum)
        {
            string result = "";

            using (SadickContext ctx = new())
            {
                using (SadickContext ctx2 = new())
                {
                    //Check group
                    if (ctx.ChildGroups.AsNoTracking().Where(group => group.GroupNum == groupNum).Count() == 0)
                    {
                        result = "There are no groups with such group number.";
                        return result;
                    }

                    //Add 1 month
                    var children = ctx.Children.AsNoTracking().Where(child => child.GroupNum == groupNum);

                    foreach (var child in children)
                    {
                        var payments = ctx2.Payments.Where(payment => payment.BirthSertificateSerNum == child.BirthSertificateSerNum);
                        foreach (var payment in payments)
                            payment.MonthNotPayed++;
                    }

                    ctx2.SaveChanges();
                    result += "Success";
                }
            }

            return result;
        }

        public string Delete1MonthFromTheGroup(int groupNum)
        {
            string result = "";

            using (SadickContext ctx = new())
            {
                using (SadickContext ctx2 = new())
                {
                    //Check group
                    if (ctx.ChildGroups.AsNoTracking().Where(group => group.GroupNum == groupNum).Count() == 0)
                    {
                        result = "There are no groups with such group number.";
                        return result;
                    }

                    //Add 1 month
                    var children = ctx.Children.AsNoTracking().Where(child => child.GroupNum == groupNum);

                    foreach (var child in children)
                    {
                        var payments = ctx2.Payments.Where(payment => payment.BirthSertificateSerNum == child.BirthSertificateSerNum);
                        foreach (var payment in payments)
                            payment.MonthNotPayed--;
                    }

                    ctx2.SaveChanges();
                    result += "Success";
                }
            }

            return result;
        }

        public string Add1MonthToTheChild(string birthSertificate)
        {
            string result = "";

            using (SadickContext ctx = new())
            {
                //Check child
                var payment = ctx.Payments.Where(payment => payment.BirthSertificateSerNum == birthSertificate);
                if (payment.Count() == 0)
                {
                    result = "There is no child with such birth sertificate.";
                    return result;
                }
                else
                {
                    foreach (var el in payment)
                        el.MonthNotPayed++;
                }

                ctx.SaveChanges();
                result = "Success!";
            }

            return result;
        }

        public string Delete1MonthFromTheChild(string birthSertificate)
        {
            string result = "";

            using (SadickContext ctx = new())
            {
                //Check child
                var payment = ctx.Payments.Where(payment => payment.BirthSertificateSerNum == birthSertificate);
                if (payment.Count() == 0)
                {
                    result = "There is no child with such birth sertificate.";
                    return result;
                }
                else
                {
                    foreach (var el in payment)
                        el.MonthNotPayed--;
                }

                ctx.SaveChanges();
                result = "Success!";
            }

            return result;
        }

        public string CheckNumChildrenInTheGroup(int groupNum)
        {
            string result = "";

            using (SadickContext ctx = new())
            {
                //Check group
                if (ctx.ChildGroups.AsNoTracking().Where(group => group.GroupNum == groupNum).Count() == 0)
                {
                    result = "There are no groups with such group number.";
                    return result;
                }

                //Check num of children
                result += "Number of children in group " + groupNum + ": " + ctx.ChildGroups.AsNoTracking().Where(group => group.GroupNum == groupNum).ToList().First().NumOfChild;
            }

            return result;
        }

        public string CHeckEducatorsPasw(long phoneNum)
        {
            string result = "";

            using (SadickContext ctx = new())
            {
                //Check educator
                if (ctx.Educators.AsNoTracking().Where(educator => educator.EducatorPhoneNum == phoneNum).Count() == 0)
                {
                    result = "There is no educator with such phone number.";
                    return result;
                }

                //Check educator pasword
                result += "Login: " + phoneNum + "\nPassword: " + ctx.LoginEducators.AsNoTracking().Where(login => login.EducatorPhoneNum == phoneNum).ToList().First().Password;
            }

            return result;
        }

        public string CreateGroup(int groupNum)
        {
            string result = "";

            using (SadickContext ctx = new())
            {
                //Check group
                if (ctx.ChildGroups.AsNoTracking().Where(group => group.GroupNum == groupNum).Count() != 0)
                {
                    result = "This group is already exists.";
                    return result;
                }

                //Create group
                ctx.ChildGroups.Add(new ChildGroup() { GroupNum = groupNum, NumOfChild = 0 });
                ctx.SaveChanges();

                result += "Group " + groupNum + " has been created.";
            }

            return result;
        }

        private string GeneratePassword(int numSymb)
        {
            if (numSymb < 5)
                throw new Exception("< 5 symb password.");

            StringBuilder symbs = new StringBuilder(77);
            symbs.AppendLine("ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz!@$?_-.,:;[]{}/1234567890");

            string result = "";
            Random rnd = new();

            //Add simple symbs
            for (int i = 0; i < numSymb; i++)
            {
                int randNum = rnd.Next(0, 76);
                result += symbs[randNum];
            }

            return result;
        }
    }
}