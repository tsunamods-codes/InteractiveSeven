﻿using InteractiveSeven.Core.Models;

namespace InteractiveSeven.Core.Memory
{
    public class MenuColorAccessor : IMenuColorAccessor
    {
        private readonly IMemoryAccessor _memoryAccessor;

        public MenuColorAccessor(IMemoryAccessor memoryAccessor)
        {
            _memoryAccessor = memoryAccessor;
        }

        public MenuColors GetMenuColors(string processName)
        {
            byte[] topLeftBuffer = new byte[3];
            byte[] botLeftBuffer = new byte[3];
            byte[] topRightBuffer = new byte[3];
            byte[] botRightBuffer = new byte[3];

            _memoryAccessor.ReadMem(processName, MemLoc.MenuTopLeft.Address, topLeftBuffer);
            _memoryAccessor.ReadMem(processName, MemLoc.MenuBotLeft.Address, botLeftBuffer);
            _memoryAccessor.ReadMem(processName, MemLoc.MenuTopRight.Address, topRightBuffer);
            _memoryAccessor.ReadMem(processName, MemLoc.MenuBotRight.Address, botRightBuffer);

            return new MenuColors
            {
                TopLeft = topLeftBuffer.ToColor(),
                BotLeft = botLeftBuffer.ToColor(),
                TopRight = topRightBuffer.ToColor(),
                BotRight = botRightBuffer.ToColor(),
            };
        }

        public void SetMenuColors(string processName, MenuColors menuColors)
        {
            _memoryAccessor.WriteMem(processName, MemLoc.MenuTopLeft.Address, menuColors.TopLeft.AsBytes());
            _memoryAccessor.WriteMem(processName, MemLoc.MenuBotLeft.Address, menuColors.BotLeft.AsBytes());
            _memoryAccessor.WriteMem(processName, MemLoc.MenuTopRight.Address, menuColors.TopRight.AsBytes());
            _memoryAccessor.WriteMem(processName, MemLoc.MenuBotRight.Address, menuColors.BotRight.AsBytes());
        }

    }
}