/*
 *
 * Created by Maxi Karnstedt 2020
 *
 */
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool
{
    //list with pooled objects
    private List<GameObject> objectList;
    public List<GameObject> ObjectList
    {
        get
        {
            return objectList;
        }
        set
        {
            objectList = value;
        }
    }

    private GameObject go;

    //ParentObj have to be an empty object in scene ,defined by StringCollection
    private Transform ParentObj;

    public ObjectPool(GameObject ObjectToPool, int totalObjectsAtstart)
    {
        ParentObj = GameObject.Find(StringCollection.OBJECTPOOLPARENT_SO).transform;

        objectList = new List<GameObject>(totalObjectsAtstart);
        go = ObjectToPool;

        for (int i = 0; i < totalObjectsAtstart; i++)
        {
            GameObject newObject = Object.Instantiate(go, ParentObj);
            newObject.transform.position = Vector3.zero;
            FreeObject(newObject);
            objectList.Add(newObject);
        }
    }

    //returns the next free obj in objpool list
    public GameObject NextFree()
    {
        var freeObject = objectList.Where(x => x.activeSelf == false).FirstOrDefault();
        if (freeObject == null)
        {
            freeObject = Object.Instantiate(go);
            FreeObject(go);
            objectList.Add(freeObject);
        }

        freeObject.transform.parent = ParentObj;
        freeObject.SetActive(true);
        return freeObject;
    }

    public void FreeObject(GameObject objectToFree)
    {
        if (objectToFree != null)
        {
            objectToFree.SetActive(false);
        }
    }
}

