using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiggedRotation : MonoBehaviour
{
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

    public Quaternion GetRotationForValue(DiceValueEnum desiredValue)
    {
        DiceValueEnum valueRolled = GetValue();

        switch(desiredValue)
        {
            case DiceValueEnum.One:
                return GetRotationForOne(valueRolled);
            case DiceValueEnum.Two:
                return GetRotationForTwo(valueRolled);
            case DiceValueEnum.Three:
                return GetRotationForThree(valueRolled);
            case DiceValueEnum.Four:
                return GetRotationForFour(valueRolled);
            case DiceValueEnum.Five:
                return GetRotationForFive(valueRolled);
            case DiceValueEnum.Six:
                return GetRotationForSix(valueRolled);
        }

        return Quaternion.identity;
    }

    // TODO FIgure this out better!!!
    private Quaternion GetRotationForOne(DiceValueEnum currentValue)
    {
        switch(currentValue)
        {
            case DiceValueEnum.Two:
                return Quaternion.AngleAxis(-90, Vector3.forward);
            case DiceValueEnum.Three:
                return Quaternion.AngleAxis(-90, Vector3.right);
            case DiceValueEnum.Four:
                return Quaternion.AngleAxis(90, Vector3.right);
            case DiceValueEnum.Five:
                return Quaternion.AngleAxis(90, Vector3.forward);
            case DiceValueEnum.Six:
                return Quaternion.AngleAxis(180, Vector3.forward);
        }

        return Quaternion.identity;
    }
    private Quaternion GetRotationForTwo(DiceValueEnum currentValue)
    {
        switch(currentValue)
        {
            case DiceValueEnum.One:
                return Quaternion.AngleAxis(90, Vector3.forward);
            case DiceValueEnum.Three:
                return Quaternion.AngleAxis(90, Vector3.up);
            case DiceValueEnum.Four:
                return Quaternion.AngleAxis(-90, Vector3.up);
            case DiceValueEnum.Five:
                return Quaternion.AngleAxis(180, Vector3.up);
            case DiceValueEnum.Six:
                return Quaternion.AngleAxis(-90, Vector3.forward);
        }

        return Quaternion.identity;
    }

    private Quaternion GetRotationForThree(DiceValueEnum currentValue)
    {
        switch(currentValue)
        {
            case DiceValueEnum.One:
                return Quaternion.AngleAxis(90, Vector3.right);
            case DiceValueEnum.Two:
                return Quaternion.AngleAxis(-90, Vector3.up);
            case DiceValueEnum.Four:
                return Quaternion.AngleAxis(180, Vector3.up);
            case DiceValueEnum.Five:
                return Quaternion.AngleAxis(90, Vector3.up);
            case DiceValueEnum.Six:
                return Quaternion.AngleAxis(-90, Vector3.right);
        }

        return Quaternion.identity;
    }

    private Quaternion GetRotationForFour(DiceValueEnum currentValue)
    {
        switch(currentValue)
        {
            case DiceValueEnum.One:
                return Quaternion.AngleAxis(-90, Vector3.right);
            case DiceValueEnum.Two:
                return Quaternion.AngleAxis(90, Vector3.up);
            case DiceValueEnum.Three:
                return Quaternion.AngleAxis(180, Vector3.up);
            case DiceValueEnum.Five:
                return Quaternion.AngleAxis(-90, Vector3.up);
            case DiceValueEnum.Six:
                return Quaternion.AngleAxis(90, Vector3.right);
        }

        return Quaternion.identity;
    }

    private Quaternion GetRotationForFive(DiceValueEnum currentValue)
    {
        switch(currentValue)
        {
            case DiceValueEnum.One:
                return Quaternion.AngleAxis(-90, Vector3.forward);
            case DiceValueEnum.Two:
                return Quaternion.AngleAxis(180, Vector3.up);
            case DiceValueEnum.Three:
                return Quaternion.AngleAxis(-90, Vector3.up);
            case DiceValueEnum.Four:
                return Quaternion.AngleAxis(90, Vector3.up);
            case DiceValueEnum.Six:
                return Quaternion.AngleAxis(90, Vector3.forward);
        }

        return Quaternion.identity;
    }

    private Quaternion GetRotationForSix(DiceValueEnum currentValue)
    {
        switch(currentValue)
        {
            case DiceValueEnum.One:
                return Quaternion.AngleAxis(180, Vector3.forward);
            case DiceValueEnum.Two:
                return Quaternion.AngleAxis(90, Vector3.forward);
            case DiceValueEnum.Three:
                return Quaternion.AngleAxis(90, Vector3.right);
            case DiceValueEnum.Four:
                return Quaternion.AngleAxis(-90, Vector3.right);
            case DiceValueEnum.Five:
                return Quaternion.AngleAxis(-90, Vector3.forward);
        }

        return Quaternion.identity;
    }
}
