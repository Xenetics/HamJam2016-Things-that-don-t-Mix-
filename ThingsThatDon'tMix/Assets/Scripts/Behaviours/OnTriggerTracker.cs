using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//A simple script used to track if a trigger volume is active or not by another script
//through the use of the public Triggered property.

public class OnTriggerTracker : MonoBehaviour 
{

	public bool Triggered {set;get;}
	private List<GameObject> targets;

	// Use this for initialization
	void Start () 
	{
		Triggered = false;
		targets = new List<GameObject>();
	}

	//set trigger has been tripped.
	void OnTriggerEnter(Collider other)
	{
		Triggered = true;
		targets.Add(other.gameObject);
	}


	//this trigger has been deactivated.
	void OnTriggerExit(Collider other)
	{
		Triggered = false;
		targets.Remove(other.gameObject);
	}

	public List<GameObject> GetTargets()
	{
		return targets;
	}
}
