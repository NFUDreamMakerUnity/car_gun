using UnityEngine;
using System.Collections;

public class RearWheelDrive : MonoBehaviour
{
	private WheelCollider[] wheels;

	public float maxAngle = 30;
	public float maxTorque = 300;
	public GameObject wheelShape;
    private Rigidbody rig;

    private enum states {Move, Idle , Back }
    states nowState = new states();

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
        nowState = states.Move;
    }

    // here we find all the WheelColliders down in the hierarchy
    public void Start()
	{
		wheels = GetComponentsInChildren<WheelCollider>();

        for (int i = 0; i < wheels.Length; ++i) 
		{
			var wheel = wheels [i];

			// create wheel shapes only when needed
			if (wheelShape != null)
			{
				var ws = GameObject.Instantiate (wheelShape);
				ws.transform.parent = wheel.transform;
			}
		}

        rig.velocity = 8 * transform.forward;
    }

    // this is a really simple approach to updating wheels
    // here we simulate a rear wheel drive car and assume that the car is perfectly symmetric at local zero
    // this helps us to figure our which wheels are front ones and which are rear
    public void Update()
	{
        if (rig.velocity.magnitude > 10)
        {
            rig.drag = rig.velocity.magnitude / 50;
        }
        else if (rig.velocity.magnitude < 8)
        {
            rig.drag = 0;
        }
      //  Debug.Log(rig.velocity.magnitude + "  time  " + Time.time);

        float angle = maxAngle * Input.GetAxis("Horizontal");
        float torque = maxTorque * 1;

        if (nowState == states.Move || nowState == states.Back)
        {
            foreach (WheelCollider wheel in wheels)
            {
                // a simple car where front wheels steer while rear ones drive
                if (wheel.transform.localPosition.z > 0)
                    wheel.steerAngle = angle;

                if (wheel.transform.localPosition.z < 0)
                {
                    if (nowState == states.Move)
                    {
                        wheel.motorTorque = torque;
                    }
                    else
                    {
                        wheel.motorTorque = torque * -1;
                    }
                }
                // update visual wheels if any
                if (wheelShape)
                {
                    Quaternion q;
                    Vector3 p;
                    wheel.GetWorldPose(out p, out q);

                    // assume that the only child of the wheelcollider is the wheel shape
                    Transform shapeTransform = wheel.transform.GetChild(0);
                    shapeTransform.position = p;
                    shapeTransform.rotation = q;
                }

            }
        }
        else
        {
            rig.drag = 10;
        }
	}

    public void enableSwithch(int i)
    {
        switch (i)
        {
            case (1):
                nowState = states.Move;
                rig.drag = 0;
                rig.velocity = 8 * transform.forward;
                break;
            case (0):
                nowState = states.Idle;
                break;
            case (-1):
                nowState = states.Back;
                rig.drag = 0;
                break;
            default:
                Debug.Log("無此狀態");
                break;
        }
        Debug.Log(nowState);
    }
}
