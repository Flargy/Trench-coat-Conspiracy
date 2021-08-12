using UnityEngine;

public class AnimationSetup : MonoBehaviour
{
    private Animator _animator;
    private int _isWalkingHash;
    void Awake()
    {
        _animator = GetComponent<Animator>();
        _isWalkingHash = Animator.StringToHash("isWalking");
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _animator.SetBool(_isWalkingHash, true);
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            _animator.SetBool(_isWalkingHash, false);
        }
    }
}
