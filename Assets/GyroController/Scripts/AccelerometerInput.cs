using UnityEngine;
using UnityEngine.Networking;

public class AccelerometerInput : NetworkBehaviour 
{	
	[SerializeField] GameObject bulletPrefab;

	Quaternion baseRot = Quaternion.Euler(new Vector3(90,180,0));
	Quaternion rotFix = new Quaternion(0,0,1,0) * Quaternion.Euler(new Vector3(-90,0,0));
	Quaternion rot;

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
		if (Input.touchCount > 0) CmdFire();
		CmdRotate(Input.gyro.attitude);
	}

	[ServerCallback]
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space)) {			
			baseRot = Quaternion.Inverse(Quaternion.Inverse(baseRot) * transform.localRotation);
			UnityEngine.VR.InputTracking.Recenter();
		}
		if (Input.GetKeyDown(KeyCode.F)) Fire();
		transform.localRotation = Quaternion.Slerp(transform.localRotation, rot, Time.deltaTime * 30f);
	}

	[Command]
	void CmdFire()
	{
		Fire();
	}

	[ServerCallback]
	void Fire()
	{
		var bullet = Instantiate (bulletPrefab);
		bullet.GetComponent<Rigidbody>().velocity = transform.forward * 10f;
	}

	[Command]
	[ServerCallback]
	void CmdRotate(Quaternion attitude)
	{
		rot = baseRot * attitude * rotFix;
	}		
}
