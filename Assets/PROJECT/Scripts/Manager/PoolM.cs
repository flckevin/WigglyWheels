using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuocAnh.pattern;
using System;

public class PoolM : Singleton<PoolM>
{
    [Header("PRE SPAWNED POOL OBJECTS")]
    [Space(30)]

    [Header("BLOOD EFFECT")]
    public ParticleSystem[] bloodSplashEffect;
    public int bloodSplashID;
    [Space(20)]

    [Header("BLOOD DECAL")]
    public GameObject[] bloodDecal;
    public int bloodDecalID;
    
    public Dictionary<string, List<object>> poolDict = new Dictionary<string, List<object>>();
    public Dictionary<string, int> poolDictID = new Dictionary<string, int>();   



}


/// <summary>
/// pool extension to getpool
/// </summary>
public static class PoolExtension
{
    //get object from pool
    public static object GetPool(object[] pool, ref int poolID) 
    {
        //if pool id is larger than pool length
        if (poolID >= pool.Length - 1)
        {
            //set pool back to beginning
            poolID = 0;
            //return first item in pool
            return pool[poolID];

        }
        else //pool id did not exced length
        {
            //increase pool id
            poolID++;
            //retrun new object from pool
            return pool[poolID];    
        
        }

    }

    /// <summary>
    /// function to get pool
    /// </summary>
    /// <param name="id"> name of object in pool </param>
    /// <returns></returns>
    public static object GetPoolDict(string id)
    {
        //get pool manager
        PoolM _poolM = PoolM.Instance;
        //if dictionary does not contain key -> stop execute
        if (!_poolM.poolDict.ContainsKey(id)) return null;

        //increase item id
        _poolM.poolDictID[id]++;
       // Debug.Log(_poolM.poolDictID[id]);

        //if dicionary item exceeded
        if (_poolM.poolDictID[id] > _poolM.poolDict[id].Count - 1)
        {
            //set it back to beginning
            _poolM.poolDictID[id] = 0;
            //return first item in dicionary
            return _poolM.poolDict[id][_poolM.poolDictID[id]];
        }
        else //it does not ecxeeded yet
        {
            
            //return new item
            return _poolM.poolDict[id][_poolM.poolDictID[id]];
        }

    }


    /// <summary>
    /// function to add item into pool
    /// </summary>
    /// <param name="_objectToAdd"> object to add into pool </param>
    /// <param name="_limit"> limit of pool object to add </param>
    /// <param name="id"> name of object </param>
    public static void AddPool(UnityEngine.Object _objectToAdd, int _limit, string id) 
    {
        //if the length of the list is already full then stop
        if (PoolM.Instance.poolDict.ContainsKey(id) && PoolM.Instance.poolDict[id].Count >= _limit) return;

        
        //spawn object
        object _newItem = MonoBehaviour.Instantiate(_objectToAdd, PoolM.Instance.transform.position, Quaternion.identity);
        
        //check if dictionary contain that key
        //it it does
        if (PoolM.Instance.poolDict.ContainsKey(id))
        {
            //add more item into pool
            PoolM.Instance.poolDict[id].Add(_newItem);
        }
        else //it does not contain given key
        { 
            //add new item into pool with given id
            PoolM.Instance.poolDict.Add(id,new List<object> { _newItem });
            //set spawn id back to begin
            PoolM.Instance.poolDictID.Add(id, 0);
        }
       

    }

}



    


