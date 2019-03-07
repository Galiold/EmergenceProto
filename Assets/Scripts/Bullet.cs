using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5;
    private Vector3 direction;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rb.AddForce(direction.normalized * speed, ForceMode.Impulse);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public Vector3 Direction
    {
        set
        {
            direction = value;
        }
    }
}
