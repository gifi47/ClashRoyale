using System;
using System.Text;

namespace Online
{
    public interface IOnline
    {
        byte[] GetData();
        void PutData(byte[] data);
    }

    public class Packet
    {
        public byte id;
        public ushort size;
        public byte[] data;

        public Packet(byte id, byte[] data)
        {
            this.id = id;
            this.data = data;
            this.size = (ushort)data.Length;
        }

        public byte[] GetData()
        {
            byte[] result = new byte[3 + data.Length];
            result[0] = id;
            BitConverter.GetBytes(size).CopyTo(result, 1);
            Array.Copy(data, 0, result, 3, data.Length);
            return result;
        }

        public int getDataLength()
        {
            return data.Length + 3;
        }
    }

    public class Msg : IOnline
    {
        public string textMessage;
        public byte sender;
        public byte distanation;

        public byte[] GetData()
        {
            //      0   -   MSG size
            //      1   -   MSG size
            //      2   -   Sender id
            //      3   -   Distanation id
            //      4   -   MSG
            //    ...   -   MSG
            //      N   -   MSG
            byte[] textData = Encoding.UTF8.GetBytes(textMessage);
            byte[] data = new byte[textData.Length + 2];
            data[0] = sender;
            data[1] = distanation;
            Array.Copy(textData, 0, data, 2, textData.Length);
            return data;
        }

        public void PutData(byte[] data)
        {
            sender = data[0];
            distanation = data[1];
            textMessage = Encoding.UTF8.GetString(data, 2, data.Length - 2);
        }
    }
}
