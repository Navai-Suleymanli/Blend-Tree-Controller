# Movement Script

This is a script written in C# for the Unity game engine. It controls the movement of a character in the game based on user input and updates the corresponding animations. The script uses the Unity `Animator` component to handle the character's animations.

## Instructions

1. Attach the script to the game object that represents the character in your Unity scene.
2. Assign the required references to the script in the Unity Inspector:
   - `animator`: Drag and drop the Animator component from the character game object to this field.
   - `acceleration`: Set the acceleration value for the character's movement.
   - `deceleration`: Set the deceleration value for the character's movement.
   - `maxWalkVelocity`: Set the maximum velocity for walking.
   - `maxRunVelocity`: Set the maximum velocity for running.

## Functionality

The script handles the character's movement by updating the velocity values based on user input and applies acceleration and deceleration to create smooth movement transitions. It also adjusts the velocity values to control the maximum movement speed based on whether the character is walking or running.

The following keys are used for input:
- `W`: Move forward.
- `A`: Move left.
- `D`: Move right.
- `LeftShift`: Run.
- `S`: Move backward.
- `Mouse1`: Aim (optional).

The script calculates the character's velocity in the X-axis and Z-axis (horizontal and vertical directions, respectively) and updates the animator's parameters accordingly. The animator's parameters control the character's animations, such as walking, running, and idle.

## Script Overview

### Initialization

- `InitializeAnimator()`: Initializes the animator component and gets the hash codes for animation parameters.

### Update Loop

- `Update()`: Called every frame.
  - Reads the user input for movement and aiming.
  - Determines the maximum velocity based on the run key and aiming state.
  - Calls the following methods to update the velocity and animator parameters.

### Velocity Update

- `UpdateVelocity()`: Updates the X-axis and Z-axis velocities based on input and handles acceleration and deceleration.
- `LockOrResetVelocity()`: Locks or resets the velocities based on input to prevent unintended movement and enforces speed limits.

### Animator Update

- `UpdateAnimator()`: Updates the animator parameters with the current velocities.

## Dependencies

This script requires the Unity game engine and is written in C# for Unity's scripting API. It should be attached to a game object with an Animator component.

Please make sure you have the necessary Unity components and the script is properly set up before using it. Feel free to modify the script according to your specific requirements.
