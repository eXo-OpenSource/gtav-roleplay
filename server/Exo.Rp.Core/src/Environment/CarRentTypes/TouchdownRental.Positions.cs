using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exo.Rp.Core.Environment.CarRentTypes;
using AltV.Net.Data;
using AltV.Net;

namespace Exo.Rp.Core.Environment.CarRentTypes
{
    public partial class TouchdownRental : RentalGroup
    {
        public void LoadPositions()
        {
            vehiclePositions = new List<RentVehiclePosition>
            {
                new RentVehiclePosition("Faggio", new Position(-778.0054321289062f, -2444.09912109375f, 14.1f), new Rotation(0f, 0f, 2.5529024600982666f)),
                new RentVehiclePosition("Faggio", new Position(-781.4602661132812f, -2442.1513671875f, 14.1f), new Rotation(0f, 0f, 2.647080898284912f)),
                new RentVehiclePosition("Faggio", new Position(-784.2218017578125f, -2440.48779296875f, 14.1f), new Rotation(0f, 0f, 2.4820876121520996f)),
                new RentVehiclePosition("Faggio3", new Position(-787.277587890625f, -2439.024658203125f, 14.1f), new Rotation(0f, 0f, 2.562288761138916f)),
                new RentVehiclePosition("Faggio", new Position(-790.727783203125f, -2437.6767578125f, 14.1f), new Rotation(0f, 0f, -0.4105246663093567f)),
                new RentVehiclePosition("Faggio3", new Position(-793.2943115234375f, -2435.56298828125f, 14.1f), new Rotation(0f, 0f, 2.3259425163269043f)),
                new RentVehiclePosition("Faggio", new Position(-796.3097534179688f, -2433.57421875f, 14.1f), new Rotation(0f, 0f, 2.585282325744629f)),
                new RentVehiclePosition("Faggio", new Position(-798.9016723632812f, -2431.759033203125f, 14.1f), new Rotation(0f, 0f, 2.688983917236328f)),
                new RentVehiclePosition("Faggio3", new Position(-775.2880859375f, -2438.127685546875f, 14.1f), new Rotation(0f, 0f, -0.497565358877182f)),
                new RentVehiclePosition("Faggio3", new Position(-778.5030517578125f, -2436.6474609375f, 14.1f), new Rotation(0f, 0f, -0.5977759957313538f)),
                new RentVehiclePosition("Faggio", new Position(-781.3875122070312f, -2434.981201171875f, 14.1f), new Rotation(0f, 0f, -0.6544541716575623f)),
                new RentVehiclePosition("Faggio", new Position(-784.6174926757812f, -2433.52294921875f, 14.1f), new Rotation(0f, 0f, -0.4502308964729309f)),
                new RentVehiclePosition("Faggio", new Position(-787.4742431640625f, -2431.9580078125f, 14.1f), new Rotation(0f, 0f, -0.4638656973838806f)),
                new RentVehiclePosition("Faggio", new Position(-790.1361083984375f, -2430.29296875f, 14.1f), new Rotation(0f, 0f, -0.46838879585266113f)),
                new RentVehiclePosition("Faggio3", new Position(-792.8132934570312f, -2428.016845703125f, 14.1f), new Rotation(0f, 0f, -0.5470837950706482f)),
                new RentVehiclePosition("Faggio", new Position(-796.0247802734375f, -2426.542724609375f, 14.1f), new Rotation(0f, 0f, -0.5565572381019592f)),
                new RentVehiclePosition("Faggio", new Position(-798.91552734375f, -2424.482666015625f, 14.1f), new Rotation(0f, 0f, -0.5944514274597168f)),
                new RentVehiclePosition("Faggio3", new Position(-802.151123046875f, -2422.94580078125f, 14.1f), new Rotation(0f, 0f, -0.46228545904159546f)),
                new RentVehiclePosition("Faggio", new Position(-805.2642211914062f, -2421.40625f, 14.1f), new Rotation(0f, 0f, -0.46843221783638f)),
                new RentVehiclePosition("Asbo", new Position(-767.951171875f, -2426.130615234375f, 14.1f), new Rotation(0f, 0f, -0.545039176940918f)),
                new RentVehiclePosition("Issi2", new Position(-771.0202026367188f, -2424.240966796875f, 14.1f), new Rotation(0f, 0f, -0.5073109865188599f)),
                new RentVehiclePosition("Baller2", new Position(-774.052734375f, -2422.1533203125f, 14.1f), new Rotation(0f, 0f, -0.5503068566322327f)),
                new RentVehiclePosition("Stanier", new Position(-776.3784790039062f, -2421.0947265625f, 14.1f), new Rotation(0f, 0f, 2.6323962211608887f)),
                new RentVehiclePosition("Stanier", new Position(-779.7185668945312f, -2418.56787109375f, 14.1f), new Rotation(0f, 0f, -0.5522474050521851f)),
                new RentVehiclePosition("Tailgater", new Position(-782.8679809570312f, -2416.875732421875f, 14.1f), new Rotation(0f, 0f, -0.517600953578949f)),
                new RentVehiclePosition("Baller2", new Position(-786.0330200195312f, -2415.42919921875f, 14.1f), new Rotation(0f, 0f, -0.4954906404018402f)),
                new RentVehiclePosition("Premier", new Position(-788.8837890625f, -2413.58349609375f, 14.1f), new Rotation(0f, 0f, -0.5099822878837585f)),
                new RentVehiclePosition("Issi2", new Position(-791.60595703125f, -2411.64453125f, 14.1f), new Rotation(0f, 0f, -0.523935079574585f)),
                new RentVehiclePosition("Raiden", new Position(-794.662353515625f, -2410.059814453125f, 14.1f), new Rotation(0f, 0f, -0.510239839553833f)),
                new RentVehiclePosition("Asbo", new Position(-798.0344848632812f, -2409.034912109375f, 14.1f), new Rotation(0f, 0f, -0.5547108054161072f)),
                new RentVehiclePosition("Asbo", new Position(-800.7445678710938f, -2407.080810546875f, 14.1f), new Rotation(0f, 0f, -0.5156710743904114f)),
                new RentVehiclePosition("Raiden", new Position(-803.4903564453125f, -2404.54931640625f, 14.1f), new Rotation(0f, 0f, -0.5246764421463013f)),
                new RentVehiclePosition("Raiden", new Position(-806.0463256835938f, -2403.71142578125f, 14.1f), new Rotation(0f, 0f, 2.5817456245422363f)),
                new RentVehiclePosition("Premier", new Position(-809.673095703125f, -2401.595458984375f, 14.1f), new Rotation(0f, 0f, -0.497532457113266f)),
                new RentVehiclePosition("Asbo", new Position(-763.5324096679688f, -2419.51171875f, 14.1f), new Rotation(0f, 0f, 2.533358573913574f)),
                new RentVehiclePosition("Asbo", new Position(-766.109619140625f, -2417.41015625f, 14.1f), new Rotation(0f, 0f, 2.6068296432495117f)),
                new RentVehiclePosition("Landstalker", new Position(-769.5214233398438f, -2416.13720703125f, 14.1f), new Rotation(0f, 0f, 2.605896472930908f)),
                new RentVehiclePosition("Asbo", new Position(-772.7866821289062f, -2413.448486328125f, 14.1f), new Rotation(0f, 0f, -0.5517098903656006f)),
                new RentVehiclePosition("Asbo", new Position(-775.7467041015625f, -2412.12548828125f, 14.1f), new Rotation(0f, 0f, -0.5083463191986084f)),
                new RentVehiclePosition("Blista", new Position(-778.2417602539062f, -2410.71533203125f, 14.1f), new Rotation(0f, 0f, 2.626418352127075f)),
                new RentVehiclePosition("Tailgater", new Position(-781.5588989257812f, -2409.29443359375f, 14.1f), new Rotation(0f, 0f, 2.6363728046417236f)),
                new RentVehiclePosition("Stanier", new Position(-784.5858154296875f, -2407.78076171875f, 14.1f), new Rotation(0f, 0f, 2.61673903465271f)),
                new RentVehiclePosition("Tailgater", new Position(-787.3114624023438f, -2405.56591796875f, 14.1f), new Rotation(0f, 0f, 2.576929807662964f)),
                new RentVehiclePosition("Stanier", new Position(-790.2285766601562f, -2404.32421875f, 14.1f), new Rotation(0f, 0f, 2.599618911743164f)),
                new RentVehiclePosition("Stanier", new Position(-792.9680786132812f, -2402.1806640625f, 14.1f), new Rotation(0f, 0f, 2.6106455326080322f)),
                new RentVehiclePosition("Issi2", new Position(-796.1881103515625f, -2400.409423828125f, 14.1f), new Rotation(0f, 0f, 2.6660947799682617f)),
                new RentVehiclePosition("Dilettante", new Position(-799.03515625f, -2398.91064453125f, 14.1f), new Rotation(0f, 0f, 2.5626559257507324f)),
                new RentVehiclePosition("Dilettante", new Position(-802.200927734375f, -2397.336669921875f, 14.1f), new Rotation(0f, 0f, 2.6678555011749268f)),
                new RentVehiclePosition("Landstalker", new Position(-805.4509887695312f, -2395.44775390625f, 14.1f), new Rotation(0f, 0f, 2.640734910964966f)),
                new RentVehiclePosition("Dilettante", new Position(-755.9507446289062f, -2408.46240234375f, 14.1f), new Rotation(0f, 0f, 2.632131814956665f)),
                new RentVehiclePosition("Blista", new Position(-759.027099609375f, -2406.740234375f, 14.1f), new Rotation(0f, 0f, 2.5922961235046387f)),
                new RentVehiclePosition("Premier", new Position(-762.3041381835938f, -2404.332275390625f, 14.1f), new Rotation(0f, 0f, -0.5373940467834473f)),
                new RentVehiclePosition("Premier", new Position(-765.3821411132812f, -2402.588134765625f, 14.1f), new Rotation(0f, 0f, -0.5466209650039673f)),
                new RentVehiclePosition("Stanier", new Position(-768.50634765625f, -2400.6748046875f, 14.1f), new Rotation(0f, 0f, -0.5310642719268799f)),
                new RentVehiclePosition("Asbo", new Position(-771.7240600585938f, -2399.537109375f, 14.1f), new Rotation(0f, 0f, -0.5557108521461487f)),
                new RentVehiclePosition("Asbo", new Position(-774.9019165039062f, -2397.7705078125f, 14.1f), new Rotation(0f, 0f, -0.5961836576461792f)),
                new RentVehiclePosition("Asbo", new Position(-777.65625f, -2396.212890625f, 14.1f), new Rotation(0f, 0f, -0.5474180579185486f)),
                new RentVehiclePosition("Asterope", new Position(-780.2186889648438f, -2393.98828125f, 14.1f), new Rotation(0f, 0f, -0.5422911047935486f)),
                new RentVehiclePosition("Asbo", new Position(-783.5275268554688f, -2392.707275390625f, 14.1f), new Rotation(0f, 0f, -0.5230249762535095f)),
                new RentVehiclePosition("Premier", new Position(-785.7886962890625f, -2391.0107421875f, 14.1f), new Rotation(0f, 0f, 2.622255325317383f)),
                new RentVehiclePosition("Premier", new Position(-789.4818725585938f, -2389.138671875f, 14.1f), new Rotation(0f, 0f, -0.5035866498947144f)),
                new RentVehiclePosition("Premier", new Position(-792.349609375f, -2387.24365234375f, 14.1f), new Rotation(0f, 0f, -0.5283519625663757f)),
                new RentVehiclePosition("Asterope", new Position(-795.0623779296875f, -2385.219482421875f, 14.1f), new Rotation(0f, 0f, -0.4987531006336212f)),
                new RentVehiclePosition("Blista", new Position(-798.0628662109375f, -2383.7724609375f, 14.1f), new Rotation(0f, 0f, -0.541303277015686f)),
                new RentVehiclePosition("Stanier", new Position(-752.563232421875f, -2400.718017578125f, 14.1f), new Rotation(0f, 0f, -0.5313113331794739f)),
                new RentVehiclePosition("Premier", new Position(-755.041259765625f, -2399.565673828125f, 14.1f), new Rotation(0f, 0f, 2.576568603515625f)),
                new RentVehiclePosition("Premier", new Position(-757.9771728515625f, -2397.8095703125f, 14.1f), new Rotation(0f, 0f, 2.638256072998047f)),
                new RentVehiclePosition("Dilettante", new Position(-760.8365478515625f, -2395.981689453125f, 14.1f), new Rotation(0f, 0f, 2.621485710144043f)),
                new RentVehiclePosition("Dilettante", new Position(-763.7481079101562f, -2394.250732421875f, 14.1f), new Rotation(0f, 0f, 2.605893611907959f)),
                new RentVehiclePosition("Premier", new Position(-766.872314453125f, -2392.671142578125f, 14.1f), new Rotation(0f, 0f, 2.5814478397369385f)),
                new RentVehiclePosition("Issi2", new Position(-769.6600952148438f, -2390.640869140625f, 14.1f), new Rotation(0f, 0f, 2.651566505432129f)),
                new RentVehiclePosition("Stanier", new Position(-773.1946411132812f, -2388.5556640625f, 14.1f), new Rotation(0f, 0f, -0.5000860095024109f)),
                new RentVehiclePosition("Landstalker", new Position(-775.8350830078125f, -2387.933837890625f, 14.1f), new Rotation(0f, 0f, 2.5850257873535156f)),
                new RentVehiclePosition("Landstalker", new Position(-778.729248046875f, -2386.047607421875f, 14.1f), new Rotation(0f, 0f, 2.6131513118743896f)),
                new RentVehiclePosition("Baller2", new Position(-781.601318359375f, -2384.30029296875f, 14.1f), new Rotation(0f, 0f, 2.6095666885375977f)),
                new RentVehiclePosition("Asbo", new Position(-784.568359375f, -2381.990234375f, 14.1f), new Rotation(0f, 0f, 2.645761251449585f)),
                new RentVehiclePosition("Asbo", new Position(-787.5162353515625f, -2380.365478515625f, 14.1f), new Rotation(0f, 0f, 2.6189193725585938f)),
                new RentVehiclePosition("Tailgater", new Position(-790.642333984375f, -2379.17041015625f, 14.1f), new Rotation(0f, 0f, 2.667346477508545f)),
                new RentVehiclePosition("Tailgater", new Position(-793.6812133789062f, -2377.517822265625f, 14.1f), new Rotation(0f, 0f, 2.6200499534606934f))
            };
        }
    }
}
