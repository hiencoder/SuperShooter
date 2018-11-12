using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleObjectPool : MonoBehaviour
{
    public GameObject prefab;
    private Stack<GameObject> inactiveInstance = new Stack<GameObject>();

    public GameObject GetObject()
    {
        GameObject gameObject;
        if (inactiveInstance.Count > 0)
        {
            gameObject = inactiveInstance.Pop();
        }
        else
        {
            gameObject = (GameObject)GameObject.Instantiate(prefab);
            PooledObject pooledObject = gameObject.AddComponent<PooledObject>();
            pooledObject.pool = this;
        }
        gameObject.SetActive(true);

        return gameObject;
    }

    public void ReturnObject(GameObject toReturn)
    {
        PooledObject pooledObject = toReturn.GetComponent<PooledObject>();
        if (pooledObject != null && pooledObject.pool == this)
        {
            toReturn.SetActive(false);

            inactiveInstance.Push(toReturn);

        }
        else
        {
			Debug.LogWarning(toReturn.name + " warning!");
			Destroy(toReturn);
        }
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public class PooledObject : MonoBehaviour
    {
        public SimpleObjectPool pool;
    }
}
