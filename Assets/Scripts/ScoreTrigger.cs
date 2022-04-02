using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    private int bonusScore = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Player.Instance.gameObject == collision.gameObject)
        {
            GameManager.Instance.AddScore(bonusScore);
        }
    }
}
