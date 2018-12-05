
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Malatawy
{
    public class ChasePlayer : MonoBehaviour
    {
        [Tooltip("This is the main player. (GameObject)")]
        public GameObject player;
        [Tooltip("This is the enemy's speed. (float)")]
        public float speed;

        private Animator animator;
        private int countHits;
        private float distanceToPlayer;
        private bool isDead;

        void Start()
        {
            animator = GetComponent<Animator>();
            countHits = 0;
            distanceToPlayer = 0.0f;
            isDead = false;

            animator.SetBool("isIdle", true);
            animator.SetBool("isRunning", false);
            animator.SetBool("isAttacking", false);
        }

        void Update()
        {
            if (isDead)
                return;

            distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);       // Vector3.Distance equivalent to (a - b).magnitude
            Vector3 direction = player.transform.position - transform.position;
            float fieldOfVisionAngle = Vector3.Angle(direction, transform.forward);

            if (distanceToPlayer < 10.0f)
            {
                direction.y = 0;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);
                animator.SetBool("isIdle", false);

                if (distanceToPlayer > 2)                   // distanceToPlayer to be updated
                {                                               
                    transform.Translate(0, 0, 0.05f * speed);
                    animator.SetBool("isRunning", true);
                    animator.SetBool("isAttacking", false);
                }
                else
                {
                    animator.SetBool("isAttacking", true);
                    animator.SetBool("isRunning", false);
                }
            }
            else
            {
                animator.SetBool("isIdle", true);
                animator.SetBool("isRunning", false);
                animator.SetBool("isAttacking", false);
            }
        }

        public void isHit()
        {
            if (isDead || distanceToPlayer > 2)             // distanceTo Player to be updated
                return;

            if (countHits == 4)                             // countHits to be updated
            {                                         
                animator.SetTrigger("isDead");
                isDead = true;
                return;
            }
            else
            {
                animator.SetTrigger("isHit");
                countHits++;
            }
        }

    }
}
