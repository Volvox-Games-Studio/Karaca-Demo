using System;
using System.Collections;
using System.Collections.Generic;
using Games.CollectGame;
using UnityEngine;

public class PhaseLock : MonoBehaviour
{
    [SerializeField] private int phase;

    private void Start()
    {
        ItemSpawner.OnPhaseCompleted += PhaseCompleted;
    }

    private void PhaseCompleted(int obj)
    {
        if (phase == obj)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        ItemSpawner.OnPhaseCompleted += PhaseCompleted;
    }
}
