using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelGeneration : MonoBehaviour
{

    [SerializeField] Transform[] startingPositions;

    [SerializeField] GameObject[] rooms; 
    //index 0 --> R 
    //index 1 --> L
    //index 2 --> T
    //index 3 --> B
    //index 4 --> LR
    //index 5 --> LB
    //index 6 --> LT
    //index 7 --> RB
    //index 8 --> RT
    //index 9 --> TB

    /*
    private int[] oneOpeningRooms = { 0, 1, 2, 3 }; // rooms with index 0,1,2,3 have only one opening
    private int[] rightOpeningRooms = { 4, 7, 8 }; // rooms with index 4, 7, 8 have right opening
    private int[] leftOpeningRooms = { 4, 5, 6 }; // rooms with index 4,5,6 have left opening
    private int[] topOpeningRooms = { 6, 8, 9 }; // rooms with index 6,8,9 have top opening
    private int[] bottomOpeningRooms = { 5, 7, 9 }; // rooms with index 5,7,9 have bottom opening
    */

    private int direction;
    private string directionStr,directionCommandStr;
    // direction = 1 or 2 --> move right
    // direction = 3 or 4 --> move left
    // direction = 5 or 6 --> move up
    // direction = 7 or 8 --> move down

    public bool stopGeneration;

    private int roomCounter = 0;

    [SerializeField] float moveAmount;
    private float timeBtwSpawn;
    [SerializeField] float startTimeBtwSpawn;

    //Boundries
    //public float minX = -5;
    //public float maxX = 65;
    //public float minY = -65;
    //public float maxY = 5;

    public List<Vector2> roomPositionsList = new List<Vector2>();
    public List<string> directionList = new List<string>();
    public List<GameObject> spawnedRoomsList;

    private Vector2 nextPos;

    [SerializeField] LayerMask whatIsRoom;


    private void Start()
    {
        // instantiate the first room randomly from the 4 possible starting possitions + use a room with only 1 opening
        int randStartingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;

        direction = 1;//PLEASE CHANGE THIS LATER
        directionStr = "Right"; //PLEASE CHANGE THIS LATER
        directionCommandStr = "Move "+ directionStr;
        Debug.Log(directionCommandStr);
        directionList.Add(directionStr);

        //direction = Random.Range(1, 9);
    }

    private void Update()
    {
        if (timeBtwSpawn <= 0 && stopGeneration == false)
        {
            if (roomCounter < 10)
            {
                Move();
                direction = Random.Range(1, 5);
                timeBtwSpawn = startTimeBtwSpawn;
            }
            else{
                stopGeneration = true;
            }
        }
        else{
            timeBtwSpawn -= Time.deltaTime;
        }
    }


    public void Move()
    {
        //

        if (direction == 1) // Move Right!
        {
            // Move Right!
            nextPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
            directionStr = "Right";

            // Check the roomsPositionList for not cutting the path
            nextPos = this.CheckRoomPosition(nextPos);
            directionCommandStr = "Move "+ directionStr;

            //INSTANTIATE THE ROOME HERE BASED ON THE DIRECTION_LIST.COUNT-1 AND DIRECTION_STR
            RoomGeneration();
            
            //Update the LevelGenerator's position
            transform.position = nextPos;
            Debug.Log(directionCommandStr);
            directionList.Add(directionStr);

            // Adding the instantiated room to the list of room position coordinates
            roomPositionsList.Add(nextPos);
        }
        //**********************
        //**********************
        else if (direction == 2) // Move left!
        {
            // Move Left!
            nextPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
            directionStr = "Left";

            // Check the roomsPositionList for not cutting the path
            nextPos=this.CheckRoomPosition(nextPos);
            directionCommandStr="Move "+directionStr;

            //INSTANTIATE THE ROOME HERE BASED ON THE DIRECTION_LIST.COUNT-1 AND DIRECTION_STR
            RoomGeneration();

            transform.position = nextPos;
            Debug.Log(directionCommandStr);
            directionList.Add(directionStr);
    
            //Adding the instantiated room to the list of room position coordinates 
            roomPositionsList.Add(nextPos);
        }

        //**********************
        //**********************
        else if (direction == 3) // Move Up!
        {
            // Move Up!
            nextPos = new Vector2(transform.position.x, transform.position.y + moveAmount);
            directionStr = "Up";

            // Check the roomsPositionList for not cutting the path
            nextPos=this.CheckRoomPosition(nextPos);
            directionCommandStr="Move "+directionStr;

            //INSTANTIATE THE ROOME HERE BASED ON THE DIRECTION_LIST.COUNT-1 AND DIRECTION_STR
            RoomGeneration();

            transform.position = nextPos;
            Debug.Log(directionCommandStr);
            directionList.Add(directionStr);

            // Adding the instantiated room to the list of room position coordinates
            roomPositionsList.Add(nextPos);
        }

        //**********************
        //**********************
        else if (direction == 4) // Move Down
        {
            // Move Down!
            nextPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
            directionStr = "Down";

            // Check the roomsPositionList for not cutting the path
            nextPos=this.CheckRoomPosition(nextPos);
            directionCommandStr="Move "+directionStr;

            //INSTANTIATE THE ROOME HERE BASED ON THE DIRECTION_LIST.COUNT-1 AND DIRECTION_STR
            RoomGeneration();

            transform.position = nextPos;
            Debug.Log(directionCommandStr);
            directionList.Add(directionStr);

            // Adding the instantiated room to the list of room position coordinates
            roomPositionsList.Add(nextPos);
        }
        
        roomCounter++;

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
                else
                {
                    nextPosition = new Vector2(transform.position.x, transform.position.y - moveAmount);
                    directionStr = "Down";
                }
            }
        return nextPosition;
    }

    private void RoomGeneration()
    {
        if ((directionList[directionList.Count-1] == "Right") && (directionStr == "Right") || (directionList[directionList.Count-1] == "Left") && (directionStr == "Left"))
            {
                //Instantiate a room with Right-Left opening
                Instantiate(rooms[4], transform.position, Quaternion.identity);
            }
            else if ((directionList[directionList.Count-1] == "Up") && (directionStr == "Up") || (directionList[directionList.Count-1] == "Down") && (directionStr == "Down"))
            {
                //Instantiate a room with Top-Bottom opening
                Instantiate(rooms[9], transform.position, Quaternion.identity);  
            }


            else if ((directionList[directionList.Count-1] == "Up") && (directionStr == "Right") || (directionList[directionList.Count-1] == "Left") && (directionStr == "Down"))
            {
                //Instantiate a room with Right-Bottom opening
                Instantiate(rooms[7], transform.position, Quaternion.identity); 
            }
            else if ((directionList[directionList.Count-1] == "Right") && (directionStr == "Down") || (directionList[directionList.Count-1] == "Up") && (directionStr == "Left"))
            {
                //Instantiate a room with Left-Bottom opening
                Instantiate(rooms[5], transform.position, Quaternion.identity);  
            }
            else if ((directionList[directionList.Count-1] == "Left") && (directionStr == "Up") || (directionList[directionList.Count-1] == "Down") && (directionStr == "Right"))
            {
                //Instantiate a room with Right-Top opening
                Instantiate(rooms[8], transform.position, Quaternion.identity);  
            }
            else if ((directionList[directionList.Count-1] == "Down") && (directionStr == "Left") || (directionList[directionList.Count-1] == "Right") && (directionStr == "Up"))
            {
                //Instantiate a room with Left-Top opening
                Instantiate(rooms[6], transform.position, Quaternion.identity); 
            }
    }

    private void FirstRoom()
    {
        //Instantiate the first room with only one opening based on the nextdirection 
    }

    private void LastRoom()
    {
        //Instantiate the final room with only one opening based on the previousdirection 
         if (spawnedRoomsList.Count == 10)
            {
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, whatIsRoom);
                roomDetection.GetComponent<Room>().RoomDestruction();
                if (directionStr == "Right")
                {
                    Instantiate(rooms[0], transform.position, Quaternion.identity); 
                }
                else if (directionStr == "Left")
                {
                    Instantiate(rooms[1], transform.position, Quaternion.identity); 
                }
                else if (directionStr == "Up")
                {
                    Instantiate(rooms[2], transform.position, Quaternion.identity); 
                }
                else if (directionStr == "Down")
                {
                    Instantiate(rooms[3], transform.position, Quaternion.identity); 
                }
            }
    }

   
}
