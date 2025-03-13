using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GamePauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject winMenu;
    public GameObject looseMenu;

    void Start()
    {

    }

    /// <summary>
    /// function to activate pause menu
    /// </summary>
    public void PauseActivation()
    {
        //freeze time
        Time.timeScale = 0;
    }


    /// <summary>
    /// function to activate win menu
    /// </summary>
    public void WinActivation()
    {
        //activate win menu
        winMenu.SetActive(true);
        ResetGameData();
    }


    /// <summary>
    /// function to continue playing game and unfreeze time
    /// </summary>
    public void Resume()
    {
        //unfreeze time
        Time.timeScale = 1;

    }

    /// <summary>
    /// function to restart game
    /// </summary>
    public void Restart(bool _checkPointReload)
    {
        if (_checkPointReload == true)
        {
            //reload current scene 
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //unfreeze time
            Time.timeScale = 1;
        }
        else if (_checkPointReload == false)
        {
            ResetGameData();
            //reload current scene 
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //unfreeze time
            Time.timeScale = 1;
        }
    }

    /// <summary>
    /// function to move to next level
    /// </summary>
    public void NextLevel()
    {
        //increase level id
        GameData.LevelToLoadID++;
        //restart game scene
        Restart(false);
    }


    /// <summary>
    /// function to load custom scene
    /// </summary>
    /// <param name="_customSceneID"> id of scene to load </param>
    public void LoadScene(int _customSceneID = 0)
    {
        //if custom scene id less or smaller than -1 which it means it not been assigned
        if (_customSceneID <= 0)
        {
            //load back to main menu
            SceneManager.LoadScene(0);

        }
        else //it has been assigned
        {
            //load to scene that have been assigned
            SceneManager.LoadScene(_customSceneID);
        }

        //incase time is freezing
        Time.timeScale = 1;
    }


    public void LoadGUI(GameObject _GUI)
    {
        _GUI.SetActive(true);
    }

    public void UnLoadGUI(GameObject _GUI)
    {
        _GUI.SetActive(false);
    }

    private void ResetGameData()
    {
        GameData.levelCheckPoint = Vector3.zero;
    }
}
