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
        #endregion

        #region Properties
        #endregion

        #region Constructor
        public TLB()
        {
            _table = new TLBEntry[MAX_ENTRIES];
        }
        #endregion

        #region Methods
        void TranslateVirtualAddress(int va)
        {
            int sp = MemoryUtility.TranslateVirtualToSP(va);

            if (HasEntryWithSP(sp)) // TLB HIT
            {

            }

            else // TLB MISS
            {
                TLBEntry newEntry = getEntryWithLRU(0);
                DecrementEntriesLRU();

                newEntry.LRU = 3;
                newEntry.SP = 0; // NEWLY INIT SP ADDRESS
                newEntry.Address = 0; //  PM[PM[s] + p]
            }
            // return “m” or “h”
            // return PA or “pf” or “err” 
        }


        bool HasEntryWithSP(int sp)
        {
            foreach (TLBEntry entry in _table)
                if (entry.SP == sp)
                    return true;
            return false;

        }

        TLBEntry GetEntryWithSP(int sp)
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

        void DecrementEntriesLRU(int limit = 0)
        {
        }

        TLBEntry getEntryWithLRU(int LRU)
        {
            return new TLBEntry();
        }


        #endregion
    }
}
