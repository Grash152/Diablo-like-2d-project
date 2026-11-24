using System;
using UnityEngine;

public class BarCD : MonoBehaviour
{

    public Transform targetPos;
    [SerializeField] private Vector3 Offset;

    private void LateUpdate()
    {
        if (targetPos != null)
        {
            transform.position = targetPos.position+ Offset;
        }
    }
}
