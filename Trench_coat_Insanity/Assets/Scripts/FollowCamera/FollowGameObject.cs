using System.Collections;
using Inspection;
using Player;
using UnityEngine;

namespace FollowCamera
{
	public class FollowGameObject : MonoBehaviour
	{
		public Transform cameraTarget;
		public Player01 playerToFollow;
		
		[Tooltip("Static mesh walls to the sides of the map that marks the end of the map")]
		public GameObject[] walls;
		
		public Vector3 cameraPosOffSet;

		[Range(0f, 5f)] public float addAmount = 2f; // amount to move camera forward in front of player
		[Range(0f, 10f)] public float smoothTime = 5f;
		[Range(0f, 40f)] public float cameraZoom = 5f;

		public UnityEngine.UI.Button zoomoutButton;

		private Camera thisCameraComponent;
		private Vector3 _velocity = Vector3.zero;

		private int _wallSide = 1;

		// coroutines
		private IEnumerator walkFollow;
		private IEnumerator zoomIn;
		private IEnumerator zoomOut;


		private void Awake()
		{
			walkFollow = FollowTarget();
			zoomOut = ZoomOut();
			thisCameraComponent = GetComponent<Camera>();

			if (thisCameraComponent == null) Debug.LogError("Couldn't get camera in children.");

			Vector3 dirOffset =
				Vector3.right * addAmount;
			cameraTarget.transform.position = cameraPosOffSet + dirOffset;

			StartCoroutine(walkFollow);
		}

		private void Start()
		{
			zoomoutButton.onClick.AddListener(OnClickZoomOut);
			zoomoutButton.gameObject.SetActive(false);
		}

		#region WalkFollow

		private bool startZoomIn;

		private IEnumerator FollowTarget()
		{
			//_camera.transform.rotation = Quaternion.Euler(Xrotation, 0, 0);
			startZoomIn = false;

			while (!startZoomIn)
			{
				Vector3 dirOffset =
					playerToFollow.lastMovementDir * addAmount; //= -Vector3.right * TargetSideOfCenter();

				if (ReachedSideWall())
				{
					// let camera follow this target only if this target is to the left of the cameras current position

					cameraTarget.transform.position = Vector3.Lerp(cameraTarget.transform.position,
						playerToFollow.transform.position + cameraPosOffSet + dirOffset, smoothTime * Time.deltaTime);

					switch (_wallSide)
					{
						case -1
							when cameraTarget.transform.position.x > thisCameraComponent.transform.position.x
							: // wall is to the left
							MakeCameraFollowCameraTarget();
							break;
						case 1 when cameraTarget.transform.position.x < thisCameraComponent.transform.position.x:
							MakeCameraFollowCameraTarget();
							break;
					}
				}
				else
				{
					cameraTarget.transform.position = Vector3.Lerp(cameraTarget.transform.position,
						playerToFollow.transform.position + cameraPosOffSet + dirOffset,
						smoothTime * Time.deltaTime);

					MakeCameraFollowCameraTarget();
				}

				yield return null;
			}

			yield return StartCoroutine(zoomIn);
		}

		private void MakeCameraFollowCameraTarget()
		{
			Vector3 cameraOffset = Vector3.back * cameraZoom;
			transform.position = cameraTarget.transform.position + cameraOffset;
		}

		private bool ReachedSideWall()
		{
			float fov = thisCameraComponent.fieldOfView;

			Transform cameraTransf = thisCameraComponent.transform;

			// Loop through each wall object. If it's somewhat within the camera view, 
			foreach (GameObject wall in walls)
			{
				Vector3 forwardVec = cameraTransf.forward;
				Vector3 wallRelativePos = wall.transform.position - thisCameraComponent.transform.position;

				Vector3 wallDir = wallRelativePos.normalized;

				// the dot product of 2 vectors == the cos of the angle between them
				float angle = Vector3.Dot(forwardVec, wallDir);

				angle = Mathf.Acos(angle) * Mathf.Rad2Deg;

				if (angle > fov) continue; // didn't reach limit / limiter/wall is not within camera fov

				Debug.DrawLine(cameraTransf.position, cameraTransf.position + wallRelativePos, Color.red);

				// check direction to block movement to
				Vector3 cross = Vector3.Cross(forwardVec, wallDir);
				_wallSide = cross.y < 0 ? -1 : 1;
				
				return true;
			}

			return false;
		}

		#endregion // follow while walking

		#region ZoomIn

		private Vector3 originalThisPos;
		private Quaternion originalThisRotation;
		private Vector3 originalCameraTarget;

		private ClickToZoom currentClickToZoom;


		private float increaseStepMove = 5f;

		public void OnClickZoomIn(ClickToZoom clickToZoom)
		{
			playerToFollow.MovementEnabled = false;

			//clickToZoom.Hide(); // hide ClickToZoom interactable object
			currentClickToZoom = clickToZoom; // save a ref to it

			zoomIn = ZoomIn(clickToZoom.transform.position, clickToZoom.focus.transform.position,
				clickToZoom.TargetRot());

			startZoomIn = true; // break walking and start zoom-in 

			zoomoutButton.gameObject.SetActive(true);
		}

		private bool startZoomout;

		private IEnumerator ZoomIn(Vector3 cameraTargetLoc, Vector3 focus, Quaternion targetRot)
		{
			originalCameraTarget = cameraTarget.transform.position;
			originalThisPos = transform.position;
			originalThisRotation = transform.rotation;

			startZoomout = false;

			while (!startZoomout)
			{
				transform.position =
					Vector3.MoveTowards(transform.position, cameraTargetLoc, increaseStepMove * Time.deltaTime);
				//   transform.position = Vector3.SmoothDamp(transform.position, cameraTargetLoc, ref _velocity, 2f,
				// 50000000f);

				cameraTarget.transform.position = Vector3.MoveTowards(cameraTarget.transform.position, focus,
					increaseStepMove * Time.deltaTime);

				transform.LookAt(cameraTarget.position);


				zoomoutButton.gameObject.SetActive(!ItemInspection.InteractionActive);


				// yield return new WaitForEndOfFrame();
				yield return null;
			}

			yield return ZoomOut();
		}

		#endregion // zoom in


		#region ZoomOut

		private void OnClickZoomOut()
		{
			zoomoutButton.gameObject.SetActive(false);
			startZoomout = true;
		}

		private IEnumerator ZoomOut()
		{
			// float timer = 3.5f;
			// float timePassed = 0f;

			while (cameraTarget.transform.position != originalCameraTarget || transform.position != originalThisPos)
			{
				// cameraTarget.transform.position = Vector3.SmoothDamp(cameraTarget.transform.position,
				// 	originalCameraTarget, ref _velocity, timer);


				if (cameraTarget.transform.position != originalCameraTarget)
				{
					cameraTarget.transform.position = Vector3.MoveTowards(cameraTarget.transform.position,
						originalCameraTarget,
						increaseStepMove * Time.deltaTime);
				}

				if (transform.position != originalThisPos)
				{
					transform.position =
						Vector3.MoveTowards(transform.position, originalThisPos,
							increaseStepMove * Time.deltaTime);
				}

				transform.LookAt(cameraTarget.position);
				
				yield return null;
			}

			transform.rotation = Quaternion.identity;

			currentClickToZoom.gameObject.SetActive(true);

			playerToFollow.MovementEnabled = true;

			yield return StartCoroutine(FollowTarget());
		}

		#endregion
	}
}