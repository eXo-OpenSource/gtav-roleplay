using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AltV.Net.Data;
using Exo.Rp.Core.Environment.CarRentTypes;

namespace Exo.Rp.Core.Environment.CarRentTypes
{
    public partial class EscaleraRental : RentalGroup
    {
        public void LoadPositions()
        {
            vehiclePositions = new List<RentVehiclePosition>
            {
                new RentVehiclePosition("Panto", new Position(-873.9658813476562f, -2296.4990234375f, 6.2f), new Rotation(0f, 0f, 1.0435607433319092f)),
                new RentVehiclePosition("Asea", new Position(-875.4417114257812f, -2299.633056640625f, 6.2f), new Rotation(0f, 0f, 1.0445311069488525f)),
                new RentVehiclePosition("Blista", new Position(-877.0795288085938f, -2302.61474609375f, 6.2f), new Rotation(0f, 0f, 0.9967959523200989f)),
                new RentVehiclePosition("Blista", new Position(-878.7391357421875f, -2305.656005859375f, 6.2f), new Rotation(0f, 0f, 1.0426626205444336f)),
                new RentVehiclePosition("Blista", new Position(-880.056884765625f, -2308.228759765625f, 6.2f), new Rotation(0f, 0f, -2.1247758865356445f)),
                new RentVehiclePosition("Rebla", new Position(-882.1396484375f, -2311.664306640625f, 6.2f), new Rotation(0f, 0f, 1.0389732122421265f)),
                new RentVehiclePosition("Blista", new Position(-884.1220092773438f, -2314.47900390625f, 6.2f), new Rotation(0f, 0f, 1.0434635877609253f)),
                new RentVehiclePosition("Blista", new Position(-885.730224609375f, -2317.443359375f, 6.2f), new Rotation(0f, 0f, 1.0669242143630981f)),
                new RentVehiclePosition("Asea", new Position(-887.6704711914062f, -2320.352294921875f, 6.2f), new Rotation(0f, 0f, 1.0482500791549683f)),
                new RentVehiclePosition("Dilettante", new Position(-888.517822265625f, -2323.219970703125f, 6.2f), new Rotation(0f, 0f, -2.126800537109375f)),
                new RentVehiclePosition("Dilettante", new Position(-890.9407348632812f, -2326.505126953125f, 6.2f), new Rotation(0f, 0f, 1.0538873672485352f)),
                new RentVehiclePosition("Blista", new Position(-892.651123046875f, -2329.3994140625f, 6.2f), new Rotation(0f, 0f, 1.0416747331619263f)),
                new RentVehiclePosition("Asea", new Position(-894.4232177734375f, -2332.263427734375f, 6.2f), new Rotation(0f, 0f, 1.062558650970459f)),
                new RentVehiclePosition("Asea", new Position(-896.1144409179688f, -2335.528076171875f, 6.2f), new Rotation(0f, 0f, 1.0409165620803833f)),
                new RentVehiclePosition("Rebla", new Position(-897.7997436523438f, -2338.549072265625f, 6.2f), new Rotation(0f, 0f, 1.087131381034851f)),
                new RentVehiclePosition("Blista", new Position(-906.1759033203125f, -2332.699462890625f, 6.2f), new Rotation(0f, 0f, -2.121769428253174f)),
                new RentVehiclePosition("Primo", new Position(-903.7479858398438f, -2326.982666015625f, 6.2f), new Rotation(0f, 0f, 1.0061641931533813f)),
                new RentVehiclePosition("Fugitive", new Position(-901.0232543945312f, -2323.93408203125f, 6.2f), new Rotation(0f, 0f, -2.1049721240997314f)),
                new RentVehiclePosition("Primo", new Position(-899.2030029296875f, -2320.730224609375f, 6.2f), new Rotation(0f, 0f, -2.150982618331909f)),
                new RentVehiclePosition("Panto", new Position(-897.8746337890625f, -2317.984375f, 6.2f), new Rotation(0f, 0f, -2.1096091270446777f)),
                new RentVehiclePosition("Panto", new Position(-896.1165161132812f, -2314.72119140625f, 6.2f), new Rotation(0f, 0f, -2.077920913696289f)),
                new RentVehiclePosition("Primo", new Position(-893.971923828125f, -2311.890380859375f, 6.2f), new Rotation(0f, 0f, -2.1219489574432373f)),
                new RentVehiclePosition("Asea", new Position(-892.1480102539062f, -2308.90576171875f, 6.2f), new Rotation(0f, 0f, -2.100905418395996f)),
                new RentVehiclePosition("Asea", new Position(-890.4380493164062f, -2305.931884765625f, 6.2f), new Rotation(0f, 0f, -2.090468168258667f)),
                new RentVehiclePosition("Primo", new Position(-888.847412109375f, -2302.943603515625f, 6.2f), new Rotation(0f, 0f, -2.0915706157684326f)),
                new RentVehiclePosition("Blista", new Position(-886.9762573242188f, -2300.102783203125f, 6.2f), new Rotation(0f, 0f, -2.085106372833252f)),
                new RentVehiclePosition("Landstalker", new Position(-885.2347412109375f, -2297.2763671875f, 6.2f), new Rotation(0f, 0f, -2.1056289672851562f)),
                new RentVehiclePosition("Panto", new Position(-884.2823486328125f, -2293.829345703125f, 6.2f), new Rotation(0f, 0f, -2.0872044563293457f)),
                new RentVehiclePosition("Serrano", new Position(-882.0855712890625f, -2290.88037109375f, 6.2f), new Rotation(0f, 0f, -2.089705467224121f)),
                new RentVehiclePosition("Serrano", new Position(-880.3733520507812f, -2288.071533203125f, 6.2f), new Rotation(0f, 0f, -2.110100030899048f)),
                new RentVehiclePosition("Dilettante", new Position(-878.7615966796875f, -2285.086181640625f, 6.2f), new Rotation(0f, 0f, -2.1070024967193604f)),
                new RentVehiclePosition("Blista", new Position(-877.08056640625f, -2282.10400390625f, 6.2f), new Rotation(0f, 0f, -2.1204581260681152f)),
                new RentVehiclePosition("Fugitive", new Position(-875.2369995117188f, -2278.983642578125f, 6.2f), new Rotation(0f, 0f, -2.0981578826904297f)),
                new RentVehiclePosition("Fugitive", new Position(-873.3709106445312f, -2275.85400390625f, 6.2f), new Rotation(0f, 0f, -2.0506539344787598f)),
                new RentVehiclePosition("Faggio", new Position(-881.1942138671875f, -2272.359375f, 6.2f), new Rotation(0f, 0f, 0.9561640024185181f)),
                new RentVehiclePosition("Faggio", new Position(-882.937744140625f, -2275.324462890625f, 6.2f), new Rotation(0f, 0f, 1.1054561138153076f)),
                new RentVehiclePosition("Faggio", new Position(-884.3975830078125f, -2278.461669921875f, 6.2f), new Rotation(0f, 0f, 0.9866516590118408f)),
                new RentVehiclePosition("Faggio3", new Position(-886.0961303710938f, -2281.078369140625f, 6.2f), new Rotation(0f, 0f, 1.0110359191894531f)),
                new RentVehiclePosition("Faggio", new Position(-888.0468139648438f, -2284.333984375f, 6.2f), new Rotation(0f, 0f, 1.0323530435562134f)),
                new RentVehiclePosition("Faggio3", new Position(-889.5203247070312f, -2287.0205078125f, 6.2f), new Rotation(0f, 0f, 1.0064697265625f)),
                new RentVehiclePosition("Faggio3", new Position(-891.6166381835938f, -2290.286376953125f, 6.2f), new Rotation(0f, 0f, 0.9673365354537964f)),
                new RentVehiclePosition("Faggio", new Position(-893.4818115234375f, -2293.239990234375f, 6.2f), new Rotation(0f, 0f, 1.0367196798324585f)),
                new RentVehiclePosition("Faggio", new Position(-895.1927490234375f, -2296.12353515625f, 6.2f), new Rotation(0f, 0f, 0.7782617807388306f)),
                new RentVehiclePosition("Faggio", new Position(-896.796875f, -2298.504638671875f, 6.2f), new Rotation(0f, 0f, -2.0746827125549316f)),
                new RentVehiclePosition("Faggio2", new Position(-898.0941162109375f, -2302.161865234375f, 6.2f), new Rotation(0f, 0f, 0.9316233992576599f)),
                new RentVehiclePosition("Faggio", new Position(-899.8744506835938f, -2305.22509765625f, 6.2f), new Rotation(0f, 0f, 1.059953212738037f)),
                new RentVehiclePosition("Faggio", new Position(-901.5973510742188f, -2307.982177734375f, 6.2f), new Rotation(0f, 0f, 0.9913992285728455f)),
                new RentVehiclePosition("Faggio", new Position(-903.4677124023438f, -2311.310302734375f, 6.2f), new Rotation(0f, 0f, 1.0130465030670166f)),
                new RentVehiclePosition("Faggio", new Position(-905.63818359375f, -2313.548828125f, 6.2f), new Rotation(0f, 0f, -2.1399688720703125f)),
                new RentVehiclePosition("Faggio", new Position(-906.58984375f, -2317.447998046875f, 6.2f), new Rotation(0f, 0f, 1.0525574684143066f))
            };
        }
    }
}
