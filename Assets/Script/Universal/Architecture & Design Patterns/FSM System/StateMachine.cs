using UnityEngine;

namespace MyStudio.Core.Architecture
{
    public class StateMachine<T>
    {
        public BaseState<T> CurrentState { get; private set; }

     
        public void Initialize(BaseState<T> startingState)
        {
            CurrentState = startingState;
            CurrentState.Enter();
        }

        public void ChangeState(BaseState<T> newState)
        {
            if (CurrentState != null)
                CurrentState.Exit();

            CurrentState = newState;

            if (CurrentState != null)
                CurrentState.Enter();
        }

        public void Update()
        {
            if (CurrentState != null)
                CurrentState.Tick();
        }
    }
}