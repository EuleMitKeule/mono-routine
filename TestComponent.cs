using System;
using System.Collections;
using UnityEngine;

public class TestComponent : MonoBehaviour
{
    MonoRoutine Routine { get; set; }

    void Awake()
    {
        Routine = new MonoRoutine(Test, this);

        Routine.Started += OnRoutineStarted;
        Routine.Paused += OnRoutinePaused;
        Routine.Unpaused += OnRoutineUnpaused;
        Routine.Stopped += OnRoutineStopped;

        Routine.Start();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Routine.TogglePause();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Routine.Stop();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Routine.Start();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Routine.Restart();
        }
    }

    IEnumerator Test()
    {
        yield return new WaitForSeconds(10f);
    }

    void OnRoutineStarted(object sender, EventArgs e)
    {
        Debug.Log("Started");
    }

    void OnRoutinePaused(object sender, EventArgs e)
    {
        Debug.Log("Paused");
    }

    void OnRoutineUnpaused(object sender, EventArgs e)
    {
        Debug.Log("Unpaused");
    }

    void OnRoutineStopped(object sender, MonoRoutineEventArgs e)
    {
        Debug.Log($"Stopped {(e.IsForced ? " forcefully" : " not forcefully")}");
    }
}