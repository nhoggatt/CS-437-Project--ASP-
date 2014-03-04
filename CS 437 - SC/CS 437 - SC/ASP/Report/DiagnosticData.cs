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
            Component[] status;
            double power;

            public DiagnosticData()
            {
                power = retrievePower();
                status = Driver.components.Values.ToArray();
            }

            private double retrievePower()
            {
                Random ran = new Random();
                return (ran.NextDouble() * 100.0);
            }

        }

    }
}