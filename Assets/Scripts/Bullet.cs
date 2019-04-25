using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Bullet : MonoBehaviour
{
    public float speed = 5;
    public float damageRadius = 2;
    public Vector2 offset;
    private Vector3 direction;
    private Rigidbody rb;

    public GameObject blast;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        blast.SetActive(false);
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
            blast.SetActive(true);
            blast.GetComponent<MissileBlast>().DamageRadius = damageRadius;
            
            blast.transform.parent = null;
            Destroy(gameObject);
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
