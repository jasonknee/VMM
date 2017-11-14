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
            return 0;
        }

        public static int TranslateVirtualToSP(int virtualAddress)
        {
            return 0;
        }

        public static Tuple<int, int, int> TranslateVirtualToSPO(int virtualAddress)
        {
            return new Tuple<int, int, int>(0, 0, 0);
        }


        public static int TranslateSPOToVirtualAddress(int segmentNumber, int pageNumber, int offsetPage)
        {
            return 0;
        }
    }
}
