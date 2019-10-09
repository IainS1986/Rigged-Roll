# Rigged-Roll
Rigged Physics Dice Roller

Messing around with Unity3D again after many years. Trying out a small project ot write a determinstic dice roller using non-deterministic physics.

All insipired and based off the reddit post (https://www.reddit.com/r/gamedev/comments/bq6c5d/deterministic_physics_dice_rolls_in_a_non/) by /u/TickTakashi

Basic idea being...

* The following occurs instantly in a single frame...
  * Roll 'blank' 3D dice
  * Record position + rotation at every frame of the roll
  * Wait for them to settle
  * Assign UV coords/dice faces based on the desired dice rolle (i.e. all 6's)
  * Reset dice to their original state

* Replay all the saved position + rotation state
