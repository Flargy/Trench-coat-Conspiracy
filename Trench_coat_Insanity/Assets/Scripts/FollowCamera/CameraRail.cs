using UnityEngine;

namespace FollowCamera
{
	public class CameraRail : MonoBehaviour
	{
		//public BezierCurveCubicPath curvePath;

		public Transform cameraTarget;

		public Transform fromFollowMovementLocation;
		public Transform toFollowMovementLocation;
		
		public Camera cam;

		public BoxCollider triggerBox;
		
		
		public bool usePositionHandleAnchors = true;
		public bool useFreeMoveHandleAnchors = true;
		public bool usePositionHandleControls = false;
		public bool useFreeMoveHandleControls = true;

		
		
		
		
		
		float t = 0;
		void Update()
		{
			//Vector3[] curve = curvePath.GetCurve();
			t += Time.deltaTime * 0.8f;
			
			//cam.transform.position = curvePath.FollowPath(t);
			cam.transform.LookAt(cameraTarget);
		}

		void OnDrawGizmos()
		{
			
		}
	}
}
