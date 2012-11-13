using UnityEngine;
using System.Collections;

namespace UnityExtension
{
    public static partial class Extensions
    {
        /// <summary>
        /// Gets a Component safely by logging an error if the Component could not be found on the GameObject.
        /// </summary>
        /// <returns>
        /// The asked-for Component.
        /// </returns>
        /// <typeparam name='T'>
        /// The type of the Component to get.
        /// </typeparam>
        public static T GetSafeComponent<T>(this GameObject gameObject) where T : MonoBehaviour
        {
            T component = gameObject.GetComponent<T>();

            if (component == null)
                Debug.LogError(
                    "Expected to find component of type " + typeof (T) + " on GameObject " + gameObject.name +
                    " but found none!", gameObject);

            return component;
        }

        /// <summary>
        /// Gets a Component safely by logging an error if the Component could not be found on the GameObject or any of its children.
        /// </summary>
        /// <returns>
        /// The asked-for Component.
        /// </returns>
        /// <typeparam name='T'>
        /// The type of the Component to get.
        /// </typeparam>
        public static T GetSafeComponentInChildren<T>(this GameObject gameObject) where T : MonoBehaviour
        {
            T component = gameObject.GetComponentInChildren<T>();

            if (component == null)
                Debug.LogError(
                    "Expected to find component of type " + typeof (T) + " on GameObject " + gameObject.name +
                    " or in its children, but found none!", gameObject);

            return component;
        }

        /// <summary>
        /// Sets the GameObject's rotation on the x axis.
        /// </summary>
        /// <param name='x'>
        /// The x value to set.
        /// </param>
        public static void SetRotationX(this GameObject gameObject, float x)
        {
            gameObject.transform.rotation = Quaternion.Euler(x, gameObject.transform.rotation.eulerAngles.y,
                                                             gameObject.transform.rotation.eulerAngles.z);
        }

        /// <summary>
        /// Sets the GameObject's rotation on the y axis.
        /// </summary>
        /// <param name='y'>
        /// The y value to set.
        /// </param>
        public static void SetRotationY(this GameObject gameObject, float y)
        {
            gameObject.transform.rotation = Quaternion.Euler(gameObject.transform.rotation.eulerAngles.y, y,
                                                             gameObject.transform.rotation.eulerAngles.z);
        }

        /// <summary>
        /// Sets the GameObject's rotation on the z axis.
        /// </summary>
        /// <param name='z'>
        /// The z value to set.
        /// </param>
        public static void SetRotationZ(this GameObject gameObject, float z)
        {
            gameObject.transform.rotation = Quaternion.Euler(gameObject.transform.rotation.eulerAngles.x,
                                                             gameObject.transform.rotation.eulerAngles.y, z);
        }

        /// <summary>
        /// Sets the GameObject's local rotation on the x axis.
        /// </summary>
        /// <param name='x'>
        /// The x value to set.
        /// </param>
        public static void SetLocalRotationX(this GameObject gameObject, float x)
        {
            gameObject.transform.localRotation = Quaternion.Euler(x, gameObject.transform.localRotation.eulerAngles.y,
                                                                  gameObject.transform.localRotation.eulerAngles.z);
        }

        /// <summary>
        /// Sets the GameObject's local rotation on the y axis.
        /// </summary>
        /// <param name='y'>
        /// The y value to set.
        /// </param>
        public static void SetLocalRotationY(this GameObject gameObject, float y)
        {
            gameObject.transform.localRotation = Quaternion.Euler(gameObject.transform.localRotation.eulerAngles.y, y,
                                                                  gameObject.transform.localRotation.eulerAngles.z);
        }

        /// <summary>
        /// Sets the GameObject's local rotation on the z axis.
        /// </summary>
        /// <param name='z'>
        /// The z value to set.
        /// </param>
        public static void SetLocalRotationZ(this GameObject gameObject, float z)
        {
            gameObject.transform.localRotation = Quaternion.Euler(gameObject.transform.localRotation.eulerAngles.x,
                                                                  gameObject.transform.localRotation.eulerAngles.y, z);
        }

        /// <summary>
        /// Sets the GameObject's position on the x axis.
        /// </summary>
        /// <param name='x'>
        /// The x value to set.
        /// </param>
        public static void SetPositionX(this GameObject gameObject, float x)
        {
            gameObject.transform.position = new Vector3(x, gameObject.transform.position.y,
                                                        gameObject.transform.position.z);
        }

        /// <summary>
        /// Sets the GameObject's position on the y axis.
        /// </summary>
        /// <param name='y'>
        /// The y value to set.
        /// </param>
        public static void SetPositionY(this GameObject gameObject, float y)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, y,
                                                        gameObject.transform.position.z);
        }

        /// <summary>
        /// Sets the GameObject's position on the z axis.
        /// </summary>
        /// <param name='z'>
        /// The z value to set.
        /// </param>
        public static void SetPositionZ(this GameObject gameObject, float z)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y,
                                                        z);
        }

        /// <summary>
        /// Sets the GameObject's local position on the x axis.
        /// </summary>
        /// <param name='x'>
        /// The x value to set.
        /// </param>
        public static void SetLocalPositionX(this GameObject gameObject, float x)
        {
            gameObject.transform.localPosition = new Vector3(x, gameObject.transform.localPosition.y,
                                                             gameObject.transform.localPosition.z);
        }

        /// <summary>
        /// Sets the GameObject's local position on the y axis.
        /// </summary>
        /// <param name='y'>
        /// The y value to set.
        /// </param>
        public static void SetLocalPositionY(this GameObject gameObject, float y)
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, y,
                                                             gameObject.transform.localPosition.z);
        }

        /// <summary>
        /// Sets the GameObject's local position on the z axis.
        /// </summary>
        /// <param name='z'>
        /// The z value to set.
        /// </param>
        public static void SetLocalPositionZ(this GameObject gameObject, float z)
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x,
                                                             gameObject.transform.localPosition.y, z);
        }

        /// <summary>
        /// Gets the delta position to the given GameObject.
        /// </summary>
        /// <returns>
        /// The delta position to the given GameObject.
        /// </returns>
        /// <param name='go'>
        /// The GameObject to calculate the delta position to.
        /// </param>
        public static Vector3 GetDeltaPosition(this GameObject gameObject, GameObject go)
        {
            return GetDeltaPosition(gameObject, go.transform);
        }

        /// <summary>
        /// Gets the delta position to the given Transform.
        /// </summary>
        /// <returns>
        /// The delta position to the given GameObject.
        /// </returns>
        /// <param name='t'>
        /// The Transform to calculate the delta position to.
        /// </param>
        public static Vector3 GetDeltaPosition(this GameObject gameObject, Transform t)
        {
            return t.position - gameObject.transform.position;
        }

        /// <summary>
        /// Gets the delta rotation to the given GameObject.
        /// </summary>
        /// <returns>
        /// The delta rotation to the given GameObject.
        /// </returns>
        /// <param name='go'>
        /// The GameObject to calculate the delta rotation to.
        /// </param>
        public static Vector3 GetDeltaRotation(this GameObject gameObject, GameObject go)
        {
            return GetDeltaRotation(gameObject, go.transform);
        }

        /// <summary>
        /// Gets the delta rotation to the given GameObject.
        /// </summary>
        /// <returns>
        /// The delta rotation to the given GameObject.
        /// </returns>
        /// <param name='go'>
        /// The GameObject to calculate the delta rotation to.
        /// </param>
        public static Vector3 GetDeltaRotation(this GameObject gameObject, Transform t)
        {
            return t.rotation.eulerAngles - gameObject.transform.rotation.eulerAngles;
        }
    }
}