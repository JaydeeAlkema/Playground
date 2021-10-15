using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
	public float speed = 10f;
	public int damage = 1;
	public Transform targetTransform;

	private void Update()
	{
		if( !targetTransform ) return;

		transform.position = Vector3.MoveTowards( transform.position, targetTransform.position, speed * Time.deltaTime );
	}

	private void OnTriggerEnter( Collider other )
	{
		other.GetComponent<IEnemy>()?.OnHit( damage );
		Destroy( this.gameObject );
	}
}
