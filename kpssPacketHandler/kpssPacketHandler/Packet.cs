using System;
using System.Text;

namespace kpssPacketHandler
{
    public class Packet
    {
        public string[] values = null;
        public Packet(params object[] args)
        {
            values = new string[args.Length];

            for (int i = 0; i < this.values.Length; i++)
            {
                values[i] = args[i].ToString();
            }
        }

        public override string ToString()
        {
            StringBuilder info = new StringBuilder();
            foreach (var a in values) info.Append(a + "\n");

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
