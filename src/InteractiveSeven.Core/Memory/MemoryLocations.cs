﻿using System;

namespace InteractiveSeven.Core.Memory
{
    public class MemLoc
    {
        public IntPtr Address { get; set; }
        public int NumBytes { get; set; }
        private MemLoc(int address, int numBytes = 1)
        {
            Address = new IntPtr(address);
            NumBytes = numBytes;
        }

        internal MemLoc(IntPtr address, int numBytes = 1)
        {
            Address = address;
            NumBytes = numBytes;
        }

        public static readonly MemLoc MenuTopLeft = new MemLoc(0x91EFC8, 3); // order blue, green, red
        public static readonly MemLoc MenuBotLeft = new MemLoc(0x91EFCC, 3);
        public static readonly MemLoc MenuTopRight = new MemLoc(0x91EFD0, 3);
        public static readonly MemLoc MenuBotRight = new MemLoc(0x91EFD4, 3);

        public static readonly MemLoc Gil = new MemLoc(0xDC08B4, 4); // 4 bytes
    }
}