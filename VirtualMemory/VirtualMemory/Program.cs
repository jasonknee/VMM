using System;

namespace VirtualMemory
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var bitMap = new BitMap(128);

            for (int i = 0; i < 1023; i++) {
                bitMap.SetBit(i);
            }

            bitMap.UnsetBit(620);


            Console.WriteLine("Next Available Bit, {0}", bitMap.GetNextAvailableBit());

            //for (int i = 0; i < 20; i++) {
            //    Console.WriteLine("{0}", bitMap.GetBitAvailability(i));
            //    int j = bitMap.GetNextAvailableBit();
            //    Console.WriteLine("Next Available Bit, {0}", j);
            //    bitMap.SetBit(j);
            //    //bitMap.UnsetBit (j);

            //}


        }
    }
}
