using System.Collections;
using UnityEngine;

/**
 * Rotates an object over the time "rotationTime". 
 * Rotation degrees are set through the editor script
 */
public class Rotate_Action : ActionType
{
    public Quaternion endRotation;
    public float rotationTime = 2.0f;

    private Quaternion _startRotation;

    private void Start()
    {
        _startRotation = transform.rotation;
    }

    public void SetRotation(Quaternion rotation)
    {
        endRotation = rotation;
    }

    public override void Activate()
    {
        StartCoroutine(RotateObject());
    }
    
    /**
     * Coroutine which rotates the object to a set amount of degrees over time.
     */
    private IEnumerator RotateObject()
    {
        float time = 0f;
        while (time <= rotationTime)
        {
            time += Time.deltaTime;

            transform.rotation = Quaternion.Slerp(_startRotation, endRotation, time / rotationTime);
            yield return null;
        }
    }
}
