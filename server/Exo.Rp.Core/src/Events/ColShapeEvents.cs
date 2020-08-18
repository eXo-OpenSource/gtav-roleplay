using AltV.Net;
using AltV.Net.Elements.Entities;

namespace Exo.Rp.Core.Events
{
    class ColShapeEvents : IScript
    {
        [ScriptEvent(ScriptEventType.ColShape)]
        public void OnEntityColShapeHit(Colshape.Colshape shape, IEntity entity, bool state)
        {
            if (state)
            {
                shape.TriggerEnter(entity);
            }
            else
            {
                shape.TriggerExit(entity);
            }
        }
    }
}