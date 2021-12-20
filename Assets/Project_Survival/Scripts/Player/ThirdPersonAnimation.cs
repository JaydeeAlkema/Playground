using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survival
{
	public class ThirdPersonAnimation : MonoBehaviour
	{
		private Animator anim;
		private Rigidbody rb;
		private float maxSpeed = 5f;

		private void Start()
		{
			anim = GetComponentInChildren<Animator>();
			rb = GetComponent<Rigidbody>();
		}

		private void Update()
		{
			anim.SetFloat( "Speed", rb.velocity.magnitude / maxSpeed );
		}
	}
}
