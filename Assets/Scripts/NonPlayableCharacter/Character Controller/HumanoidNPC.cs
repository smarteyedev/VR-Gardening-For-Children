using UnityEngine;
using UnityEngine.AI;

namespace Smarteye.VRGardening.NPC
{
    public class HumanoidNPC : NPCController
    {
        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            ChangeState(BehaviourState.Standby); // Set default state
        }

        // Override interaksi untuk Humanoid
        public override void Interact()
        {
            PlayInteractionAnimation();
        }

        private void PlayInteractionAnimation()
        {
            if (animator != null)
            {
                animator.SetTrigger("interact");
                ChangeState(BehaviourState.Interact);
            }
        }
    }
}
