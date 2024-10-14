using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuStartButton : MonoBehaviour
{
    [SerializeField] private GameObject gameObject;
    [SerializeField] private Button button;
    
    void Start()
    {
        button.onClick.AddListener(() => gameObject.SetActive(false));
    }

    
}
