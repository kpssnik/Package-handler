using System;
using System.Collections.Generic;
using System.Text;

namespace kpssPacketHandler
{
    public static class PacketPacker
    {
        public static string PackPacket(Packet packet)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var a in packet.values) sb.Append(a + '%');

            return sb.ToString().TrimEnd('%');
        }

    }

}
