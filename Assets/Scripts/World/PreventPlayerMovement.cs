using StarterAssets;
using UnityEngine;
using UnityEngine.Animations;

namespace World
{
    /// <summary>
    /// This prevents the player from moving while this state is active.
    /// </summary>
    public class PreventPlayerMovement : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var thirdPersonController = animator.GetComponent<ThirdPersonController>();
            if (thirdPersonController != null)
            {
                thirdPersonController.enabled = false;
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var thirdPersonController = animator.GetComponent<ThirdPersonController>();
            if (thirdPersonController != null)
            {
                thirdPersonController.enabled = true;
            }
        }
    }
}