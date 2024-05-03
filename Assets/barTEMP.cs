using System.Collections;
using System.Collections.Generic;
using Games.CollectGame;
using UnityEngine;
using UnityEngine.UI;

public class barTEMP : MonoBehaviour
{
    [SerializeField] private ProgressUI _progressUI;
    [SerializeField] private Image _myImage;
    

    // Update is called once per frame
    void Update()
    {
        _myImage.fillAmount = _progressUI.GetRatio() + 0.01f;
    }
}
