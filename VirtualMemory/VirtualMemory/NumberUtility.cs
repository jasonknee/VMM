using System;
namespace VirtualMemory
{
    public static class NumberUtility
    {
        #region Methods
        public static byte ConvertIntToByte(int i)
        {
            return Convert.ToByte(i);
        }

        public static int ConvertByteToInt(byte b) 
        {
            return Convert.ToInt32(b);   
        }
        #endregion
    }
}
