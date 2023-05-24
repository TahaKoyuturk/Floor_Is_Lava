using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Transform Target;
    [SerializeField] private Transform camTransform;
    [SerializeField] private Vector3 Offset;
    [SerializeField] private float SmoothTime = 0.3f;

    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        Offset = camTransform.position - Target.position;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = Target.position + Offset;
        camTransform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, SmoothTime);

        transform.LookAt(Target);
    }
   
}
