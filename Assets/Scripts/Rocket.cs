using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class Rocket : MonoBehaviour
{
    private const string ROCKET_TAG = "Rocket";
    private const string PLAYER_TAG = "Player";
    public float speed = 5;
    public float damage = 5;
    private Vector3 direction;
    private Rigidbody rb;
    private bool hasHit;
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
        rb.GetComponent<Rigidbody>().velocity = -(speed * direction);
        gameControl.AddRocket(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == ROCKET_TAG)
        {
            Destroy(gameObject, 0.1f);
        }
        else if (other.gameObject.tag == PLAYER_TAG)
        {
            other.gameObject.GetComponent<Player>().DamagePlayer(damage);
            Destroy(gameObject, 0.1f);
        }
        gameControl.DeleteRocket(gameObject);
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
    public bool HasHit
    {
        get
        {
            return hasHit;
        }
    }
}
