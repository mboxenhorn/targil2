using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace dotNet5780_02_9041_0910
{
    public class HostingUnit : IComparable
    {
        const int NumOfDaysInAMonth = 31;
        const int NumOfMonthsInAYear = 12;

        private static long stSerialKey = 10000000;
        public long HostingUnitKey { get; private set; }
        private bool[,] diary;
        public HostingUnit()
        {
            HostingUnitKey = stSerialKey++;
            diary = new bool[NumOfMonthsInAYear, NumOfDaysInAMonth];
        }

        //returns details about the hosting unit;
        public override string ToString()
        {
            return "serial number: " + HostingUnitKey + "\n" + OccupiedPeriods();
        }

        //return a string of all occupied periods in diary
        private string OccupiedPeriods()
        {
            string periods = "";

            for (DateTime date = new DateTime(2020, 1, 1); date < new DateTime(2021, 1, 1); date = date.AddDays(1))
            {
                if (this[date] && (date == new DateTime(2020, 1, 1) || !this[date.AddDays(-1)])) // start of an occupied period
                    periods += date.ToString("dd/MM/yy") + " - ";

                if ((date >= new DateTime(2021, 1, 1).AddDays(-1) || !this[date]) && this[date.AddDays(-1)]) // end of an occupied period
                    periods += date.ToString("dd/MM/yy") + '\n';
            }

            return periods + '\n';
        }

        //receiving a guest request for staying in the hosting unit.
        //checks availability in the notified dates and if available signs the guest in the diary
        public bool ApproveRequest(GuestRequest guestReq)
        {
            //correcting numbers to match the array
            DateTime entryDate = guestReq.EntryDate;
            DateTime releaseDate = guestReq.ReleaseDate;
            entryDate.AddDays(-1);
            entryDate.AddMonths(-1);
            releaseDate.AddDays(-1);
            releaseDate.AddMonths(-1);

            //checking if diary is occupied for this request
            if (!CheckAvailability(entryDate, releaseDate)) return false;

            //updating the diary and notifying the GuestRequest
            guestReq.isApproved = true;
            DiaryUpdating(entryDate, releaseDate);
            return true;
        }

        //checking if diary is occupied for a certain request
        private bool CheckAvailability(DateTime entryDate, DateTime releaseDate)
        {
            if (entryDate > releaseDate)
                return false;

            for (DateTime date = entryDate; date < releaseDate; date = date.AddDays(1))
                if (this[date]) return false;
            return true;
        }

        //updating the diary for the request to occupy the indicated dates
        private void DiaryUpdating(DateTime entryDate, DateTime releaseDate)
        {
            for (DateTime date = entryDate; date < releaseDate; date = date.AddDays(1))
                this[date] = true;
        }

        //returns the number of occupied days in the diary
        public int GetAnnualBusyDays()
        {
            //counter
            int numOfBusyDays = 0;

            //for adding the final day of each period to the calculation
            bool inAnOccupiedPeriod = false;

            foreach (bool isOcc in diary)
            {

                if (isOcc)
                {
                    inAnOccupiedPeriod = true;
                    ++numOfBusyDays;
                }
                else if (!isOcc && inAnOccupiedPeriod)
                {
                    inAnOccupiedPeriod = false;
                    ++numOfBusyDays;
                }
            }
            return numOfBusyDays;
        }

        //returns the percentage of occupied days out of the year
        public float GetAnnualBusyPercentage()
        {
            int numOfBusyDays = GetAnnualBusyDays();
            return ((float)numOfBusyDays / (NumOfDaysInAMonth * NumOfMonthsInAYear)) * 100;
        }

        //compares between two hosting units and returns int>0 if this hosting unit 
        //has more occupied days than the other hosting unit(int=0 if they're equal)
        public int CompareTo(object obj)
        {
            return GetAnnualBusyDays() - ((HostingUnit)obj).GetAnnualBusyDays();
        }

        // Indexer.
        public bool this[DateTime date]
        {
            get => diary[date.Month - 1, date.Day - 1];
            set => diary[date.Month - 1, date.Day - 1] = value;
        }
    }
}
