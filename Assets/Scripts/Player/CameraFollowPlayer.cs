using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 15f;

    public Vector3 offset;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position;
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
