using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAimCamera : MonoBehaviour
{
    public GameObject target;
    public float rotateSpeed = 5;
    Vector3 offset;

    void Start()
    {
        offset = target.transform.position - transform.position;
    }

    void LateUpdate()
    {
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        float vertical = Input.GetAxis("Mouse Y") * -1 * rotateSpeed;

        target.transform.rotation = Quaternion.Euler(new Vector3(0.0f, horizontal, 0.0f));


    }
}
