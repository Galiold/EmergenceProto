using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public GameObject rocket;
    private void Awake()
    {
        StartCoroutine(Generate());
    }

    private IEnumerator Generate()
    {
        float radius = Camera.main.ViewportToWorldPoint(new Vector3(Screen.width, 0, 0.05f)).x;
        while (true)
        {
            float teta = Random.Range(0, 360);
            Vector3 pos = new Vector3(radius * Mathf.Cos(teta), radius * Mathf.Sin(teta));
            GameObject r = Instantiate(rocket, pos, Quaternion.identity);
            r.GetComponent<Rocket>().Direction = pos;
            yield return new WaitForSeconds(Random.Range(1, 3));
        }
    }

}
