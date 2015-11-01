using UnityEngine;
using UnityEngine.Networking;

public class AccelerometerInput : NetworkBehaviour 
{	
	[SerializeField] GameObject bulletPrefab;

	Quaternion baseRot = Quaternion.Euler(90, 180, 0);
	Quaternion attitude;
	Quaternion initialAttitude = Quaternion.Euler(0, 0, 180);

	void Start()
	{
		EnableClient();
	}

	[ClientCallback]
	void EnableClient()
	{
		Input.gyro.enabled = true;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}

	public override float GetNetworkSendInterval()
	{
		return 0.02f;
	}
		
	[ClientCallback]
	void FixedUpdate()
	{		
		for (int i = 0; i < Input.touchCount; i++)
			if (Input.GetTouch(i).phase == TouchPhase.Began)
				CmdFire();
		CmdRotate(Input.gyro.attitude);
	}

	[ServerCallback]
	void Update()
	{		
		if (Input.GetKeyDown(KeyCode.Space)) {
			initialAttitude = attitude;
			UnityEngine.VR.InputTracking.Recenter();
		}
		if (Input.GetKeyDown(KeyCode.F)) Fire();
		transform.localRotation = baseRot * Quaternion.Inverse(initialAttitude) * attitude;
	}

	[Command]
	void CmdFire()
	{
		Fire();
	}

	[ServerCallback]
	void Fire()
	{
		var bullet = Instantiate(bulletPrefab);
		bullet.GetComponent<Rigidbody>().velocity = -transform.up * 10f;
	}

	[Command]
	[ServerCallback]
	void CmdRotate(Quaternion attitude)
	{		
		this.attitude = attitude;
	}		
}
