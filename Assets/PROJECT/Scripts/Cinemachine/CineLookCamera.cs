using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CineLookCamera : CinemachineExtension
{
    public float m_position = 10;
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {

        if (stage == CinemachineCore.Stage.Finalize)
        {
            var pos = state.RawPosition;
            pos.y = m_position;
            state.RawPosition = pos;
        }

    }


}
