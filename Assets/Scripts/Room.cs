using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    public int roomType;
    private LevelGeneration levelGeneration;

    void Start()
    {
        levelGeneration = GameObject.FindGameObjectWithTag("LevelGenerator").GetComponent<LevelGeneration>();
        
    }

    public void RoomDestruction() {

        levelGeneration.spawnedRoomsList.Remove(this.gameObject);
        Destroy(gameObject);
    }
}
