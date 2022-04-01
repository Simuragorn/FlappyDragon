using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private float jumpDelayInSeconds;
    private bool jumpLoading;

    void Update()
    {
        ControlsHandle();
    }

    private void ControlsHandle()
    {
#if UNITY_STANDALONE
        if (Input.GetKeyUp(KeyCode.Space))
        {
            TryJump();
        }
#else
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            TryJump();
        }
#endif
    }

    private void TryJump()
    {
        if (!jumpLoading)
        {
            rigidbody.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            StartCoroutine(StartJumpDelay());
        }
    }

    private IEnumerator StartJumpDelay()
    {
        jumpLoading = true;
        yield return new WaitForSeconds(jumpDelayInSeconds);
        jumpLoading = false;
    }
}
