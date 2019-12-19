using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaterComponent : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(0f, 0f, -5f * Time.deltaTime), Space.Self);
    }
}
