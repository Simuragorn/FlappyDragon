using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private Rigidbody2D rigidbody;
    private Quaternion downRotation;
    private Quaternion forwardRotation;
    [SerializeField] float tiltSmooth;
    [SerializeField] Animator animator;

    private void Start()
    {
        downRotation = Quaternion.Euler(0, 0, -90);
        forwardRotation = Quaternion.Euler(0, 0, 35);
    }

    void Update()
    {
        ControlsHandle();
    }

    private void ControlsHandle()
    {
#if UNITY_STANDALONE
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            TryJump();
        }
#else
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            TryJump();
        }
#endif
        transform.rotation = Quaternion.Lerp(transform.rotation, downRotation, tiltSmooth * Time.deltaTime);
    }

    private void TryJump()
    {
        animator.SetTrigger("Flapping");
        rigidbody.velocity = Vector2.zero;
        rigidbody.AddForce(Vector2.up * force, ForceMode2D.Force);

        transform.rotation = forwardRotation;
    }
}
