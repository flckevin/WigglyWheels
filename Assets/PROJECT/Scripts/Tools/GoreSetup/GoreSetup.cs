using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;
public class GoreSetup : MonoBehaviour
{
    public Rigidbody head_BodyPart;

    public Rigidbody upperArm_L_Whole_BodyPart;
    public Rigidbody upperArm_L_Piece_BodyPart;
    public Rigidbody lowerArm_L_BodyPart;

    public Rigidbody upperLeg_L_Whole_BodyPart;
    public Rigidbody upperLeg_L_Piece_BodyPart;
    public Rigidbody lowerLeg_L_BodyPart;

    public Rigidbody upperArm_R_Whole_BodyPart;
    public Rigidbody upperArm_R_Piece_BodyPart;
    public Rigidbody lowerArm_R_BodyPart;

    public Rigidbody upperLeg_R_Whole_BodyPart;
    public Rigidbody upperLeg_R_Piece_BodyPart;
    public Rigidbody lowerLeg_R_BodyPart;



    public Transform upperArm_L_Spawn;
    public Transform lowerArm_L_Spawn;

    public Transform upperLeg_L_Spawn;
    public Transform lowerLeg_L_Spawn;

    public Transform upperArm_R_Spawn;
    public Transform lowerArm_R_Spawn;

    public Transform upperLeg_R_Spawn;
    public Transform lowerLeg_R_Spawn;


    public LimbsBehaviour upperArm_L_LB;
    public LimbsBehaviour lowerArm_L_LB;

    public LimbsBehaviour upperLeg_L_LB;
    public LimbsBehaviour lowerLeg_L_LB;

    public LimbsBehaviour upperArm_R_LB;
    public LimbsBehaviour lowerArm_R_LB;

    public LimbsBehaviour upperLeg_R_LB;
    public LimbsBehaviour lowerLeg_R_LB;


    public ObiParticleAttachment upperArm_Attach_L;
    public ObiParticleAttachment lowerArm_Attach_L;

    public ObiParticleAttachment upperLeg_Attach_L;
    public ObiParticleAttachment lowerLeg_Attach_L;

    public ObiParticleAttachment upperArm_Attach_R;
    public ObiParticleAttachment lowerArm_Attach_R;

    public ObiParticleAttachment upperLeg_Attach_R;
    public ObiParticleAttachment lowerLeg_Attach_R;


    public void OnSetup() 
    {
       
    
    }
   
}
