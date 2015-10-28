using UnityEngine;
using System.Collections;
using System.IO;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class VRFlyingController : MonoBehaviour 
{
	[SerializeField] float desiredSpeed = 5;
	public float speed;

	[SerializeField] Transform centerEyeAnchor;
	
	Quaternion? baseRot;
	new Rigidbody rigidbody;
	AudioSource audioWind;
	AudioSource[] audioCoins;
	int activeCoinSource;
	Transform camPoint;

	void Start()
	{
		rigidbody = GetComponent<Rigidbody>();
		var sources = GetComponents<AudioSource>();
		audioWind = sources[0];
		audioCoins = new AudioSource[2];
		audioCoins[0] = sources[1];
		audioCoins[1] = sources[2];		
		camPoint = transform.GetChild(0);
		camPoint.parent = transform.parent;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
			baseRot = centerEyeAnchor.localRotation;

		if (Input.GetKeyDown(KeyCode.Escape)) Application.LoadLevel("Atmospherics");

		if (baseRot != null) {
			Quaternion relative = Quaternion.Inverse(baseRot.Value) * centerEyeAnchor.localRotation;
			camPoint.localRotation *= Quaternion.Lerp(Quaternion.identity, relative, Time.deltaTime);

			speed = Mathf.Lerp(speed, desiredSpeed, Time.deltaTime * 0.2f);
			rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, centerEyeAnchor.forward * speed, Time.deltaTime * 5);
			camPoint.position = Vector3.Lerp(camPoint.position, transform.position, Time.deltaTime * 10);
			audioWind.pitch = Mathf.Clamp(speed / 45 + 0.5f, 0.5f, 2);
			audioCoins[activeCoinSource].pitch = Mathf.Clamp(speed / 50 + 0.5f, 0.5f, 2) * 0.5f;
			audioCoins[activeCoinSource].volume = (1 - Mathf.Clamp01(speed / 60)) * 0.5f + 0.04f;
		}
	}

	public void CoinGrab()
	{
		activeCoinSource = (activeCoinSource + 1) % audioCoins.Length;
		audioCoins[activeCoinSource].Play();
	}

	void OnCollisionEnter(Collision collision) 
	{
		speed *= 0.5f;
	}

}
