using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl Instance;
    public CubeMovement lastCube { get; set; }
    public CubeMovement CurrentCube { get; set; }
    CubeSpawner cubeSpawner;
    void Awake()
    {
        Instance = this;
    }
    void Start() { 

        cubeSpawner = GetComponent<CubeSpawner>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CurrentCube.Stop();
            cubeSpawner.Spawn();
            Camera.main.transform.position += Vector3.up * 0.1f;
        }
    }
   
}
