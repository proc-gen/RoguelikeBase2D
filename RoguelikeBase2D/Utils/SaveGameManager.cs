using Arch.Core;
using Arch.Core.Extensions;
using Newtonsoft.Json;
using RoguelikeBase2D.Containers;
using RoguelikeBase2D.ECS.Components;
using RoguelikeBase2D.Serializaton;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.Utils
{
    public static class SaveGameManager
    {
        public static GameWorld NewGame()
        {
            DeleteSaveData();
            var world = new GameWorld();
            return world;
        }

        public static GameWorld LoadGame()
        {
            string data = string.Empty;
            using (StreamReader file = new StreamReader("savegame.json"))
            {
                data = file.ReadToEnd();
            }

            GameWorld world = null;

            using (var sr = new StringReader(data))
            {
                using (JsonReader reader = new JsonTextReader(sr))
                {
                    JsonSerializer serializer = new JsonSerializer();

                    world = serializer.Deserialize<GameWorld>(reader);
                }
            }

            world = PostLoadProcessing(world);
            return world;
        }

        private static GameWorld PostLoadProcessing(GameWorld world)
        {
            world.World = SerializableWorld.CreateWorldFromSerializableWorld(world.SerializableWorld);

            QueryDescription query = new QueryDescription().WithAll<Position>();
            world.World.Query(in query, (Entity entity, ref Position pos) =>
            {
                var reference = entity.Reference();
                if (entity.Has<Player>())
                {
                    world.PlayerRef = reference;
                }
            });

            world.PhysicsWorld.Populate(world.World);

            return world;
        }

        public static void SaveGame(GameWorld world)
        {
            world.SerializableWorld = SerializableWorld.CreateSerializableWorld(world.World);

            string jsonData;
            using (var sw = new StringWriter())
            {
                if (world != null)
                {
                    using (JsonWriter writer = new JsonTextWriter(sw))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(writer, world);
                    }
                }
                jsonData = sw.ToString();
            }

            DeleteSaveData();

            using (StreamWriter file = new StreamWriter("savegame.json"))
            {
                file.Write(jsonData);
            }
        }

        public static void DeleteSaveData()
        {
            if (File.Exists("savegame.json"))
            {
                File.Delete("savegame.json");
            }
        }
    }
}
