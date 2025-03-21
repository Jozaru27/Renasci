using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWarriorAnimation : MonoBehaviour
{
    public Animator SkeletonWarriorAnim;
    // Start is called before the first frame update
    void Start()
    {
        SkeletonWarriorAnim = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    public void Idle()
    {

    }
    public void Attack()
    {

    }
    public void Hit()
    {

    }
    public void Death()
    {

    }
}
