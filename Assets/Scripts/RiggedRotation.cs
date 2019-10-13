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
        switch(desiredValue)
        {
            case DiceValueEnum.One:
            case DiceValueEnum.Six:
                return GetRotationForOneAndSix(GetValue(), desiredValue);
            case DiceValueEnum.Two:
            case DiceValueEnum.Five:
                return GetRotationForTwoAndFive(GetValue(), desiredValue);
            case DiceValueEnum.Three:
            case DiceValueEnum.Four:
                return GetRotationForThreeAndFour(GetValue(), desiredValue);
        }

        return Quaternion.identity;
    }

    private Quaternion GetRotationForOneAndSix(DiceValueEnum currentValue, DiceValueEnum desiredValue)
    {
        bool opposite = desiredValue == DiceValueEnum.Six;
        if( (currentValue == DiceValueEnum.One && !opposite) ||
            (currentValue == DiceValueEnum.Six && opposite))
        {
            return Quaternion.identity;
        }

        switch(currentValue)
        {
            case DiceValueEnum.Two:
                return Quaternion.AngleAxis((opposite) ? 90 : -90 , Vector3.forward);
            case DiceValueEnum.Three:
                return Quaternion.AngleAxis((opposite) ? 90 : -90, Vector3.right);
            case DiceValueEnum.Four:
                return Quaternion.AngleAxis((opposite) ? -90 : 90, Vector3.right);
            case DiceValueEnum.Five:
                return Quaternion.AngleAxis((opposite) ? -90 : 90, Vector3.forward);
            case DiceValueEnum.One:
            case DiceValueEnum.Six:
                return Quaternion.AngleAxis(180, Vector3.forward);
        }

        return Quaternion.identity;
    }

    private Quaternion GetRotationForTwoAndFive(DiceValueEnum currentValue, DiceValueEnum desiredValue)
    {
        bool opposite = desiredValue == DiceValueEnum.Five;
        if( (currentValue == DiceValueEnum.Two && !opposite) ||
            (currentValue == DiceValueEnum.Five && opposite))
        {
            return Quaternion.identity;
        }

        switch(currentValue)
        {
             case DiceValueEnum.One:
                return Quaternion.AngleAxis((opposite) ? -90 : 90, Vector3.forward);
            case DiceValueEnum.Three:
                return Quaternion.AngleAxis((opposite) ? -90 : 90, Vector3.up);
            case DiceValueEnum.Four:
                return Quaternion.AngleAxis((opposite) ? 90 : -90, Vector3.up);
            case DiceValueEnum.Two:
            case DiceValueEnum.Five:
                return Quaternion.AngleAxis(180, Vector3.up);
            case DiceValueEnum.Six:
                return Quaternion.AngleAxis((opposite) ? 90 : -90, Vector3.forward);
        }

        return Quaternion.identity;
    }

    private Quaternion GetRotationForThreeAndFour(DiceValueEnum currentValue, DiceValueEnum desiredValue)
    {
        bool opposite = desiredValue == DiceValueEnum.Four;
        if( (currentValue == DiceValueEnum.Three && !opposite) ||
            (currentValue == DiceValueEnum.Four && opposite))
        {
            return Quaternion.identity;
        }

        switch(currentValue)
        {
            case DiceValueEnum.One:
                return Quaternion.AngleAxis((opposite) ? -90 : 90, Vector3.right);
            case DiceValueEnum.Two:
                return Quaternion.AngleAxis((opposite) ? 90 : -90, Vector3.up);
            case DiceValueEnum.Three:
            case DiceValueEnum.Four:
                return Quaternion.AngleAxis(180, Vector3.up);
            case DiceValueEnum.Five:
                return Quaternion.AngleAxis((opposite) ? -90 : 90, Vector3.up);
            case DiceValueEnum.Six:
                return Quaternion.AngleAxis((opposite) ? 90 : -90, Vector3.right);
        }

        return Quaternion.identity;
    }
}
