using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;



    public class BitArrayUint32
    {
        public int Length { get { return (array.Length); } }
        public BitArray array;
        public UInt64 GetInt() { return (BitConverter.ToUInt64(BitArrayToByteArray(array), 0)); }
        public BitArrayUint32(UInt64 startingValue = 0)
        {
            this.SetInt(startingValue);
        }
        public void SetInt(UInt64 v)
        {
            byte[] byteArray = BitConverter.GetBytes(v);
            array = new BitArray(byteArray);
        }

        public void SetBit(int index, bool v) => array.Set(index, v);
        public bool GetBit(int index) => array.Get(index);

        public string GetBase64() { return Convert.ToBase64String(BitArrayToByteArray(array)); }
        public void SetFromBase64(string s)
        {
            array = new BitArray(Convert.FromBase64String(s));
        }
        public static BitArrayUint32 FromBase64(string s)
        {
            return (new BitArrayUint32()
            {
                array = new BitArray(Convert.FromBase64String(s))
            });
        }
        public override string ToString()
        {
            string s = "";
            foreach (bool item in array)
            {
                s += (item ? "1" : "0");
            }
            return s;
        }
        private byte[] BitArrayToByteArray(BitArray bits)
        {
            byte[] ret = new byte[(bits.Length - 1) / 8 + 1];
            bits.CopyTo(ret, 0);
            return ret;
        }
        public BitArrayUint32() { this.SetInt(0); }
    }

