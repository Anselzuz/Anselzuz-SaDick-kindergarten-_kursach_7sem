using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaDick.EducatorLogic
{
    interface IEducatorL
    {
        public string CheckPayment();
        public List<shortChild> NoteTheVisit(DateTime date);
        public string CheckParentPasw(long phoneNum);
        public void ChangeVisit(List<shortChild> shtChild, DateTime date);
    }
}
