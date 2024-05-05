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
        int setWidth = 2340; // ����� ���� �ʺ�
        int setHeight = 1080; // ����� ���� ����
        int deviceWidth = Screen.width; // ��� �ʺ� 
        int deviceHeight = Screen.height; // ��� ���� 
        float setRatio=((float)setWidth/(float)setHeight);
        float deviceRatio=((float)deviceWidth/ (float)deviceHeight);
        float pRatio; // ��ǥ�� �ϴ� H, W����
        Screen.SetResolution(deviceWidth, (deviceWidth * setHeight / setWidth), true);
        if (setRatio >= deviceRatio)// ��谡 ���η� ��ٸ�
        {
            // x, y, w, h y�� ���� ��ȭ�� w,h�� ���� ��ȭ 
            pRatio = ((float)(setHeight * deviceWidth) / (float)(deviceHeight * setWidth)); //��ǥ�� �ϴ� H����
            Camera.main.rect = new Rect(0, (float)(1 - pRatio) / 2f, 1, pRatio); // x, y, w, h
        }
        else  // ��谡 ���η� ��ٸ�
        {
            pRatio = setRatio / deviceRatio;//��ǥ�� �ϴ� W����
            Camera.main.rect = new Rect((1-pRatio)/2f , 0, pRatio, 1); // x, y, w, h
        }
    }

}
