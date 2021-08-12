using System.Collections;
using Interaction;
using InventorySystem;
using UnityEngine;

namespace Player
{
	public class Player01 : MonoBehaviour
	{
		public float movementSpeed = 5f;
		[SerializeField] private float interactionOffset = 0.8f;
		[SerializeField] private float rotationTime = 0.3f;

		// [Tooltip("Affects mouse movement, where the player ends up")]
		// public float characterWidthMovementOffset = 0.5f;

		Coroutine _movePlayer;
		private Animator _animator;
		private int _isWalkingHash;

		bool _isMoving = false;

		bool _movementEnabled;

		public bool MovementEnabled
		{
			get => _movementEnabled;
			set
			{
				_movementEnabled = value;
				if (!_movementEnabled) // if disabling
				{
					StopMoving();
				}
			}
		}
		// used by the camera. Disables player movement when zoomed in to inspection area.

		public Vector3 lastMovementDir = Vector3.right;

		private void Awake()
		{
			if (TryGetComponent(out Animator value))
			{
				_animator = value;
				_isWalkingHash = Animator.StringToHash("isWalking");
			}

			MovementEnabled = true;
		}

		public void Interact(Vector3 point)
		{
			print("Called");
			print(point);

			if (MovementEnabled)
			{
				MoveTo(point);
			}
		}

		public void StopMoving()
		{
			if (_isMoving)
			{
				StopCoroutine(_movePlayer);
			}
			_animator.SetBool(_isWalkingHash, false);
			
			_isMoving = false;
		}

		public void MoveTo(Vector3 moveToPos, IInteractable interactionObject = null)
		{
			Vector3 movementDir = Vector3.right;

			StopMoving();

			_isMoving = true;
			if (MovementEnabled)
			{
				_movePlayer = StartCoroutine(interactionObject == null
					? MovePlayer(moveToPos)
					: MovePlayer(moveToPos, interactionObject));
			}

		}


		// add rotation at increased speed to the while loops
		IEnumerator MovePlayer(Vector3 moveToPos, IInteractable interactable = null)
		{
			Vector3 movementDir = Vector3.right;
			float itemOffset = 0f;
			Transform myTransform = transform;
			Quaternion oldRotation = myTransform.rotation;
			Quaternion newRotation = Quaternion.identity;
			float t = 0f;

			if (interactable != null)
				itemOffset = interactionOffset;

			if (_animator != null) // remove later when the prefab is fully finsihed
			{
				_animator.SetBool(_isWalkingHash, true);
			}

			if (moveToPos.x <= myTransform.position.x) // moving left
			{
				movementDir = -movementDir;
				lastMovementDir = movementDir;
				newRotation = new Quaternion(0, -0.7071f, 0, 0.7071f);
				while (moveToPos.x + itemOffset <= myTransform.position.x)
				{
					if (Physics.Raycast(myTransform.position + Vector3.up, movementDir, interactionOffset - 0.1f, 1)
					) // + Vector3.up to fix animation issue
					{
						break;
					}

					myTransform.rotation = Quaternion.Lerp(oldRotation, newRotation, t);
					t += Time.deltaTime / rotationTime;
					myTransform.position += movementDir * (movementSpeed * Time.deltaTime);
					yield return null; // null gives an effect more similar to update function
				}
			}
			else // moving right
			{
				lastMovementDir = movementDir;
				newRotation = new Quaternion(0, 0.7071f, 0, 0.7071f);
				while (moveToPos.x - itemOffset >= myTransform.position.x)
				{
					if (Physics.Raycast(myTransform.position + Vector3.up, movementDir, interactionOffset - 0.1f, 1))
					{
						break;
					}

					myTransform.rotation = Quaternion.Lerp(oldRotation, newRotation, t);
					t += Time.deltaTime / rotationTime;
					myTransform.position += movementDir * (movementSpeed * Time.deltaTime);
					yield return null; // null gives an effect more similar to update function
				}
			}

			if (interactable != null)
			{
				interactable.Interact();
				_animator.SetTrigger("pickingUpItem");
				Inventory.ReleaseHeldItem();
			}


			_animator.SetBool(_isWalkingHash, false);

			_isMoving = false;
		}
	}
}