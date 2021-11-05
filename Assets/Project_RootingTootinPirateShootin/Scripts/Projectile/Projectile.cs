using UnityEngine;

[RequireComponent( typeof( Rigidbody ), typeof( SphereCollider ) )]
public class Projectile : MonoBehaviour
{
	[SerializeField] private Rigidbody _rb = default;
	[SerializeField] private GameObject _onHitParticles = default;
	[SerializeField] private int _damage = 100;

	private void Start()
	{
		_rb = GetComponent<Rigidbody>();

		Destroy( gameObject, 10f );
	}

	public void Initialize( float force )
	{
		_rb.AddRelativeForce( transform.forward * force, ForceMode.Impulse );
	}

	private void OnTriggerEnter( Collider other )
	{
		other.GetComponent<IDamageable>()?.OnHit( _damage );
	}
}
