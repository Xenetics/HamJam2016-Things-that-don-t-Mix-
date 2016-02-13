using UnityEngine;
using System.Collections;

public class ActivateOnTrigger : MonoBehaviour 
{
	public GameObject Target;

	void OnTriggerEnter(Collider other)
	{
		Target.SetActive(true);
	}
}
