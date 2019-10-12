using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiggedDice : MonoBehaviour
{   
    private int _stepIndex = 0;

    private Rigidbody _rigidBody;

    private List<Vector3> _positions = new List<Vector3>();
    
    private List<Quaternion> _rotations = new List<Quaternion>();

    // The rotation offset to result in the rigged roll
    public Quaternion RotationOffest { get; set; }

    public void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    public void Reset()
    {
        _stepIndex = 0;
        RotationOffest = Quaternion.identity;
        _positions.Clear();
        _rotations.Clear();
    }

    public void RecordStep()
    {
        _positions.Add(transform.position);
        _rotations.Add(transform.rotation);
    }

    public bool IsRolling()
    {
        bool isRolling = false;

        // Check velcoity
        if (!Mathf.Approximately(_rigidBody.velocity.x, 0) ||
            !Mathf.Approximately(_rigidBody.velocity.y, 0) ||
            !Mathf.Approximately(_rigidBody.velocity.z, 0))
        {
            isRolling |= true;
        }

        // Check we've "stopped" rotating
        if (!Mathf.Approximately(_rigidBody.angularVelocity.x, 0) ||
            !Mathf.Approximately(_rigidBody.angularVelocity.y, 0) ||
            !Mathf.Approximately(_rigidBody.angularVelocity.z, 0))
        {
            isRolling |= true;
        }

        return isRolling;
    }

    public void PhysicsStep()
    {
        transform.position = _positions[_stepIndex];
        transform.rotation = _rotations[_stepIndex] * RotationOffest;

        _stepIndex++;
    }

    public bool HasPhysicStepToPlay()
    {
        return _positions != null && _positions.Count > 0 && _positions.Count > _stepIndex;
    }
}
