using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstaclesColumn : MonoBehaviour, IDestroyable
{
    [SerializeField] private float spaceBetweenLines;
    [SerializeField] private int maxSpacesInRow;
    private BoxCollider2D collider;
    private List<Obstacle> obstacles;

    public void Init(float xPosition)
    {
        ClearObstacles();

        float yPosition = ObstaclesManager.Instance.BottomBorder.position.y;
        int randomValue = Random.Range(0, 2);
        bool startFromSpace = randomValue == 1;
        if (startFromSpace)
        {
            float space = Random.Range(1, maxSpacesInRow + 1) * spaceBetweenLines;
            yPosition += space;
        }

        do
        {
            var obstacle = ObstaclesManager.Instance.ObstaclesPull.GetRandomFromPull();

            if (ObstaclesManager.Instance.TopBorder.position.y - yPosition - obstacle.Heidth < spaceBetweenLines)
            {
                ObstaclesManager.Instance.ObstaclesPull.PutToPull(obstacle);
                break;
            }
            obstacle.gameObject.transform.parent = transform;
            yPosition += obstacle.Heidth / 2;
            Vector3 spawnPosition = new Vector3(0, yPosition, 0);
            obstacle.gameObject.transform.localPosition = spawnPosition;
            obstacles.Add(obstacle);

            yPosition += obstacle.Heidth / 2;

            float space = Random.Range(1, maxSpacesInRow + 1) * spaceBetweenLines;
            yPosition += space;
        } while (ObstaclesManager.Instance.TopBorder.position.y - yPosition > spaceBetweenLines);

        float additionalWidth = 0;
        if (obstacles.Any())
        {
            additionalWidth = obstacles.Max(o => o.Width) / 2;
        }
        transform.localPosition = new Vector3(xPosition + additionalWidth, transform.localPosition.y, transform.localPosition.z);
        AddTrigger();
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
            obstacles.ForEach(o => ObstaclesManager.Instance.ObstaclesPull.PutToPull(o));
        }
        obstacles = new List<Obstacle>();
    }

    private void FixedUpdate()
    {
        Move();
        CheckBorder();
    }

    private void Move()
    {
        float speed = ObstaclesManager.Instance.Speed;
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
