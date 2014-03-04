using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ASP.SystemControl
{
    class Scheduler
    {
        DateTime startTime;
        long envDataLifetime;
        bool expirationStart;


        string[] reportName = {
                                 "status",
                                 "environmental",
                                 "maintenance"
                             };
        long[] frequency = new long[3];
        DateTime[] lastReport = new DateTime[3];


        public long EnvironmentalDataLifetime
        {
            get { return envDataLifetime; }
            set
            {
                Console.WriteLine("DRIVER:SCHEDULER -> ER New lifetime duration: " + value); expirationStart = false; envDataLifetime = value;
                startTime = Driver.locationTime.Time; ;
            }
        }
        public long Maintenance
        {
            get { return frequency[2]; }
            set
            {
                frequency[2] = value;
            }
        }
        public long Environmental
        {
            get { return frequency[1]; }
            set
            {
                frequency[1] = value;
            }
        }
        public long Status
        {
            get { return frequency[0]; }
            set
            {
                frequency[0] = value;
            }
        }

        public Scheduler()
        {
            Console.WriteLine("DRIVER:SCHEDULER Started.");
            startTime = Driver.locationTime.Time;
            EnvironmentalDataLifetime = 30;

            frequency[0] = 5;
            frequency[1] = 15;
            frequency[2] = 30;

            lastReport[0] = Driver.locationTime.Time;
            lastReport[1] = Driver.locationTime.Time;
            lastReport[2] = Driver.locationTime.Time;

            Thread updateLoop = new Thread(new ThreadStart(update));
            updateLoop.Start();
        }

        public Scheduler(long lifetime, long maintenance, long environmental, long status)
        {
            Console.WriteLine("DRIVER:SCHEDULER Started.");
            startTime = Driver.locationTime.Time;
            EnvironmentalDataLifetime = lifetime;

            frequency[0] = status;
            frequency[1] = environmental;
            frequency[2] = maintenance;

            lastReport[0] = Driver.locationTime.Time;
            lastReport[1] = Driver.locationTime.Time;
            lastReport[2] = Driver.locationTime.Time;

            Thread updateLoop = new Thread(new ThreadStart(update));
            updateLoop.Start();
        }

        void update()
        {
            while (true)
            {
                TimeSpan temp;


                for (int i = 0; i < 3; i++)
                {
                    temp = Driver.locationTime.Time - lastReport[i];
                    if (temp.TotalSeconds >= frequency[i])
                    {
                        //Driver send report.
                        Console.WriteLine("DRIVER:SCHEDULER Time to send " + reportName[i] + " report.");

                        Driver.report.SendReport(i);
                        lastReport[i] = Driver.locationTime.Time;
                    }
                }


                temp = Driver.locationTime.Time - startTime;


                if (temp.TotalSeconds >= envDataLifetime && !expirationStart)
                {
                    Console.WriteLine("DRIVER:SCHEDULER ER has begun deleting old data.");
                    expirationStart = true;
                }

            }

        }
    }
}



    //class Job<T>
    //{
    //    private long time;
    //    private ICollection<T> collection;

    //    public Job(ICollection<T> collection, long time)
    //    {
    //        this.time = time;
    //        this.collection = collection;
    //    }

    //    public long Time
    //    {
    //        get { return time; }
    //        set { time = value; }
    //    }

    //    public ICollection<T> Collection
    //    {
    //        get { return Collection; }
    //        set { Collection = value; }
    //    }
    //}
