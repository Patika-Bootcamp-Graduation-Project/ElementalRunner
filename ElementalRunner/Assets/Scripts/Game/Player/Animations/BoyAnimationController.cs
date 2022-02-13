using System;
using UnityEngine;

namespace Olcay.Animations
{
    public class BoyAnimationController : MonoBehaviour
    {
        [SerializeField] private State state;
        [SerializeField] private string currentState;

        [SerializeField] private Animator animator;

        //ANIMATION STATES
        private const string BOY_IDLE = "BoyIdle";
        private const string BOY_DANCE = "BoyDance";
        private const string BOY_FALLING = "BoyFalling";
        private const string BOY_FALLINGTOLANDING = "BoyFallingToLanding";
        private const string BOY_RUNNING = "BoyRunning";
        private const string BOY_SWEEPFALL = "BoySweepFall";
        private const string BOY_THROW = "BoyThrow";

        private void Awake()
        {
            AnimationController.ChangeBoyAnimation += ChangeAnimationState;
        }

        private void OnDestroy()
        {
            AnimationController.ChangeBoyAnimation -= ChangeAnimationState;
        }

        private void ChangeAnimationState(State newState)
        {
            currentState = string.Empty;

            switch (newState)
            {
                case State.Idle:
                    currentState = BOY_IDLE;
                    break;
                case State.Dance:
                    currentState = BOY_DANCE;
                    break;
                case State.Falling:
                    currentState = BOY_FALLING;
                    break;
                case State.FallingToLanding:
                    currentState = BOY_FALLINGTOLANDING;
                    break;
                case State.Running:
                    currentState = BOY_RUNNING;
                    break;
                case State.SweepFall:
                    currentState = BOY_SWEEPFALL;
                    break;
                case State.Throw:
                    currentState = BOY_THROW;
                    break;
            }

            animator.Play(currentState);
        }
    }
}