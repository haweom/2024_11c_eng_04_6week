using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public GameObject creditsPanel;
    private void Start()
    {
        creditsPanel.SetActive(false);
    }

    public void Show()
    {
        creditsPanel.SetActive(true);
    }
    public void Hide()
    {
        creditsPanel.SetActive(false);
    }
}
