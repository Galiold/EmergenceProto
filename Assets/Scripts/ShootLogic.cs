using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShootLogic : MonoBehaviour, IPointerDownHandler
{
    //TODO: Reload Time for player
    //TODO: Explode Rockets when they collide eachother
    //TODO: Explode Bullets when they collide with Rockets
    //TODO: Different Bullets

    [SerializeField]
    private GameObject bullet;
    private bool canShoot = true;
    public void OnPointerDown(PointerEventData data)
    {
        if (canShoot)
        {
            GameObject b = Instantiate(bullet, Vector3.zero, Quaternion.identity);
            Vector3 direction = Camera.main.ScreenToWorldPoint(new Vector3(data.position.x, data.position.y, -Camera.main.transform.position.z));
            b.GetComponent<Bullet>().Direction = direction;
            canShoot = false;
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(0.5f);
        canShoot = true;
    }
}
