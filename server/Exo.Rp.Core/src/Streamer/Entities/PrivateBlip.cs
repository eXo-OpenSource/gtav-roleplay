using System.Numerics;

namespace server.Streamer.Entities
{
    public class PrivateBlip : PrivateEntity
    {

        public int? Sprite
        {
            get
            {
                if (!TryGetData("sprite", out int sprite))
                    return null;

                return sprite;
            }
            set => SetData("sprite", value);
        }

        public string Name
        {
            get
            {
                if (!TryGetData("name", out string name))
                    return null;

                return name;
            }
            set => SetData("name", value);
        }

        public PrivateBlip(Vector3 position, int dimension, uint range) : base(0, position, dimension, range)
        {
        }
    }
}