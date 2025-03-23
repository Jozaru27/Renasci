using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] Material material1, material2;

    public void ChangeHealthAmount(int amount, Vector3 enemyPosition, float pushForce)
    {
        StatsManager.Instance.life += amount;

        if (amount < 0)
        {
            if (StatsManager.Instance.life < 0)
                PlayerDeath();
            else
                GetComponent<PlayerAnimation>().Hit();

            PushCharacter(enemyPosition, pushForce);
            StartCoroutine(ChangingColor());
            GameManager.Instance.playerCannotMove = true;
        }
        else if (StatsManager.Instance.life > 10)
            StatsManager.Instance.life = 10;
        
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
        GameObject.Find("DummyMesh").GetComponent<Renderer>().material = material2;

        yield return new WaitForSeconds(0.125f);

;       //GetComponent<Renderer>().material.color = Color.white;
        GameObject.Find("DummyMesh").GetComponent<Renderer>().material = material1;
    }
}
