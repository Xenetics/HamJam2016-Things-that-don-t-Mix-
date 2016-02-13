using UnityEngine;
using System.Collections;

[RequireComponent (typeof (SphereCollider))]

public class Blast : MonoBehaviour 
{
	public float magnitude = 50000.0F;
    void Start() 
	{
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, ((SphereCollider)GetComponent<Collider>()).radius);
		
        foreach (Collider hit in colliders) 
		{
            //if (!hit)
            if (hit.GetComponent<Rigidbody>())
			{
            	hit.GetComponent<Rigidbody>().AddExplosionForce(magnitude, explosionPos, ((SphereCollider)GetComponent<Collider>()).radius,0);
			}
        }
    }
}
