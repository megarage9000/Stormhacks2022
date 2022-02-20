using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Taken for Dilmers Singleton Implementation
public class Singleton<T> : MonoBehaviour
    where T : Component 
    {
        public static T _Instance;
        public static T Instance {
            get {
                if(_Instance == null) {
                    var objs = FindObjectOfType(typeof(T)) as T[];
                    if(objs.Length > 0) {
                        _Instance = objs[0];
                    }
                    if(objs.Length > 1) {
                        Debug.LogError("There is more than one " + typeof(T).Name + " in the scene.");
                    }
                    if(_Instance == null) {
                        GameObject obj = new GameObject();
                        obj.name = string.Format("_{0}", typeof(T).Name);
                        _Instance = obj.AddComponent<T>();
                    }
                }
                return _Instance;
            }
        }
    }

