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
    private List<Rigidbody> _dice;

    [SerializeField]
    private GameObject _diceSpawner;

    [SerializeField]
    private GameObject _dicePrefab;

    private List<RiggedDice> _riggedDice;

    private bool _isRolling;

    private bool _replaying;

    void Start()
    {
        Setup();
    }

    void FixedUpdate()
    {
        if (_replaying)
        {
            //Step all the dice
            bool diceDone = true;
            foreach(var dice in _riggedDice)
            {
                if (dice.HasPhysicStepToPlay())
                {
                    diceDone = false;
                    dice.PhysicsStep();
                }
            }

            //When all dice "done", finish
            if (diceDone)
            {
                _replaying = false;
                Physics.autoSimulation = true;
                _isRolling = false;

                 // Clear Physics + Colliders
                foreach(var dice in _dice)
                {
                    dice.isKinematic = false;
                    dice.detectCollisions = true;
                }
            }
        }
    }

    private void Setup()
    {
        _riggedDice = new List<RiggedDice>();

        for(int i=0; i<_dice.Count; i++)
        {
            _riggedDice.Add(_dice[i].GetComponent<RiggedDice>());
        }
    }

    private void Roll()
    {
        if (_isRolling)
        {
            return;
        }

        _isRolling = true;

        // Reset dice state
        foreach(var dice in _riggedDice)
        {
            dice.Reset();
        }

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

        FastForward();
    }

    private void FastForward()
    {
        Physics.autoSimulation = false;

        // Fast forward physics and record dice positions + rotations at each step
        bool fastfowarding = true;
        while (fastfowarding)
        {
            fastfowarding = false;

            // Step physics
            Physics.Simulate(Time.fixedDeltaTime);

            // Record Rotation and Position of each dice
            foreach(var dice in _riggedDice)
            {
                dice.RecordStep();
            }

            // Check if all dice are "settled", if so stop
            foreach(var dice in _riggedDice)
            {
                fastfowarding |= dice.IsRolling();
            }
        }

        // Clear Physics + Colliders
        foreach(var dice in _dice)
        {
            dice.isKinematic = true;
            dice.detectCollisions = false;
        }

        // Calculate the rotation needed to get the dice to be all 6s
        foreach(var dice in _riggedDice)
        {
            dice.RotationOffest = dice.GetComponent<RiggedRotation>().GetRotationForValue(dice.DesiredRoll);
        }

        // Replay
        _replaying = true;
    }

    void OnGUI()
    {
        Rect boundary = GetWidgetBoundary(4 + _riggedDice.Count);
        GUI.Box(boundary, "Rigged Roll");

        int index = 1;
        AddButton(boundary, index++, "Roll", () => Roll());
        AddButton(boundary, index++, "Add Dice", () => AddDice());
        AddButton(boundary, index++, "Remove Dice", () => RemoveDice());

        index++;
        for(int i=0; i<_riggedDice.Count; i++)
        {
            AddButton(boundary, index++, "Dice " + (i + 1) + " - " + _riggedDice[i].DesiredRoll, () => AlterDiceValue(i));
        }
    }

    public void AddDice()
    {
        if (_dice?.Count < 16)
        {
            var dice = Instantiate(_dicePrefab, 
                                    _diceSpawner.transform.position + UnityEngine.Random.onUnitSphere * UnityEngine.Random.Range(0, 10),
                                    UnityEngine.Random.rotationUniform);

            _dice.Add(dice.GetComponent<Rigidbody>());

            Setup();
        }
    }

    public void RemoveDice()
    {
        if (_dice?.Count > 0)
        {
            Destroy(_dice[_dice.Count - 1].gameObject);

            _dice.RemoveAt(_dice.Count - 1);

            Setup();
        }
    }

    public void AlterDiceValue(int index)
    {
        int currentDesiredRoll = (int)_riggedDice[index].DesiredRoll;
        if (currentDesiredRoll == 5)
        {
            currentDesiredRoll = 0;
        }
        else
        {
            currentDesiredRoll++;
        }

        _riggedDice[index].DesiredRoll = (DiceValueEnum)currentDesiredRoll;
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
