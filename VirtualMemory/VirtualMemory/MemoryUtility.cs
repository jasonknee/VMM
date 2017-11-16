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

            Console.WriteLine("sp = {0} for VA {1}", sp, virtualAddress);
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
            string segmentBinary;
            string pageBinary;
            string offsetBinary;

            segmentBinary = Convert.ToString(segmentNumber, 2);
            pageBinary = Convert.ToString(pageNumber, 2);
            offsetBinary = Convert.ToString(offsetPage, 2);

            while (segmentBinary.Length != 9)
                segmentBinary = string.Concat("0", segmentBinary);
            while (pageBinary.Length != 10)
                pageBinary = string.Concat("0", pageBinary);
            while (offsetBinary.Length != 9)
                offsetBinary = string.Concat("0", offsetBinary);

            string binaryIntInStr = string.Concat(string.Concat(segmentBinary, pageBinary), offsetBinary);
            while (binaryIntInStr.Length != 32)
                binaryIntInStr = string.Concat("0", binaryIntInStr);

            return Convert.ToInt32(binaryIntInStr,2);
        }

        static void MemoryUtilityTest()
        {

            Console.WriteLine("Segment: ");
            string segment = Console.ReadLine();
            int s = Int32.Parse(segment);

            Console.WriteLine("Page: ");
            string page = Console.ReadLine();
            int p = Int32.Parse(page);

            Console.WriteLine("Offset: ");
            string offset = Console.ReadLine();
            int o = Int32.Parse(offset);

            int VA = MemoryUtility.TranslateSPOToVirtualAddress(s, p, o);
            Tuple<int, int, int> spo = MemoryUtility.TranslateVirtualToSPO(VA);

            Console.WriteLine(VA);
            Console.WriteLine("{0} {0} {0}", spo.Item1, spo.Item2, spo.Item3);
        }
    }
}
