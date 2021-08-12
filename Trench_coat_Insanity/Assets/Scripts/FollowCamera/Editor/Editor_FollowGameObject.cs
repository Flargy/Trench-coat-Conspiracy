using UnityEditor;
using UnityEngine;
using Utility;

namespace FollowCamera
{
	[CustomEditor(typeof(FollowGameObject))]
	public class Editor_FollowGameObject : Editor
	{
		FollowGameObject thiss;

		void OnEnable()
		{
			thiss = (FollowGameObject) target;
		}


		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			
			// EditorToolsUtility.TitleAuto("Setup");
			//
			// EditorToolsUtility.ObjectFieldAuto(ref thiss.cameraTarget,
			// 	"Reference to an empty GameObject (transform only).");
			//
			// EditorToolsUtility.ObjectFieldAuto(ref thiss.playerToFollow, "Reference to Player01 script on the player.");
			//
			//
			// EditorGUILayout.BeginHorizontal();
			// {
			// 	EditorGUILayout.BeginVertical();
			// 	EditorToolsUtility.ObjectFieldAuto(ref thiss.wall0, "Left wall");
			// 	EditorGUILayout.EndVertical();
			// 	EditorGUILayout.BeginVertical();
			// 	EditorToolsUtility.ObjectFieldAuto(ref thiss.wall1, "Right wall");
			// 	EditorGUILayout.EndVertical();
			// 	thiss.walls[0] = thiss.wall0;
			// 	thiss.walls[1] = thiss.wall1;
			// }
			// EditorGUILayout.EndHorizontal();
			//
			// EditorToolsUtility.ObjectFieldAuto(ref thiss.zoomoutButton, "Zoom out button. Reference to the UI button on the canvas that is used to zoom back out after zooming in.");
			//
			// EditorGUILayout.Space();
			//
			// EditorToolsUtility.TitleAuto("Camera settings");
			// // thiss.FOV = EditorGUILayout.Slider(thiss.FOV, 1f, 100f);
			// // EditorToolsUtility.DetailsAuto("FOV");
			//
			// thiss.addAmount = EditorGUILayout.Slider(thiss.addAmount, 0f, 5f);
			// EditorToolsUtility.DetailsAuto("Add amount.\nHow much to move the camera in front of the player.");
			//
			// thiss.smoothTime = EditorGUILayout.Slider(thiss.smoothTime, 0f, 10f);
			// EditorToolsUtility.DetailsAuto("Smooth time.\nNumber of seconds before the camera reaches the designated target.");
			//
			// thiss.cameraZoom = EditorGUILayout.Slider(thiss.cameraZoom, 0f, 10f);
			// EditorToolsUtility.DetailsAuto("Camera zoom.\nDistance to keep from player to camera.");
			//
			//
			
			//EditorToolsUtility.ObjectFieldAuto(ref thiss.addAmount, "How much to move the camera in front of the player");



			//EditorToolsUtility.ObjectFieldAuto(ref thiss.walls, "Wall limiters.\n 0 = left wall\n1 = right wall");

			//EditorToolsUtility.
		}
	}
}