using Assets.Scripts.Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Obstacle : MonoBehaviour
{
    [SerializeField] private BoxCollider2D collider;
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private int clashSoreReward;
    [SerializeField] private float onClashGravityScale;
    public float Width => collider.size.x;
    public float Heidth => collider.size.y;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!enabled)
        {
            return;
        }
        if (Player.Instance != null && collision.gameObject == Player.Instance.gameObject)
        {
            Player.Instance.StartDeath(DeathTypeEnum.Clash);
        }
        else if (!collision.gameObject.CompareTag(TagConstants.OBSTACLE_TAG) ||
            !GameManager.Instance.GameEnded)
        {
            return;
        }
        GameManager.Instance.AddScore(clashSoreReward, BonusScoreTypeEnum.Clash);
        rigidbody.bodyType = RigidbodyType2D.Dynamic;
        rigidbody.gravityScale = onClashGravityScale;
        enabled = false;
    }
}
