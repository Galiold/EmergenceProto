using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Rocket : MonoBehaviour
{
    public float speed = 5;
    private Vector3 direction;
    private Rigidbody rb;
    public float teta;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }
    private void Start()
    {
        teta = Mathf.Atan2(transform.position.y, transform.position.x) * Mathf.Rad2Deg + 90;
        transform.eulerAngles = new Vector3(0, 0, teta);
        rb.GetComponent<Rigidbody>().velocity = -(speed * direction);
    }

    public Vector3 Direction
    {
        set
        {
            direction = value;
        }
    }

}
