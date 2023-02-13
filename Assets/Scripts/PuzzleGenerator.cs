using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGenerator : MonoBehaviour
{
    private LevelGeneration levelGeneration;

    public GameObject[] RoomPuzzleTypeArr;
    
    int abilityPuzzleDiff = 1; // ability-base puzzles' difficulty level
    int skillPuzzleDiff = 0; // ability-base puzzles' difficulty level

    public List<GameObject> GeneratedPuzzleList; // store the generated puzzles

    // Start is called before the first frame update
    void Start()
    {
        levelGeneration = GameObject.FindGameObjectWithTag("LevelGenerator").GetComponent<LevelGeneration>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RoomPuzzleTypeGeneration()
    {
        for (int room = 0; room < levelGeneration.spawnedRoomsList.Count-1; room++)
        {
            if(levelGeneration.directionList.Contains ("Right") || levelGeneration.directionList.Contains("Left")) // Horizontal challenge
            {
                if (room == 1)
                {
                    //Place a H-AP-D1 in the first room
                }

                // change this


                for (room = 1; room < levelGeneration.spawnedRoomsList.Count-1; room++)
                {
                    // randomly select between abilityPuzzle or skillPuzzle with horizontal challenge FOR EVERY ROOM
                    //Store whatever room you choose in the GeneratedPuzzleList
                    
                    //Increase the difficulty of each category if:
                    if (abilityPuzzleDiff > skillPuzzleDiff)
                    {
                        skillPuzzleDiff++;
                    }
                    else if (skillPuzzleDiff > abilityPuzzleDiff)
                    {
                        abilityPuzzleDiff++;
                    }
                    else
                    {
                        // randomly select between abilityPuzzle or skillPuzzle with horizontal challenge FOR EVERY ROOM
                    }
                }
                   
               
                if (room == levelGeneration.spawnedRoomsList.Count-1)
                {
                    //Place a skillPuzzleDiff == 4 in the final room
                }
                

            }
            /////////////////////////////////////////////
            /////////////////////////////////////////////
            else if (levelGeneration.directionList.Contains("Up") || levelGeneration.directionList.Contains("Down")) // Vertical challenge
            {
                if (room == 1)
                {
                    //Place an abilityPuzzleDiff == 1 in the first room
                }
                
                for (room = 1; room < levelGeneration.spawnedRoomsList.Count-1; room++)
                {
                    // randomly select between abilityPuzzle or skillPuzzle with horizontal challenge FOR EVERY ROOM
                    //Store whatever room you choose in the GeneratedPuzzleList

                    //Increase the difficulty of each category if:
                    if (abilityPuzzleDiff > skillPuzzleDiff)
                    {
                        skillPuzzleDiff++;
                    }
                    else if (skillPuzzleDiff > abilityPuzzleDiff)
                    {
                        abilityPuzzleDiff++;
                    }
                    else
                    {
                        // randomly select between abilityPuzzle or skillPuzzle with vertical challenge FOR EVERY ROOM
                    }
                }
                

                if (room == levelGeneration.spawnedRoomsList.Count)
                {
                    //Place a skillPuzzleDiff == 4 in the final room
                }
            
            }
        }
        
        
        
        
    }
}
