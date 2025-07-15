using System;
using UnityEngine;

public class OnComplete : GameBehaviour
{
    private void Start()
    {
        OnImmediateComplete(() =>
        {
            print("Completed the function");
        });

        OnCompleteDelay(() =>
        {
            print("Completed the function");
        });
    }

    public void OnImmediateComplete(Action _onComplete = null)
    {
        print("Starting the Function");

        _onComplete?.Invoke();
    }

    public void OnCompleteDelay(Action _onComplete = null)
    {
        print("Starting the Function");

        ExecuteAfterSeconds(1, () =>
        {
            _onComplete?.Invoke();
        });
    }
}
