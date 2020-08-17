using AltV.Net;

namespace server.Streamer.Entities
{
	public class StaticBlip : IWritable
	{
		public string Name { get; set; }
		public int SpriteId { get; set; }
		public int Color { get; set; }
		public int Scale { get; set; } = 1;
		public float X { get; set; }
		public float Y { get; set; }
		public float Z { get; set; }
		public int Dimension { get; set; } = 0;

		public void OnWrite(IMValueWriter writer)
		{
			writer.BeginObject( );
			writer.Name( "name" );
			writer.Value( Name );
			writer.Name( "sprite" );
			writer.Value( SpriteId );
			writer.Name( "color" );
			writer.Value( Color );
			writer.Name( "scale" );
			writer.Value( Scale );
			writer.Name( "x" );
			writer.Value( X );
			writer.Name( "y" );
			writer.Value( Y );
			writer.Name( "z" );
			writer.Value( Z );
			writer.Name( "dimension" );
			writer.Value( Dimension );
			writer.EndObject( );
		}
	}
}
