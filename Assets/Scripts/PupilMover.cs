using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PupilMover : MonoBehaviour
{
    [SerializeField] private SpriteRenderer eyeBallRenderer;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float border;

    private float maxMove;

    private void Start()
    {
        maxMove = Mathf.Min(eyeBallRenderer.bounds.size.x / 2 - border,
            eyeBallRenderer.bounds.size.y / 2 - border);

    }

    void Update()
    {
        WatchingPlayer();
    }

    private void WatchingPlayer()
    {
        if (Player.Instance == null)
        {
            return;
        }
        var direction = (Player.Instance.transform.position - transform.position).normalized;
        float distance = Time.deltaTime * moveSpeed;
        transform.Translate(direction.x * distance, direction.y * distance, 0, Space.Self);

        float xPos = transform.localPosition.x;
        xPos = xPos < -maxMove ? -maxMove : xPos;
        xPos = xPos > maxMove ? maxMove : xPos;


        float yPos = transform.localPosition.y;
        yPos = yPos < -maxMove ? -maxMove : yPos;
        yPos = yPos > maxMove ? maxMove : yPos;

        transform.localPosition = new Vector3(xPos, yPos, transform.localPosition.z);
    }
}
