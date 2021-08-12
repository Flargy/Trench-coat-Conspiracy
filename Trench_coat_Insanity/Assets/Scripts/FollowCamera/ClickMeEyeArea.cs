using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FollowCamera
{
	public class ClickMeEyeArea : MonoBehaviour
	{
		public ClickMe clickMe;
		
		BoxCollider _collider;
		void Start()
		{
			_collider = GetComponent<BoxCollider>();
		}

		// Update is called once per frame



		// public void OnTriggerEnter(Collider other)
		// {
		// 	if (!other.CompareTag("Player")) return;
		// 	print("enter1");
		// 	clickMe.Show();
		// }
		//
		// public void OnTriggerExit(Collider other)
		// {
		// 	if (!other.CompareTag("Player")) return;
		// 	print("exit");
		// 	clickMe.Hide();
		// }
	}

}
