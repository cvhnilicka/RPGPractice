using RPG.Core;
using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        Transform target;

        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f; // default 1 sec

        [SerializeField] float weaponDamage = 5f;


        float timeSinceLastAttack;

        Mover mover;
        ActionScheduler actionScheduler;
        Animator animator;

        private void Start()
        {
            mover = GetComponent<Mover>();
            actionScheduler = GetComponent<ActionScheduler>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null) return;

            if (!GetInRange())
            {
                // we want to move towards it probs
                mover.MoveTo(target.position);
            }
            else
            {
                mover.Cancel();
                AttackBehavior();
       
            }
        }

        private void AttackBehavior()
        {
            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                // this will trigger the Hit() event below
                animator.SetTrigger("attack");  
                timeSinceLastAttack = 0;
            }
        }
        // Animation Event 
        void Hit()
        {
            Health targetHealth = target.GetComponent<Health>();
            targetHealth.TakeDamage(weaponDamage);
        }

        private bool GetInRange()
        {
            return Vector3.Distance(target.position, transform.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            target = combatTarget.transform;
            actionScheduler.StartAction(this);
            
        }

        public void Cancel()
        {
            target = null;
        }    

     
    }
}

