using UnityEngine;

public class IslandBehaviour : MonoBehaviour, IDamageable
{
	[SerializeField] private float health;

	public void OnHit( int damage )
	{
		if( health <= 0 )
		{
			Destroy( this.gameObject );
		}
	}
}
