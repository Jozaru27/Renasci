using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public void ChangeHealth(int amount)
    {
        StatsManager.Instance.life += amount;

        if (StatsManager.Instance.life < 0)
        {
            GetComponent<PlayerAnimation>().Death();
            GameManager.Instance.gameOver = true;
            UIManager.Instance.EnableGameOverMenu();
            StatsManager.Instance.life = 0;
        }
        else
            GetComponent<PlayerAnimation>().Hit(); 

        GameManager.Instance.cannotMove = true;
    }
}
