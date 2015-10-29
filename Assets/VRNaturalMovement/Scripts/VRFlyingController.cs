using UnityEngine;
using VR = UnityEngine.VR;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class VRFlyingController : MonoBehaviour 
{
	[SerializeField] float desiredSpeed = 5;
	float speed;
	
	Quaternion? baseRot;
	new Rigidbody rigidbody;
	new Transform camera;

	void Start()
	{
		rigidbody = GetComponent<Rigidbody>();
		camera = GetComponentInChildren<Camera>().transform;
		Recenter();
	}

	void Update()
	{		
		UpdateKeyboard();
		UpdateMovement();
	}

	void UpdateMovement()
	{
		Quaternion relative = Quaternion.Inverse(baseRot.Value) * camera.localRotation;
		transform.localRotation *= Quaternion.Lerp(Quaternion.identity, relative, Time.deltaTime);

		speed = Mathf.Lerp(speed, desiredSpeed, Time.deltaTime * 0.2f);
		rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, camera.forward * speed, Time.deltaTime * 5);
		transform.position = Vector3.Lerp(transform.position, transform.position, Time.deltaTime * 10);	
	}

	void UpdateKeyboard()
	{
		if (Input.GetKeyDown(KeyCode.Space)) Recenter();
		if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
#if UNITY_EDITOR
	    // for debugging
		transform.RotateAround(transform.position, Vector3.up, Input.GetAxis("Mouse X") * 50 * Time.deltaTime);
		transform.Rotate(Vector3.right * -Input.GetAxis("Mouse Y") * 50 * Time.deltaTime);
#endif
	}

	void Recenter()
	{
		VR.InputTracking.Recenter();
		baseRot = camera.localRotation;
	}

	void OnCollisionEnter(Collision collision) 
	{
		speed *= 0.5f;
	}

}
