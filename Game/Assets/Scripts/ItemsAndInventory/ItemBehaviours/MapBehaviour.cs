using UnityEngine;

/// <summary>
/// Class for Map Behaviour. Implements IUsable
/// </summary>
public class MapBehaviour : MonoBehaviour, IUsable
{
    private Canvas map;

    /// <summary>
    /// Determins MapBehaviour action when used.
    /// If the player uses the map, the map is set to active.
    /// </summary>
    public void Use()
    {
        map = GameObject.FindGameObjectWithTag("MapCanvas").
            GetComponent<Canvas>();

        if (map.enabled)
        {
            map.enabled = false;
        }
        else
        {
            map.enabled = true;
        }
    }
}
