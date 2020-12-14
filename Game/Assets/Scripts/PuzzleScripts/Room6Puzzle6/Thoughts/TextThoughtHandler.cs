using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for handling the text thought displaying
/// </summary>
public class TextThoughtHandler : ThoughtHandler
{
    /// <summary>
    /// Displays the text thought
    /// </summary>
    /// <param name="thoughtIndex">Index of the thought</param>
    public override void ExecuteThought(int thoughtIndex)
    {
        RevealTextThought(thoughtIndex);
    }
}
