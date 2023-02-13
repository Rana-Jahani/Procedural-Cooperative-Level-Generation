using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour
{
    private LevelGeneration levelGeneration;
    void Start()
    {
        levelGeneration = GameObject.FindGameObjectWithTag("LevelGenerator").GetComponent<LevelGeneration>();
        levelGeneration.spawnedRoomsList.Add(this.gameObject);
    }

}
