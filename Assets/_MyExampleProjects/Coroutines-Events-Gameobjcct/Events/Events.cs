using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets._MyExampleProjects.Coroutines.Events
{
    [Serializable]
    public class UnityEventForEnemyCloseBy : UnityEvent<string,float> 
    {}

    [Serializable]
    public class UnityEventForEnemyWithNameCloseBy : UnityEvent<string>
    { }
}
