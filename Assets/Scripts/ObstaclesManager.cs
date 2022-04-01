using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesManager : MonoBehaviour
{
    [SerializeField] private List<Transform> obstaclePrefabs;
    [SerializeField] private float speed;
    [SerializeField] private float spaceBetweenObstacles;
    [SerializeField] private float spaceBetweenLines;

    private void Update()
    {
        MoveObstacles();
    }

    private void MoveObstacles()
    {
        obstaclePrefabs.ForEach(o =>
        {
            float xPosition = -speed * Time.deltaTime + o.position.x;
            o.position = new Vector3(xPosition, o.position.y, o.position.z);
        });
    }
}
