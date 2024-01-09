using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    [SerializeField] Transform target = null;
    [SerializeField][Range(20, 90)] float pitch = 40;
    [SerializeField][Range(2, 8)] float distance = 5;
    [SerializeField][Range(0.1f, 0.2f)] float sensitivty = 0.2f;

    float yaw = 0;

    void Update()
    {
        yaw += Input.GetAxis("Mouse X") * sensitivty;

        Quaternion qpitch = Quaternion.AngleAxis(pitch, Vector3.right);
        Quaternion qyaw = Quaternion.AngleAxis(yaw, Vector3.up);
        Quaternion rotation = qyaw * qpitch;

        transform.position = target.position + (qpitch * Vector3.back * distance);
        transform.rotation = rotation;
    }
}
