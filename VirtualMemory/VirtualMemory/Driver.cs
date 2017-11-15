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

            mem.InsertIntoMemory(2, 2048);
            bitMap.SetBit(2048 / 512);

            mem.InsertIntoMemory(2048, 512);
            mem.InsertIntoMemory(2048 + 1, -1);

            Tuple<int, int, int> vector = MemoryUtility.TranslateVirtualToSPO(0);
            int re = mem.Read(vector.Item1, vector.Item2, vector.Item3);

            vector = MemoryUtility.TranslateVirtualToSPO(1048576);
            re = mem.Read(vector.Item1, vector.Item2, vector.Item3);
            Console.WriteLine("Reading Line: {0}", re);


            Tuple<int, int, int> vector1 = MemoryUtility.TranslateVirtualToSPO(1048586);
            int wr = mem.Write(vector1.Item1, vector1.Item2, vector1.Item3);
            Console.WriteLine("Writing Line: {0}", wr);

            Tuple<int, int, int> vector2 = MemoryUtility.TranslateVirtualToSPO(1049088);
            int wr1 = mem.Write(vector2.Item1, vector2.Item2, vector2.Item3);

        }
    }
}
