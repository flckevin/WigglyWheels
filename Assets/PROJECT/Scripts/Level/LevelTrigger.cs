using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    public TriggerType trigger; // trigger type

    private void OnTriggerEnter2D(Collider2D collision)
    {

        //if player collider with other game object
        if (collision.CompareTag("Player") || collision.CompareTag("Vehicle"))
        {
            //Debug.Log(collision.transform.name);
            switch (trigger)
            {
                case TriggerType.finishline:
                    FinishLineTrig();
                    break;
                case TriggerType.checkPoint:
                    CheckPointTrig();
                    break;
            }

        }

    }

    /// <summary>
    /// function for finishing game
    /// </summary>
    void FinishLineTrig()
    {
        //activate game menu
        GameManager.Instance.gameMenu.WinActivation();
    }

    /// <summary>
    /// function to store checkpoint
    /// </summary>
    void CheckPointTrig()
    {
        //storing checkpoint
        GameData.levelCheckPoint = this.transform.position;
    }


}

public enum TriggerType
{
    finishline,
    checkPoint
}
