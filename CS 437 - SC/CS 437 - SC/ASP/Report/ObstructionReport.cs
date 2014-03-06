using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP.General;

namespace ASP
{
    namespace Report
    {
        public class ObstructionReport : AbstractReport
        {

            private ObstructionData data;

            public ObstructionData Data
            {
                get { return data; }
            }
          
            public ObstructionReport(ObstructionData data)
            {
                this.data = data;
            }

        }

    }
}