using UnityEngine;

/// <summary>
/// > Generic means the general form, not specific. In C#, generic means not specific to a particular data type.
/// > C# allows you to define generic classes, interfaces, abstract classes, fields, methods, static methods, properties, events, delegates, and operators using the type parameter and without the specific data type. 
/// > A type parameter is a placeholder for a particular type specified when creating an instance of the generic type.
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                var objs = FindObjectsOfType(typeof(T)) as T[];
                if(objs.Length > 0)
                {
                    _instance = objs[0];
                }
                if(objs.Length > 1)
                {
                    Debug.LogError("There is more than one" + typeof(T).Name + "in the scene");
                }
                if(_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = string.Format("_{0}", typeof(T).Name);
                    _instance = obj.AddComponent<T>();
                }
            }

            return _instance;
        }
    }

}
