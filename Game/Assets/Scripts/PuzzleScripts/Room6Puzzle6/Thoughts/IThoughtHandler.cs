using System.Collections;

/// <summary>
/// Defines methods and property to handle thought mechanics
/// </summary>
public interface IThoughtHandler
{
	/// <summary>
	/// Collection of thoughts
	/// </summary>
	ICollection Thoughts { get; set; }

	/// <summary>
	/// Reveals the thought to the player
	/// </summary>
	void ExecuteThought();

	/// <summary>
	/// Gets the interface, through which the thought will be presented to 
	/// the player
	/// </summary>
	void GetThoughtInterface();
}