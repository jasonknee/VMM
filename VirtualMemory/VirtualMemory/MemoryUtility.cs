using System;
namespace VirtualMemory
{
    public static class MemoryUtility
    {

        public static int TranslateVirtualToSegment(int virtualAddress)
        {
            return 0;
        }

        public static int TranslateVirtualToPage(int virtualAddress)
        {
            return 0;
        }

        public static int TranslateVirtualToPhysical(int virtualAddress)
        {
            return 0;
        }

        public static int TranslateVirtualToOffset(int virtualAddress)
        {
            string binaryIntInStr = Convert.ToString(virtualAddress, 2);
            while (binaryIntInStr.Length != 32)
                binaryIntInStr = string.Concat("0", binaryIntInStr);

            string offsetStr = binaryIntInStr.Substring(23, 9);
            int offset = Convert.ToInt32(offsetStr, 2);
            return offset;
        }

        public static int TranslateVirtualToSP(int virtualAddress)
        {
            string binaryIntInStr = Convert.ToString(virtualAddress, 2);
            while (binaryIntInStr.Length != 32)
                binaryIntInStr = string.Concat("0", binaryIntInStr);

            string spStr = binaryIntInStr.Substring(4, 19);
            int sp = Convert.ToInt32(spStr, 2);
            return sp;
        }

        public static Tuple<int, int, int> TranslateVirtualToSPO(int virtualAddress)
        {
            string binaryIntInStr = Convert.ToString(virtualAddress, 2);
            while (binaryIntInStr.Length != 32) 
                binaryIntInStr = string.Concat("0", binaryIntInStr);

            string segmentStr = binaryIntInStr.Substring(4, 9);
            string pageStr = binaryIntInStr.Substring(13, 10);
            string offsetStr = binaryIntInStr.Substring(23, 9);

            int segment = Convert.ToInt32(segmentStr, 2);
            int page = Convert.ToInt32(pageStr, 2);
            int offset = Convert.ToInt32(offsetStr, 2);

            return new Tuple<int, int, int>(segment, page, offset);
        }


        public static int TranslateSPOToVirtualAddress(int segmentNumber, int pageNumber, int offsetPage)
        {
            return 0;
        }
    }
}
