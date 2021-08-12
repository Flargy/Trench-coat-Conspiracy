using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Utility;

namespace FollowCamera
{
	[CustomEditor(typeof(ClickMe))]
	public class Editor_ClickMe : Editor
	{
		ClickMe thiss;
		void OnEnable()
		{
			thiss = (ClickMe) target;
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			
			// EditorToolsUtility.TitleAuto("Zoom parameters");
			// thiss.cameraPos.cameraSpeed =
			// 	EditorGUILayout.Slider(thiss.cameraPos.cameraSpeed, 0f, 15f);
			// EditorToolsUtility.DetailsAuto("Camera speed");
			// thiss.cameraPos.targetSpeed =
			// 	EditorGUILayout.Slider(thiss.cameraPos.targetSpeed, 0f, 15f);
			// EditorToolsUtility.DetailsAuto("Target speed");
			//
			// if (thiss.cameraPos.cameraSpeed > thiss.cameraPos.targetSpeed)
			// {
			// 	thiss.cameraPos.cameraSpeed = thiss.cameraPos.targetSpeed;
			// 	//thiss.cameraPos.targetSpeed = thiss.cameraPos.cameraSpeed;
			// }
		}

		
	}

}
