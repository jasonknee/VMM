using System;
namespace VirtualMemory
{
    public class BitMap
    {
        #region Fields
        byte[] _bitMap;
        byte[] _mask;
        #endregion

        #region Properties

        public int Size { get; set; }

        #endregion

        #region Constructor
        public BitMap(int size)
        {
            Size = size;
            _bitMap = new byte[Size];
            _mask = new byte[8] { 128, 64, 32, 16, 8, 4, 2, 1 };

            RunInit();
        }
        #endregion

        #region Methods
        public int SetBit(int frameNumber)
        {
            int index = frameNumber / 8;
            int byteDigit = frameNumber % 8;
            _bitMap[index] = (byte)(_bitMap[index] | _mask[byteDigit]);
            return frameNumber;
        }

        public int UnsetBit(int frameNumber)
        {
            int index = frameNumber / 8;
            int byteDigit = frameNumber % 8;
            _bitMap[index] = (byte)(_bitMap[index] & ~_mask[byteDigit]);
            return frameNumber;
        }

        public bool GetBitAvailability(int frameNumber)
        {
            int index = frameNumber / 8;
            int byteDigit = frameNumber % 8;
            if ((byte)(_bitMap[index] & _mask[byteDigit]) != _mask[byteDigit]) {
                return true;
            }
            return false;
        }

        public int GetNextFreeBit()
        {
            for (int i = 0; i < 128; i++) {
                for (int j = 0; j < 8; j++) {
                    if ((byte) (_bitMap[i] & Convert.ToByte(_mask[j])) == 0) {
                        return i*8 + j;
                    }
                }
            }
            return -1;
        }


        public int GetNextConsecutiveFreeBit()
        {
            int startingBit = 0;
            int found = 0;

            for (int i = 0; i < 128; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((byte)(_bitMap[i] & _mask[j]) == 0)
                    {
                        if (found == 0) 
                        {
                            startingBit = i * 8 + j;
                            found = 1;
                        }
                        else if (found == 1)
                        {
                            return startingBit;
                        }
                    }
                    else
                    {
                        found = 0;
                    }
                }
            }
            return -1;
        }

        public int GetBitAt(int loc)
        {

            return (_bitMap[loc / 8] & (1 << (7 - (loc % 8)))) == 0 ? 0 : 1;
        }

        public void PrintMap()
        {
            foreach (var b in _bitMap)
            {
                Console.WriteLine(Convert.ToString(b, 2).PadLeft(8, '0'));
            }
        }
        #endregion

        #region Helpers
        void RunInit()
        {
            SetBit(0);
        }

        #endregion
    }
}
