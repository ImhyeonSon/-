using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SetScreenSize();
    }

    void SetScreenSize() 
    {
        int setWidth = 2340; // 사용자 설정 너비
        int setHeight = 1080; // 사용자 설정 높이
        int deviceWidth = Screen.width; // 기기 너비 
        int deviceHeight = Screen.height; // 기기 높이 
        float setRatio=((float)setWidth/(float)setHeight);
        float deviceRatio=((float)deviceWidth/ (float)deviceHeight);
        float pRatio; // 목표로 하는 H, W비율
        Screen.SetResolution(deviceWidth, (deviceWidth * setHeight / setWidth), true);
        if (setRatio >= deviceRatio)// 기계가 세로로 길다면
        {
            // x, y, w, h y는 높이 변화량 w,h는 비율 변화 
            pRatio = ((float)(setHeight * deviceWidth) / (float)(deviceHeight * setWidth)); //목표로 하는 H비율
            Camera.main.rect = new Rect(0, (float)(1 - pRatio) / 2f, 1, pRatio); // x, y, w, h
        }
        else  // 기계가 가로로 길다면
        {
            pRatio = setRatio / deviceRatio;//목표로 하는 W비율
            Camera.main.rect = new Rect((1-pRatio)/2f , 0, pRatio, 1); // x, y, w, h
        }
    }

}
