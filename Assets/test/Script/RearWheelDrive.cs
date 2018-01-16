using UnityEngine;
using System.Collections;

public class RearWheelDrive : MonoBehaviour {

	private WheelCollider[] wheels;

	public float maxAngle = 30;
	public float maxTorque = 300;
	public GameObject wheelShape;
    private Rigidbody rig;

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    // here we find all the WheelColliders down in the hierarchy
    public void Start()
	{
		wheels = GetComponentsInChildren<WheelCollider>();
        rig.velocity = 10 * transform.forward;

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
	}

	// this is a really simple approach to updating wheels
	// here we simulate a rear wheel drive car and assume that the car is perfectly symmetric at local zero
	// this helps us to figure our which wheels are front ones and which are rear
	public void Update()
	{
        if (rig.velocity.magnitude > 20)
        {
            rig.drag = rig.velocity.magnitude / 150;
        }
        else if (rig.velocity.magnitude < 15)
        {
            rig.drag = 0;
        }
        Debug.Log(rig.velocity.magnitude + "  time  " + Time.time);

        float angle = maxAngle * Input.GetAxis("Horizontal");
		float torque = maxTorque * 1;

		foreach (WheelCollider wheel in wheels)
		{
			// a simple car where front wheels steer while rear ones drive
			if (wheel.transform.localPosition.z > 0)
				wheel.steerAngle = angle;

			if (wheel.transform.localPosition.z < 0)
				wheel.motorTorque = torque;

			// update visual wheels if any
			if (wheelShape) 
			{
				Quaternion q;
				Vector3 p;
				wheel.GetWorldPose (out p, out q);

				// assume that the only child of the wheelcollider is the wheel shape
				Transform shapeTransform = wheel.transform.GetChild (0);
				shapeTransform.position = p;
				shapeTransform.rotation = q;
			}

		}
	}
}
