using UnityEditor;
using UnityEngine;
using Utility;

namespace FollowCamera
{
	[CustomEditor(typeof(CameraRail))]
	public class Editor_CameraRail : Editor
	{
		CameraRail thiss;
		//BezierCurveCubicPath _curve;

		void OnEnable()
		{
			thiss = (CameraRail) target;
			//_curve = thiss.curvePath;
		}

		public override void OnInspectorGUI()
		{
			EditorToolsUtility.TitleAuto("Setup");
			
			EditorToolsUtility.ObjectFieldAuto(ref thiss.cam, "Camera reference to the camera that will follow the provided path.");

			EditorGUILayout.BeginHorizontal();
			{
				
			} EditorGUILayout.EndHorizontal();

			EditorToolsUtility.ObjectFieldAuto(ref thiss.cameraTarget, "The target the camera will aim at.");
			//EditorToolsUtility.ObjectFieldAuto(ref thiss.curvePath, "The BezierCurveCubicPath Scriptable Object containing the data for this rail.");
			
			GUI.color = Color.green;
			if (GUILayout.Button("Create new curve path"))
			{
				//_curve =
					//EditorToolsUtility.CreateScriptableObjectAsset<BezierCurveCubicPath>(
						//"Assets/Scripts/FollowCamera/Data/", out string completeAssetPathAndName, "Curve 1");
				//_curve.Init(thiss.transform.position);
			}
			
			{
				//GUI.color = new Color(1f, 0f, 0f, 0.7f);
				if (GUILayout.Button("Delete selected asset"))
				{
					//_curve.Delete();
				}
			
				GUI.color = Color.white;
			}
			
			GUI.color = Color.white;

			EditorToolsUtility.TitleAuto("Handles");
			
			EditorGUILayout.BeginHorizontal();
			{
				EditorGUILayout.BeginVertical();
				{
					thiss.usePositionHandleAnchors = EditorGUILayout.ToggleLeft("XYZ handle", thiss.usePositionHandleAnchors);
					thiss.useFreeMoveHandleAnchors = EditorGUILayout.ToggleLeft("Sphere handle", thiss.useFreeMoveHandleAnchors);
					EditorToolsUtility.DetailsAuto("Anchor handles");
				}
				EditorGUILayout.EndVertical();
				
				EditorGUILayout.BeginVertical();
				{
					thiss.usePositionHandleControls = EditorGUILayout.ToggleLeft("XYZ handle", thiss.usePositionHandleControls);
					thiss.useFreeMoveHandleControls = EditorGUILayout.ToggleLeft("Sphere handle", thiss.useFreeMoveHandleControls);
					EditorToolsUtility.DetailsAuto("Control handles");
				}
				EditorGUILayout.EndVertical();
			}
			EditorGUILayout.EndHorizontal();
		}
		
		// Start is called before the first frame update
		void OnSceneGUI()
		{
			// if (thiss.curvePath == null) return;
			//
			// thiss.curvePath.DrawCurveWithHandles(thiss.usePositionHandleAnchors,
			// 	thiss.useFreeMoveHandleAnchors, thiss.usePositionHandleControls,
			// 	thiss.useFreeMoveHandleControls);
			//
			// Handles.BeginGUI();
			// GUI.color = Color.green;
			//
			// if (GUI.Button(new Rect(0f, 0f, 200f, 20f), "Add segment"))
			// {
			// 	thiss.curvePath.AddSegment();
			// }
			
			Handles.EndGUI();
		}
	}
}