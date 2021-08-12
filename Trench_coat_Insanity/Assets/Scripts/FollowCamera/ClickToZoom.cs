using UnityEditor;
using UnityEngine;

namespace FollowCamera
{
	public class ClickToZoom : MonoBehaviour
	{
		public ClickMe clickMe;
		public Transform focus;

		public Quaternion TargetRot()
		{
			transform.LookAt(focus);
			return transform.rotation;
		}

#if UNITY_EDITOR
		void OnSceneGUI()
		{
			Vector3 pos = transform.position;
			Handles.color = Color.green;
			Handles.DrawLine(pos, pos + transform.up * 3f);

			Handles.color = Color.blue;
			Handles.DrawLine(pos, pos + transform.forward * 3f);
			Handles.color = Color.white;
		}
#endif
	}
}