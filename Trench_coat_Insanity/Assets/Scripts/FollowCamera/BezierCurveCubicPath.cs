// using System.Collections;
// using System.Collections.Generic;
// using UnityEditor;
// using UnityEngine;
// using Utility;
//
// namespace FollowCamera
// {
// 	public class BezierCurveCubicPath : ScriptableObject
// 	{
// 		public List<Anchor> anchors = new List<Anchor>(); // (anchor0, control0, control1), anchor
//
// 		public Anchor this[int i]
// 		{
// 			get => anchors[i];
// 			set => anchors[i] = value;
// 		}
//
// 		public void Init(Vector3 startPoint)
// 		{
// 			if (anchors.Count > 0) return; // already initialized
//
// 			Anchor firstAnchor =
// 				EditorToolsUtility.CreateScriptableObjectAsset<Anchor>("Assets/Scripts/FollowCamera/Data/Anchors/",
// 					out string completeAssetPathAndName);
// 			firstAnchor.Init(startPoint);
// 			
// 			Anchor secondAnchor =
// 				EditorToolsUtility.CreateScriptableObjectAsset<Anchor>("Assets/Scripts/FollowCamera/Data/Anchors/",
// 					out completeAssetPathAndName);
// 			secondAnchor.Init(startPoint + Vector3.right * 1f);
//
// 			anchors.Add(firstAnchor);
// 			anchors.Add(secondAnchor);
// 		}
//
// 		public Vector3[] GetCurve()
// 		{
// 			Vector3[] curve = new Vector3[] {anchors[0].Position, anchors[0][1], anchors[1][0], anchors[1].Position};
// 			return curve;
// 		}
//
// 		bool pathRunning = false;
//
// 		public Vector3 FollowPath(float t)
// 		{
// 			int i = (int) t;
//
// 			i %= anchors.Count - 1;
//
// 			Vector3 fromAnchor = anchors[i].Position;
// 			Vector3 fromControl = anchors[i][1];
// 			Vector3 toControl = anchors[i + 1][0];
// 			Vector3 toAnchor = anchors[i + 1].Position;
// 			
// 			return Henrix.TravelCubicBezierCurve(fromAnchor, fromControl, toControl, toAnchor, t % 1);
// 		}
//
// 		public void AddSegment()
// 		{
// 			Anchor nextAnchor =
// 				EditorToolsUtility.CreateScriptableObjectAsset<Anchor>("Assets/Scripts/FollowCamera/Data/Anchors/",
// 					out string completeAssetPathAndName);
// 			int count = anchors.Count;
// 			Vector3 direction = (anchors[count - 1].Position - anchors[count - 1][0]).normalized;
// 			nextAnchor.Init(anchors[count - 1].Position + direction * 1f);
// 			//nextAnchor.Init(direction * 1f);
// 			
// 			anchors.Add(nextAnchor);
// 		}
//
// 		public void Delete()
// 		{
// 			foreach (Anchor anchor in anchors)
// 			{
// 				if (!EditorToolsUtility.DeleteScriptableObjectAsset(anchor))
// 				{
// 					Debug.LogError("Scriptable object path not found");
// 				}
// 			}
// 			
// 			EditorToolsUtility.DeleteScriptableObjectAsset(this);
// 		}
//
// #if UNITY_EDITOR
// 		public void DrawCurveWithHandles(bool usePositionHandleAnchors,
// 			bool useFreeMoveHandleAnchors, bool usePositionHandleControls, bool useFreeMoveHandleControls)
// 		{
// 			Handles.color = Color.white;
//
// 			if (anchors == null) return;
//
// 			//draw lines between anchor points
// 			for (int i = 0; i < anchors.Count; i++)
// 			{
// 				Handles.color = Color.red;
//
// 				// path
// 				if (i < anchors.Count - 1)
// 				{
// 					Handles.DrawBezier(anchors[i].Position, anchors[i + 1].Position, anchors[i][1], anchors[i + 1][0],
// 						Color.green, Texture2D.whiteTexture, 2f);
// 				}
//
// 				if (usePositionHandleAnchors)
// 					anchors[i].Position =
// 						Handles.PositionHandle(anchors[i].Position, Quaternion.identity);
// 				if (useFreeMoveHandleAnchors)
// 					anchors[i].Position = Handles.FreeMoveHandle(anchors[i].Position,
// 						Quaternion.identity, .1f, Vector3.zero, Handles.SphereHandleCap);
//
// 				Handles.color = Color.white;
//
// 				// draw control points and their handles
//
// 				Handles.color = Color.gray;
// 				if (i == 0)
// 				{
// 					DrawControlPointHandleAndLine(i, 1, usePositionHandleControls,
// 						useFreeMoveHandleControls);
// 				}
// 				else if (i == anchors.Count - 1)
// 				{
// 					DrawControlPointHandleAndLine( i, 0, usePositionHandleControls,
// 						useFreeMoveHandleControls);
// 				}
// 				else
// 				{
// 					DrawControlPointHandleAndLine(i, 0, usePositionHandleControls,
// 						useFreeMoveHandleControls);
// 					DrawControlPointHandleAndLine(i, 1, usePositionHandleControls,
// 						useFreeMoveHandleControls);
// 				}
// 			}
//
// 			void DrawControlPointHandleAndLine(int anchor,
// 				int controlPoint, bool usePositionHandle, bool useFreeMoveHandle)
// 			{
// 				anchors[anchor].DrawLineToControlPoint(controlPoint);
//
// 				if (usePositionHandle)
// 					anchors[anchor][controlPoint] =
// 						Handles.PositionHandle(anchors[anchor][controlPoint], Quaternion.identity);
// 				if (useFreeMoveHandle)
// 					anchors[anchor][controlPoint] = Handles.FreeMoveHandle(
// 						anchors[anchor][controlPoint], Quaternion.identity, .1f, Vector3.zero,
// 						Handles.SphereHandleCap);
// 			}
//
// 			Handles.color = Color.white;
// 		}
//
//
// #endif
// 	}
// }