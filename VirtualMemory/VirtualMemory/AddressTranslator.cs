using System;
namespace VirtualMemory
{
    public static class AddressTranslator
    {
        public static Tuple<int,int,int> TranslateToSPO(int virtualAddress)
        {
            return new Tuple<int, int, int>(0, 0, 0);
        }

        public static int TranslateToVirtualAddress(int segmentNumber, int pageNumber, int pageOffset)
        {
            return 0;
        }
    }
}
