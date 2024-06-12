using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace FinalProject
{
    //static class SpawnMngr
    //{
    //    private static int queueSize = 10;
    //    private static Queue<Enemy>[] enemies;
    //    //private static List<Enemy> activeEnemies;

    //    private static float nextSpawn;

    //    public static void Init()
    //    {
    //        enemies = new Queue<Enemy>[(int)EnemyType.LAST];

    //        for (int i = 0; i < enemies.Length; i++)
    //        {
    //            enemies[i] = new Queue<Enemy>(queueSize);

    //            switch((EnemyType)i)
    //            {
    //                case EnemyType.Enemy01:
    //                    for (int j = 0; j < queueSize; j++)
    //                    {
    //                        enemies[i].Enqueue(new Enemy_01());
    //                    }
    //                    break;

    //                case EnemyType.EnemyBig:
    //                    for (int j = 0; j < queueSize; j++)
    //                    {
    //                        enemies[i].Enqueue(new Enemy_Big());
    //                    }
    //                    break;
    //            }
    //        }

    //        nextSpawn = RandomGenerator.GetRandomInt(1, 3);
    //    }

    //    public static void Update()
    //    {
    //        nextSpawn -= Game.DeltaTime;

    //        if(nextSpawn <= 0)
    //        {
    //            int enemyType = RandomGenerator.GetRandomInt(0, (int)EnemyType.LAST);
    //            nextSpawn = RandomGenerator.GetRandomInt(3, 5);
    //            SpawnEnemy((EnemyType)enemyType);
    //        }
    //    }

    //    private static void SpawnEnemy(EnemyType type)
    //    {
    //        int queueIndex = (int)type;

    //        if(enemies[queueIndex].Count > 0)
    //        {
    //            Enemy enemy = enemies[queueIndex].Dequeue();

    //            if(enemy is Enemy_01)
    //            {
    //                enemy.Position = new Vector2(Game.Window.Width + enemy.HalfWidth, RandomGenerator.GetRandomInt(enemy.HalfHeight, Game.Window.Height - enemy.HalfHeight));
    //            }

    //            if (enemy is Enemy_Big)
    //            {
    //                enemy.Position = new Vector2(Game.Window.Width + enemy.HalfWidth, RandomGenerator.GetRandomInt(enemy.HalfHeight, Game.Window.Height - enemy.HalfHeight));
    //            }

    //            enemy.IsActive = true;
    //            enemy.Reset();
    //        }
    //    }

    //    public static void RestoreEnemy(Enemy enemy)
    //    {
    //        enemy.IsActive = false;
    //        enemies[(int)enemy.Type].Enqueue(enemy);
    //    }

    //    public static void ClearAll()
    //    {
    //        for (int i = 0; i < enemies.Length; i++)
    //        {
    //            enemies[i].Clear();
    //        }

    //        enemies = null;
    //    }
    //}
}
