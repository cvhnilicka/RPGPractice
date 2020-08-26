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
        private Fighter fighter;
        private GameObject player;
        private Health health;
        private Vector3 guardLocation;
        private Mover mover;


        private void Start()
        {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
            health = GetComponent<Health>();
            guardLocation = transform.position;
            mover = GetComponent<Mover>();
        }


        private void Update()
        {
            if (health.IsDead()) return;
            CombatHandler();

        }

        private void CombatHandler()
        {
            
            if (GetInRange() && fighter.CanAttack(player))
            {
                fighter.Attack(player);
            }
            else
            {
                // stop attacking 
                // the move action will automatically cancel fighting action
                mover.StartMoveAction(guardLocation);


            }
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
