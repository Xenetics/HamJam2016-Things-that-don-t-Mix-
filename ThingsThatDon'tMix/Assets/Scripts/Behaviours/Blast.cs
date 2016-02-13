using UnityEngine;
using System.Collections;

public class Blast : MonoBehaviour 
{
	public float magnitude = 50000.0F;
	public float radius = 5.0f;
    public void Explode() 
	{
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
		
        foreach (Collider hit in colliders) 
		{
            //if (!hit)
            if (hit.GetComponent<Rigidbody>())
			{
            	hit.GetComponent<Rigidbody>().AddExplosionForce(magnitude, explosionPos, radius,0);
			}
        }
    }
}
