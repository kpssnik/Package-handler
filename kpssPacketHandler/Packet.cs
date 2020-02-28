using System;
using System.Collections.Generic;
using System.Text;

namespace kpssPacketHandler
{
    public class Packet
    {
        public List<string> Values = new List<string>();
        public Packet(params object[] args)
        {
            foreach(var a in args)
            {
                Values.Add(a.ToString());
            }
        }

        public override string ToString()
        {
            StringBuilder info = new StringBuilder();
            foreach (var a in Values) info.Append(a + "\n");

            return info.ToString();
        }

        PacketType GetPacketType(string type)
        {
            int tp = 0;
            int.TryParse(type, out tp);
            return (PacketType)tp;
        }
    }


    public enum PacketType
    {
        Type1,
        Type2
    }
}
