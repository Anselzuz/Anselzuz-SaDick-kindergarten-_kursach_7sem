using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SaDick.UsersLogic.AdminLogic
{
    interface IAdminL
    {
        string AddChild(string birthSertificate, string nameChild, string surnameChild, int groupNum,
            long parentPhoneNum, string nameParent, string surnameParent);

        string DeleteChild(string birthSertificate);
        string ChangeChildGroup(string birthSertificate, int groupNum);

        string AddEducator(long phoneNum, int groupNum, string surname, string name);

        string DeleteEducator(long phoneNum);

        string ChangeEducatorGroup(long phoneNum, int groupNum);

        void Add1MonthToAll();

        void Delete1MonthFromAll();

        string CheckGroupPayment(int groupNum);

        string Add1MonthToTheGroup(int groupNum);

        string Delete1MonthFromTheGroup(int groupNum);

        string Add1MonthToTheChild(string birthSertificate);

        string Delete1MonthFromTheChild(string birthSertificate);

        string CheckNumChildrenInTheGroup(int groupNum);

        string CHeckEducatorsPasw(long phoneNum);

        string CreateGroup(int groupNum);
    }
}