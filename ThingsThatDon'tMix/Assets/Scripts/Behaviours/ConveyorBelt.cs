using UnityEngine;
using System.Collections;

public class ConveyorBelt : MonoBehaviour 
{
	public float speed = 3.0f;
	public Transform spawnPoint;
	public float spawnMin;
	public float spawnMax;
	public GameObject[] prefabs;


	void Start()
	{
		SpawnFlask();
	}

	void SpawnFlask()
	{
		//spawn a chemical
		Instantiate(prefabs[Random.Range(0,(int)CHEMICAL.Size-1)],spawnPoint.position,Quaternion.identity);

		//Call this again later
		Invoke("SpawnFlask",Random.Range(spawnMin,spawnMax));
	}

	void OnCollisionStay(Collision collision) 
	{
		if(speed > 0)
		{
			// Assign velocity based upon direction of conveyor belt
			// Ensure that conveyor mesh is facing towards its local Z-axis		
			float conveyorVelocity = speed * Time.deltaTime;
		
			Rigidbody rigidbody = collision.gameObject.GetComponent<Rigidbody>();
			rigidbody.velocity = conveyorVelocity * transform.forward;
		}
	}
}

