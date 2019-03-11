using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public float life;
    public Image lifeBar;
    public void DamagePlayer(float damage)
    {
        life -= damage;

        lifeBar.fillAmount = life / 100;
    }
}
