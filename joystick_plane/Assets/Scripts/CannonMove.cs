using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonMove : MonoBehaviour
{
    private const int CANNON_UP = 0;
    private const int CANNON_RIGHT = 1;
    private const int CANNON_DOWN = 2;
    private const int CANNON_LEFT = 3;

    private int curState = 0;
    private int lastState = 0;

    private float moveSpeed = 10.0f;
    public Vector3 moveVector { set; get; }
    public JoyStick joyStick;


    private Vector3 newPos;
    private Vector2 newScreenPos;

    // Start is called before the first frame update
    void Start()
    {
        //maxX = Screen.width;
        //maxY = Screen.height;
        //Debug.Log("nearclip: " + Camera.main.nearClipPlane);
    }

    // Update is called once per frame
    void Update()
    {
        moveVector = PoolInput();
        Move();
    }

    private void Move()
    {
        transform.position += moveVector * moveSpeed * Time.deltaTime;
    }

    private Vector3 PoolInput()
    {
        Vector3 dir = Vector3.zero;

        dir.x = joyStick.Horizontal();
        dir.z = joyStick.Vertical();

        return dir;
    }
}
