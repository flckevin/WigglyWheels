using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public Vector3 spawnPos;
    public Color bgColor;
    private void Start()
    {
        Camera.main.backgroundColor = bgColor;
    }
}
