using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class StateEffect : MonoBehaviour
{
    bool inState;

    public void GetFreeze()
    {
        if (!inState)
            StartCoroutine(ApplyIceState());
    }

    IEnumerator ApplyIceState()
    {
        inState = true;

        if (TryGetComponent<SkeletonWarrior>(out SkeletonWarrior warriorScript))
            warriorScript.frozen = true;
        if (TryGetComponent<SkeletonArcher>(out SkeletonArcher archerScript))
            archerScript.frozen = true;
        if (TryGetComponent<SkeletonMage>(out SkeletonMage mageScript))
            mageScript.frozen = true;

        GetComponent<NavMeshAgent>().isStopped = true;
        GetComponent<Animator>().speed = 0;

        yield return new WaitForSeconds(2.5f);

        inState = false;

        if (warriorScript != null)
            warriorScript.frozen = false;
        else if (archerScript != null)
            archerScript.frozen = false;
        else if (mageScript != null)
            mageScript.frozen = false;

        GetComponent<NavMeshAgent>().isStopped = false;
        GetComponent<Animator>().speed = 1;
    }
}
