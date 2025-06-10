using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [HideInInspector] public bool damaged;
    
    [SerializeField] float invencibleTime;
    [SerializeField] Material material1, material2;
    [SerializeField] Renderer[] rend;

    bool invencible;
    bool gotDamage;
    bool dashed;
    bool lifeRegen;

    public void ChangeHealthAmount(float amount, Vector3 enemyPosition, float pushForce)
    {
        float randomNum = Random.Range(0, 100);

        if (amount < 0)
        {
            if (!invencible)
            {
                if (randomNum > StatsManager.Instance.evasion)
                {
                    damaged = true;

                    StatsManager.Instance.life += amount;
                    InventoryMenu.Instance.UpdateStats();

                    if (StatsManager.Instance.life <= 0)
                        PlayerDeath();
                    else
                    {
                        VibrationManager.Instance.RumbleGamepad(0.5f, 0.25f, 0.5f);
                        GetComponent<PlayerAnimation>().Hit();
                        StopAllCoroutines();

                        if (lifeRegen)
                            StartCoroutine(LifeRegeneration());

                        StartCoroutine(MakePlayerVencible(invencibleTime));
                    }

                    PushCharacter(enemyPosition, pushForce);
                    StartCoroutine(ChangingColor());
                    GameManager.Instance.playerCannotMove = true;
                }
            }
        }
        else
            StatsManager.Instance.life += amount;

        if (StatsManager.Instance.life > StatsManager.Instance.maxLife)
            StatsManager.Instance.life = StatsManager.Instance.maxLife;

        UIManager.Instance.ChangeLife();
    }

    void PlayerDeath()
    {
        VibrationManager.Instance.RumbleGamepad(0.75f, 0.5f, 2f);
        GetComponent<PlayerAnimation>().Death();
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().freezeRotation = true;
        GameManager.Instance.gameOver = true;
        StatsManager.Instance.life = 0;
    }

    public void OpenGameOverMenu()
    {
        UIManager.Instance.EnableGameOverMenu();
    }

    void PushCharacter(Vector3 enemyPosition, float pushForce)
    {
        Vector3 impulseDirection = gameObject.transform.position - enemyPosition;
        impulseDirection = new Vector3(impulseDirection.x, 0, impulseDirection.z);

        GetComponent<Rigidbody>().AddForce(impulseDirection.normalized * pushForce, ForceMode.Impulse);
    }

    IEnumerator ChangingColor()
    {
        //GetComponent<Renderer>().material.color = Color.blue;
        //GameObject.Find("PlayerCh").GetComponent<Renderer>().material = material2;////

        foreach(Renderer thisRend in rend)
        {
            thisRend.material.color = Color.red;
        }

        yield return new WaitForSeconds(0.125f);

;       //GetComponent<Renderer>().material.color = Color.white;
        //GameObject.Find("PlayerCh").GetComponent<Renderer>().material = material1;////

        foreach (Renderer thisRend in rend)
        {
            thisRend.material.color = Color.white;
        }

        ChangeVencibleColor();
    }

    public IEnumerator MakePlayerVencible(float time)
    {
        gotDamage = true;
        invencible = true;

        yield return new WaitForSeconds(time);

        //GetComponent<Renderer>().material.color = Color.white;

        if (!dashed)
        {
            //GameObject.Find("PlayerCh").GetComponent<Renderer>().material.color = Color.white;////
            //GameObject.Find("PlayerCh").GetComponent<Renderer>().material = material1;////

            foreach (Renderer thisRend in rend)
            {
                thisRend.material.color = Color.white;
            }

            invencible = false;
        }
        
        gotDamage = false;
        StartCoroutine(LifeRegeneration());
    }

    public IEnumerator InvencibleDash(float time)
    {
        invencible = true;
        dashed = true;

        VibrationManager.Instance.RumbleGamepad(0.25f, 0f, 0.25f);

        yield return new WaitForSeconds(time);

        //GetComponent<Renderer>().material.color = Color.white;

        if (!gotDamage)
        {
            //GameObject.Find("PlayerCh").GetComponent<Renderer>().material.color = Color.white;////
            //GameObject.Find("PlayerCh").GetComponent<Renderer>().material = material1;////

            foreach (Renderer thisRend in rend)
            {
                thisRend.material.color = Color.white;
            }

            invencible = false;
        }

        dashed = false;
    }

    public void ChangeVencibleColor()
    {
        foreach (Renderer thisRend in rend)
        {
            thisRend.material.color = Color.blue;
        }
        //GetComponent<Renderer>().material.color = Color.blue;
        //GameObject.Find("PlayerCh").GetComponent<Renderer>().material = material2;////
        //GameObject.Find("PlayerCh").GetComponent<Renderer>().material.color = Color.blue;////
    }

    public IEnumerator LifeRegeneration()
    {
        lifeRegen = true;

        while (StatsManager.Instance.life < StatsManager.Instance.maxLife)
        {
            yield return new WaitForSeconds(1f);

            StatsManager.Instance.life += StatsManager.Instance.lifeRegeneration;
            UIManager.Instance.ChangeLife();
        }

        StatsManager.Instance.life = StatsManager.Instance.maxLife;
    }
}
