Simple implementation of a Hinge Joint into Photon TrueSync. 
This is supposed to be a workaround until True Sync has a builtin HingeJoint component (in the current version 1.0.9 it is not available)).
The inspetor field of the TSHingeJoint needs the same inputs as Unitys builtin HingeJoint, though it does have less features.
 
There is also a simple game included where you can let spheres drop on a seesaw (press space for light sphere, enter for heavy spehre). The heavy sphere will break the hinge joint unless you increase the break force value.
You can control the min and max angles of the seesaw, the break force and include a spring.
Note: In the current status the orientation of both bodies connected to the Joint have to be zero at the beginning.
