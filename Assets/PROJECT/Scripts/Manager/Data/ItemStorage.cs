using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemStorage", menuName = "ScriptableObjects/ItemStorage", order = 1)]
public class ItemStorage : ScriptableObject
{

    public GameObject[] items;
}
