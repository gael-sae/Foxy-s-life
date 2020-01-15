using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanelGame : MonoBehaviour
{
    [SerializeField] GameObject panelPause;

    bool Pause = true;

    void Start()
    {
        panelPause.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Pause)
        {
            panelPause.SetActive(false);
            Pause = false;
        }

        if (Input.GetKeyDown(KeyCode.P) && !Pause)
        {
            panelPause.SetActive(true);
            Pause = true;
        }
    }

    public void ChangePanel(GameObject panel)
    {
        panelPause.SetActive(false);
    }
}
