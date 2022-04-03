using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstaclesColumn : MonoBehaviour, IDestroyable
{
    private BoxCollider2D collider;
    private List<Obstacle> obstacles;
    [SerializeField] private int maxSpacesCount;
    [SerializeField] private float paddingSpace;

    public void Init(float xPosition)
    {
        ClearObstacles();

        float yPosition = ObstaclesManager.Instance.BottomBorder.position.y;
        float maxYPosition = ObstaclesManager.Instance.TopBorder.position.y;

        while (yPosition < maxYPosition)
        {
            var obstacle = ObstaclesManager.Instance.ObstaclesPull.GetRandomFromPull();

            obstacle.gameObject.transform.parent = transform;
            yPosition += obstacle.Heidth / 2;
            Vector3 spawnPosition = new Vector3(0, yPosition, 0);
            obstacle.gameObject.transform.localPosition = spawnPosition;
            obstacles.Add(obstacle);

            yPosition += obstacle.Heidth / 2 + paddingSpace;
        }
        SetSpaces();
        float additionalWidth = 0;
        if (obstacles.Any())
        {
            additionalWidth = obstacles.Max(o => o.Width) / 2;
        }
        transform.localPosition = new Vector3(xPosition + additionalWidth, transform.localPosition.y, transform.localPosition.z);
        AddTrigger();
    }

    private void SetSpaces()
    {
        for (int i = 0; i < maxSpacesCount; ++i)
        {
            int obstacleId = Random.Range(0, obstacles.Count);
            RemoveObstacleById(obstacleId);
        }
    }

    private void AddTrigger()
    {
        if (collider == null)
        {
            collider = gameObject.AddComponent<BoxCollider2D>();
            collider.isTrigger = true;
            float width = obstacles.Max(o => o.Width);
            float height = ObstaclesManager.Instance.TopBorder.position.y - ObstaclesManager.Instance.BottomBorder.position.y;
            collider.size = new Vector2(width, height);
            gameObject.AddComponent<ScoreTrigger>();
        }
    }

    private void ClearObstacles()
    {
        if (obstacles != null)
        {
            for (int i = 0; i < obstacles.Count; ++i)
            {
                RemoveObstacleById(i);
            }
        }
        obstacles = new List<Obstacle>();
    }

    private void RemoveObstacleById(int obstacleId)
    {
        if (obstacles.Count > obstacleId)
        {
            var obstacle = obstacles[obstacleId];
            obstacles.RemoveAt(obstacleId);
            ObstaclesManager.Instance.ObstaclesPull.PutToPull(obstacle);
        }
    }

    private void FixedUpdate()
    {
        Move();
        CheckBorder();
    }

    private void Move()
    {
        float speed = ObstaclesManager.Instance.ActualSpeed;
        float distance = speed * Time.deltaTime;

        transform.position = new Vector3(transform.position.x - distance, transform.position.y, transform.position.z);
    }

    private void CheckBorder()
    {
        float borderX = ObstaclesManager.Instance.ObstaclesEnd.transform.position.x;
        if (borderX >= transform.position.x)
        {
            Destroy();
        }
    }

    public void Destroy()
    {
        ObstaclesManager.Instance.InitObstacleColumn(this);
    }
}
