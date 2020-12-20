using UnityEngine;

/// <summary>
/// Class with every items in game. Used to compare items
/// </summary>
public class ItemComparer : MonoBehaviour
{
    [SerializeField] private ScriptableItem audioTape;
    [SerializeField] private ScriptableItem cabinetKey;
    [SerializeField] private ScriptableItem flashLight;
    [SerializeField] private ScriptableItem map;
    [SerializeField] private ScriptableItem noBatteryFlashlight;
    [SerializeField] private ScriptableItem noBatteryWalkman;
    [SerializeField] private ScriptableItem notRewoundAudioTape;
    [SerializeField] private ScriptableItem oldBattery;
    [SerializeField] private ScriptableItem pen;
    [SerializeField] private ScriptableItem pianoKey1;
    [SerializeField] private ScriptableItem pianoKey2;
    [SerializeField] private ScriptableItem pianoKey3;
    [SerializeField] private ScriptableItem pillBottle;
    [SerializeField] private ScriptableItem walkmanBatteries;
    [SerializeField] private ScriptableItem walkmanWithoutTape;
    [SerializeField] private ScriptableItem walkman;

    private ScriptableItem[] possibleItems;

    public ScriptableItem AudioTape { get => audioTape; }
    public ScriptableItem CabinetKey { get => cabinetKey; }
    public ScriptableItem FlashLight { get => flashLight; }
    public ScriptableItem Map { get => map; }
    public ScriptableItem NoBatteryFlashlight { get => noBatteryFlashlight; }
    public ScriptableItem NoBatteryWalkman { get => noBatteryWalkman; }
    public ScriptableItem NotRewoundAudioTape { get => notRewoundAudioTape; }
    public ScriptableItem OldBattery { get => oldBattery; }
    public ScriptableItem Pen { get => pen; }
    public ScriptableItem PianoKey1 { get => pianoKey1; }
    public ScriptableItem PianoKey2 { get => pianoKey2; }
    public ScriptableItem PianoKey3 { get => pianoKey3; }
    public ScriptableItem PillBottle { get => pillBottle; }
    public ScriptableItem WalkmanBatteries { get => walkmanBatteries; }
    public ScriptableItem WalkmanWithoutTape { get => walkmanWithoutTape; }
    public ScriptableItem Walkman { get => walkman; }

    /// <summary>
    /// List with every item
    /// </summary>
    public ScriptableItem[] PossibleItems { get => possibleItems; }

    /// <summary>
    /// Awake method for ItemComparer
    /// </summary>
    private void Awake()
    {
        possibleItems = new ScriptableItem[]
        {
            audioTape, cabinetKey, flashLight, map, noBatteryFlashlight,
            noBatteryWalkman, notRewoundAudioTape, oldBattery, pen,
            pianoKey1, pianoKey2, pianoKey3, pillBottle, walkmanBatteries,
            walkmanWithoutTape, walkman
        };
    }
}
