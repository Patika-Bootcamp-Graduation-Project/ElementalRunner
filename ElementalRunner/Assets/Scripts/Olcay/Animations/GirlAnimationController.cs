using System;
using UnityEngine;

namespace Olcay.Animations
{
    public class GirlAnimationController : MonoBehaviour
    {
        [SerializeField] private State state;
        [SerializeField] private string currentState;
        [SerializeField] private Animator animator;
        //private Animator _animator;
        //private Animator animator => _animator = GetComponent<Animator>();
        
        //ANIMATION STATES
        private const string GIRL_IDLE = "GirlIdle";
        private const string GIRL_DANCE = "GirlDance";
        private const string GIRL_FALLING = "GirlFalling";
        private const string GIRL_FALLINGTOLANDING = "GirlFallingToLanding";
        private const string GIRL_RUNNING = "GirlRunning";
        private const string GIRL_SWEEPFALL = "GirlSweepFall";
        private const string GIRL_THROW = "GirlThrow";

        private void Awake()
        {
            AnimationController.ChangeGirlAnimation += ChangeAnimationState;
        }

        private void ChangeAnimationState(State newState)
        {
            currentState = string.Empty;
                switch (newState)
                {
                    case State.Idle:
                        currentState = GIRL_IDLE;
                        break;
                    case State.Dance:
                        currentState = GIRL_DANCE;
                        //animator.SetBool("isWon",true);
                        break;
                    case State.Falling:
                        currentState = GIRL_FALLING;
                        //animator.SetBool("isFalling",true);
                        break;
                    case State.FallingToLanding:
                        currentState = GIRL_FALLINGTOLANDING;
                        //animator.SetBool("isGrounded",true);
                        break;
                    case State.Running:
                        currentState = GIRL_RUNNING;
                        //animator.SetBool("isRunning",true);
                        break;
                    case State.SweepFall:
                        currentState = GIRL_SWEEPFALL;
                        //animator.SetBool("isLost",true);
                        break;
                    case State.Throw:
                        currentState = GIRL_THROW;
                        //animator.SetBool("isThisFirstGate",true);
                        break;
                }
                animator.Play(currentState);
        }
    }
}

