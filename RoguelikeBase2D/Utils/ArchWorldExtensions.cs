using Arch.Core.Utils;
using Arch.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arch.Core.Extensions;

namespace RoguelikeBase2D.Utils
{
    public static class ArchWorldExtensions
    {
        private static ComponentType[] GetComponentTypesForArchetype(object[] components)
        {
            ComponentType[] types = new ComponentType[components.Length];
            for (int i = 0; i < components.Length; i++)
            {
                ComponentType type;
                if (!ComponentRegistry.TryGet(components[i].GetType(), out type))
                {
                    type = ComponentRegistry.Add(components[i].GetType());
                }
                types[i] = type;
            }
            return types;
        }

        public static Entity CreateFromArray(this World world, object[] components)
        {
            ComponentType[] types = GetComponentTypesForArchetype(components);
            Entity entity = world.Create(types);
            world.SetFromArray(entity, components);
            return entity;
        }

        public static void AddFromArray(this World world, Entity entity, object[] components)
        {
            entity.AddRange(components.AsSpan());
        }

        public static void SetFromArray(this World world, Entity entity, object[] components)
        {
            entity.SetRange(components.AsSpan());
        }

        public static void RemoveFromArray(this World world, Entity entity, object[] components)
        {
            ComponentType[] componentsToRemove = new ComponentType[components.Length];

            for (int i = 0; i < components.Length; i++)
            {
                ComponentType type;
                if (!ComponentRegistry.TryGet(components[i].GetType(), out type))
                {
                    type = ComponentRegistry.Add(components[i].GetType());
                }
                componentsToRemove[i] = type;
            }

            world.RemoveRange(entity, componentsToRemove);
        }

    }
}
