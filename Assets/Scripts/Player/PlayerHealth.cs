using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [HideInInspector] public bool damaged;
    
    [SerializeField] float invencibleTime;
    [SerializeField] Material material1, material2;

    bool invencible;
    bool gotDamage;
    bool dashed;

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
                        GetComponent<PlayerAnimation>().Hit();
                        StopAllCoroutines();
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
        GetComponent<PlayerAnimation>().Death();
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().freezeRotation = true;
        GameManager.Instance.gameOver = true;
        UIManager.Instance.EnableGameOverMenu();
        StatsManager.Instance.life = 0;
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

        yield return new WaitForSeconds(0.125f);

;       //GetComponent<Renderer>().material.color = Color.white;
        //GameObject.Find("PlayerCh").GetComponent<Renderer>().material = material1;////

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
            invencible = false;
        }
        
        gotDamage = false;
        StartCoroutine(LifeRegeneration());
    }

    public IEnumerator InvencibleDash(float time)
    {
        invencible = true;
        dashed = true;

        yield return new WaitForSeconds(time);

        //GetComponent<Renderer>().material.color = Color.white;

        if (!gotDamage)
        {
            //GameObject.Find("PlayerCh").GetComponent<Renderer>().material.color = Color.white;////
            //GameObject.Find("PlayerCh").GetComponent<Renderer>().material = material1;////
            invencible = false;
        }

        dashed = false;
    }

    public void ChangeVencibleColor()
    {
        //GetComponent<Renderer>().material.color = Color.blue;
        //GameObject.Find("PlayerCh").GetComponent<Renderer>().material = material2;////
        //GameObject.Find("PlayerCh").GetComponent<Renderer>().material.color = Color.blue;////
    }

    public IEnumerator LifeRegeneration()
    {
        while (StatsManager.Instance.life < StatsManager.Instance.maxLife)
        {
            yield return new WaitForSeconds(1f);

            StatsManager.Instance.life += StatsManager.Instance.lifeRegeneration;
            UIManager.Instance.ChangeLife();
        }

        StatsManager.Instance.life = StatsManager.Instance.maxLife;
    }
}
