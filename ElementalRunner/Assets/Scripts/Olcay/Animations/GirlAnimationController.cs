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
                        break;
                    case State.Falling:
                        currentState = GIRL_FALLING;
                        break;
                    case State.FallingToLanding:
                        currentState = GIRL_FALLINGTOLANDING;
                        break;
                    case State.Running:
                        currentState = GIRL_RUNNING;
                        break;
                    case State.SweepFall:
                        currentState = GIRL_SWEEPFALL;
                        break;
                    case State.Throw:
                        currentState = GIRL_THROW;
                        break;
                }
                animator.Play(currentState);
        }
    }
}

