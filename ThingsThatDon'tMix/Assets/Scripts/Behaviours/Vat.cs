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

	// Use this for initialization
	void Awake () 
	{
		GameManager.Instance.theVat = this;
		LabelMake();
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

		Debug.Log("Fluid Height: "+ fluidHeight);
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
