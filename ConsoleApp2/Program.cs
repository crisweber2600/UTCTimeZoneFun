using System;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime now = DateTime.Now;
            bool CurrentDST = now.IsDaylightSavingTime();
            DateTime start = new DateTime(2021, 10, 31, 17, 0, 0).ToUniversalTime();
            DateTime end = start.AddHours(7);
            ProcessedTime newTime = SetNewTime(start, end);

        }

        private static ProcessedTime SetNewTime(DateTime start, DateTime end)
        {
            ProcessedTime oldTime = new ProcessedTime();
            oldTime.End = end;
            oldTime.Start = start;
            ProcessedTime newTime = new ProcessedTime();
            DateTime NewStart = start.AddDays(20);
            DateTime NewEnd = end.AddDays(20);
            newTime.Start = NewStart;
            newTime.End = NewEnd;
            newTime = processDST(newTime,oldTime);
            return newTime;
        }

        private static ProcessedTime processDST(ProcessedTime newTime, ProcessedTime oldTime)
        {
            ProcessedTime oldLocalTime = oldTime;
            ProcessedTime newLocalTime = newTime;

            
            oldLocalTime.Start = oldLocalTime.Start.ToLocalTime();
            oldLocalTime.End = oldLocalTime.End.ToLocalTime();
            newLocalTime.Start = newLocalTime.Start.ToLocalTime();
            newLocalTime.End = newLocalTime.End.ToLocalTime();
            oldLocalTime.StartDST = oldLocalTime.Start.IsDaylightSavingTime();
            oldLocalTime.EndDST = oldLocalTime.End.IsDaylightSavingTime();
            newLocalTime.StartDST = newLocalTime.Start.IsDaylightSavingTime();
            newLocalTime.EndDST = newLocalTime.End.IsDaylightSavingTime();
            if(newLocalTime.StartDST ==true && oldLocalTime.StartDST == false)
            {
                newTime.Start = ChangeTime(newTime.Start, -1);
            }
            if (newLocalTime.EndDST == true && oldLocalTime.EndDST == false)
            {
                newTime.End = ChangeTime(newTime.End, -1);
            }

            if (newLocalTime.StartDST == false && oldLocalTime.StartDST == true)
            {
                newTime.Start = ChangeTime(newTime.Start, 1);
            }
            if (newLocalTime.EndDST == false && oldLocalTime.EndDST == true)
            {
                newTime.End = ChangeTime(newTime.End, 1);
            }

            return newTime;
        }

        private static DateTime ChangeTime(DateTime time, int hoursChange)
        {
            return time.AddHours(hoursChange);
        }
    }
}
