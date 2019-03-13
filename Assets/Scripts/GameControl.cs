using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameControl : MonoBehaviour
{
    public GameObject rocket;
    public GameObject block;
    public int numberOfRocketsForEachRound = 5;
    public float eachRoundInterval;
    public Text roundUI;
    private int counter = 0;
    private ArrayList rockets;
    private ShootLogic shootLogic;
    private int round;
    public Text instruction;
    public Text finalText;
    private void Awake()
    {
        finalText.enabled = false;
        round = 0;
        shootLogic = GameObject.FindObjectOfType<ShootLogic>();
        rockets = new ArrayList();
        StartCoroutine(Generate());
    }

    private IEnumerator Generate()
    {
        yield return new WaitForSeconds(eachRoundInterval);
        if (instruction.enabled)
            instruction.enabled = false;
        float radius = Camera.main.ViewportToWorldPoint(new Vector3(Screen.width, 0, 0.05f)).x;
        round++;
        UpdateUI();
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
        GameObject b = Instantiate(block, transform.position + new Vector3(0, 15), Quaternion.identity);
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

    private void UpdateUI()
    {
        int currentRound;
        int.TryParse(roundUI.text, out currentRound);
        currentRound = round;
        roundUI.text = "Round " + round.ToString();
    }

    private void OnAllRocketsHit()
    {
        ClearScreen();
        CreateBlock();
        counter = 0;
        numberOfRocketsForEachRound += Random.Range(5, 10);
        StopAllCoroutines();
        StartCoroutine(Generate());
    }

    public void OnPlayerDeath()
    {
        ShowFinalText();
    }

    private void ShowFinalText()
    {
        finalText.enabled = true;
        StartCoroutine(ReloadScene());
    }

    private IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("SampleScene");
    }
}
