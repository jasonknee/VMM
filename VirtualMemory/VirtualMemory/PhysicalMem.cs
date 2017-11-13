using System;
namespace VirtualMemory
{
    public class PhysicalMem
    {

        #region Fields
        int[] _memory;
        #endregion

        #region Constructor
        public PhysicalMem()
        {
            _memory = new int[512 * 1024];
        }
        #endregion
    }
}
