using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrueSync;
using MyJoints;
using System;

[Serializable]
public class SpringElements 
{
    public FP spring;
    public FP damper;
    public FP tagetPosition;
}

[Serializable]
public class LimitElements
{
    public FP Min;
    public FP Max;
    //TODO: Bouciness, Bounce Mi Vel, Contact Distance...
}


public class TSHingeJoint : TrueSyncBehaviour
{
    MyHingeJoint thisJoint;
    TSRigidBody thisBody;
       
    
    [SerializeField]
    TSCollider connectedBody;
    [SerializeField]
    Vector3 anchor;
    [SerializeField]
    TSVector Axis;
    [SerializeField]
    bool useLimits = false;
    [SerializeField]
    LimitElements Limits;
    [SerializeField]
    bool useSpring;
    [SerializeField]
    SpringElements Spring;
    [SerializeField]
    FP breakForce=FP.PositiveInfinity;

    

    public override void OnSyncedStart()
    {
        thisBody = GetComponent<TSRigidBody>();
        IBody3D body1 = GetComponent<TSCollider>().Body;
        IBody3D body2 = connectedBody.Body;
        
        Vector3 worldPos = transform.TransformPoint(anchor);
        TSVector TSworldPos = worldPos.ToTSVector();

        if (useLimits)
            thisJoint = new LimitedHingeJoint(PhysicsWorldManager.instance.GetWorld(), body1, body2, TSworldPos, Axis, -Limits.Min,Limits.Max);
        else
            thisJoint= new MyHingeJoint(PhysicsWorldManager.instance.GetWorld(), body1, body2, TSworldPos, Axis);

        thisJoint.Activate();
    }

    public override void OnSyncedUpdate()
    {
        if (useSpring)
        {
            //Adding a spring and damper Term to the Equation of Motion 
            thisBody.AddTorque((-1) * Axis * ((thisJoint.getHingeAngle()-Spring.tagetPosition) * Spring.spring + thisJoint.getAngularVel() * Spring.damper));
        }        

        if (TSMath.Abs(thisJoint.AppliedImpulse) >= breakForce)//@TODO: Add break torque
        {
            thisJoint.Deactivate();
            Destroy(this);
        }
                            
    }

    

    

}
