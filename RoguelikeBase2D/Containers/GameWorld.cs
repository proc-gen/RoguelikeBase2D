using Arch.Core;
using Arch.Core.Extensions;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using RoguelikeBase2D.Constants;
using RoguelikeBase2D.ECS.Components;
using RoguelikeBase2D.Maps;
using RoguelikeBase2D.Pathfinding;
using RoguelikeBase2D.Serializaton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.Containers
{
    public class GameWorld
    {
        [JsonIgnore]
        public World World { get; set; }
        [JsonIgnore]
        public PhysicsWorld PhysicsWorld { get; set; }
        [JsonIgnore]
        public EntityReference PlayerRef { get; set; }
        public SerializableWorld SerializableWorld { get; set; }
        public GameState CurrentState { get; set; }
        public List<string> GameLog { get; set; }
        public Map Map { get; set; }
        public HashSet<Point> PlayerFov { get; set; }

        public GameWorld() 
        {
            World = World.Create();
            PhysicsWorld = new PhysicsWorld();
            CurrentState = GameState.Loading;
            GameLog = new List<string>();
            PlayerFov = new HashSet<Point>();
            PlayerRef = EntityReference.Null;
        }

        public void SetMap(Map map)
        {
            LogEntry(string.Format("Now entering: {0}", map.Seed));
            Map = map;
            PhysicsWorld.Populate(World);
        }

        public void RemoveAllNonPlayerOwnedEntities()
        {
            PhysicsWorld.Clear();
            var ownerQuery = new QueryDescription().WithAll<Owner>();
            var nonPlayerQuery = new QueryDescription().WithNone<Player, Owner>();

            World.Query(in ownerQuery, (Entity entity, ref Owner owner) =>
            {
                if (!owner.OwnerReference.Entity.Has<Player>())
                {
                    entity.Add<Remove>();
                }
            });

            World.Query(in nonPlayerQuery, (Entity entity) =>
            {
                entity.Add<Remove>();
            });

            World.Destroy(new QueryDescription().WithAll<Remove>());
        }

        public void HandlePlayerDeathOrRestart(bool isDead)
        {
            if (isDead)
            {
                LogEntry("You died.");
            }
            else
            {
                LogEntry("You end your journey to start anew");
            }

            var ownerQuery = new QueryDescription().WithAll<Owner>();
            World.Query(in ownerQuery, (Entity entity, ref Owner owner) =>
            {
                if (owner.OwnerReference.Entity.Has<Player>())
                {
                    entity.Add<Remove>();
                }
            });

            World.Destroy(PlayerRef);
            PlayerRef = EntityReference.Null;
        }

        public void LogEntry(string entry)
        {
            GameLog.Add(string.Format("{0}: {1}", DateTime.Now, entry));
        }

        public void SaveGame()
        {
            /*
            SerializableWorld = SerializableWorld.CreateSerializableWorld(World);
            SaveGameManager.SaveGame(this);
            */
        }

        public void LoadGame()
        {
            /*
            var world = SaveGameManager.LoadGame();
            
            World = SerializableWorld.CreateWorldFromSerializableWorld(world.SerializableWorld);
            CurrentState = world.CurrentState;
            GameLog = world.GameLog;
            Maps = world.Maps;
            CurrentMap = world.CurrentMap;
            PlayerFov = world.PlayerFov;

            PopulatePhysicsWorld();
            GameLog.Add("Welcome back traveler");
            */
        }
    }
}
