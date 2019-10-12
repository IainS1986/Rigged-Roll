# Rigged-Roll
Rigged Physics Dice Roller

Messing around with Unity3D again after many years. Trying out a small project ot write a determinstic dice roller using non-deterministic physics.

All insipired and based off the reddit post (https://www.reddit.com/r/gamedev/comments/bq6c5d/deterministic_physics_dice_rolls_in_a_non/) by /u/TickTakashi

Basic idea being...

* The following occurs instantly in a single frame...
  * Roll 'blank' 3D dice
  * Record position + rotation at every frame of the roll
  * Wait for them to settle
  * Determine what rotation is needed to achieve the desired result
  * Reset dice to their original state

* Replay all the saved position + rotation state
* After each step, apply the rotation required to get the desired result


# Demos

Here's a GIF demo showing the system in action. The Frame Rate is poor due to the gif screen capture, in reality it runs smoothly.

The demo is setting 5 dice to roll all 6's

![All Sixes](https://github.com/IainS1986/Rigged-Roll/blob/master/GIFs/test.gif)

![All Sixes](https://github.com/IainS1986/Rigged-Roll/blob/master/GIFs/Demo.gif)