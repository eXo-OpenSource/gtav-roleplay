using System.Numerics;
using AltV.Net;
using AltV.Net.EntitySync;

namespace Exo.Rp.Core.Streamer.Entities
{
    public class StreamObject : Entity, IEntity
    {

        public uint? Model
        {
            get => !TryGetData("model", out uint model) ? (uint?) null : model;
            set => SetData("model", value);
        }

        public StreamObject(Vector3 position, int dimension, uint range) : base(1, position, dimension, range)
        {
        }

        public void AttachToBone(AttachOptions options)
        {
            SetData("attachToEntity", options);
        }
    }

    public class AttachOptions : IWritable
    {
        public ushort Entity { get; set; }
        public int BoneID { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float XRot { get; set; }
        public float YRot { get; set; }
        public float ZRot { get; set; }
        public int VertexIndex { get; set; }


        public void OnWrite(IMValueWriter writer)
        {
            writer.BeginObject();
            writer.Name("entity");
            writer.Value(Entity);
            writer.Name("boneId");
            writer.Value(BoneID);
            writer.Name( "x" );
            writer.Value( X );
            writer.Name( "y" );
            writer.Value( Y );
            writer.Name( "z" );
            writer.Value( Z );
            writer.Name( "xRot" );
            writer.Value( XRot );
            writer.Name( "yRot" );
            writer.Value( YRot );
            writer.Name( "zRot" );
            writer.Value( ZRot );
            writer.Name("vertexIndex");
            writer.Value(VertexIndex);
            writer.EndObject();
        }
    }
}