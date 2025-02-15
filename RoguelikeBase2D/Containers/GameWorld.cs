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
            Map = map;
            PhysicsWorld.Populate(World);
        }

        public void StartPlayerTurn(Point direction)
        {
            /*
            var input = PlayerRef.Entity.Get<Input>();
            input.Direction = direction;
            input.SkipTurn = direction == Point.Zero;
            input.Processed = false;
            PlayerRef.Entity.Set(input);
            CurrentState = GameState.PlayerTurn;
            */
        }

        public void RemoveAllNonPlayerOwnedEntities()
        {
            /* PhysicsWorld.Clear();
            List<Entity> entities = new List<Entity>();
            World.GetEntities(new QueryDescription(), entities);

            foreach(var entity in entities)
            {
                if (entity.Has<Owner>())
                {
                    if (entity.Get<Owner>().OwnerReference != PlayerRef)
                    {
                        entity.Add(new Remove());
                    }
                }
                else if(entity.Reference() != PlayerRef)
                {
                    entity.Add(new Remove());
                }
            }

            World.Destroy(new QueryDescription().WithAll<Remove>());
            */
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
