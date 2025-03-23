using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonWarrior : MonoBehaviour, IDamageable
{
    public float life = 3;

    SkeletonWarriorStates FSM;
    public GameObject playerObject;
    public GameObject skeletonWarriorObject;
    public Animator skeletonWarriorAnimator;

    void Start()
    {
        playerObject = GameObject.Find("Player");
        skeletonWarriorObject = this.gameObject;
        skeletonWarriorAnimator=skeletonWarriorObject.GetComponent<Animator>();
      
        FSM = new SkeletonWarriorIdle(this);
    }

    void Update()
    {
        FSM = FSM.Process();
    }

    public void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<PlayerHealth>().ChangeHealthAmount(-2,skeletonWarriorObject.transform.position,15);
        }
    }
    
    public void TakeDamage(int amount)
    {
        life += amount;

        if (life <= 0)
        {
            life = 0;
            Destroy(this.gameObject);
        }
    }
}
