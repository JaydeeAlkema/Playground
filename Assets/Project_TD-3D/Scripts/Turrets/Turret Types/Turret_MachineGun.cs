using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_MachineGun : TurretBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		GetTarget();
	}

	private void Update()
	{
		LookAtTarget();
	}
}
