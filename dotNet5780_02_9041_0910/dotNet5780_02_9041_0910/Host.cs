using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace dotNet5780_02_9041_0910
{
    public class Host : IEnumerable
    {
        public int HostKey;
        public List<HostingUnit> HostingUnitCollection { get; private set; }

        // constructor.
        public Host(int _HostKey, int numOfHostingUnits)
        {
            HostKey = _HostKey;
            HostingUnitCollection = new List<HostingUnit>();

            for (int i = 0; i < numOfHostingUnits; i++)
                HostingUnitCollection.Add(new HostingUnit());
        }

        // returns a strung of all hosting units.
        public override string ToString()
        {
            string s = "";

            foreach (HostingUnit unit in HostingUnitCollection)
                s += '\n' + unit.ToString();


            return s;
        }

        // if can, books requested date and returns the hosting unit's key. otherwise, retorns -1.
        private long SubmitRequest(GuestRequest guestReq)
        {
            foreach (HostingUnit unit in HostingUnitCollection)
                if (unit.ApproveRequest(guestReq)) return unit.HostingUnitKey;

            return -1;
        }

        // returns the sum of occupied days in all hosting units.
        public int GetHostAnnualBusyDays()
        {
            int sum = 0;

            foreach (HostingUnit unit in HostingUnitCollection)
                sum += unit.GetAnnualBusyDays();

            return sum;
        }

        // sorts the hosting units.
        public void SortUnits()
        {
            HostingUnitCollection.Sort();
        }

        // trys to assign a numbuer of requests and returns true if they were all assignd.
        public bool AssignRequests(params GuestRequest[] requests)
        {
            bool allAccepted = true;

            foreach (GuestRequest req in requests)
                if (SubmitRequest(req) == -1) allAccepted = false;

            return allAccepted;
        }

        // returns IEnumerator to the beginning of the list.
        public IEnumerator GetEnumerator()
        {
            return HostingUnitCollection.GetEnumerator();
        }

        // Indexer.
        public HostingUnit this[int i]
        {
            get => HostingUnitCollection[i];
            set => HostingUnitCollection[i] = value;
        }

    }
}
