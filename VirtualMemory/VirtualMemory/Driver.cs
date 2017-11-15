using System;
using System.IO;
using System.Text;

namespace VirtualMemory
{
    class Driver
    {
        static BitMap _bitMap;
        static PhysicalMem _physicalMemory;

        static void InitFileOne(string[] lines)
        {
            InitPageTables(lines[0].Trim());
            InitPages(lines[1].Trim());
        }

        static void InitPageTables(string sf)
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

        static void InitPages(string psf)
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

        static void InitFileTwo(string[] lines)
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
                        Console.Write("{0} ", re);
                }

                else // WRITE
                {
                    Tuple<int, int, int> vector1 = MemoryUtility.TranslateVirtualToSPO(VA);
                    int wr = _physicalMemory.Write(vector1.Item1, vector1.Item2, vector1.Item3);
                    if (wr != 0 && wr != -1)
                        Console.Write("{0} ", wr);
                }
            }
        }

        static string[] GetFileLines(string file)
        {
            string startupPath = Environment.CurrentDirectory;
            string[] fileLines = System.IO.File.ReadAllLines(@startupPath + "/input/" + file);
            return fileLines;
        }


        static void Main(string[] args)
        {
            FileStream ostrm;
            StreamWriter writer;
            TextWriter oldOut = Console.Out;

            _bitMap = new BitMap(128);
            _physicalMemory = new PhysicalMem(_bitMap);

            Console.WriteLine("Enter input file #1: ");
            string fileOne = Console.ReadLine();
            string[] fileOneLines = GetFileLines(fileOne);
            InitFileOne(fileOneLines);

            Console.WriteLine("Enter input file #2: ");
            string fileTwo = Console.ReadLine();
            string[] fileTwoLines = GetFileLines(fileTwo);


            try
            {
                ostrm = new FileStream("./Redirect.txt", FileMode.OpenOrCreate, FileAccess.Write);
                writer = new StreamWriter(ostrm);
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot open Redirect.txt for writing");
                Console.WriteLine(e.Message);
                return;
            }

            Console.SetOut(writer);
            InitFileTwo(fileTwoLines);
            Console.SetOut(oldOut);
            writer.Close();
            ostrm.Close();

        }
    }
}
