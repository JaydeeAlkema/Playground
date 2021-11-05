using UnityEngine;

[CreateAssetMenu( fileName = "Player Stats", menuName = "ScriptableObjects/New Player Stats" )]
public class ScriptablePlayerStats : ScriptableObject
{
	[SerializeField] private int health;
	[Space]
	[SerializeField] private float fireRate;
	[Space]
	[SerializeField] private int projectileInventory;
	[SerializeField] private int projectileRestockTime;
	[SerializeField] private float projectileFireForce;
	[SerializeField] private float projectileMaxFireForce;
	[Space]
	[SerializeField] private float rotateAcceleration;
	[SerializeField] private float moveAcceleration;
	[Space]
	[SerializeField] private float maxMoveSpeed;
	[SerializeField] private float maxRotateSpeed;

	public int Health { get => health; set => health = value; }
	public float FireRate { get => fireRate; set => fireRate = value; }
	public int ProjectileInventory { get => projectileInventory; set => projectileInventory = value; }
	public int ProjectileRestockTime { get => projectileRestockTime; set => projectileRestockTime = value; }
	public float ProjectileFireForce { get => projectileFireForce; set => projectileFireForce =  value ; }
	public float ProjectileMaxFireForce { get => projectileMaxFireForce; set => projectileMaxFireForce = value; }
	public float RotateAcceleration { get => rotateAcceleration; set => rotateAcceleration = value; }
	public float MoveAcceleration { get => moveAcceleration; set => moveAcceleration = value; }
	public float MaxMoveSpeed { get => maxMoveSpeed; set => maxMoveSpeed = value; }
	public float MaxRotateSpeed { get => maxRotateSpeed; set => maxRotateSpeed = value; }
}
