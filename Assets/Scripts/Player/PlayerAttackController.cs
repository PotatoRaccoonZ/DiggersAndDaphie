using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] private Collider swordCollider;
    private Animator animator;
    private int comboNr = 0;
    //private float maxTimeBetweenAttack = 1f;
    //private float timeBetweenAttack = 0f;
    //private bool isAnimating = false;
    // Start is called before the first frame update
    public bool isAttacking = false;
    private bool attack1 = false;
    private bool attack2 = false;
    private bool attack3 = false;

    private bool attack4 = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        attack1 = animator.GetBool("Attack1");
        attack2 = animator.GetBool("Attack2");
        attack3 = animator.GetBool("Attack3");
        attack4 = animator.GetBool("Attack4");

        if(attack1 || attack2 || attack3 || attack4){
            isAttacking = true;
        }else{
            isAttacking = false;
        }

        /*if (timeBetweenAttack > 0f)
        {
            timeBetweenAttack -= Time.deltaTime;
        }*/
    }

    public void Attack()
    {   
        comboNr ++;
        swordCollider.enabled = true;

        if (comboNr == 1){
            animator.SetTrigger("Attack1");
        }
        else if ( comboNr == 2){
            animator.SetTrigger("Attack2");
        }
        else if ( comboNr == 3){
            animator.SetTrigger("Attack3");
        }
        else if ( comboNr == 4){
            animator.SetTrigger("Attack4");
        }
        else {
            comboNr = 0;
        }

        
        /*
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        if (timeBetweenAttack <= 0f && info.normalizedTime > 1)
        {
            comboNr++;
            Debug.Log("Attacking - " + comboNr);
            if (comboNr == 1)
            {
                animator.SetTrigger("Attack1");
            }
            else if (comboNr == 2)
            {
                animator.SetTrigger("Attack2");
            }
            else if (comboNr == 3)
            {
                animator.SetTrigger("Attack3");
            }
            else if (comboNr == 4)
            {
                animator.SetTrigger("Attack4");
            }
            isAnimating = true;
            timeBetweenAttack = maxTimeBetweenAttack;
            if (comboNr >= 1)
            {
                comboNr = 0;
            };
            // Animate();
        }*/
    }
/*
    private void Animate()
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        if (isAnimating && info.normalizedTime > 1)
        {
            if (info.IsName("Attack1"))
            {
                animator.ResetTrigger("Attack1");
            }
            else if (info.IsName("Attack2"))
            {
                animator.ResetTrigger("Attack2");
            }
            else if (info.IsName("Attack3"))
            {
                animator.ResetTrigger("Attack3");
            }
            else if (info.IsName("Attack4"))
            {
                animator.ResetTrigger("Attack4");
                comboNr = 0;
            }
            if (comboNr >= 4)
            {
                comboNr = 0;
            };
            isAnimating = false;
        }
    }
*/
}
