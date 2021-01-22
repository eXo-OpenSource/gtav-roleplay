using System;
using AltV.Net.Elements.Entities;
using System.Collections.Generic;

namespace Exo.Rp.Core.Colshape
{
    public class Colshape : AltV.Net.Elements.Entities.ColShape
    {

        public delegate void ColShapeEnter(Colshape colshape, IEntity entity);
        public delegate void ColShapeExit(Colshape colshape, IEntity entity);

        public event ColShapeEnter OnColShapeEnter;
        public event ColShapeExit OnColShapeExit;

        public List<IEntity> Entities = new List<IEntity>();

        public Colshape(IntPtr nativePointer) : base(nativePointer)
        {
        }

        public void TriggerEnter(IEntity entity)
        {
            Entities.Add(entity);
            OnColShapeEnter?.Invoke(this, entity);
        }

        public void TriggerExit(IEntity entity)
        {
            Entities.Remove(entity);
            OnColShapeExit?.Invoke(this, entity);
        }
    }
}