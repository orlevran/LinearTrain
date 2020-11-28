using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public Transform train;

    void LateUpdate()
    {
        Vector3 newPosition = train.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
    }
}
