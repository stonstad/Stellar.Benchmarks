using Stellar.Collections;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Stellar.Benchmarking
{
    public class FastDBConcurrency
    {
        public static async Task Test(int writers, int readers)
        {
            // test: concurrency
            FastDBOptions options = new FastDBOptions();
            await TestConcurrency("Concurrency", options, writers, readers);
        }

        public static async Task TestConcurrency(string test, FastDBOptions options, int writers, int readers)
        {
            FastDB FastDB = new FastDB(options);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int readsPerSecond = 0;
            int writesPerSecond = 0;
            int reads = 0;
            int writes = 0;

            TimeSpan duration = TimeSpan.FromSeconds(300);
            TimeSpan updateRate = TimeSpan.FromMilliseconds(20);

            var players = FastDB.GetCollection<int, Player>();
            var playerPositions = FastDB.GetCollection<int, Spatial>();

            void CreateWriter(int id)
            {
                _ = Task.Run(async () =>
                {
                    Player player = new Player();
                    player.Id = id;
                    player.Name = $"Writer {player.Id}";

                    Spatial spatial = new Spatial();
                    spatial.Id = player.Id;

                    players.Add(player.Id, player);
                    playerPositions.Add(spatial.Id, spatial);

                    while (stopwatch.Elapsed < duration)
                    {
                        player.Action = (ActionType)Random.Shared.Next(0, 4);

                        if (player.Action == ActionType.Walking)
                        {
                            spatial.Position[0] += Random.Shared.Next(-1, 2);
                            spatial.Position[1] += Random.Shared.Next(-1, 2);
                            spatial.Position[2] += Random.Shared.Next(-1, 2);
                        }

                        player.Name = Random.Shared.Next(1, 10000).ToString();
                        players.Update(player.Id, player);
                        playerPositions.Update(spatial.Id, spatial);
                        Interlocked.Add(ref writesPerSecond, 2);

                        await Task.Delay(updateRate);
                    }
                });
            }

            void CreateReader(int id)
            {
                _ = Task.Run(async () =>
                {
                    var players = FastDB.GetCollection<int, Player>();
                    var playerPositions = FastDB.GetCollection<int, Spatial>();

                    while (stopwatch.Elapsed < duration)
                    {
                        Player player = players.Where(a => a.Id == id).FirstOrDefault();
                        Spatial position = playerPositions.Where(a => a.Id == id).FirstOrDefault();
                        Interlocked.Add(ref readsPerSecond, 2);
                        await Task.Delay(updateRate);
                    }
                });
            }

            for (int i = 1; i <= writers; i++)
                CreateWriter(i);

            // create reader
            for (int i = 1; i <= readers; i++)
                CreateReader(Random.Shared.Next(1, writers + 1));

            _ = Task.Run(async () =>
            {
                Console.Clear();

                var players = FastDB.GetCollection<int, Player>();
                var playerPositions = FastDB.GetCollection<int, Spatial>();

                while (stopwatch.Elapsed < duration)
                {
                    writes += writesPerSecond;
                    reads += readsPerSecond;

                    Console.SetCursorPosition(0, 0);
                    TestOutput.WriteTestHeader(test, $"{writers} writers, {readers} readers", FastDB.Count());
                    Console.WriteLine();
                    Console.WriteLine($"{writes} writes ({writesPerSecond.ToString("N0")}/sec)   ");
                    Console.WriteLine($"{reads} reads ({readsPerSecond.ToString("N0")}/sec)   ");
                    Console.WriteLine();

                    writesPerSecond = 0;
                    readsPerSecond = 0;

                    for (int i = 1; i <= Math.Min(5, writers); i++)
                    {
                        Player player = players.Where(a => a.Id == i).FirstOrDefault();
                        Spatial position = playerPositions.Where(a => a.Id == i).FirstOrDefault();
                        Interlocked.Add(ref readsPerSecond, writers * 2);
                        if (player != null)
                            Console.WriteLine($"Player {player.Id} is {player.Action} at ({position.Position[0]}, {position.Position[1]}, {position.Position[2]})   ");
                    }

                    if (writers > 5)
                    {
                        Console.WriteLine("...");
                        for (int i = writers - 4; i <= writers; i++)
                        {
                            Player player = players.Where(a => a.Id == i).FirstOrDefault();
                            Spatial position = playerPositions.Where(a => a.Id == i).FirstOrDefault();
                            Interlocked.Add(ref readsPerSecond, writers * 2);
                            if (player != null)
                                Console.WriteLine($"Player {player.Id} is {player.Action} at ({position.Position[0]}, {position.Position[1]}, {position.Position[2]})   ");
                        }
                    }

                    await Task.Delay(1000);
                }
            });

            await Task.Delay(duration);
        }

        public enum ActionType { Standing, Walking, Chatting, Sleeping }

        public sealed class Player
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public ActionType Action { get; set; }
            public DateTime UpdatedDate { get; set; }
        }

        public sealed class Spatial
        {
            public int Id { get; set; }
            public float[] Position { get; set; } = new float[3];
        }

    }

    public enum ActionType { Standing, Walking, Chatting, Sleeping }

    public sealed class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ActionType Action { get; set; }
        public DateTime UpdatedDate { get; set; }
    }

    public sealed class Spatial
    {
        public int Id { get; set; }
        public float[] Position { get; set; } = new float[3];
    }
}
