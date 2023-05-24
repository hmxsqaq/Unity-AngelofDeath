using System;
using UnityEngine;

namespace tool
{
    public class SingletonMonoDDOL<T> : MonoBehaviour where T: SingletonMonoDDOL<T>
    {
        public static T Instance;
        private void Awake()
        {
            if (Instance is null)
            {
                Instance = this as T;
                DontDestroyOnLoad(this);
            }
        }
    }
}