using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f; // default 5 meters
        [SerializeField] float suspicionTime = 3f;
        private Fighter fighter;
        private GameObject player;
        private Health health;
        private Vector3 guardLocation;
        private Mover mover;
        private ActionScheduler actionScheduler;

        // remember last time guard saw player
        float timeSinceLastSawPlayer = Mathf.Infinity;


        private void Start()
        {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
            health = GetComponent<Health>();
            guardLocation = transform.position;
            mover = GetComponent<Mover>();
            actionScheduler = GetComponent<ActionScheduler>();
        }


        private void Update()
        {
            if (health.IsDead()) return;
            if (GetInRange() && fighter.CanAttack(player))
            {
                timeSinceLastSawPlayer = 0;
                AttackBehavior();
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehavior();
            }
            else
            {
                // stop attacking 
                // the move action will automatically cancel fighting action
                GuardBehaviour();
            }
            timeSinceLastSawPlayer += Time.deltaTime;



        }

        private void GuardBehaviour()
        {
            mover.StartMoveAction(guardLocation);
        }

        private void SuspicionBehavior()
        {
            actionScheduler.CancelCurrentAction();
        }

        private void AttackBehavior()
        {
            fighter.Attack(player);
        }

        private bool GetInRange()
        {
            
            return Vector3.Distance(player.transform.position, transform.position) < chaseDistance;
        }

        // called by unity to draw gizmos
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }

    }

}
