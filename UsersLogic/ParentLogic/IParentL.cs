using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaDick.UsersLogic.ParentLogic
{
    interface IParentL
    {
        string CheckPayment();
        string CheckVisitingOnASpecificDate(DateTime date);

        string CheckVisitingFor30DayFromACertainDate(DateTime date);
    }
}
