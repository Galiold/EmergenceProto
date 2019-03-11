﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public GameObject rocket;
    public GameObject block;
    public int numberOfRocketsForEachRound = 5;
    public float eachRoundInterval;
    public int counter = 0;
    public int round;
    private ArrayList rockets;
    private ShootLogic shootLogic;

    private void Awake()
    {
        shootLogic = GameObject.FindObjectOfType<ShootLogic>();
        StartCoroutine(Generate());
        rockets = new ArrayList();
    }

    private IEnumerator Generate()
    {
        yield return new WaitForSeconds(eachRoundInterval);
        float radius = Camera.main.ViewportToWorldPoint(new Vector3(Screen.width, 0, 0.05f)).x;
        round++;
        while (counter < numberOfRocketsForEachRound)
        {
            float teta = Random.Range(0, 360);
            Vector3 pos = new Vector3(radius * Mathf.Cos(teta), radius * Mathf.Sin(teta));
            GameObject r = Instantiate(rocket, pos, Quaternion.identity);
            r.GetComponent<Rocket>().Direction = pos;
            counter++;
            yield return new WaitForSeconds(Random.Range(1, 3));
        }
    }

    private void CreateBlock()
    {
        GameObject b = Instantiate(block, transform.position, Quaternion.identity);
        b.GetComponent<Block>().Interval = eachRoundInterval;
    }

    private void ClearScreen()
    {
        Rocket[] enemies = GameObject.FindObjectsOfType<Rocket>();

        foreach (var enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }

    }

    public void AddRocket(GameObject rocket)
    {
        rockets.Add(rocket);
    }

    public void DeleteRocket(GameObject rocket)
    {
        rockets.Remove(rocket);

        if (rockets.Count == 0 && counter == numberOfRocketsForEachRound)
        {
            OnAllRocketsHit();
        }
    }

    private void OnAllRocketsHit()
    {
        ClearScreen();
        CreateBlock();
        counter = 0;
        numberOfRocketsForEachRound += Random.Range(5, 10);
        StartCoroutine(Generate());
    }
}
