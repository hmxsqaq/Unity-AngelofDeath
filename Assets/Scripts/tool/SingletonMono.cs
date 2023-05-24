using UnityEngine;

namespace tool
{
    public class SingletonMono<T> : MonoBehaviour where T: SingletonMono<T>
    {
        public static T Instance;
        private void Awake()
        {
            if (Instance is null)
            {
                Instance = this as T;
            }
        }
    }
}