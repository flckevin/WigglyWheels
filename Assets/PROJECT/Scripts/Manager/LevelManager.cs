using Kamgam.BikeAndCharacter25D;
using QuocAnh.pattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    [Header("LEVEL INFO"), Space(10)]
    public Vector3 spawner; // start position

    [Space(10)]

    [Header("CHARACTER INFO"), Space(10)]
    public ItemStorage vehicle; // all vehicle in game
    public ItemStorage player; // all character in game
    public ItemStorage level; // all level in game

    [Header("CANVAS INFO"), Space(10)]
    public GameObject canvas; // start position

    // Start is called before the first frame update
    void Start()
    {
        //if canvas does exist
        if (canvas != null)
        {
            //acvtivate it
            canvas.SetActive(true);
        }

        //set up level normal
        LevelSetup();


    }






    /// <summary>
    /// function to spawn vehicle
    /// </summary>
    public void LevelSetup()
    {
        //set game back to default
        SetGameDefault();

        // if it not main scene then stop execute
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {

            //spawn out map
            GameObject _map = Instantiate(level.items[GameData.LevelToLoadID], Vector3.zero, Quaternion.identity);

            //if there is checkpoint
            if (GameData.levelCheckPoint != Vector3.zero)
            {
                //load at there
                spawner = GameData.levelCheckPoint;
            }
            else // there is no checkpoint
            {
                //get spawn position of the map
                spawner = _map.GetComponent<LevelData>().spawnPos;

            }


        }
        else
        {
            //spawn position for main menu
            spawner = new Vector3(0, 2, 0);

        }

        //spawn vehicle
        SpawnVehicle(GameData.VehicleID, GameData.PlayerID);
    }

    /// <summary>
    /// function to spawn vehicle
    /// </summary>
    /// <param name="_vehicleID"></param>
    /// <param name="_playerID"></param>
    public void SpawnVehicle(int _vehicleID, int _playerID)
    {
        //spawn out vehicle
        GameObject _vehicle = Instantiate(vehicle.items[_vehicleID], spawner, Quaternion.identity);
        //spawn out character
        GameObject _player = Instantiate(player.items[_playerID], spawner, Quaternion.identity);

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            //assign cam target to follow
            GameManager.Instance.cineMachineVirtualCam.Follow = GameManager.Instance.characterBehaviour.targetToLook;
            //GameManager.Instance.cineMachineVirtualCam.LookAt = GameManager.Instance.characterBehaviour.targetToLook;
        }

        //assign bones
        _vehicle.GetComponent<VehicleSetup>().AssignBones(_player.GetComponent<CharacterBones>(), _player.GetComponent<CharacterInfoHolder>());

    }

    //set every value in game back to default
    private void SetGameDefault()
    {
        //clear limb manager so it won't override on top of each other
        LimbManager.limbInfo.Clear();
    }

}
