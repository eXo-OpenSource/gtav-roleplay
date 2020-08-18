using System.Numerics;
using AltV.Net.EntitySync;

namespace Exo.Rp.Core.Streamer.Entities
{
    public class StreamPed : Entity, IEntity
    {
        public uint? Model
        {
            get => !TryGetData("model", out uint model) ? (uint?) null : model;
            set => SetData("model", value);
        }
        
        public float Heading
        {
            get
            {
                if (!TryGetData("heading", out float heading))
                    return -1;

                return heading;
            }
            set => SetData("heading", value);
        }
        
        public bool Static
        {
            get
            {
                if (!TryGetData("static", out bool Static))
                    return false;

                return Static;
            }
            set => SetData("static", value);
        }
        
        public StreamPed(Vector3 position, int dimension, uint range = 400) : base(2, position, dimension, range)
        {
        }
    }
}