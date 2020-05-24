﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    /// <summary>
    /// The associated character that is assigned to this character controller
    /// </summary>
    protected Character AssocoatedCharacter;

    #region monobehaviour methods
    protected virtual void Awake()
    {
        AssocoatedCharacter = GetComponent<Character>();
    }

    #endregion monobehaviour methods


    #region button command classes
    /// <summary>
    /// Generic class that will execute two
    /// </summary>
    protected abstract class ActionCommand
    {
        public virtual void ExecuteActionPress(Character AssociatedCharacter) { }
        public virtual void ExecuteActionReleased(Character AssociateCharacter) { }
    }

    protected class CommandJump : ActionCommand
    {
        public override void ExecuteActionPress(Character AssociatedCharacter)
        {
            AssociatedCharacter.CharacterMovement.Jump();
        }

        public override void ExecuteActionReleased(Character AssociateCharacter)
        {
            AssociateCharacter.CharacterMovement.JumpReleased();
        }
    }
    #endregion button command classes

    #region axis command classes
    /// <summary>
    /// Command class to execute functions that use an axis value.
    /// </summary>
    protected abstract class AxisCommand
    {
        public virtual void ExecuteAxisAction(Character AssociatedCharacter, float RawAxisValue) { }
        
    }

    /// <summary>
    /// 
    /// </summary>
    protected class AxisHorizontalMovement : AxisCommand
    {
        public override void ExecuteAxisAction(Character AssociatedCharacter, float RawAxisValue)
        {
            AssociatedCharacter.CharacterMovement.ApplyHorizontalInput(RawAxisValue);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    protected class AxisVerticalMovement : AxisCommand
    {
        public override void ExecuteAxisAction(Character AssociatedCharacter, float RawAxisValue)
        {
            AssociatedCharacter.CharacterMovement.ApplyVerticalInput(RawAxisValue);
        }
    }
    #endregion axis command classes
}
