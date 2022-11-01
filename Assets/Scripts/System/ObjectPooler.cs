using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{

    //We do a little pooling
    #region Explanation
    //Example of spawning something from a pool that has been already assigned in the inspector
    // GameObject objectToSpawn = ObjectPooler.Instance.SpawnFromPool("pool_name", position, rotation);
    //When it's time to discard an object in a pool, instead of doing a Destroy, SetActive(false) instead
    //When you spawn an object from a pool, if the gameobject has a default state, you will need to reset them again OnEnable
    //We can possibly have a reset interface for this
    //This is only helpful for objects we constantly spawn over and over, like a dust effect on a character stomping around, or bullets from a gun
    //Can also be used to limit the amount of objects of the same type in a scene, I.E we don't want more than 10 health pickups on the floor
    #endregion

    [System.Serializable]
    public class Pool
    {
        //Our available pools, set in the inspector
        [Tooltip("Name of this pool")]
        public string tag;
        [Tooltip("Object occupying this pool")]
        public GameObject prefab;
        [Tooltip("Amount of objects this pool will be populated with on scene load, if you spawn too few, you may see objects disappear and reappear at the new spawn location")]
        public int size;
    }

    #region Variables_Instance
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    public static ObjectPooler Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion
    #region StartMethod
    //We do -something-
    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectPool = new();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }
    #endregion

    //Call this to spawn an object from a pool
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        //If we attempt to pull from a pool that doesn't exist, we throw an error
        if(!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " does not exist!");
            return null;
        }
        //If we add a interface for an effect that plays when an object already exists, but gets relocated somewhere else, call it here
        
        //We pull the oldest object in the pool
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        //If we add a reset object interface, we call it here

        //Set it active
        objectToSpawn.SetActive(true);
        //Move it to where we are spawning it
        objectToSpawn.transform.SetLocalPositionAndRotation(position, rotation);
        //Put it back in the queue as the newest object
        poolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }
}
