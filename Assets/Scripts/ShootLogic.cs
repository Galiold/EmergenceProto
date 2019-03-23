using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShootLogic : MonoBehaviour, IPointerDownHandler
{
    //TODO: Different Bullets


    [SerializeField]
    private GameObject bullet;
    private bool canShoot = true;
    private Vector3 instancePosition;
    private void Awake()
    {
        instancePosition = GameObject.FindGameObjectWithTag("Player").transform.position;
    }
    public void OnPointerDown(PointerEventData data)
    {
        if (canShoot)
        {
            GameObject b = Instantiate(bullet, instancePosition, Quaternion.identity);
            Vector3 direction = Camera.main.ScreenToWorldPoint(new Vector3(data.position.x, data.position.y, -Camera.main.transform.position.z));
            b.GetComponent<Bullet>().Direction = direction;
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        canShoot = false;
        yield return new WaitForSeconds(0.5f);
        canShoot = true;
    }

    public bool CanShoot
    {
        set
        {
            canShoot = value;
        }
    }
}
