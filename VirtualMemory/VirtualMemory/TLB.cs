using System;
namespace VirtualMemory
{
    public class TLB
    {
        #region Constants
        int MAX_ENTRIES = 4;
        #endregion

        #region Fields
        TLBEntry[] _table { get; set; }
        PhysicalMem _pm;
        #endregion

        #region Properties
        #endregion

        #region Constructor
        public TLB(PhysicalMem physicalMemRef)
        {
            _table = new TLBEntry[MAX_ENTRIES];
            for (int i = 0; i < MAX_ENTRIES; i++)
            {
                _table[i] = new TLBEntry();
            }
            _pm = physicalMemRef;

        }
        #endregion

        #region Methods
        public int FindTLBMatch(int va)
        {
            int sp = MemoryUtility.TranslateVirtualToSP(va);

            int PA;
            if (HasEntryWithSP(sp)) // TLB HIT
            {
                Console.Write("h ");
                TLBEntry newEntry = GetEntryWithSP(sp);
                PA = newEntry.SP + MemoryUtility.TranslateVirtualToOffset(va);
                DecrementLRU(newEntry.LRU);
                newEntry.LRU = 3;
            }

            else // TLB MISS
            {
                Console.Write("m ");

                Tuple<int, int, int> vector1 = MemoryUtility.TranslateVirtualToSPO(va);
                TLBEntry newEntry = GetOldestEntry();

                newEntry.LRU = 3;
                newEntry.SP = sp; // NEWLY INIT SP ADDRESS
                newEntry.Address = _pm.ReadPageTableEntry(_pm.ReadSegmentTableEntry(vector1.Item1), vector1.Item2); //  PM[PM[s] + p]
                DecrementLRUExcept(newEntry.SP);
                PA = newEntry.Address + vector1.Item3;

            }
            return PA;
        }


        public bool HasEntryWithSP(int sp)
        {
            foreach (TLBEntry entry in _table)
            {
                if (entry.SP == sp)
                {
                    return true;
                }
            }
            return false;
        }

        public TLBEntry GetEntryWithSP(int sp)
        {

            foreach (TLBEntry entry in _table)
            {
                if (entry.SP == sp)
                { // TLB HIT
                    return entry;
                }
            }
            return null;
        }

        public void DecrementLRU(int limit = 0)
        {
            foreach (TLBEntry entry in _table)
                if (entry.LRU > limit)
                    entry.LRU = entry.LRU - 1;
        }

        public void DecrementLRUExcept(int sp)
        {
            foreach (TLBEntry entry in _table)
                if (entry.SP != sp)
                    entry.LRU = entry.LRU - 1;
        }

        public TLBEntry GetOldestEntry()
        {
            foreach (TLBEntry entry in _table)
            {
                if (entry.LRU == -1)
                {
                    return entry;
                }
            }

            foreach (TLBEntry entry in _table)
            {
                if (entry.LRU == 0)
                {
                    return entry;
                }
            }

            return new TLBEntry();
        }


        #endregion
    }
}
