using System.Linq;

using UnityEngine;
using UnityExtension;
using Object = UnityEngine.Object;

public class BaseMonoBehaviour : MonoBehaviour
{
    protected delegate void Task();

    /// <summary>
    /// Instantiates a Manager of type T by attempting to find a GameObject with the Component on in the Resources/Managers folder.
    /// </summary>
    /// <returns>
    /// The instantiated manager.
    /// </returns>
    /// <typeparam name='T'>
    /// The type of the Manager to instantiate.
    /// </typeparam>
    public static T InstantiateManager<T>() where T : MonoBehaviour
    {
        if (Object.FindObjectOfType(typeof(T)) != null)
        {
            Debug.LogError("Manager of type " + typeof(T) + " already exists in scene and only one Manager is allowed at a time!");
            return null;
        }

        foreach (GameObject go in Resources.LoadAll("Managers").Cast<GameObject>().Where(go => go.GetComponent<T>() != null))
        {
            return (Instantiate(go, Vector3.zero, Quaternion.identity) as GameObject).GetSafeComponent<T>();
        }

        Debug.LogError("Attempted to instantiate Manager of type " + typeof(T) + ", but could not find the Manager in \"Resources/Managers\"!");
        return null;
    }

    /// <summary>
    /// Invokes a method after the specified amount if time
    /// </summary>
    /// <param name="task">The method identifier, which must fit the signature of the delegate Task</param>
    /// <param name="time">How long time before the method is invoked</param>
    protected void Invoke(Task task, float time)
    {
        this.Invoke(task.Method.Name, time);
    }
}