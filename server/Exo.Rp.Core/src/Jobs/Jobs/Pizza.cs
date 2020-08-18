using Exo.Rp.Core.Streamer.Entities;

namespace Exo.Rp.Core.Jobs.Jobs
{
    public class Pizza
    {
        public double MaxCapacity { get; set; }
        public double Capacity { get; set; }
        public int Pay { get; set; }
        public int PayPerPizza = 25;
        public PrivateEntity DeliveryBlip { get; set; }
        public Colshape.Colshape DeliveryCol { get; set; }
        public PrivateEntity IntakeBlip { get; set; }
        public Colshape.Colshape IntakeCol { get; set; }
    }
}