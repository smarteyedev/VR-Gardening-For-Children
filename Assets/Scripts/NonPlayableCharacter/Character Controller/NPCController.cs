using UnityEngine;
using UnityEngine.AI;

namespace Smarteye.VRGardening.NPC
{
    public enum BehaviourState
    {
        Standby,
        Interact,
        Pursuit,
        Patrol
    }

    public abstract class NPCController : MonoBehaviour
    {
        protected NavMeshAgent agent;
        protected Animator animator;
        protected BehaviourState currentState;

        private void Start()
        {
            currentState = BehaviourState.Standby; // NPC mulai dalam mode standby
        }

        // Method untuk movement
        public virtual void MoveTo(Vector3 destination)
        {
            if (agent != null)
            {
                agent.SetDestination(destination);
                PlayWalkAnimation();
                currentState = BehaviourState.Pursuit; // Contoh perubahan state
            }
        }

        // Method untuk memainkan animasi berjalan
        protected virtual void PlayWalkAnimation()
        {
            if (animator != null)
            {
                animator.SetBool("isWalking", true);
            }
        }

        // Method untuk menghentikan animasi berjalan
        protected virtual void StopWalking()
        {
            if (animator != null)
            {
                animator.SetBool("isWalking", false);
            }
        }

        // Method interaksi umum (bisa di-override di subclass)
        public abstract void Interact();

        // Method untuk mengubah state NPC
        public void ChangeState(BehaviourState newState)
        {
            currentState = newState;
            HandleStateChange();
        }

        // Logika untuk menangani perubahan state
        protected virtual void HandleStateChange()
        {
            switch (currentState)
            {
                case BehaviourState.Standby:
                    // StopWalking();
                    Debug.Log("NPC is in Standby mode.");
                    break;

                case BehaviourState.Interact:
                    // Interact();
                    Debug.Log("NPC is Interacting.");
                    break;

                case BehaviourState.Pursuit:
                    Debug.Log("NPC is Pursuing.");
                    break;

                case BehaviourState.Patrol:
                    Debug.Log("NPC is Patrolling.");
                    // Anda bisa menambahkan logika patrol di sini
                    break;
            }
        }
    }
}
