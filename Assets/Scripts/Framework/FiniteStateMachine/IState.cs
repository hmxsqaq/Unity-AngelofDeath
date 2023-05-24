using System;

namespace Framework
{
    public interface IState<T> where T : Enum
    {
        T Type { get; }
        void OnEnter();
        void OnExit();
        void OnUpdate();
        bool Transition(out T type);
    }
}
