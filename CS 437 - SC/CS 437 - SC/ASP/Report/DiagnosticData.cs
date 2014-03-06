using System;
using ASP.SystemControl;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP.General;

namespace ASP
{
    namespace Report
    {
        public class DiagnosticData
        {
            private Component[] status;
            private double power;


            public double Power
            {
                get { return power; }
            }
            public Component[] Status
            {
                get { return status; }
            }

            public DiagnosticData()
            {
                power = retrievePower();
                status = Driver.storedComponents.ListComponent().ToArray();
            }

            private double retrievePower()
            {
                Random ran = new Random();
                return (ran.NextDouble() * 100.0);
            }

        }

    }
}