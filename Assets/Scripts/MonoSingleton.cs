using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
public abstract class MonoSingleton<T> : MonoBehaviour where T: MonoSingleton<T>
=======
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T> 
>>>>>>> eb781022e2b73789a7fd0915f4cc4c49a0a56ac0
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this as T;
<<<<<<< HEAD

        Init();
    }

    public virtual void Init()
    {
        
=======
>>>>>>> eb781022e2b73789a7fd0915f4cc4c49a0a56ac0
    }
}
