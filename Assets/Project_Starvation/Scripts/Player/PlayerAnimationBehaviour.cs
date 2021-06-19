using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace project_starvation
{
	public class PlayerAnimationBehaviour : MonoBehaviour
	{
		[SerializeField] private Animator animator;

		public void AnimateOnInput( float x, float y )
		{
			if( x != 0 || y != 0 )
			{
				animator.SetFloat( "X Input", x );
				animator.SetFloat( "Y Input", y );
			}

			if( x != 0 || y != 0 ) animator.SetBool( "Moving", true );
			else animator.SetBool( "Moving", false );
		}
	}
}
