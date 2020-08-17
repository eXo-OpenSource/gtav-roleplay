using System;
using System.IO;

namespace models.Elements
{

	// Serverside : server.SharedUtil.Vector3DtoHelper in server/src/SharedUtil
	// Clientside : shared.Elements.Vector3Dto in client/src/SharedUtil
	public partial class Vector3Dto
	{
		public float X { get; set; }
		public float Y { get; set; }
		public float Z { get; set; }

		public Vector3Dto()
		{
			X = 0;
			Y = 0;
			Z = 0;
		}

		public Vector3Dto(float x, float y, float z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public string SerializeObject()
		{
			using (var memoryStream = new MemoryStream())
			{
				using (var writer = new BinaryWriter(memoryStream))
				{
					writer.Write(typeof(Vector3Dto).Name);

					writer.Write(X);
					writer.Write(Y);
					writer.Write(Z);

					return Convert.ToBase64String(memoryStream.ToArray());
				}
			}
		}
	}
}
