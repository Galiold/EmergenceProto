using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Block : MonoBehaviour
{
    public float life = 20;
    private Rigidbody rb;
    private bool canMove;
    private float interval;


    private void Awake()
    {
        gameObject.layer = 10;
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;

    }

    private void Start()
    {
        StartCoroutine(CanMove());

        gameObject.transform.localScale = Random.Range(0, 2) == 1 ?
         new Vector3(5, Random.Range(15, 20), 1) :
         new Vector3(Random.Range(15, 20), 5, 1);
    }

    private void OnMouseDrag()
    {
        if (canMove)
        {
            gameObject.transform.position = Camera.main.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        life -= 5;
        if (life < 5)
            Destroy(gameObject, 0.1f);
    }

    private IEnumerator CanMove()
    {
        canMove = true;
        yield return new WaitForSeconds(interval);
        canMove = false;
        gameObject.layer = 0;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    public float Interval
    {
        set
        {
            interval = value;
        }
    }
}
