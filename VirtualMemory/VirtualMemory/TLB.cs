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
            Tuple<int,int,int> spo = AddressTranslator.TranslateToSPO(va); 
            foreach (TLBEntry entry in _table) {
                if (entry.SP == spo.Item1) {
                    
                }
            }
            // return “m” or “h”
            // return PA or “pf” or “err” 
        }

        void DecrementLRUEntries(int? limit)
        {
        }

        #endregion
    }
}
