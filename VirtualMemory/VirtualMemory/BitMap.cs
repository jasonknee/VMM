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
            InitMask();
        }
        #endregion

        #region Methods
        public void SetBit(int pos)
        {
            int frameNumber = pos / 8;
            int positionInFrame = pos % 8;
            _bitMap[frameNumber] = (byte)(_bitMap[frameNumber] ^ _mask[positionInFrame]);
        }

        public void UnsetBit(int pos)
        {
            int frameNumber = pos / 8;
            int positionInFrame = pos % 8;
            _bitMap[frameNumber] = (byte)(_bitMap[frameNumber] & ~_mask[positionInFrame]);
        }

        public bool GetBitAvailability(int pos)
        {
            int frameNumber = pos / 8;
            int positionInFrame = pos % 8;
            if ((byte)(_bitMap[frameNumber] & _mask[positionInFrame]) != _mask[positionInFrame]) {
                return true;
            }
            return false;
        }

        public int GetNextAvailableBit()
        {
            for (int i = 0; i < 128; i++) {
                for (int j = 0; j < 8; j++) {
                    if ((byte) (_bitMap[i] & _mask[j]) != _mask[j]) {
                        return i*8 + j;
                    }
                }
            }
            return -1;
        }
        #endregion

        #region Helpers
        void InitMask()
        {
            _mask = new byte[8] { 1, 2, 4, 8, 16, 32, 64, 128 };
        }
        #endregion
    }
}
