using UnityEngine;

[RequireComponent( typeof( Rigidbody ), typeof( SphereCollider ) )]
public class Projectile : MonoBehaviour
{
	[SerializeField] private Rigidbody _rb = default;
	[SerializeField] private GameObject _onHitParticles = default;
	[SerializeField] private int _damage = 100;
	[SerializeField] private Vector3 _destination;

	private void Start()
	{
		_rb = GetComponent<Rigidbody>();

		Destroy( gameObject, 10f );
	}

	private void Update()
	{
		Vector3 movementVector = Vector3.MoveTowards( transform.position, _destination, 15f * Time.deltaTime );
		transform.position = movementVector;
	}

	public void SetDestination( Vector3 destination )
	{
		_destination = destination;
	}

	private void OnTriggerEnter( Collider other )
	{
		other.GetComponent<IDamageable>()?.OnHit( _damage );
		Destroy( this.gameObject );
	}
}
