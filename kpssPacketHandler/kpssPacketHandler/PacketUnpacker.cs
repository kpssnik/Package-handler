using System;
using System.Collections.Generic;
using System.Text;

namespace kpssPacketHandler
{
    public static class PacketUnpacker
    {
        public static Packet UnpackPacket(string pack)
        {
            string[] income = pack.Split('%');

            return new Packet(income);
        }

        static PacketType GetPacketType(string type)
        {
            int tp = 0;
            int.TryParse(type, out tp);
            return (PacketType)tp;
        }
    }
}
