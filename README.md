# vg-vrt-mixedreality
Virtual Reality Video Game for Vestibular Rehabilitation Therapy (Mixed Reality Edition)

## Requirements

* Unity Version: 2022.3.3F1
* Windows 10 or higher
* Meta Quest 3

## Packages Used

* [Meta XR All-in-One SDK](https://assetstore.unity.com/packages/tools/integration/meta-xr-all-in-one-sdk-269657) (Version 60.0.0)

## Project Structure

* Assets/Scenes/
    * GameScene.unity -- Primary scene containing the entire game.
    * GazeFollowDemo.unity -- Demo for making UI elements follow the user's gaze.
    * CylinderRaycastDemo.unity -- Demo for showing how to track user gaze on a cylinder.
* Assets/Scripts/
    * Menu/ -- Contains scripts for managing the game's menus.
        * BeginningOfGame -- Logic for beginning recalibration screen and quitting the app.
            * Component of Title Screen.
        * CloseCurrentOpenNext -- Used throughout the game for disabling current objects and enabling others in sequence.
            * Component of About Screen, Firefly Difficulty Menu, Rock Throw Difficulty Menu.
        * HandMenu -- Hand Menu/Watch interactions.
            * Component of LeftHandAnchor.
        * MenuMaintenance -- Handles the countdown before recalibration begins, and then recalibrates the user's root position to where user is currently located.
            * Component of Root.
        * TagAlong -- Makes an object follow the user's gaze, with a specified delay, offset, and deadzone.
            * Component of Title Screen, About Screen, Recalibration Screen, Results.
        * WatchColorChange -- Changes an object's color when pressed for a better UX. 
            * Component of Watch, which is a child of LeftHandAnchor.
    * ExerciseManager/ -- Contains common functionality across exercises, such as tracking data and starting exercises.
        * Exercise -- General logic that every exercise should use to define reps and log events.
            * Component of Sphere, Cube.
        * ExerciseManager -- Used to start specific exercises based on index in an array, with each index representing a specific exercise. Additional exercises may be added to the exercises array through the Unity Editor.
            * Component of Root.
        * Itracker -- Abstract class for getting current user gaze and current object position for current and future exercises.
        * LogToCSV -- Logs user gaze and object position data in the scene to a new CSV file each time an exercise is performed.
            * Component of Logger.
        * PerformanceTracker -- Script that takes distance between user gaze and object position each frame and adds to total distance, then divides by number of update calls (TotalDistance/updateCounter) to determine a letter grade between an A and F (standard grading scale).
            * Component of Sphere, Cube.
        * Winscreen -- Displays win screen for a fixed amount of time before disappearing.
            * Component of Results.
    * Firefly/ -- Contains functionality specific to the "firefly" exercise.
        * SemicircleMovement -- Movement functionality for the "firefly" exercise.
            * Component of Sphere.
        * TrackObjectSphere -- Draws an invisible ray based on the user's gaze that, when intersecting with the sphere, turns the sphere's color from red to green.
            * Component of Sphere.
        * TrackOnCylinder -- Tracks the user's gaze and an optional object's position on a cylinder. Ensure that the object's movement is aligned with the cylinder.
            * Component of Cylinder.
    * RockThrow/ -- Contains functionality specific to the "rock throw" exercise.
        * ArchMovement -- Movement functionality for the "rock throw" exercise.
            * Component of Cube.
        * TrackObject --  Draws an invisible ray based on the user's gaze that, when intersecting with the cube, turns the cube's color from red to green.
            * Component of Cube.
        * TrackOnPlane -- Tracks a user's gaze on a plane. Ensure plane being used has 'plane' layer mask.
            * Component of Cube.

## How to Create a New Exercise

TODO Describe the steps needed to create a new exercise: describe the GameObjects in the Scene that need to be modified (like the wrist menu, adding a new button, element in the array, etc.). Also describe what classes need to be created for a new exercise, what needs to be inherited, methods that need to be modified, etc.

In order to create a new exercise follow these step by step instructions.

1. Create an Exercise GameObject:
    * Create an Exercise GameObject as a child of the Root. 
    * Depending on how the exercise is implemented, it is recommended to have a Beginning Point GameObject as a child of the Exercise GameObject so that the object spawns in a specific place and the exercise's functionality can begin. Any object involving user interaction would be a child of the Beginning Point, and all other assets or GameObjects pertaining to the exercise would be a child of the Exercise. 
        * Each Beginning Point GameObject should have: 
            * A 'TextMeshPro' GameObject that displays the number of reps that have been done, and 
            * An object such as a sphere or cube that serves as the object that the user will track.
        * Each Exercise GameObject should have: 
            * An Explanation Screen GameObject that describes the exercise to the user, 
            * A Difficulty Menu GameObject that allows the user to increase the difficulty of the game to their liking, and 
            * A GameObject that is used to measure where the current user gaze and current object position. 
                * For example, Firefly uses a cylinder so that the object's X and Z coordinates can be tracked, and Rock Throw uses a plane so that the object's X and Y coordinates can be tracked.

2. Add Components to the Exercise:
    * Add the following scripts to the trackable object: Exercise, PerformanceTracker, a script to change the object's color based on user gaze (ex. TrackObject, TrackObjectSphere), a script to track the object position and user gaze against a flat surface for logging purposes (ex. TrackOnPlane, TrackOnCylinder), and a movement script (ex. ArchMovement, SemicircleMovement). 
        * Either use pre-existing scripts or create new scripts to fit the desired exercise implementation.
        * The second tracking script may instead be made a component of the aforementioned flat surface, such as the Cylinder used for Firefly.
    * Add the CloseCurrentOpenNext script as a component of the exercise's Difficulty Menu, and then add the Difficulty Menu to the "Current" array and adding the Explanation Screen and Beginning Point to the "Next" array. 
        * This makes it so that once the user chooses a difficulty, the Difficulty Menu closes and the Explanation Screen and the Beginning Point with the exercise's object both appear.
    * Add the Exercise GameObject to the exercise array of the Root's ExerciseManager component.

3. Alter HandMenu Script: 
    * Add the new exercise's Difficulty Menu GameObject as an attribute in the HandMenu script so that it can be called upon later on. 
    * Add a function to start the new exercise, similar to the other start-exercise functions in this script. 
    * Change the exercise name and index to match the new exercise's name and index that was referenced in the call to Exercise Manager that was created earlier. 
    * Add a reference to the new exercise's Difficulty Menu on the HandMenu script, which can be found as a component of the Left Hand Anchor.

4. Integrate Exercise into Hand Menu: 
    * Create a new PokeButton GameObject as a child of the Hand Menu GameObject.
    * Change the Pointable Unity Event Wrapper 'When Select' function so that it calls the new exercise's start function in the HandMenu script, which is tied to the Left Hand Anchor GameObject.

5. Test! 
    * In following these steps, an exercise has been created and implemented into this game. Now it's time to make sure it all works properly. 

