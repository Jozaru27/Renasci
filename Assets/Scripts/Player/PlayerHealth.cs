using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public void ChangeHealthAmount(int amount)
    {
        StatsManager.Instance.life += amount;

        if (StatsManager.Instance.life < 0)
        {
            GetComponent<PlayerAnimation>().Death();
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().freezeRotation = true;
            GameManager.Instance.gameOver = true;
            UIManager.Instance.EnableGameOverMenu();
            StatsManager.Instance.life = 0;
        }
        else
            GetComponent<PlayerAnimation>().Hit();

        UIManager.Instance.ChangeLife();
        GameManager.Instance.playerCannotMove = true;
    }
}
