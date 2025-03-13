#if UNITY_EDITOR

using System.Collections.Generic;
using QuocAnh.pattern;
using UnityEngine;
using IngameDebugConsole;

public class DebugController : Singleton<DebugController>
{

    public LevelManager levelMana;

    #region DEBUG VARIBLES
    // public bool useVechicleScript = true;
    [Space(30)]
    [Header("DEBUG"), Space(10)]

    public bool debug;

    //================================ DELETE WHEN RELEASE STABLE VERSION ==========================
    [Header("DEBUG_CONTROLL"), Space(5)]
    public bool pcControll; // for whether to have pc controlling type

    [Header("DEBUG_LEVEL"), Space(5)]
    public int mapID; // map id to spawn
    public int characterID; // character id to spawn
    public int vehicleID; // vehicle id to spawn
    public GameObject testLevel; // test level for debugging

    [Header("DEBUG_PLAYER"), Space(5)]
    //public bool outOfVehicleOnStart; // for out of vehicle on start to test sone features
    public List<LimbContact> debug_LimbStorage;
    public int debug_limbStorageID;
    #endregion

    void Awake()
    {
        levelMana.enabled = !debug;
        if (debug == false) return;
        DebugSetup();
    }



    private void DebugSetup()
    {
        GameData.LevelToLoadID = mapID;
        GameData.VehicleID = vehicleID;
        GameData.PlayerID = characterID;

        if (pcControll == true)
        {
            GameObject _pcControll = new GameObject("PC Controller");
            _pcControll.AddComponent<DebugPCControll>();
        }


        //if dev selected test map
        if (mapID <= -1)
        {
            //spawn debug level
            GameObject _map = Instantiate(testLevel, Vector3.zero, Quaternion.identity);
            //if there is a checkpoint
            if (GameData.levelCheckPoint != Vector3.zero)
            {
                //load at checkpoint
                levelMana.spawner = GameData.levelCheckPoint;
            }
            else // there is no checkpoint
            {
                //get spawn position of the map
                levelMana.spawner = _map.GetComponent<LevelData>().spawnPos;

            }
            //Debug.Log(spawner);
            //spawn vehicle
            levelMana.SpawnVehicle(GameData.VehicleID, GameData.PlayerID);
            //assign cam target to follow
            GameManager.Instance.cineMachineVirtualCam.Follow = GameManager.Instance.characterBehaviour.targetToLook;

            // if (outOfVehicleOnStart == true)
            // {

            // }
        }
        else // not test map
        {
            levelMana.enabled = true;
            //setup normal level
            // levelMana.LevelSetup();

            // if (outOfVehicleOnStart == true)
            // {
            //     EventDispatcherExtension.FireEvent(EventID.OnDeath);
            // }
        }


    }

}


public static class CommandStorage
{

    [ConsoleMethod("die", "make player get out of vehicle")]
    public static void OutofVehicle()
    {
        EventDispatcherExtension.FireEvent(EventID.OnDisconnect);

    }

    #region Limb Commands

    [ConsoleMethod("lbb", "break selected limb from the player")]
    public static void LimbBreak() { DebugController.Instance.debug_LimbStorage[DebugController.Instance.debug_limbStorageID].BreakLimb(); }

    [ConsoleMethod("lb+", "Increase selected limb")]
    public static void IncreaseLimbBreakVal() { DebugController.Instance.debug_limbStorageID++; Debug.Log("Increased Limb - current limb : " + DebugController.Instance.debug_LimbStorage[DebugController.Instance.debug_limbStorageID]); }

    [ConsoleMethod("lb-", "Decrease selected limb")]
    public static void DereaseLimbBreakVal() { DebugController.Instance.debug_limbStorageID--; Debug.Log("Increased Limb - current limb : " + DebugController.Instance.debug_LimbStorage[DebugController.Instance.debug_limbStorageID]); }

    [ConsoleMethod("lbc", "check selected limb")]
    public static void CheckLimb() { Debug.Log("Increased Limb - current limb : " + DebugController.Instance.debug_LimbStorage[DebugController.Instance.debug_limbStorageID]); }

    #endregion

    #region Time Commands

    [ConsoleMethod("time0", "Slow down time")]
    public static void SlowDownTime() { Time.timeScale = 0.3f; }
    [ConsoleMethod("time1", " Resume time ")]
    public static void ResumeTime() { Time.timeScale = 1; }

    #endregion
}

#endif