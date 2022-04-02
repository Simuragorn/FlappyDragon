using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesPull : MonoBehaviour
{
    private List<Obstacle> obstacles;
    private List<Obstacle> prefabs;

    public void Init(List<Obstacle> obstaclePrefabs, int quantity)
    {
        prefabs = obstaclePrefabs;
        obstacles = new List<Obstacle>();
        int prefabId = 0;
        for (int i = 0; i < quantity; ++i)
        {
            Obstacle obstacle = CreateObstacle(prefabs[prefabId]);
            obstacles.Add(obstacle);
            prefabId++;
            if (prefabId >= prefabs.Count)
            {
                prefabId = 0;
            }
        }
    }

    public Obstacle GetRandomFromPull()
    {
        int prefabId = Random.Range(0, prefabs.Count);
        if (obstacles.Count == 0)
        {
            obstacles.Add(CreateObstacle(prefabs[prefabId]));
        }
        int obstacleId = Random.Range(0, obstacles.Count);
        var obstacle = obstacles[obstacleId];
        obstacles.RemoveAt(obstacleId);
        obstacle.gameObject.SetActive(true);
        return obstacle;
    }

    public void PutToPull(Obstacle obstacle)
    {
        if (!obstacles.Contains(obstacle))
        {
            obstacle.gameObject.SetActive(false);
            obstacle.transform.parent = null;
            obstacles.Add(obstacle);
        }
    }

    private Obstacle CreateObstacle(Obstacle prefab)
    {
        var obstacle = Instantiate(prefab);
        obstacle.transform.localPosition = Vector3.zero;
        obstacle.gameObject.SetActive(false);
        return obstacle;
    }
}
