using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPopUpManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Tuto1;
    public GameObject Tuto2;
    public GameObject Tuto3;
    public GameObject Tuto4;

    // 선택된 버튼
    public GameObject Tuto1SelectButton;
    public GameObject Tuto2SelectButton;
    public GameObject Tuto3SelectButton;
    public GameObject Tuto4SelectButton;

    public GameObject TutoPannel;

    public void TogglePopUp() {
        TutoPannel.SetActive(!TutoPannel.activeSelf);
    }
    public void ClosePopUp()
    {
        TutoPannel.SetActive(false);
    }

    public void SelectTuto1() {
        Tuto1SelectButton.SetActive(true);
        Tuto2SelectButton.SetActive(false);
        Tuto3SelectButton.SetActive(false);
        Tuto4SelectButton.SetActive(false);

        Tuto1.SetActive(true);
        Tuto2.SetActive(false);
        Tuto3.SetActive(false);
        Tuto4.SetActive(false);
    }
    public void SelectTuto2() {
        Tuto1SelectButton.SetActive(false);
        Tuto2SelectButton.SetActive(true);
        Tuto3SelectButton.SetActive(false);
        Tuto4SelectButton.SetActive(false);

        Tuto1.SetActive(false);
        Tuto2.SetActive(true);
        Tuto3.SetActive(false);
        Tuto4.SetActive(false);
    }
    public void SelectTuto3()
    {
        Tuto1SelectButton.SetActive(false);
        Tuto2SelectButton.SetActive(false);
        Tuto3SelectButton.SetActive(true);
        Tuto4SelectButton.SetActive(false);
        Tuto1.SetActive(false);
        Tuto2.SetActive(false);
        Tuto3.SetActive(true);
        Tuto4.SetActive(false);

    }

    public void SelectTuto4()
    {
        Tuto1SelectButton.SetActive(false);
        Tuto2SelectButton.SetActive(false);
        Tuto3SelectButton.SetActive(false);
        Tuto4SelectButton.SetActive(true);

        Tuto1.SetActive(false);
        Tuto2.SetActive(false);
        Tuto3.SetActive(false);
        Tuto4.SetActive(true);

    }

}
