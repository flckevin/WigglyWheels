using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bikecycle : VehicleScript
{
    private float animsDirect;
    public override void Start()
    {
        base.Start();

    }

    public override void AnimationPlay()
    {
        base.AnimationPlay();
        //if animator does exist
        if (blendAnims != null)
        {
            //if player is moving
            if (_ableToMove == true)
            {
                //set float in blend tree to play animation with suitable speed
                blendAnims.SetFloat(animName, _direction);

                //Debug.Log(_direction);
                //if animator speed can be increase
                if (blendAnims.speed <= maxAnimationSpeed)
                {
                    //increase it till it cannot
                    blendAnims.speed += (_motor2D.motorSpeed / (enginePower * _direction)) * Time.fixedDeltaTime;

                }

            }
            else //player not moving
            {
                //set pedal back to normal
                blendAnims.SetFloat(animName, 0);

            }
            

        }
    }
}
