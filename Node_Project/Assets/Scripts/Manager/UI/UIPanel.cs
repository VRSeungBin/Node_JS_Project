using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;               //UI 를 사용하기위해 

public class UIPanel : MonoBehaviour
{
    public Button SelectedButton;
    public void OnEnable()
    {
        SelectedButton.Select();
    }
    public void Show()
    {
        this.gameObject.SetActive(true);
    }
    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

}
