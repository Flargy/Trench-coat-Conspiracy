using UnityEditor;
using UnityEngine;

namespace FollowCamera
{
	public class Anchor : ScriptableObject
	{
		public Vector3 Position
		{
			get => _pos;
			set
			{
				Vector3 move = value - _pos;
				_pos = value;
				_controlPoints[0] += move; // move control points with anchor position
				_controlPoints[1] += move;
			}
		}

		Vector3 _pos;
		Vector3[] _controlPoints;

		public void Init(Vector3 position)
		{
			_pos = position;
			_controlPoints = new Vector3[2]
			{
				position + Vector3.up * 0.5f, position + Vector3.down * 0.5f
			};
		}

		public Vector3 this[int i]
		{
			get => _controlPoints[i];
			set => _controlPoints[i] = value;
		}

#if UNITY_EDITOR
		public void DrawLineToControlPoint(int i)
		{
			Handles.color = Color.yellow;
			Handles.DrawLine(_pos, _controlPoints[i], 1f);
			Handles.color = Color.white;
		}
#endif
	}
}