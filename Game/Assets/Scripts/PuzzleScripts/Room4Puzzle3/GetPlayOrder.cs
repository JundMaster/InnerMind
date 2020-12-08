using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GetPlayOrder : MonoBehaviour
{
    private Room4 room4;
    [SerializeField] private InteractionPianoKey[] pianoKeys;
    public CustomVector3 PianoPlayerInput { get; set; }
    private int playCounter;

    void Start()
    {
        playCounter = 0;
        PianoPlayerInput = new CustomVector3(0, 0, 0);
        room4 = FindObjectOfType<Room4>();
    }

    private void OnEnable()
    {
        for (int i = 0; i < pianoKeys.Length; i++)
        {
            pianoKeys[i].KeyID += GetPianoKeyPressed;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < pianoKeys.Length; i++)
        {
            pianoKeys[i].KeyID -= GetPianoKeyPressed;
        }
    }

    private void GetPianoKeyPressed(PianoKeyID id)
    {
        if (room4.FinishedPuzzle == false)
        {
            if (playCounter == 0)
                switch (id)
                {
                    case PianoKeyID.Key1:
                        playCounter++;
                        PianoPlayerInput += new CustomVector3(1,0,0);
                        break;
                    case PianoKeyID.Key2:
                        playCounter++;
                        PianoPlayerInput += new CustomVector3(2, 0, 0);
                        break;
                    case PianoKeyID.Key3:
                        playCounter++;
                        PianoPlayerInput += new CustomVector3(3, 0, 0);
                        break;
                }
            else if (playCounter == 1)
                switch (id)
                {
                    case PianoKeyID.Key1:
                        playCounter++;
                        PianoPlayerInput += new CustomVector3(0, 1, 0);
                        break;
                    case PianoKeyID.Key2:
                        playCounter++;
                        PianoPlayerInput += new CustomVector3(0, 2, 0);
                        break;
                    case PianoKeyID.Key3:
                        playCounter++;
                        PianoPlayerInput += new CustomVector3(0, 3, 0);
                        break;
                }
            else if (playCounter == 2)
            {
                switch (id)
                {
                    case PianoKeyID.Key1:
                        playCounter++;
                        PianoPlayerInput += new CustomVector3(0, 0, 1);
                        break;
                    case PianoKeyID.Key2:
                        playCounter++;
                        PianoPlayerInput += new CustomVector3(0, 0, 2);
                        break;
                    case PianoKeyID.Key3:
                        playCounter++;
                        PianoPlayerInput += new CustomVector3(0, 0, 3);
                        break;
                }
                playCounter = 0;
                room4.VictoryCheck();
                PianoPlayerInput = new CustomVector3(0, 0, 0);
            }
        }
    }
}

