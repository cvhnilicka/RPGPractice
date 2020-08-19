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
            animator.SetTrigger("attack");
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

        // Animation Event 
        void Hit()
        {
            print("HITTTT");
        }
    }
}

