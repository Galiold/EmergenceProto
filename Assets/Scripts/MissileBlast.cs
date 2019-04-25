using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SphereCollider))]
public class MissileBlast : MonoBehaviour
{
    private float propagationSpeed = 40;
    private readonly float MAX_RADIUS = 90;
    public float DamageRadius { get; set; }
    
    private void Update()
    {
        //TODO:  The damage radius here and in the editor are not the same, max radius is used instead  
        if (transform.localScale.x < MAX_RADIUS)
        {
            transform.localScale = new Vector3(transform.localScale.x + Time.deltaTime * propagationSpeed,
                transform.localScale.y + Time.deltaTime * propagationSpeed, 1);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Rocket")
        {
            other.GetComponent<Rigidbody>().AddForce(other.gameObject.transform.position - transform.position, ForceMode.Impulse);
            print(other.name);
        }
    }

    
}