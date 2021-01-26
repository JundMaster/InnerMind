using UnityEngine;
using System;

/// <summary>
/// Interface for player inputs.
/// </summary>
public interface IPlayerInput
{
    /// <summary>
    /// Property for player current control.
    /// </summary>
    TypeOfControl CurrentControl { get;}

    /// <summary>
    /// Property for ZAxis.
    /// </summary>
    float ZAxis { get; }

    /// <summary>
    /// Property for XAxis.
    /// </summary>
    float XAxis { get; }

    /// <summary>
    /// Propert for horizontal mouse speed.
    /// </summary>
    float HorizontalMouse { get; }

    /// <summary>
    /// Propert for vertical mouse speed.
    /// </summary>
    float VerticalMouse { get; }

    /// <summary>
    /// Propert for left click.
    /// </summary>
    bool LeftClick { get; }

    /// <summary>
    /// Propert for left click.
    /// </summary>
    bool RightClick { get; }

    /// <summary>
    /// Property for right click.
    /// </summary>
    bool Inventory { get; }

    /// <summary>
    /// Property for pause input.
    /// </summary>
    bool Pause { get; }

    /// <summary>
    /// Property to get key to pass a scene.
    /// </summary>
    bool Space { get; }

    /// <summary>
    /// Property for cursor position relative to cursor in game.
    /// </summary>
    Vector2Int CursorPosition { get; }

    /// <summary>
    /// Property for mouse speed.
    /// </summary>
    float MouseSpeed { get; }

    /// <summary>
    /// Method to change type of control.
    /// </summary>
    /// <param name="control">TypeOfControl to change to</param>
    void ChangeTypeOfControl(TypeOfControl control);

    /// <summary>
    /// Event that happens when control is changed
    /// </summary>
    event Action ChangeControl;
}
