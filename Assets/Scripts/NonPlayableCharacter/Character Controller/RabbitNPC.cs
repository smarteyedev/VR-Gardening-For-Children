using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

namespace Smarteye.VRGardening.NPC
{
    public class RabbitNPC : NPCController
    {
        [SerializeField] private Transform targetPos;

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            ChangeState(BehaviourState.Patrol); // Rabbit mulai dengan patrol

            MoveTo(targetPos.position);
        }

        // Rabbit tidak memiliki interaksi
        public override void Interact()
        {
            Debug.Log("Rabbit cannot interact.");
        }

        protected override void PlayWalkAnimation()
        {
            if (animator != null)
            {
                animator.SetTrigger("hop");
            }
        }
    }
}
