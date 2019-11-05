using System;
using System.IO;
using server.Util.Extensions;
using server.Util.Serialization;

namespace server.Models.Characters
{
    [Serializable]
    public class FaceFeaturesDto : Serializable<FaceFeaturesDto>
    {
        public int Gender { get; set; }
        public int Hair { get; set; }
        public int HairColor { get; set; }
        public int HairColorHighlight { get; set; }
        public int EyeColor { get; set; }
        public Face Face { get; set; }
        public Parents Parents { get; set; }
        public Overlay Overlay { get; set; }
        public OverlayColor OverlayColor { get; set; }

        public override string SerializeObject()
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(memoryStream))
                {
                    writer.Write(GetClassName());

                    writer.Write(Gender);
                    writer.Write(Hair);
                    writer.Write(HairColor);
                    writer.Write(HairColorHighlight);
                    writer.Write(EyeColor);
                    writer.Write(Face);
                    writer.Write(Parents);
                    writer.Write(Overlay);
                    writer.Write(OverlayColor);

                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }

        public override FaceFeaturesDto DeserializeObject(string value)
        {
            using (var memoryStream = new MemoryStream(Convert.FromBase64String(value)))
            {
                using (var reader = new BinaryReader(memoryStream))
                {
                    var type = reader.ReadString();

                    if (type != GetClassName())
                        throw new ArgumentException($"Expected data from type {GetClassName()} got {type}.");

                    Gender = reader.ReadInt32();
                    Hair = reader.ReadInt32();
                    HairColor = reader.ReadInt32();
                    HairColorHighlight = reader.ReadInt32();
                    EyeColor = reader.ReadInt32();
                    Face = reader.ReadSerializable<Face>();
                    Parents = reader.ReadSerializable<Parents>();
                    Overlay = reader.ReadSerializable<Overlay>();
                    OverlayColor = reader.ReadSerializable<OverlayColor>();

                    return this;
                }
            }
        }
    }

    public class Face : Serializable<Face>
    {
        public int NoseWidth { get; set; }
        public int NoseHeight { get; set; }
        public int NoseLength { get; set; }
        public int NoseBridge { get; set; }
        public int NoseTip { get; set; }
        public int NoseShift { get; set; }
        public int BrowWidth { get; set; }
        public int BrowHeight { get; set; }
        public int CheekboneHeight { get; set; }
        public int CheekboneWidth { get; set; }
        public int CheeksWidth { get; set; }
        public int EyesWidth { get; set; }
        public int LipsWidth { get; set; }
        public int JawWidth { get; set; }
        public int JawHeight { get; set; }
        public int ChinLength { get; set; }
        public int ChinPosition { get; set; }
        public int ChinWidth { get; set; }
        public int ChinShape { get; set; }
        public int NeckWidth { get; set; }

        public override string SerializeObject()
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(memoryStream))
                {
                    writer.Write(GetClassName());

                    writer.Write(NoseWidth);
                    writer.Write(NoseHeight);
                    writer.Write(NoseLength);
                    writer.Write(NoseBridge);
                    writer.Write(NoseTip);
                    writer.Write(NoseShift);
                    writer.Write(BrowWidth);
                    writer.Write(BrowHeight);
                    writer.Write(CheekboneHeight);
                    writer.Write(CheekboneWidth);
                    writer.Write(CheeksWidth);
                    writer.Write(EyesWidth);
                    writer.Write(LipsWidth);
                    writer.Write(JawWidth);
                    writer.Write(JawHeight);
                    writer.Write(ChinLength);
                    writer.Write(ChinPosition);
                    writer.Write(ChinWidth);
                    writer.Write(ChinShape);
                    writer.Write(NeckWidth);

                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }

        public override Face DeserializeObject(string value)
        {
            using (var memoryStream = new MemoryStream(Convert.FromBase64String(value)))
            {
                using (var reader = new BinaryReader(memoryStream))
                {
                    var type = reader.ReadString();

                    if (type != GetClassName())
                        throw new ArgumentException($"Expected data from type {GetClassName()} got {type}.");

                    NoseWidth = reader.ReadInt32();
                    NoseHeight = reader.ReadInt32();
                    NoseLength = reader.ReadInt32();
                    NoseBridge = reader.ReadInt32();
                    NoseTip = reader.ReadInt32();
                    NoseShift = reader.ReadInt32();
                    BrowWidth = reader.ReadInt32();
                    BrowHeight = reader.ReadInt32();
                    CheekboneHeight = reader.ReadInt32();
                    CheekboneWidth = reader.ReadInt32();
                    CheeksWidth = reader.ReadInt32();
                    EyesWidth = reader.ReadInt32();
                    LipsWidth = reader.ReadInt32();
                    JawWidth = reader.ReadInt32();
                    JawHeight = reader.ReadInt32();
                    ChinLength = reader.ReadInt32();
                    ChinPosition = reader.ReadInt32();
                    ChinWidth = reader.ReadInt32();
                    ChinShape = reader.ReadInt32();
                    NeckWidth = reader.ReadInt32();

                    return this;
                }
            }
        }
    }

    public class Parents : Serializable<Parents>
    {
        public int ShapeFirst { get; set; }
        public int ShapeSecond { get; set; }
        public int ShapeThird { get; set; }
        public int SkinFirst { get; set; }
        public int SkinSecond { get; set; }
        public int SkinThird { get; set; }
        public int ShapeMix { get; set; }
        public int SkinMix { get; set; }
        public int ThirdMix { get; set; }

        public override string SerializeObject()
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(memoryStream))
                {
                    writer.Write(GetClassName());

                    writer.Write(ShapeFirst);
                    writer.Write(ShapeSecond);
                    writer.Write(ShapeThird);
                    writer.Write(SkinFirst);
                    writer.Write(SkinSecond);
                    writer.Write(SkinThird);
                    writer.Write(ShapeMix);
                    writer.Write(SkinMix);
                    writer.Write(ThirdMix);

                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }

        public override Parents DeserializeObject(string value)
        {
            using (var memoryStream = new MemoryStream(Convert.FromBase64String(value)))
            {
                using (var reader = new BinaryReader(memoryStream))
                {
                    var type = reader.ReadString();

                    if (type != GetClassName())
                        throw new ArgumentException($"Expected data from type {GetClassName()} got {type}.");

                    ShapeFirst = reader.ReadInt32();
                    ShapeSecond = reader.ReadInt32();
                    ShapeThird = reader.ReadInt32();
                    SkinFirst = reader.ReadInt32();
                    SkinSecond = reader.ReadInt32();
                    SkinThird = reader.ReadInt32();
                    ShapeMix = reader.ReadInt32();
                    ThirdMix = reader.ReadInt32();
                    SkinMix = reader.ReadInt32();

                    return this;
                }
            }
        }
    }

    public class Overlay : Serializable<Overlay>
    {
        public int Blemishes { get; set; }
        public int FacialHair { get; set; }
        public int Eyebrows { get; set; }
        public int Ageing { get; set; }
        public int Makeup { get; set; }
        public int Blush { get; set; }
        public int Complexion { get; set; }
        public int SunDamage { get; set; }
        public int Lipstick { get; set; }
        public int Freckles { get; set; }
        public int ChestHair { get; set; }
        public int BodyBlemishes { get; set; }
        public int AddBodyBlemishes { get; set; }

        public override string SerializeObject()
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(memoryStream))
                {
                    writer.Write(GetClassName());

                    writer.Write(Blemishes);
                    writer.Write(FacialHair);
                    writer.Write(Eyebrows);
                    writer.Write(Ageing);
                    writer.Write(Makeup);
                    writer.Write(Blush);
                    writer.Write(Complexion);
                    writer.Write(SunDamage);
                    writer.Write(Lipstick);
                    writer.Write(Freckles);
                    writer.Write(ChestHair);
                    writer.Write(BodyBlemishes);
                    writer.Write(AddBodyBlemishes);

                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }

        public override Overlay DeserializeObject(string value)
        {
            using (var memoryStream = new MemoryStream(Convert.FromBase64String(value)))
            {
                using (var reader = new BinaryReader(memoryStream))
                {
                    var type = reader.ReadString();

                    if (type != GetClassName())
                        throw new ArgumentException($"Expected data from type {GetClassName()} got {type}.");

                    Blemishes = reader.ReadInt32();
                    FacialHair = reader.ReadInt32();
                    Eyebrows = reader.ReadInt32();
                    Ageing = reader.ReadInt32();
                    Makeup = reader.ReadInt32();
                    Blush = reader.ReadInt32();
                    Complexion = reader.ReadInt32();
                    SunDamage = reader.ReadInt32();
                    Lipstick = reader.ReadInt32();
                    Freckles = reader.ReadInt32();
                    ChestHair = reader.ReadInt32();
                    BodyBlemishes = reader.ReadInt32();
                    AddBodyBlemishes = reader.ReadInt32();

                    return this;
                }
            }
        }
    }

    public class OverlayColor : Serializable<OverlayColor>
    {
        public Color Blemishes { get; set; }
        public Color FacialHair { get; set; }
        public Color Eyebrows { get; set; }
        public Color Ageing { get; set; }
        public Color Makeup { get; set; }
        public Color Blush { get; set; }
        public Color Complexion { get; set; }
        public Color SunDamage { get; set; }
        public Color Lipstick { get; set; }
        public Color Freckles { get; set; }
        public Color ChestHair { get; set; }
        public Color BodyBlemishes { get; set; }
        public Color AddBodyBlemishes { get; set; }

        public override string SerializeObject()
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(memoryStream))
                {
                    writer.Write(GetClassName());

                    writer.Write(Blemishes);
                    writer.Write(FacialHair);
                    writer.Write(Eyebrows);
                    writer.Write(Ageing);
                    writer.Write(Makeup);
                    writer.Write(Blush);
                    writer.Write(Complexion);
                    writer.Write(SunDamage);
                    writer.Write(Lipstick);
                    writer.Write(Freckles);
                    writer.Write(ChestHair);
                    writer.Write(BodyBlemishes);
                    writer.Write(AddBodyBlemishes);

                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }

        public override OverlayColor DeserializeObject(string value)
        {
            using (var memoryStream = new MemoryStream(Convert.FromBase64String(value)))
            {
                using (var reader = new BinaryReader(memoryStream))
                {
                    var type = reader.ReadString();

                    if (type != GetClassName())
                        throw new ArgumentException($"Expected data from type {GetClassName()} got {type}.");

                    Blemishes = reader.ReadSerializable<Color>();
                    FacialHair = reader.ReadSerializable<Color>();
                    Eyebrows = reader.ReadSerializable<Color>();
                    Ageing = reader.ReadSerializable<Color>();
                    Makeup = reader.ReadSerializable<Color>();
                    Blush = reader.ReadSerializable<Color>();
                    Complexion = reader.ReadSerializable<Color>();
                    SunDamage = reader.ReadSerializable<Color>();
                    Lipstick = reader.ReadSerializable<Color>();
                    Freckles = reader.ReadSerializable<Color>();
                    ChestHair = reader.ReadSerializable<Color>();
                    BodyBlemishes = reader.ReadSerializable<Color>();
                    AddBodyBlemishes = reader.ReadSerializable<Color>();

                    return this;
                }
            }
        }
    }

    public class Color : Serializable<Color>
    {
        public int Color1 { get; set; }
        public int Color2 { get; set; }

        public override string SerializeObject()
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(memoryStream))
                {
                    writer.Write(GetClassName());

                    writer.Write(Color1);
                    writer.Write(Color2);

                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }

        public override Color DeserializeObject(string value)
        {
            using (var memoryStream = new MemoryStream(Convert.FromBase64String(value)))
            {
                using (var reader = new BinaryReader(memoryStream))
                {
                    var type = reader.ReadString();

                    if (type != GetClassName())
                        throw new ArgumentException($"Expected data from type {GetClassName()} got {type}.");

                    Color1 = reader.ReadInt32();
                    Color2 = reader.ReadInt32();

                    return this;
                }
            }
        }
    }
}