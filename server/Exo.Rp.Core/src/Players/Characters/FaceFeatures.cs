using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exo.Rp.Core.Players.Characters
{
    [Table("FaceFeatures")]
    public class FaceFeatures
    {
        [Key]
        public int Id { get; set; }

        public int Gender { get; set; }
        public int Hair { get; set; }
        public int HairColor { get; set; }
        public int HairColorHighlight { get; set; }
        public int EyeColor { get; set; }

        public double NoseWidth { get; set; }
        public double NoseHeight { get; set; }
        public double NoseLength { get; set; }
        public double NoseBridge { get; set; }
        public double NoseTip { get; set; }
        public double NoseShift { get; set; }
        public double BrowWidth { get; set; }
        public double BrowHeight { get; set; }
        public double CheekboneHeight { get; set; }
        public double CheekboneWidth { get; set; }
        public double CheeksWidth { get; set; }
        public double EyesWidth { get; set; }
        public double LipsWidth { get; set; }
        public double JawWidth { get; set; }
        public double JawHeight { get; set; }
        public double ChinLength { get; set; }
        public double ChinPosition { get; set; }
        public double ChinWidth { get; set; }
        public double ChinShape { get; set; }
        public double NeckWidth { get; set; }

        public int ShapeFirst { get; set; }
        public int ShapeSecond { get; set; }
        public int ShapeThird { get; set; }
        public int SkinFirst { get; set; }
        public int SkinSecond { get; set; }
        public int SkinThird { get; set; }
        public double ShapeMix { get; set; }
        public double SkinMix { get; set; }
        public int ThirdMix { get; set; }

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

        public int BlemishesColor1 { get; set; }
        public int FacialHairColor1 { get; set; }
        public int EyebrowsColor1 { get; set; }
        public int AgeingColor1 { get; set; }
        public int MakeupColor1 { get; set; }
        public int BlushColor1 { get; set; }
        public int ComplexionColor1 { get; set; }
        public int SunDamageColor1 { get; set; }
        public int LipstickColor1 { get; set; }
        public int FrecklesColor1 { get; set; }
        public int ChestHairColor1 { get; set; }
        public int BodyBlemishesColor1 { get; set; }
        public int AddBodyBlemishesColor1 { get; set; }

        public int BlemishesColor2 { get; set; }
        public int FacialHairColor2 { get; set; }
        public int EyebrowsColor2 { get; set; }
        public int AgeingColor2 { get; set; }
        public int MakeupColor2 { get; set; }
        public int BlushColor2 { get; set; }
        public int ComplexionColor2 { get; set; }
        public int SunDamageColor2 { get; set; }
        public int LipstickColor2 { get; set; }
        public int FrecklesColor2 { get; set; }
        public int ChestHairColor2 { get; set; }
        public int BodyBlemishesColor2 { get; set; }
        public int AddBodyBlemishesColor2 { get; set; }


    }
}