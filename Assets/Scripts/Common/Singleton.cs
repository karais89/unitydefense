// http://wiki.unity3d.com/index.php/Singleton
/*
The advantage of using singletons, in Unity, rather than static parameters and methods, are basically:
(1) Static classes are lazy-loaded when they are first referenced, but must have an empty static constructor (or one is generated for you). This means it's easier to mess up and break code if you're not careful and know what you're doing. As for using the Singleton Pattern, you automatically already do lots of neat stuff, such as creating them with a static initialization method and making them immutable.
(2) Singleton can implement an interface (Static cannot). This allows you to build contracts that you can use for other Singleton objects or just any other class you want to throw around. In other words, you can have a game object with other components on it for better organization!
(3) You can also inherit from base classes, which you can't do with Static classes.
P.S.: Unfortunately there is no good way to remove the need of a "Instance keyword" right there, calling the singleton.
P.S.(2): This is made as MonoBehaviour because we need Coroutines. A lot of times it makes sense to leave one in a singleton, so it will persist between scenes.
*/


using UnityEngine;

namespace Common
{

    /// <summary>
    /// Be aware this will not prevent a non singleton constructor
    ///   such as `T myT = new T();`
    /// To prevent that, add `protected T () {}` to your singleton class.
    /// 
    /// As a note, this is made as MonoBehaviour because we need Coroutines.
    /// </summary>
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        private static object _lock = new object();

        public static T Instance
        {
            get
            {
                if ( applicationIsQuitting )
                {
                    Debug.LogWarning( "[Singleton] Instance '" + typeof( T ) +
                        "' already destroyed on application quit." +
                        " Won't create again - returning null." );
                    return null;
                }

                lock ( _lock )
                {
                    if ( _instance == null )
                    {
                        _instance = (T) FindObjectOfType( typeof( T ) );

                        if ( FindObjectsOfType( typeof( T ) ).Length > 1 )
                        {
                            Debug.LogError( "[Singleton] Something went really wrong " +
                                " - there should never be more than 1 singleton!" +
                                " Reopening the scene might fix it." );
                            return _instance;
                        }

                        if ( _instance == null )
                        {
                            GameObject singleton = new GameObject();
                            _instance = singleton.AddComponent<T>();
                            singleton.name = "(singleton) " + typeof( T ).ToString();

                            DontDestroyOnLoad( singleton );

                            Debug.Log( "[Singleton] An instance of " + typeof( T ) +
                                " is needed in the scene, so '" + singleton +
                                "' was created with DontDestroyOnLoad." );
                        }
                        else {
                            Debug.Log( "[Singleton] Using instance already created: " +
                                _instance.gameObject.name );
                        }
                    }

                    return _instance;
                }
            }
        }

        private static bool applicationIsQuitting = false;
        /// <summary>
        /// When Unity quits, it destroys objects in a random order.
        /// In principle, a Singleton is only destroyed when application quits.
        /// If any script calls Instance after it have been destroyed, 
        ///   it will create a buggy ghost object that will stay on the Editor scene
        ///   even after stopping playing the Application. Really bad!
        /// So, this was made to be sure we're not creating that buggy ghost object.
        /// </summary>
        public void OnDestroy()
        {
            applicationIsQuitting = true;
        }
    }
}
