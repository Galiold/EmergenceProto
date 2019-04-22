using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Rocket : MonoBehaviour
{
    private const string ROCKET_TAG = "Rocket";
    private const string PLAYER_TAG = "Player";
    public float speed = 5;
    public float damage = 5;
    public Vector3 transformUp;
    private Vector3 direction;
    private Rigidbody rb;
    private GameControl gameControl;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        gameControl = GameObject.FindObjectOfType<GameControl>();
    }

    private void Start()
    {
        float teta = Mathf.Atan2(transform.position.y, transform.position.x) * Mathf.Rad2Deg + 90;
        transform.eulerAngles = new Vector3(0, 0, teta);
        rb.velocity = -(speed * direction.normalized);
        gameControl.AddRocket(gameObject);
    }

    private void FixedUpdate()
    {
        // direction = new Vector3(Mathf.Sin(transform.forward), Mathf.Cos(transform.rotation.z), 0).normalized;
        // rb.velocity = transform.up * speed;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == PLAYER_TAG)
        {
            other.gameObject.GetComponent<Player>().DamagePlayer(damage);
        }
    }

    private void OnBecameInvisible()
    {
        gameControl.DeleteRocket(gameObject);
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
