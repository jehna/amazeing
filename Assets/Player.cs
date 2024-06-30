using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        MoveTowardsMouse();
    }

    void MoveTowardsMouse()
    {
        if (!Input.GetMouseButton(0))
        {
            rigidbody.velocity = Vector3.zero;
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetPosition = hit.point;
            targetPosition.y = 0;

            Vector3 direction = targetPosition - transform.position;
            direction.y = 0;
            direction.x = Mathf.Min(5, direction.x);
            direction.z = Mathf.Min(5, direction.z);
            rigidbody.velocity = direction * 10;
        }
    }
}
