using UnityEngine;
using System.Collections;

public class Vat : MonoBehaviour 
{
	public CHEMICAL type;
	public float fluidHeight = 5;
	public float maxFluid = 10;
	public Blast blastObject;
	public ConveyorBelt[] conveyors;

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
			for(int i=0; i<conveyors.Length; i++)
			{
				conveyors[i].speed = 0;
			}
			blastObject.Explode();
			Invoke("GameOver",2f);
		}
	}


	void MixFluid(GameObject flask)
	{
		Chemical chemical = flask.GetComponent<Chemical>();

		//if the chemical reacts badly
		if (GameManager.Instance.reactions[(int)type].values[(int)chemical.type])
		{
			fluidHeight++;
		}
		//else the chemical reacts well.
		else
		{
			fluidHeight--;
		}

		Debug.Log("Fluid Height: "+ fluidHeight);
	}

	void GameOver()
	{
		GameManager.Instance.NewGameState(GameManager.Instance.stateGameOver);
	}
}
