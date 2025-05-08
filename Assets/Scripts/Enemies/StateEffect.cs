using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.AI;

public class StateEffect : MonoBehaviour
{
    NavMeshAgent agent;
    EnemyStats enemyStats;

    bool inIceState;
    bool inWindState;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyStats = GetComponent<EnemyStats>();
    }

    public void GetFreeze()
    {
        if (!inIceState)
            StartCoroutine(ApplyIceState());
    }

    public void GetPushed()
    {
        agent.speed = 0;
        StopAllCoroutines();

        if (inIceState)
            StartCoroutine(ApplyIceState());
    }

    public void StopPushing()
    {
        if (!inWindState)
            StartCoroutine(UnableWindState());
    }

    IEnumerator ApplyIceState()
    {
        inIceState = true;

        if (TryGetComponent<SkeletonWarrior>(out SkeletonWarrior warriorScript))
            warriorScript.frozen = true;
        if (TryGetComponent<SkeletonArcher>(out SkeletonArcher archerScript))
            archerScript.frozen = true;
        if (TryGetComponent<SkeletonMage>(out SkeletonMage mageScript))
            mageScript.frozen = true;

        GetComponent<NavMeshAgent>().isStopped = true;
        GetComponent<Animator>().speed = 0;

        yield return new WaitForSeconds(2.5f);

        inIceState = false;

        if (warriorScript != null)
            warriorScript.frozen = false;
        else if (archerScript != null)
            archerScript.frozen = false;
        else if (mageScript != null)
            mageScript.frozen = false;

        GetComponent<NavMeshAgent>().isStopped = false;
        GetComponent<Animator>().speed = 1;
    }

    IEnumerator UnableWindState()
    {
        yield return new WaitForSeconds(0.25f);

        agent.speed = enemyStats.movementSpeed;
    }
}
