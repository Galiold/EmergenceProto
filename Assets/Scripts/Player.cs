using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public float life;
    public Image lifeBar;

    public void DamagePlayer(float damage)
    {
        life -= damage;

        lifeBar.fillAmount = life / 100;

        if (life <= 0)
        {
            GameObject.FindObjectOfType<GameControl>().OnPlayerDeath();
            Destroy(gameObject, 0.1f);
        }
    }


}
