using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoller : MonoBehaviour
{
     /// <summary>
    /// Global GUI border size
    /// </summary>
    public static int Border = 10;

    /// <summary>
    /// Global GUI Button width
    /// </summary>
    public static int Width = 150;

    /// <summary>
    /// Global GUI Button height
    /// </summary>
    public static int Height = 20;

    [SerializeField]
    private Rigidbody[] _dice;

    void OnGUI()
    {
        Rect boundary = GetWidgetBoundary(1);
        GUI.Box(boundary, "Rigged Roll");

        int index = 1;
        AddButton(boundary, index++, "Roll", () => Roll());
    }

    private void Roll()
    {
        foreach(var dice in _dice)
        {
            // Add Some throw

            // Randomise the Force
            var thrust = UnityEngine.Random.Range(24, 40);

            // Randomise the Force direction (Still want it to be "up" but allow it to be off slightly)
            var forceRange = 10;
            var direction = transform.up;
            var minorXRotation = UnityEngine.Random.Range(-forceRange, forceRange);
            var minorYRotation = UnityEngine.Random.Range(-forceRange, forceRange);
            var minorZRotation = UnityEngine.Random.Range(-forceRange, forceRange);

            direction = Quaternion.Euler(minorXRotation, minorYRotation, minorZRotation) * direction;

            // Roll
            dice.AddForce(direction * thrust, ForceMode.Impulse);

            // Add Some spin

            // Randomise spin force
            var spinForce = UnityEngine.Random.Range(8, 16);

            // Randomise spin direction
            var spinVector = UnityEngine.Random.onUnitSphere;

            // Spin
            dice.AddTorque(spinVector * spinForce, ForceMode.Impulse);
        }
    }

    public static Rect GetWidgetBoundary(int num_buttons)
    {
        return new Rect(Border / 2, Border / 2, Width + Border, num_buttons * (Border + Height) + Height +(Border / 2));
    }

    // TODO MOVE THIS TO HELPER CLASS
    /// <summary>
    /// Renders a Button in an OnGUI and triggers the Action passed in
    /// when pressed.
    /// </summary>
    /// <param name="boundary">The boundary the button is inside</param>
    /// <param name="i">The index of the button within the list inside the boundary</param>
    /// <param name="s">The string displayed on the Button</param>
    /// <param name="action">The action triggered when the button is pressed</param>
    public static void AddButton(Rect boundary, int i, string s, Action action)
    {
        Rect r = new Rect(Border, boundary.y + (Border * i + Height * i), Width, Height);
        if (GUI.Button(r, s))
        {
            if (action != null)
                action();
        }
    }
}
