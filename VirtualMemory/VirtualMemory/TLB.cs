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
            int PA = 0;

            if (HasEntryWithSP(sp)) // TLB HIT
            {
                Console.Write("h ");
                int index = GetEntryIndexWithSP(sp);
                DecrementLRU(_table[index].LRU);

                _table[index].Address = _table[index].Address;
                _table[index].LRU = 3;
                PA = _table[index].Address + MemoryUtility.TranslateVirtualToOffset(va);
            }

            else // TLB MISS
            {
                Console.Write("m ");

                Tuple<int, int, int> vector1 = MemoryUtility.TranslateVirtualToSPO(va);
                int index = GetOldestEntryIndex();

                if (index != -1)
                {
                    _table[index].LRU = 3;
                    _table[index].SP = sp; // NEWLY INIT SP ADDRESS
                    _table[index].Address = _pm.ReadPageTableEntry(_pm.ReadSegmentTableEntry(vector1.Item1), vector1.Item2); //  PM[PM[s] + p]
                    DecrementLRUExcept(sp);
                    PA = _table[index].Address + vector1.Item3;
                }

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

        public int GetEntryIndexWithSP(int sp)
        {
            int i = 0;
            foreach (TLBEntry entry in _table)
            {
                if (entry.SP == sp)
                { // TLB HIT
                    return i;
                }
                i++;
            }
            return -1;
        }

        public void DecrementLRU(int limit = 0)
        {
            foreach (TLBEntry entry in _table)
                if (limit < entry.LRU)
                    entry.LRU = entry.LRU - 1;
        }

        public void DecrementLRUExcept(int sp)
        {
            foreach (TLBEntry entry in _table)
                if (entry.SP != sp && entry.LRU > 0)
                    entry.LRU = entry.LRU - 1;
        }

        public int GetOldestEntryIndex()
        {
            int i = 0;
            foreach (TLBEntry entry in _table)
            {
                if (entry.LRU == -1)
                {
                    return i;
                }
                i++;
            }

            i = 0;
            foreach (TLBEntry entry in _table)
            {
                if (entry.LRU == 0)
                {
                    return i;
                }
                i++;
            }

            return -1;
        }


        #endregion
    }
}
