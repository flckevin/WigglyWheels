 
namespace QuocAnh.pattern 
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance; // store the instance
        private static object _lock = new object(); // lock
        public static T Instance
        {
            get
            {
                //lock this line before do anything elese
                lock (_lock)
                {
                    //if instance does not exist
                    if (_instance == null)
                    {
                        //find it
                        var instances = FindObjectsOfType<T>();
                        //if it does find it
                        if (instances.Length > 0)
                        {
                            //get the first instance that it found
                            _instance = instances[0];
                            return _instance;
                        }
                    }
                    //if it still null after finding it
                    if (_instance == null)
                    {
                        //create new one
                        _instance = new GameObject(typeof(T).Name).AddComponent<T>();
                        //DontDestroyOnLoad(_instance);
                    }
                    return _instance;
                }
            }
        }
    }



}

