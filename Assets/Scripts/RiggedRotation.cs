using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiggedRotation : MonoBehaviour
{
    // Face orientation:
    // 1 - up
    // 6 - -up
    
    // 2 - right
    // 5 - -right

    // 3 - -forward
    // 4 - forward 
    public DiceValueEnum GetValue()
    {
        var upAngle = Vector3.Angle(transform.up, Vector3.up);
        var downAngle = Vector3.Angle(-transform.up, Vector3.up);
        var rightAngle = Vector3.Angle(transform.right, Vector3.up);
        var leftAngle = Vector3.Angle(-transform.right, Vector3.up);
        var forwardAngle = Vector3.Angle(transform.forward, Vector3.up);
        var backwardAngle = Vector3.Angle(-transform.forward, Vector3.up);

        var minAngle = Mathf.Min(upAngle, downAngle, rightAngle, leftAngle, forwardAngle, backwardAngle);

        if (Mathf.Approximately(minAngle, upAngle))
        {
            return DiceValueEnum.One;
        }

        if (Mathf.Approximately(minAngle, downAngle))
        {
            return DiceValueEnum.Six;
        }

        if (Mathf.Approximately(minAngle, rightAngle))
        {
            return DiceValueEnum.Two;
        }

        if (Mathf.Approximately(minAngle, leftAngle))
        {
            return DiceValueEnum.Five;
        }

        if (Mathf.Approximately(minAngle, backwardAngle))
        {
            return DiceValueEnum.Three;
        }

        if (Mathf.Approximately(minAngle, forwardAngle))
        {
            return DiceValueEnum.Four;
        }

        // Shrug
        return DiceValueEnum.One;
    }
}
