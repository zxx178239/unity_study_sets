using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMove : MonoBehaviour
{
    private float moveSpeed = 10.0f;
    public Vector3 moveVector { set; get; }
    public JoyStick joyStick;

    //private float maxX = 0.0f;
    //private float maxY = 0.0f;

    private Vector3 newPos;
    private Vector2 newScreenPos;

    // Start is called before the first frame update
    void Start()
    {
        //maxX = Screen.width;
        //maxY = Screen.height;
        Debug.Log("nearclip: " + Camera.main.nearClipPlane);
    }

    // Update is called once per frame
    void Update()
    {
        moveVector = PoolInput();
        Move();
    }

    private void Move()
    {
        newPos = transform.position + moveVector * moveSpeed * Time.deltaTime;
        newScreenPos = Camera.main.WorldToScreenPoint(newPos);
        if (newScreenPos.x < 0)
        {
            newScreenPos.x = 0;
        }
        else if (newScreenPos.x > Screen.width)
        {
            newScreenPos.x = Screen.width;
        }

        if (newScreenPos.y < 0)
        {
            newScreenPos.y = 0;
        }
        else if (newScreenPos.y > Screen.height)
        {
            newScreenPos.y = Screen.height;
        }
        newPos = Camera.main.ScreenToWorldPoint(new Vector3(newScreenPos.x, newScreenPos.y,
                                                                10.3f));

        transform.position = newPos;
        
    }

    private Vector3 PoolInput()
    {
        Vector3 dir = Vector3.zero;

        dir.x = joyStick.Horizontal();
        dir.y = joyStick.Vertical();
        
        return dir;
    }
}
