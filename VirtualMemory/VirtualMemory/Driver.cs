using System;

namespace VirtualMemory
{
    class Driver
    {
        static BitMap _bitMap;
        static PhysicalMem _physicalMemory;

        static void initFileOne(string[] lines)
        {
            initPageTables(lines[0].Trim());
            initPages(lines[1].Trim());
        }

        static void initPageTables(string sf)
        {
            string[] arrayOfInputs = sf.Split();
            for (int i = 0, s, f; i < arrayOfInputs.Length; i=i+2)
            {
                s = Int32.Parse(arrayOfInputs[i]);
                f = Int32.Parse(arrayOfInputs[i+1]);

                _physicalMemory.InsertIntoMemory(s, f);
                _bitMap.SetBit(s / 512);
            }
        }

        static void initPages(string psf)
        {
            string[] arrayOfInputs = psf.Split();
            for (int i = 0, p, s, f; i < arrayOfInputs.Length; i = i + 3)
            {
                p = Int32.Parse(arrayOfInputs[i]);
                s = Int32.Parse(arrayOfInputs[i+1]);
                f = Int32.Parse(arrayOfInputs[i+2]);

                int ptAdd = _physicalMemory.ReadSegmentTableEntry(s);
                _physicalMemory.InsertIntoMemory(ptAdd+p, f);
                if (f > 511)
                    _bitMap.SetBit(f / 512);
            }
        }

        static void initFileTwo(string[] lines)
        {
            string[] arrayOfInputs = lines[0].Trim().Split();
            for (int i = 0, o, VA; i < arrayOfInputs.Length; i = i + 2)
            {
                o = Int32.Parse(arrayOfInputs[i]);
                VA = Int32.Parse(arrayOfInputs[i + 1]);

                if (o == 0) // READ
                {
                    Tuple<int, int, int> vector = MemoryUtility.TranslateVirtualToSPO(VA);
                    int re = _physicalMemory.Read(vector.Item1, vector.Item2, vector.Item3);
                    if (re != 0 && re != -1)
                        Console.WriteLine("{0}", re);
                }

                else // WRITE
                {
                    Tuple<int, int, int> vector1 = MemoryUtility.TranslateVirtualToSPO(VA);
                    int wr = _physicalMemory.Write(vector1.Item1, vector1.Item2, vector1.Item3);
                    if (wr != 0 && wr != -1)
                        Console.WriteLine("{0}", wr);
                }
            }
        }

        static void Main(string[] args)
        {
            _bitMap = new BitMap(128);
            _physicalMemory = new PhysicalMem(_bitMap);


            _physicalMemory.InsertIntoMemory(2, 2048);
            _bitMap.SetBit(2048 / 512);

            string startupPath = Environment.CurrentDirectory;

            Console.WriteLine("Enter input file #1: ");
            string filenameOne = Console.ReadLine();
            string[] fileOneLines = System.IO.File.ReadAllLines(@startupPath + "/input/" + filenameOne);
            initFileOne(fileOneLines);


            Console.WriteLine("Enter input file #2: ");
            string filenameTwo = Console.ReadLine();
            string[] fileTwoLines = System.IO.File.ReadAllLines(@startupPath + "/input/" + filenameTwo);
            initFileTwo(fileTwoLines);
        }
    }
}
