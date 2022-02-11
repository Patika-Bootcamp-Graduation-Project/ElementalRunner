using System;
using Olcay.Player;
using UnityEngine;

namespace Olcay.Animations
{
    public class AnimationController : MonoSingleton<AnimationController>
    {
        [SerializeField] private bool isGirlActive;
        [SerializeField] private State state;
        public static event Action<State> ChangeGirlAnimation;
        public static event Action<State> ChangeBoyAnimation;
        private void Awake()
        {
            Players.playerChanged += ChangeCurrentPlayer;
        }

        private void OnDestroy()
        {
            Players.playerChanged -= ChangeCurrentPlayer;
        }

        private void ChangeCurrentPlayer(bool whoActive)
        {
            isGirlActive = whoActive;
            ChangeAnimationState(state);
        }

        public void ChangeAnimationState(State newState)
        {
            if (isGirlActive)
            {
                ChangeGirlAnimation?.Invoke(newState);
            }
            else
            {
                ChangeBoyAnimation?.Invoke(newState);
            }
            state = newState;
        }


    }
}