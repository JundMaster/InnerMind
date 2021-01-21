using UnityEngine;

/// <summary>
/// Class with every items in game. Used to compare items.
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

    /// <summary>
    /// Gets audio tape.
    /// </summary>
    public ScriptableItem AudioTape { get => audioTape; }
    /// <summary>
    /// Gets cabinet key.
    /// </summary>
    public ScriptableItem CabinetKey { get => cabinetKey; }
    /// <summary>
    /// Gets flash light.
    /// </summary>
    public ScriptableItem FlashLight { get => flashLight; }
    /// <summary>
    /// Gets map.
    /// </summary>
    public ScriptableItem Map { get => map; }
    /// <summary>
    /// Gets no battery flash light.
    /// </summary>
    public ScriptableItem NoBatteryFlashlight { get => noBatteryFlashlight; }
    /// <summary>
    /// Gets no battery walkman.
    /// </summary>
    public ScriptableItem NoBatteryWalkman { get => noBatteryWalkman; }
    /// <summary>
    /// Gets not rewound audio tape.
    /// </summary>
    public ScriptableItem NotRewoundAudioTape { get => notRewoundAudioTape; }
    /// <summary>
    /// Gets old battery.
    /// </summary>
    public ScriptableItem OldBattery { get => oldBattery; }
    /// <summary>
    /// Gets pen
    /// </summary>
    public ScriptableItem Pen { get => pen; }
    /// <summary>
    /// Gets piano key 1.
    /// </summary>
    public ScriptableItem PianoKey1 { get => pianoKey1; }
    /// <summary>
    /// Gets piano key 2.
    /// </summary>
    public ScriptableItem PianoKey2 { get => pianoKey2; }
    /// <summary>
    /// Gets piano key 3.
    /// </summary>
    public ScriptableItem PianoKey3 { get => pianoKey3; }
    /// <summary>
    /// Gets pill bottle.
    /// </summary>
    public ScriptableItem PillBottle { get => pillBottle; }
    /// <summary>
    /// Gets walkman batteries.
    /// </summary>
    public ScriptableItem WalkmanBatteries { get => walkmanBatteries; }
    /// <summary>
    /// Gets walkman without tape.
    /// </summary>
    public ScriptableItem WalkmanWithoutTape { get => walkmanWithoutTape; }
    /// <summary>
    /// Gets walkman.
    /// </summary>
    public ScriptableItem Walkman { get => walkman; }

    /// <summary>
    /// List with every item.
    /// </summary>
    public ScriptableItem[] PossibleItems { get => possibleItems; }

    /// <summary>
    /// Awake method for ItemComparer.
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
