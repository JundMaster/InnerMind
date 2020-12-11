using UnityEngine;

/// <summary>
/// Class used to get the order of the played keys by the player
/// </summary>
public class GetPlayOrder : MonoBehaviour
{
    //Components
    private Room4 room4;

    //Inspector Variables
    [SerializeField] private InteractionPianoKey[] pianoKeys;

    /// <summary>
    /// Property used to save the played keys order
    /// </summary>
    public CustomVector3 PianoPlayerInput { get; set; }

    //Variable used to count the times the player plays a key
    private int playCounter;


    /// <summary>
    /// Start method of GetPlayOrder
    /// </summary>
    void Start()
    {
        playCounter = 0;
        PianoPlayerInput = new CustomVector3(0, 0, 0);
        room4 = FindObjectOfType<Room4>();
        

    }

   
    /// <summary>
    /// OnEnable method of GetPlayOrder
    /// </summary>
    private void OnEnable()
    {
       
        for (int i = 0; i < pianoKeys.Length; i++)
        {
            pianoKeys[i].KeyID += GetPianoKeyPressed;
        }
    }

    /// <summary>
    /// OnDisable method of GetPlayOrder
    /// </summary>
    private void OnDisable()
    {
        for (int i = 0; i < pianoKeys.Length; i++)
        {
            pianoKeys[i].KeyID -= GetPianoKeyPressed;
        }
    }

    /// <summary>
    /// Changes the Player's Input based on the playCounter and PianoKeyID
    /// </summary>
    /// <param name="id">The id of the played piano key</param>
    private void GetPianoKeyPressed(PianoKeyID id)
    {
        //Checks if the player has finished the puzzle
        if (room4.FinishedPuzzle == false)
        {
            //If its 1st time playing a key it changes the 1st input value
            if (playCounter == 0)

                //Changes the input value based on the key's Id and 
                //increments the counter
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

            //If its 2nd time playing a key it changes the 2nd input value
            else if (playCounter == 1)

                //Changes the input value based on the key's Id and 
                //increments the counter
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

            //If its 3rd time playing a key it changes the 3rd input value
            //resets the play counter and player's input 
            //and checks if the player solved the puzzle
            else if (playCounter == 2)
            {
                //Changes the input value based on the key's Id and 
                //increments the counter
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

