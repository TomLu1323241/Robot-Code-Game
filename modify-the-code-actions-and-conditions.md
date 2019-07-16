---
description: Understanding how to modify the code to create new actions and conditions
---

# Modify the code \(Actions and Conditions\)

## Adding new actions and conditions

The code for this game is written to be modified without required too much bug fixing after. In the PlayerController.cs there are functions already written such as:

```csharp
    bool OnGround()...

    bool OnEdge()...

    bool InMidAir()...

    bool HitWall()...

    void Walk()...

    void Jump()...

    void Turn()..
```

To add a new condition simply create the function for it. It must return void if it is a action and must return bool if it is a condition. Go to the file ActionsAndConditions.cs and add an enum that matches the name of the function you just created into which ever enum group that make sense. Now we must add this to the CodeHandler function in PlayerController.cs. If you added a new condition got to the Condition switch case and add a new case with your new enum and change the bool ifRunning to equal your condition function. If you added an action there are two places where you need to add a new enum. Look for the two Action enum switch statements and add your new enum and function.

