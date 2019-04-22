using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Bullet : MonoBehaviour
{
    public float speed = 5;
    public float damageRadius = 2;
    public Vector2 offset;
    public LayerMask layer;
    public float explosionForce;
    private Vector3 direction;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        float teta = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, teta + 180);
        rb.AddForce(direction.normalized * speed, ForceMode.Impulse);
        Destroy(gameObject, 20);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Rocket")
        {
            Destroy(gameObject, 0.2f);
        }

        Collider[] rockets = Physics.OverlapSphere(transform.position, damageRadius, layer);


        foreach (var rocket in rockets)
        {
            // if (rocket.gameObject != other.gameObject)
            rocket.GetComponent<Rigidbody>().AddForce(rocket.gameObject.transform.position - transform.position, ForceMode.Impulse);
        }

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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + new Vector3(offset.x, offset.y, 0), damageRadius);
    }
}
