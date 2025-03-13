using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public static class GameData
{
    public static int LevelToLoadID
    {
        get { return _levelToLoadID; }
        set
        {
            if (_levelToLoadID >= 100)
            {
                _levelToLoadID = 0;
            }
            else
            {
                _levelToLoadID = value;
            }
        }
    }

    public static int PlayerID { get; set; }

    public static int VehicleID { get; set; }

    public static int Money
    {
        get { return PlayerPrefs.GetInt("Money", 0); }
        set { PlayerPrefs.SetInt("Money", value); }
    }

    public static Vector3 levelCheckPoint
    {
        get { return _levelCheckPoint; }
        set { _levelCheckPoint = value; }
    }


    private static Vector3 _levelCheckPoint = Vector3.zero;
    private static int _levelToLoadID;




}
