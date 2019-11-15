using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoyStick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image bgImg;
    private Image joystickImg;
    private Vector3 inputVector;

    // Start is called before the first frame update
    void Start()
    {
        bgImg = GetComponent<Image>();
        joystickImg = transform.GetChild(0).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        // 将当前点击位置换算成相对于摇杆的位置
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform, eventData.position,
                                                                eventData.pressEventCamera, out pos))
        {
            pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);

            inputVector = new Vector3(pos.x * 2, 0, pos.y * 2);
            // 归一化处理
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            joystickImg.rectTransform.anchoredPosition = new Vector3(
                            inputVector.x * (bgImg.rectTransform.sizeDelta.x / 2),
                            inputVector.z * (bgImg.rectTransform.sizeDelta.y / 2));
        }
    }

    // 手指抬起时，摇杆位置归0
    public virtual void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector3.zero;
        joystickImg.rectTransform.anchoredPosition = Vector3.zero;
    }

    // 手指按下时，执行OnDrag方法
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public float Horizontal()
    {
        if(inputVector.x >= float.Epsilon && inputVector.x <= float.Epsilon)
        {
            return Input.GetAxis("Horizontal");
        }else
        {
            return inputVector.x;
        }
    }

    public float Vertical()
    {
        if (inputVector.z >= float.Epsilon && inputVector.z <= float.Epsilon)
        {
            return Input.GetAxis("Vertical");
        }
        else
        {
            return inputVector.z;
        }
    }
}
