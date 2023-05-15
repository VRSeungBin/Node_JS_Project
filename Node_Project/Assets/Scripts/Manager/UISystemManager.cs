using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;               //UI 를 사용하기위해 

public class UISystemManager : MonoBehaviour
{
    [SerializeField] private List<UIPanel> panels;
    [SerializeField] private UIPopup popupPrefabs;

    [SerializeField]
    private GameObject Canvas;
    private int currentPanelIndex;
    private Stack<UIPopup> popupStack = new Stack<UIPopup>();

    public void ShowPanel(int index)
    {//패널 index를 가져와서 해당 패널만 보여줌
        for (int i = 0; i < panels.Count; i++)
        {
            if(i == index)
            {
                panels[i].Show();
            }
            else
            {
                panels[i].Hide();
            }
        }
    }

    public void NextPanel()     //다음 패널을 보여줌
    {
        currentPanelIndex = (currentPanelIndex + 1) % panels.Count; //패널 숫자만큼 나머지 값으로 돌림
        ShowPanel(currentPanelIndex);
    }

    public void PreviousPanel()
    {
        currentPanelIndex--;
        if(currentPanelIndex < 0)               //0 이하로 내려가면 안되서
        {
            currentPanelIndex = panels.Count - 1;
        }
        ShowPanel(currentPanelIndex);
    }

    // Start is called before the first frame update
    void Start()
    {
        if(panels.Count > 0)
        {
            currentPanelIndex = 0;
            ShowPanel(currentPanelIndex);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PreviousPanel();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextPanel();
        }
    }
}
