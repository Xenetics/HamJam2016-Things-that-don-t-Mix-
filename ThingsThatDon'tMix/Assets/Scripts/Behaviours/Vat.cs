using UnityEngine;
using System.Collections.Generic;

public class Vat : MonoBehaviour 
{
	public CHEMICAL type;
	public float fluidHeight = 5;
	public float maxFluid = 10;
	public Blast blastObject;
	public ConveyorBelt[] conveyors;
    public SpriteRenderer Label;
    public List<Sprite> Labels = new List<Sprite>();
	public Transform FluidObject;
	private float originalHeight;
	private float originalPosition;
	private float POSITION_DRIFT = 1.59f;

	// Use this for initialization
	void Awake () 
	{
		GameManager.Instance.theVat = this;
		LabelMake();
	}

	void Start()
	{
		originalPosition = FluidObject.transform.position.y;
		originalHeight = FluidObject.transform.localScale.y;
		UpdateFluid();
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
        SoundManager.Instance.PlaySound(SoundManager.SoundType.SFX , "splash");

		if(fluidHeight > maxFluid)
		{
			for(int i=0; i<conveyors.Length; i++)
			{
				conveyors[i].speed = 0;
			}
			blastObject.Explode();
            SoundManager.Instance.PlaySound(SoundManager.SoundType.SFX, "JazzyExplosion");
			Invoke("GameOver",2f);
			FluidObject.transform.localScale = new Vector3(FluidObject.transform.localScale.x,0.1f,FluidObject.transform.localScale.z);
			FluidObject.transform.position = new Vector3(FluidObject.transform.position.x,-6.90f,FluidObject.transform.position.z);
		}
	}


	void MixFluid(GameObject flask)
	{
		Chemical chemical = flask.GetComponent<Chemical>();

		//if the chemical reacts badly
		if (GameManager.Instance.reactions[(int)type].values[(int)chemical.type] || type == chemical.type)
		{
			fluidHeight++;
		}
		//else the chemical reacts well.
		else
		{
			fluidHeight--;
		}

		UpdateFluid();

		Debug.Log("Fluid Height: "+ fluidHeight);
	}

	public void UpdateFluid()
	{
		//adjust goo size
		FluidObject.transform.localScale = new Vector3(FluidObject.transform.localScale.x, fluidHeight / maxFluid * originalHeight, FluidObject.transform.localScale.z);
		FluidObject.transform.position = new Vector3(FluidObject.transform.position.x, originalPosition - (POSITION_DRIFT - (fluidHeight / maxFluid * POSITION_DRIFT)), FluidObject.transform.position.z);

		//adjust goo colour
		switch (type)
		{
		case CHEMICAL.Beryllium:
			FluidObject.gameObject.GetComponent<Renderer>().material.color = new Color32(153,153,102,255);
			break;
		case CHEMICAL.Boron:
			FluidObject.gameObject.GetComponent<Renderer>().material.color = new Color32(25,25,25,255);
			break;
		case CHEMICAL.Carbon:
			FluidObject.gameObject.GetComponent<Renderer>().material.color = new Color32(204,255,51,255);
			break;
		case CHEMICAL.Fluorine:
			FluidObject.gameObject.GetComponent<Renderer>().material.color = new Color32(0,255,255,255);
			break;
		case CHEMICAL.Helium:
			FluidObject.gameObject.GetComponent<Renderer>().material.color = new Color32(255,153,204,255);
			break;
		case CHEMICAL.Hydrogen:
			FluidObject.gameObject.GetComponent<Renderer>().material.color = new Color32(204,51,255,255);
			break;
		case CHEMICAL.Lithium:
			FluidObject.gameObject.GetComponent<Renderer>().material.color = new Color32(255,255,153,255);
			break;
		case CHEMICAL.Neon:
			FluidObject.gameObject.GetComponent<Renderer>().material.color = new Color32(255,0,0,255);
			break;
		case CHEMICAL.Nitrogen:
			FluidObject.gameObject.GetComponent<Renderer>().material.color = new Color32(217,217,217,150);
			break;
		case CHEMICAL.Oxygen:
			FluidObject.gameObject.GetComponent<Renderer>().material.color = new Color32(0,0,179,255);
			break;
		case CHEMICAL.Sodium:
			FluidObject.gameObject.GetComponent<Renderer>().material.color = new Color32(255,255,255,200);
			break;
		}
	}

	void GameOver()
	{
		GameManager.Instance.NewGameState(GameManager.Instance.stateGameOver);
	}

    public void LabelMake()
    {
		type = (CHEMICAL)Random.Range (1,(int)CHEMICAL.Size);

        switch (type)
        {
            case CHEMICAL.Beryllium:
                Label.sprite = Labels[0];
                break;
            case CHEMICAL.Boron:
                Label.sprite = Labels[1];
                break;
            case CHEMICAL.Carbon:
                Label.sprite = Labels[2];
                break;
            case CHEMICAL.Fluorine:
                Label.sprite = Labels[3];
                break;
            case CHEMICAL.Helium:
                Label.sprite = Labels[4];
                break;
            case CHEMICAL.Hydrogen:
                Label.sprite = Labels[5];
                break;
            case CHEMICAL.Lithium:
                Label.sprite = Labels[6];
                break;
            case CHEMICAL.Neon:
                Label.sprite = Labels[7];
                break;
            case CHEMICAL.Nitrogen:
                Label.sprite = Labels[8];
                break;
            case CHEMICAL.Oxygen:
                Label.sprite = Labels[9];
                break;
            case CHEMICAL.Sodium:
                Label.sprite = Labels[10];
                break;
        }
    }
}
