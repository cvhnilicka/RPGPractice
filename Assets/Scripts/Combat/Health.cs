using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField]
        float healthPoints = 100f;

        Animator animator;

        bool isDead = false;

        public bool IsDead()
        {
            return isDead;
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
        }


        public void TakeDamage(float damageDone)
        {
            if (isDead) return;
            healthPoints = Mathf.Max(healthPoints - damageDone, 0);
            if (healthPoints == 0)
            {
                // trigger death anim
                isDead = true;
                animator.SetTrigger("die");
            }
        }

    }
}

