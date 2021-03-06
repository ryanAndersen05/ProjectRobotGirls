﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterAnimation))]

/// <summary>
/// This is the most generic form of character that will be found in our game. Every
/// NPC and playable character should derive from this class
/// </summary>
public class Character : MonoBehaviour
{

    #region main variables
    [Tooltip("The name associated with the character. This is primarily for any dialogue conversations that we may encounter, but it can also be used to to keep track of types of enemies we kill for stats")]
    public string CharacterName = "No Name";
    [Tooltip("The maximum amount of helath this character can have")]
    public float CharacterMaxHealth = 100;
    [Tooltip("This is the container object that will contain our hitboxes as well as sprite components that are attached to our character. This should always be a direct child to the root transform")]
    public Transform CharacterContainerTransform;
    /// <summary>
    /// The current health of our character. This can only be set through method in this class
    /// </summary>
    public float CharacterCurrentHealth { get; private set; }
    
    public SpriteRenderer CharacterSpriteRenderer { get; private set; }

    public CharacterAnimation CharacterAnimationComponent { get; private set; }

    #endregion main varialbes

    #region delegates
    public UnityAction Delegate_HealthUpdated;
    #endregion delegates

    #region monobehaivour methods
    protected virtual void Awake()
    {
        CharacterCurrentHealth = CharacterCurrentHealth;
        CharacterAnimationComponent = GetComponent<CharacterAnimation>();
        CharacterSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (CharacterContainerTransform == null)
        {
            Debug.LogWarning("The character container is null. Please be sure to attach a container transform to the Character component");
        }
    }

    protected virtual void OnValidate()
    {
        if (CharacterAnimationComponent == null) CharacterAnimationComponent = GetComponent<CharacterAnimation>();
    }
    #endregion monobehaviour methods

    #region character health methods
    /// <summary>
    /// This will set the health of the character without applying damage to our character.
    /// If health is set to less than 0 Death will not be called.
    /// </summary>
    public virtual void SetCharacterHealth(float HealthToSet)
    {

        CharacterCurrentHealth = HealthToSet;
        Delegate_HealthUpdated?.Invoke();
    }

    /// <summary>
    /// Use this method to appropriately apply damage to our character
    /// </summary>
    /// <param name="damageTaken"></param>
    /// <param name="characterThatGaveDamage"></param>
    public virtual void CharacterTakeDamage(float damageTaken, Character characterThatGaveDamage = null)
    {
        CharacterCurrentHealth -= damageTaken;
        if (CharacterCurrentHealth <= 0)
        {
            OnCharacterDead();
        }
        Delegate_HealthUpdated?.Invoke();
    }

    public virtual void CharacterAddHealth(float healthPointsToAdd)
    {
        CharacterCurrentHealth += healthPointsToAdd;
        if (CharacterCurrentHealth > CharacterMaxHealth)
        {
            CharacterCurrentHealth = CharacterMaxHealth;
        }
        Delegate_HealthUpdated?.Invoke();
    }

    /// <summary>
    /// This method will be called whenever a player's health falls at or below 0.
    /// </summary>
    public virtual void OnCharacterDead()
    {
        Destroy(this.gameObject);
    }
    #endregion character health methods

}
