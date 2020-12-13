using UnityEngine;

/// <summary>
/// Class for Map Behaviour. Implements IUsable
/// </summary>
public class MapBehaviour : MonoBehaviour, IUsable
{
    /// <summary>
    /// Determins MapBehaviour action when used.
    /// If the player uses the map, the map is set to active.
    /// </summary>
    public void Use()
    {
        GameObject mapGO = GameObject.FindGameObjectWithTag("MapCanvas");

        Canvas map = null;

        if (mapGO != null)
            map = mapGO.GetComponent<Canvas>();

        if (mapGO != null)
        {
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
}
