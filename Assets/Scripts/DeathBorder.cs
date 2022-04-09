using Assets.Scripts.Constants;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DeathBorder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Player.Instance == null)
        {
            return;
        }
        if (collision.gameObject == Player.Instance.gameObject)
        {
            Player.Instance.StartDeath(DeathTypeEnum.OutOfBorder);
        }
    }
}
