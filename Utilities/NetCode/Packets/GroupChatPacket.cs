using AlienBloxUtility.Utilities.Abstracts;
using System.IO;

namespace AlienBloxUtility.Utilities.NetCode.Packets
{
    public class GroupChatPacket : AlienBloxPacket
    {
        public override void OnPacketHandled(BinaryReader reader)
        {
            string commandType = reader.ReadString();

            switch (commandType)
            {
                case "creategroup":
                    break;
                case "delgroup":
                    break;
                case "setpassword":
                    break;
                case "kickuser":
                    break;
            }
        }
    }
}