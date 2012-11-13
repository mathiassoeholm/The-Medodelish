using UnityEngine;

public class UnityManager<T> : BaseMonoBehaviour where T : BaseMonoBehaviour
{
    private static T instance;

    /// <summary>
    /// Gets the current instance of the Singleton.
    /// If no instance is found, one will be attempted created.
    /// </summary>
    /// <value>
    /// The instance of the Singleton.
    /// </value>
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                var objects = FindObjectsOfType(typeof(T));

                if (objects.Length == 0)
                {
                    Debug.Log("Object of type " + typeof(T) + " is required in scene but could not be found!\nAttempting to spawn one...");
                    instance = BaseMonoBehaviour.InstantiateManager<T>();
                }
                else if (objects.Length > 1)
                {
                    Debug.LogError("Object of type " + typeof(T) + " is a Singleton but several (" + objects.Length + ") were found in scene!");
                    instance = (T)objects[0];
                }
                else
                {
                    instance = (T)objects[0];
                }
            }

            return instance;
        }
    }
}