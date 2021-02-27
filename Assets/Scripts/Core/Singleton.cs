using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static object m_Lock = new object();

    private static T _instance;
    public static T Instance
    {
    get
        {
            lock (m_Lock)
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();

                    if (_instance == null && SceneManager.GetActiveScene().name != "MainMenu")
                    {
                        GameObject container = new GameObject();
                        _instance = container.AddComponent<T>();
                    }
                }

                return _instance;
            }
            
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
        }
        else if (_instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
    }

}
