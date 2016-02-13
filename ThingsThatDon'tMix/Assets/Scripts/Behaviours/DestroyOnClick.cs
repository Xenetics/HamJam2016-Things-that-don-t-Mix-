using UnityEngine;
using System.Collections;

public class DestroyOnClick : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown()
	{
		// this object was clicked - do something
		Chemical chem = gameObject.GetComponent<Chemical>();
		GameManager.Instance.Consume(chem.type);

		//clean up the object
		Destroy (this.gameObject);
	}   

}
