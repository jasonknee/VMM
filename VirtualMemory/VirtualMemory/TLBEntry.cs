using System;
namespace VirtualMemory
{
    public class TLBEntry
    {
        #region Properties
        public int LRU { get; set; }
        public int SP { get; set; }
        public int Address { get; set; } // PA of sp
        #endregion

        #region Constructor
        public TLBEntry()
        {
            SP = -1;
            Address = -1;
        }
        #endregion
    }
}
