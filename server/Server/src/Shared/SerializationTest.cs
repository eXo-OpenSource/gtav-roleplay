using System.IO;

namespace shared
{
    public class SerializationTest
    {
        public int number { get; set; }
        public string message { get; set; }

        public SerializationTest(string message)
        {
            number = 1;
            this.message = message;
        }

        private SerializationTest(int number, string message)
        {
            this.number = number;
            this.message = message;
        }

        public string Serialize()
        {
            using (var mems = new MemoryStream())
            {
                using (var writer = new BinaryWriter(mems))
                {
                    writer.Write(number);
                    writer.Write(message);
                }

                return System.Convert.ToBase64String(mems.ToArray());
            }
        }

        public static SerializationTest Deserialize(string str)
        {
            using (var mems = new MemoryStream(System.Convert.FromBase64String(str)))
            {
                using (var reader = new BinaryReader(mems))
                {
                    return new SerializationTest(reader.ReadInt32(), reader.ReadString());
                }
            }
        }

    }
}
