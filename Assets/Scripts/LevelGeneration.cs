using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelGeneration : MonoBehaviour
{
    //The 4 central squares on the scene are starting positions.
    [SerializeField] Transform[] startingPositions; 
    

    // Different types of rooms with specific openings has created as prefabs and added to the rooms array attached to the Level Generator gameobject.
    [SerializeField] GameObject[] rooms; 
    //index 0 --> R (Rigt openning) 
    //index 1 --> L (Left Openning)
    //index 2 --> T (Top Openning)
    //index 3 --> B (Bottom Openning)
    //index 4 --> LR
    //index 5 --> LB
    //index 6 --> LT
    //index 7 --> RB
    //index 8 --> RT
    //index 9 --> TB


    private int direction; // Four directions of 1: right, 2: left, 3: up, 4: down
    private string directionStr,directionCommandStr; //debugging purposes


    public bool stopGeneration;

    private int roomCounter = 0;

    [SerializeField] float moveAmount;
    private float timeBtwSpawn; //to be able to see and track the room generation on the scene 
    [SerializeField] float startTimeBtwSpawn;


    public List<Vector2> roomPositionsList = new List<Vector2>(); //list of visited room positions
    public List<string> directionList = new List<string>(); //debugging purposes
    public List<GameObject> spawnedRoomsList; //debugging purposes

    private Vector2 nextPos; //this is a temporary variable for the level generator to check the next available position before updating the its position for the new room 
    [SerializeField] LayerMask whatIsRoom;

    private void Start()
    {
    
        // instantiate the first room randomly from the 4 possible starting possitions + use a room with only 1 opening
        int randStartingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;

        direction = 1;// CHANGE THIS LATER
        directionStr = "Right"; // CHANGE THIS LATER
        directionCommandStr = "Move "+ directionStr;
        Debug.Log(directionCommandStr);
        directionList.Add(directionStr);
    }

    private void Update()
    {
        if (timeBtwSpawn <= 0 && stopGeneration == false)
        {
            if (roomCounter < 10)
            {
                GenerateLevel();
                timeBtwSpawn = startTimeBtwSpawn;
            }
            else
            {
                stopGeneration = true;
            }
        }
        else{
            timeBtwSpawn -= Time.deltaTime;
        }
    }
    
    public void GenerateLevel()
    {
            // Move!
            MoveDirection();

            // Check the roomsPositionList for not cutting the path
            nextPos = this.CheckRoomPosition(nextPos);
            directionCommandStr = "Move "+ directionStr;

            //Imstantiate a room based on the previous amd current directions
            GenerateRoom();
            
            //Update the LevelGenerator's position
            transform.position = nextPos;
            Debug.Log(directionCommandStr);
            directionList.Add(directionStr);

            // Adding the instantiated room to the list of room position coordinates
            roomPositionsList.Add(nextPos);

            roomCounter++;

    }
    private void MoveDirection()
    {
        direction = Random.Range(1, 5);
        if (direction == 1) // Move Right!
        {
            // Move Right!
            nextPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
            directionStr = "Right";
        }
        else if (direction == 2) // Move left!
        {
            // Move Left!
            nextPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
            directionStr = "Left";
        }
        else if (direction == 3) // Move Up!
        {
            // Move Up!
            nextPos = new Vector2(transform.position.x, transform.position.y + moveAmount);
            directionStr = "Up";
        }
        else if (direction == 4) // Move Down
        {
            // Move Down!
            nextPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
            directionStr = "Down";
        }
    }

    private Vector2 CheckRoomPosition(Vector2 nextPosition)
    {
        // if the position we moved into (nextPosition) exist in the list, change the direction
        while (roomPositionsList.Contains(nextPosition))
        {
            direction = Random.Range(1, 5);
            if (direction == 1)
            {
                nextPosition = new Vector2(transform.position.x + moveAmount, transform.position.y);
                directionStr = "Right";
            }
            else if (direction == 2)
            {
                nextPosition = new Vector2(transform.position.x - moveAmount, transform.position.y);
                directionStr = "Left";
            }

            else if (direction == 3)
            {
                nextPosition = new Vector2(transform.position.x, transform.position.y + moveAmount);
                directionStr = "Up";
            }
            else if (direction == 4)
            {
                nextPosition = new Vector2(transform.position.x, transform.position.y - moveAmount);
                directionStr = "Down";
            }
        }
        return nextPosition;
    }

    private void GenerateRoom()
    {
        string previousDirection = directionList[directionList.Count - 1]; // Get the direction before the last move
        string currentDirection = directionStr; // Get the current direction
        
        if ((previousDirection == "Right") && (currentDirection == "Right") || (previousDirection == "Left") && (currentDirection == "Left"))
            {
                //Instantiate a room with Right-Left opening
                Instantiate(rooms[4], transform.position, Quaternion.identity);
            }
            else if ((previousDirection == "Up") && (currentDirection == "Up") || (previousDirection == "Down") && (currentDirection == "Down"))
            {
                //Instantiate a room with Top-Bottom opening
                Instantiate(rooms[9], transform.position, Quaternion.identity);  
            }

            else if ((previousDirection == "Up") && (currentDirection == "Right") || (previousDirection == "Left") && (currentDirection == "Down"))
            {
                //Instantiate a room with Right-Bottom opening
                Instantiate(rooms[7], transform.position, Quaternion.identity); 
            }
            else if ((previousDirection == "Right") && (currentDirection == "Down") || (previousDirection == "Up") && (currentDirection == "Left"))
            {
                //Instantiate a room with Left-Bottom opening
                Instantiate(rooms[5], transform.position, Quaternion.identity);  
            }
            else if ((previousDirection == "Left") && (currentDirection == "Up") || (previousDirection == "Down") && (currentDirection == "Right"))
            {
                //Instantiate a room with Right-Top opening
                Instantiate(rooms[8], transform.position, Quaternion.identity);  
            }
            else if ((previousDirection == "Down") && (currentDirection == "Left") || (previousDirection == "Right") && (currentDirection == "Up"))
            {
                //Instantiate a room with Left-Top opening
                Instantiate(rooms[6], transform.position, Quaternion.identity); 
            }
    }

}
