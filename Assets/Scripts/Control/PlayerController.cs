using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private Health health;
        // Start is called before the first frame update
        void Start()
        {
            health = GetComponent<Health>();

        }

        // Update is called once per frame
        void Update()
        {
            if (health.IsDead()) return;
            if (CombatHandler()) return;
            if (MovementHandler()) return;
            print("Nothing To Do");
        }

        private bool CombatHandler()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            foreach (RaycastHit hit in hits)
            {
                CombatTarget target;
                if (hit.collider.TryGetComponent<CombatTarget>(out target))
                {
                    GameObject targetGameObject = target.gameObject;
                    if (!GetComponent<Fighter>().CanAttack(targetGameObject))
                    {
                        continue;
                    }
                    if (Input.GetMouseButtonDown(0))
                    {
                        GetComponent<Fighter>().Attack(targetGameObject);
                    }
                    return true;
                }
                
            }
            return false;
        }

        private bool MovementHandler()
        {
            RaycastHit hitInfo;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hitInfo);
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(hitInfo.point);
                }
                return true;
            }
            return false;
        }


        private Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}

