using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperateObject : MonoBehaviour
{
    private float scaleRatio = 10.0f;           // 缩放系数

    private Vector2 oldPosition1 = new Vector2(0, 0);               // 记录上一次手指触摸位置1
    private Vector2 oldPosition2 = new Vector2(0, 0);               // 记录上一次手指触摸位置2

    private Vector3 touchDelta;                 // 记录触摸的差值
    private float moveSpeed = 0.01f;           // 移动的速度

    private bool isFirstFrameTwoTouch = true;   // 是否是第一次两指触摸
    private float lastAngle;                    // 上次旋转的角度
    private Vector3 lastEulerAngle;             // 欧拉角


    // Start is called before the first frame update
    void Start()
    {
        this.scaleRatio = this.transform.localScale.x;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount <= 0)
        {
            return;
        }

        if(Input.touchCount == 1)
        {
            this.isFirstFrameTwoTouch = true;
            this.lastEulerAngle = this.transform.localEulerAngles;

            Touch touchInfo = Input.GetTouch(0);
            switch(touchInfo.phase)
            {
                case TouchPhase.Began:
                    //nodePos = this.transform.position;
                    break;
                case TouchPhase.Moved:
                    touchDelta = Input.GetTouch(0).deltaPosition;
                    //nodePos = Camera.main.ScreenToWorldPoint(touchInfo.position);
                    break;
                case TouchPhase.Ended:
                    touchDelta = new Vector2(0, 0);
                    break;
            }
        }else
        {
            // 注： 这里还是会导致冲突， 两个还是单独使用吧， 同时用两个手指操作不现实似乎，总会出现操作失误。
            if(Input.GetTouch(0).phase == TouchPhase.Moved &&
                Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                // 缩放
                this.handleScaleChange(Input.GetTouch(0).position, Input.GetTouch(1).position);
            }else if(
                (Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase != TouchPhase.Moved) ||
                (Input.GetTouch(0).phase != TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
                )
            {
                // 旋转
                this.handleRotation(Input.GetTouch(0).position, Input.GetTouch(1).position);
            }
        }
    }

    private void LateUpdate()
    {
        this.transform.Translate(touchDelta.x * moveSpeed,
                                touchDelta.y * moveSpeed,
                                0);

        this.transform.localScale = new Vector3(this.scaleRatio, this.scaleRatio, this.scaleRatio);
        //Debug.Log("localScale: " + this.transform.localScale);
        //Debug.Log("position: " + this.transform.position);
    }

    private void handleScaleChange(Vector2 touch1, Vector2 touch2)
    {
        Vector2 tmpPos1 = touch1;
        Vector2 tmpPos2 = touch2;

        if(this.isEnlarge(this.oldPosition1, this.oldPosition2, tmpPos1, tmpPos2))
        {
            this.scaleRatio = Mathf.Clamp(this.scaleRatio + 0.1f, 1, 10);
        }else
        {
            this.scaleRatio = Mathf.Clamp(this.scaleRatio - 0.1f, 1, 10);
        }

        this.oldPosition1 = touch1;
        this.oldPosition2 = touch2;
    }

    private bool isEnlarge(Vector2 op1, Vector2 op2, Vector2 np1, Vector2 np2)
    {
        float len1 = (op1 - op2).sqrMagnitude;          // sqrMagnitude计算的是两个向量的距离的平方
        float len2 = (np1 - np2).sqrMagnitude;

        if(len1 > len2)
        {
            return false;
        }else
        {
            return true;
        }
    }

    private void handleRotation(Vector2 touch1, Vector2 touch2)
    {
        float diffX = touch1.x - touch2.x;
        float diffY = touch1.y - touch2.y;
        float curTouchAngle = Mathf.Atan2(diffY, diffX) * Mathf.Rad2Deg;
        if(this.isFirstFrameTwoTouch)
        {
            this.isFirstFrameTwoTouch = false;
            this.lastAngle = curTouchAngle;
        }
        float angleDelta = curTouchAngle - this.lastAngle;
        this.transform.localEulerAngles = this.lastEulerAngle - new Vector3(0, angleDelta * 3.0f, 0);
    }
}
