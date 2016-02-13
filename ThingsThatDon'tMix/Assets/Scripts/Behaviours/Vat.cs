﻿using UnityEngine;
using System.Collections;

public class Vat : MonoBehaviour 
{
	public CHEMICAL type;
	public float fluidHeight = 5;
	public float maxFluid = 10;
	public Blast blastObject;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	//set trigger has been tripped.
	void OnTriggerEnter(Collider other)
	{
		MixFluid(other.gameObject);
		Destroy(other.gameObject);

		if(fluidHeight > maxFluid)
		{
			blastObject.Explode();
		}
	}


	void MixFluid(GameObject flask)
	{
		Chemical chemical = flask.GetComponent<Chemical>();

		//if the chemical reacts badly
		if (chemical.type > type)
		{
			fluidHeight++;
		}
		//else the chemical reacts well.
		else
		{
			fluidHeight--;
		}
	}
}
