using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstaclesManager : MonoBehaviour
{
    public Transform ObstaclesSpawn => obstaclesSpawn;
    public Transform ObstaclesEnd => obstaclesEnd;
    public Transform TopBorder => topBorder;
    public Transform BottomBorder => bottomBorder;
    public ObstaclesPull ObstaclesPull => obstaclePull;
    public static ObstaclesManager Instance;
    public float ActualSpeed => actualSpeed;

    [SerializeField] private Transform obstaclesSpawn;
    [SerializeField] private Transform obstaclesEnd;

    [SerializeField] private ObstaclesColumn obstacleColumnPrefab;
    [SerializeField] private List<Obstacle> obstaclePrefabs;

    [SerializeField] private float speed;
    private float actualSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float speedStep;

    [SerializeField] private float spaceBetweenColumns;
    [SerializeField] private Transform topBorder;
    [SerializeField] private Transform bottomBorder;
    [SerializeField] private int columnsCount;
    [SerializeField] private ObstaclesPull obstaclePull;

    private List<ObstaclesColumn> obstaclesColumns;
    private ObstaclesColumn lastColumn;

    private void Awake()
    {
        Instance = this;
        actualSpeed = speed;
    }

    private void Start()
    {
        obstaclePull.Init(obstaclePrefabs, obstaclePrefabs.Count * 20);
        SpawnColumns();
    }

    private void SpawnColumns()
    {
        obstaclesColumns = new List<ObstaclesColumn>();
        for (int i = 0; i < columnsCount; ++i)
        {
            var column = Instantiate(obstacleColumnPrefab, obstaclesSpawn);
            obstaclesColumns.Add(column);
            InitObstacleColumn(column);
        }
    }

    public void InitObstacleColumn(ObstaclesColumn column)
    {
        float xPosition = 0;
        if (lastColumn != null)
        {
            xPosition = lastColumn.gameObject.transform.localPosition.x + spaceBetweenColumns;
        }
        column.Init(xPosition);
        lastColumn = column;
    }

    public void Stop()
    {
        actualSpeed = 0;
    }

    public void Continue()
    {
        actualSpeed = speed;
    }

    public void IncreaseSpeed()
    {
        if (actualSpeed == 0)
        {
            return;
        }
        speed += speedStep;
        speed = Mathf.Min(speed, maxSpeed);
        actualSpeed = speed;
    }
}
