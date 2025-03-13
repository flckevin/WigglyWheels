using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuggerCheat : MonoBehaviour
{
    public int vehicleID;
    public int characterID;
    // Start is called before the first frame update
    void Awake()
    {
        GameData.VehicleID = vehicleID;
        GameData.VehicleID = characterID;
    }


}
