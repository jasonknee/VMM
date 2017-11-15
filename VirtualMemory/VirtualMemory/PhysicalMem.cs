using System;
namespace VirtualMemory
{
    public class PhysicalMem
    {

        #region Fields
        int[] _memory;
        BitMap _bitmap;
        #endregion

        #region Constructor
        public PhysicalMem(BitMap bitmapRef)
        {
            _memory = new int[512 * 1024];
            _bitmap = bitmapRef;
        }
        #endregion

        #region Method
        public int Read(int segmentNumber, int pageNumber, int offset)
        {
            int pageAddress;
            int pageTableAddress = ReadSegmentTableEntry(segmentNumber);

            if (IsSegmentTableEntryValid(segmentNumber))
            {
                Console.Write("pf ");
                return -1; // OUTPUT: 'pf'
            }
               
            if (IsPageTableEntryInvalid(pageTableAddress, pageNumber))
            {
                Console.Write("pf ");
                return -1; // OUTPUT: 'pf'
            }

            if (IsSegmentTableEntryFree(segmentNumber))
            {
                Console.Write("err ");
                return 0; // OUTPUT: 'err'
            }

            if (IsPageTableEntryFree(pageTableAddress, pageNumber))
            {
                Console.Write("err ");
                return 0; // OUTPUT: 'err'
            }

            pageAddress = ReadSegmentTableEntry(pageTableAddress + pageNumber);
            return pageAddress + offset;
        }

        public int Write(int segmentNumber, int pageNumber, int offset)
        {
            int pageAddress = 0;
            int pageTableAddress;


            if (IsSegmentTableEntryValid(segmentNumber)){
                Console.Write("pf ");
                return -1; // OUTPUT: 'pf'
            }

            if (IsSegmentTableEntryFree(segmentNumber))
            {
                int newPageTableAddress = AllocateNewPageTable();
                InsertIntoMemory(segmentNumber, newPageTableAddress);
            }

            pageTableAddress = ReadSegmentTableEntry(segmentNumber);
            if (IsPageTableEntryInvalid(pageTableAddress, pageNumber))
            {
                Console.Write("pf ");
                return -1; // OUTPUT: 'pf'
            }

            if (IsPageTableEntryFree(pageTableAddress, pageNumber))
            {
                pageAddress = AllocateNewPage();
                InsertIntoMemory(pageNumber + pageTableAddress, pageAddress);
            }

            pageAddress = ReadPageTableEntry(pageTableAddress, pageNumber);
            return pageAddress + offset;
        }
        #endregion

        #region Helpers
        public bool IsSegmentTableEntryFree(int segmentNumber) => (ReadFromMemory(segmentNumber) == 0);
        public bool IsSegmentTableEntryValid(int segmentNumber) => (ReadFromMemory(segmentNumber) == -1);
        public int ReadSegmentTableEntry(int segmentAddress) => ReadFromMemory(segmentAddress);

        public bool IsPageTableEntryFree(int pageTableAddress, int pageNumber) => (ReadFromMemory(pageTableAddress + pageNumber) == 0);
        public bool IsPageTableEntryInvalid(int pageTableAddress, int pageNumber) => (ReadFromMemory(pageTableAddress + pageNumber) == -1);
        public int ReadPageTableEntry(int pageTableAddress, int pageNumber) => ReadFromMemory(pageTableAddress + pageNumber);

        public int AllocateNewPage() 
        {
            int frameNumber = _bitmap.GetNextFreeBit();
            _bitmap.SetBit(frameNumber);
            return frameNumber * 512;
        }

        public int AllocateNewPageTable()
        {
            int frameNumber = _bitmap.GetNextConsecutiveFreeBit();
            _bitmap.SetBit(frameNumber);
            _bitmap.SetBit(frameNumber + 1);
            return frameNumber * 512;
        }

        public int ReadFromMemory(int address)
        {
            return _memory[address];
        }

        public void InsertIntoMemory(int address, int value)
        {
            _memory[address] = value;
        }

        #endregion
    }
}
