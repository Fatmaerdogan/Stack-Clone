using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    public MoveDirection moveDirection { get;  set; }
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }
    private void OnEnable()
    {
        PlayerControl.Instance.CurrentCube = this; 
        if(PlayerControl.Instance.lastCube == null) { PlayerControl.Instance.lastCube =GameObject.Find("StartCube").GetComponent<CubeMovement>(); }
        transform.localScale = new Vector3(PlayerControl.Instance.lastCube.transform.localScale.x, transform.localScale.y, PlayerControl.Instance.lastCube.transform.localScale.z);
    }
    void Update()
    {
        if(moveDirection==MoveDirection.Z) transform.position += transform.forward * Time.deltaTime * moveSpeed;
        else transform.position += transform.right * Time.deltaTime * moveSpeed;

    }
    public void Stop()
    {
        MoveSpeed = 0;

        float hangover = HangoverSet();
        float max=MaxDirectionSet();
        if (Mathf.Abs(hangover) >= max)
        {
            PlayerControl.Instance.lastCube = null;
            PlayerControl.Instance.CurrentCube = null;
            SceneManager.LoadScene(0);
        }

        float direction = DirectionSet(hangover);
        if(moveDirection == MoveDirection.X) SplitCubeOnX(hangover, direction);
        else SplitCubeOnZ(hangover,direction);
        PlayerControl.Instance.lastCube = this;
    }
    public float HangoverSet()
    {
        if (moveDirection == MoveDirection.X) return transform.position.x - PlayerControl.Instance.lastCube.transform.position.x;
        else return transform.position.z - PlayerControl.Instance.lastCube.transform.position.z;
    }
    public float MaxDirectionSet()
    {
        return moveDirection == MoveDirection.Z ? PlayerControl.Instance.lastCube.transform.localScale.z : PlayerControl.Instance.lastCube.transform.localScale.x;
    }
    public float DirectionSet(float hangover)
    {
        return hangover > 0 ? 1f : -1f;
    }
    private void SplitCubeOnX(float hangover, float direction)
    {
        float newXSize = PlayerControl.Instance.lastCube.transform.localScale.x - Mathf.Abs(hangover);
        float fallingBlockSize = transform.localScale.x - newXSize;
        float newXPosition = PlayerControl.Instance.lastCube.transform.position.x + (hangover / 2);
        transform.localScale = new Vector3(newXSize, transform.localScale.y,transform.localScale.z );
        transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);

        float cubeEdge = transform.position.x + (newXSize / 2f * direction);
        float fallingBlockXPosition = cubeEdge + fallingBlockSize / 2f * direction;

        SpawnDropCube(fallingBlockXPosition, fallingBlockSize);
    }

    private void SplitCubeOnZ(float hangover,float direction)
    {
        float newZSize = PlayerControl.Instance.lastCube.transform.localScale.z-Mathf.Abs(hangover);
        float fallingBlockSize = transform.localScale.z - newZSize;
        float newZPosition = PlayerControl.Instance.lastCube.transform.position.z + (hangover / 2);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newZSize);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZPosition);

        float cubeEdge = transform.position.z + (newZSize / 2f*direction);
        float fallingBlockZPosition = cubeEdge + fallingBlockSize / 2f*direction;

        SpawnDropCube(fallingBlockZPosition,fallingBlockSize);

    }

    private void SpawnDropCube(float fallingBlockPosition, float fallingBlockSize)
    {
        var cube =GameObject.CreatePrimitive(PrimitiveType.Cube);
        if (moveDirection == MoveDirection.Z)
        {
            cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingBlockSize);
            cube.transform.position = new Vector3(transform.position.x, transform.position.y, fallingBlockPosition);
        }
        else
        {
            cube.transform.localScale = new Vector3(fallingBlockSize, transform.localScale.y, transform.localScale.z);
            cube.transform.position = new Vector3(fallingBlockPosition, transform.position.y, transform.position.z);
        }
        cube.GetComponent<Renderer>().material = GetComponent<Renderer>().material;
        cube.AddComponent<Rigidbody>();
        Destroy(cube.gameObject, 1f);
    }
}
