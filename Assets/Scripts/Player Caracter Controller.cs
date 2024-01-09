using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerCaracterController : MonoBehaviour
{
    Rigidbody rb;
    Vector3 force = Vector3.zero;
    [SerializeField][Range(1, 10)] float maxForce = 5;
    [SerializeField][Range(1, 10)] float maxJForce = 5;
    [SerializeField] Transform view;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 direction = Vector3.zero;

        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");

        force = view.rotation * direction * maxForce;

        if(Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * maxJForce, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(force, ForceMode.Force);
    }
}
