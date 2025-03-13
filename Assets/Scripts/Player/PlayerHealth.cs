using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public void ChangeHealth(int amount)
    {
        StatsManager.Instance.life += amount;

        if (StatsManager.Instance.life < 0)
            StatsManager.Instance.life = 0;
    }
}
