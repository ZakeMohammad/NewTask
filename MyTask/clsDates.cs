using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTask
{
   public class clsDates
    {
        public clsDates()
        {
            this.CurrentDate = DateTime.UtcNow.AddHours(3);
        }
        public DateTime CurrentDate;
        public DateTime earlyMorningStart; public DateTime earlyMorningEnd;
        public DateTime sunriseStart; public DateTime sunriseEnd;
        public DateTime morningStart; public DateTime morningEnd;
        public DateTime noonStart; public DateTime noonEnd;
        public DateTime eveningStart; public DateTime eveningEnd;
        public DateTime sunsetStart; public DateTime sunsetEnd;
        public DateTime nightStart; public DateTime nightEnd;

    }
}
