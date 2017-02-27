using TrueSync;

namespace MyJoints
{
    public class MyHingeJoint : Joint
    {
        private PointOnPoint[] worldPointConstraint;
        private IBody3D firstBody;
        private IBody3D secondBody;
        private TSVector hingeA;

        public PointOnPoint PointOnPointConstraint1
        {
            get
            {
                return worldPointConstraint[0];
            }
        }

        public PointOnPoint PointOnPointConstraint2
        {
            get
            {
                return worldPointConstraint[1];
            }
        }

        public virtual FP AppliedImpulse
        {
            get
            {
                return worldPointConstraint[0].AppliedImpulse + worldPointConstraint[1].AppliedImpulse;
            }
        }



        public MyHingeJoint(IWorld world, IBody3D body1, IBody3D body2, TSVector position, TSVector hingeAxis) : base((World)world)
        {
            firstBody = body1;
            secondBody = body2;
            hingeA = hingeAxis;
            worldPointConstraint = new PointOnPoint[2];
            hingeAxis *= FP.Half;
            TSVector anchor = position;
            TSVector.Add(ref anchor, ref hingeAxis, out anchor);
            TSVector anchor2 = position;
            TSVector.Subtract(ref anchor2, ref hingeAxis, out anchor2);
            worldPointConstraint[0] = new PointOnPoint((RigidBody)body1, (RigidBody)body2, anchor);
            worldPointConstraint[1] = new PointOnPoint((RigidBody)body1, (RigidBody)body2, anchor2);
            StateTracker.AddTracking(worldPointConstraint[0]);
            StateTracker.AddTracking(worldPointConstraint[1]);
           
        }


        public FP getHingeAngle()
        {
            return TSVector.Dot(hingeA, (firstBody.TSOrientation.eulerAngles - secondBody.TSOrientation.eulerAngles));
        }

        public FP getAngularVel()
        {
            return TSVector.Dot(hingeA, (firstBody.TSAngularVelocity - secondBody.TSAngularVelocity));
;        }

        public override void Activate()
        {
            World.AddConstraint(worldPointConstraint[0]);
            World.AddConstraint(worldPointConstraint[1]);
        }

        public override void Deactivate()
        {
            World.RemoveConstraint(worldPointConstraint[0]);
            World.RemoveConstraint(worldPointConstraint[1]);
        }
    }
}
