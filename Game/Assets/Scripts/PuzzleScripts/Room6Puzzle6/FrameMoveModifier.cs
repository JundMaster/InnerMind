using System;
/// <summary>
/// Establishes a connection between a frame in the order in which it should
/// be moved when a chain reaction movement occurs, that causes it
/// to be moved too.
/// </summary>
[Serializable]
public struct FrameMoveModifier
{
	public Frame frame;
	public int moveOrder;
}