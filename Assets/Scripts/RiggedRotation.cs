using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiggedRotation : MonoBehaviour
{
    [SerializeField]
    private Vector3[] _diceRotationToNextValue = new Vector3[6];


    // Rotations:

    // 1 -> 2 : Rotate Z 90
    // 2 -> 3 : Rotate X 90
    // 3 -> 4 : Rotate X 180
    // 4 -> 5 : Rotate X -90
    // 5 -> 6 : Rotate Z -90
    // 6 -> 1 : Rotate X -180

    // 1 -> 5:
    //     Z+90
    //     Z+90, X+90
    //     Z+90, X+270
    //     Z+90, X+180


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
