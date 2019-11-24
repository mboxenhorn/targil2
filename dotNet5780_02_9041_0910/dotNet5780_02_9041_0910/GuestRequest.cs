using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5780_02_9041_0910
{
    public class GuestRequest
    {
        public DateTime EntryDate;   // a desirable date for the start of the holiday.
        public DateTime ReleaseDate;   // a desirable date for ending the holiday.
        public bool isApproved;   // whether or not the application was approved.

        //
        public GuestRequest(DateTime entry, DateTime release)
        {
            EntryDate = entry;
            ReleaseDate = release;
            isApproved = false;
        }

        public GuestRequest()
        {
            EntryDate = new DateTime();
            ReleaseDate = new DateTime();
            isApproved = false;
        }

        // 
        public override string ToString()
        {
            return "----------------------------------" +
                "\nentry date: " + EntryDate +
                "\nrelease date: " + ReleaseDate +
                "\nis approved: " + isApproved +
                "\n----------------------------------";
        }

    }
}
