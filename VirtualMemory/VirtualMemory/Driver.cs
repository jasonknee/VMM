using System;

namespace VirtualMemory
{
    class Driver
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var bitMap = new BitMap(128);
            var mem = new PhysicalMem(bitMap);

            Console.WriteLine("Next Consecutive Bit, {0}", bitMap.GetNextConsecutiveFreeBit());

            for (int i = 0; i < 1000; i+=2)
            {
                bitMap.SetBit(i);
            }

            for (int i = 0; i < 1000; i += 3)
            {
                bitMap.SetBit(i);
            }

            int s = 10;
            int p = 256;
            int w = 0;
            int re = mem.Read(s, p, w);
            int wr = mem.Write(s, p, w);
            Console.WriteLine("Reading Line: {0}", re);
            Console.WriteLine("Writing Line: {0}", wr);

            //bitMap.UnsetBit(620);

            Console.WriteLine("Next Available Bit, {0}", bitMap.GetNextFreeBit());
            Console.WriteLine("Next Consecutive Bit, {0}", bitMap.GetNextConsecutiveFreeBit());

            //for (int i = 0; i < 20; i++) {
                //Console.WriteLine("i = {0}", i);
                //Console.WriteLine("{0}", bitMap.GetBitAvailability(i));
                //int j = bitMap.GetNextFreeBit();
                //Console.WriteLine("Next Available Bit, {0}", j);
                //bitMap.SetBit(j);
                //bitMap.UnsetBit (j);

            //}

        }
    }
}
