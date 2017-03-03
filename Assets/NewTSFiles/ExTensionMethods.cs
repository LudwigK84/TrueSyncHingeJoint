using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrueSync;

public static class ExTensionMethods
{

    public static void TSApplyRelativeTorque(this IBody3D thisBody, TSVector torque)
    {
        TSVector relCord = TSVector.Transform(torque, thisBody.TSOrientation);
        thisBody.TSApplyTorque(relCord);
    }

    public static void AddRelativeTorque(this TSRigidBody thisTSBody, TSVector torque)
    {
        thisTSBody.tsCollider.Body.TSApplyRelativeTorque(torque);
    }

}
