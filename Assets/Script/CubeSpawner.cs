using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private CubeMovement cubePrefab;
    [SerializeField] private MoveDirection moveDirection;
    public List<Transform> SpawnPoint;

    public void Spawn()
    {
        var cube=Instantiate(cubePrefab);
        moveDirection = (moveDirection == MoveDirection.X) ? MoveDirection.Z : MoveDirection.X;
        if (PlayerControl.Instance.lastCube !=null && PlayerControl.Instance.lastCube.gameObject != GameObject.Find("StartCube"))
        {
            float x=moveDirection==MoveDirection.X? SpawnPoint[(int)moveDirection].transform.position.x: PlayerControl.Instance.lastCube.transform.position.x;
            float z=moveDirection==MoveDirection.Z? SpawnPoint[(int)moveDirection].transform.position.z: PlayerControl.Instance.lastCube.transform.position.z;

            cube.transform.position=new Vector3(x, PlayerControl.Instance.lastCube.transform.position.y+cubePrefab.transform.localScale.y, z);
        }
        else
        {
            cube.transform.position=transform.position;
        }
        cube.moveDirection = moveDirection;
        UIManager.Instance.Score();
    }
}
public enum MoveDirection { X=0,Z=1 }
