using Interaction;
using UnityEngine;

namespace FollowCamera
{
	public class ClickMe : MonoBehaviour, IInteractable
	{
		public FollowGameObject mainCamera;

		[HideInInspector] public ClickToZoom cameraPos;

		private SpriteRenderer renderer;

		private bool clickable = false;
		
		void Awake()
		{
			renderer = GetComponent<SpriteRenderer>();
		}

		public void Interact()
		{
			mainCamera.OnClickZoomIn(cameraPos);
		}
	}
}