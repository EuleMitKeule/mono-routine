using System;
using System.Collections;
using UnityEngine;

public class MonoRoutine
{
    Func<IEnumerator> Enumerator { get; }
    MonoBehaviour Behaviour { get; }
    Coroutine Coroutine { get; set; }

    bool isRunning;
    public bool IsRunning
    {
        get => isRunning;
        private set
        {
            if (value) Started?.Invoke(Behaviour, EventArgs.Empty);
            if (!value) isPaused = false;
            isRunning = value;
        }
    }

    bool isPaused;
    public bool IsPaused
    {
        get => isPaused;
        set
        {
            if (value) Paused?.Invoke(Behaviour, EventArgs.Empty);
            else Unpaused?.Invoke(Behaviour, EventArgs.Empty);
            isPaused = value;
        }
    }

    public event EventHandler<MonoRoutineEventArgs> Stopped;
    public event EventHandler Paused;
    public event EventHandler Unpaused;
    public event EventHandler Started;

    public MonoRoutine(Func<IEnumerator> enumerator, MonoBehaviour behaviour)
    {
        Enumerator = enumerator;
        Behaviour = behaviour;
    }

    public void Start()
    {
        if (IsRunning) return;
        IsRunning = true;
        Coroutine = Behaviour.StartCoroutine(Wrapper());
    }
    public void Stop()
    {
        if (!IsRunning) return;
        IsRunning = false;
        if (Coroutine != null) Behaviour.StopCoroutine(Coroutine);
        Stopped?.Invoke(Behaviour, new MonoRoutineEventArgs(true));
    }

    public void Restart()
    {
        Stop();
        Start();
    }

    public void Pause()
    {
        if (!IsRunning) return;
        IsPaused = true;
    }

    public void Unpause()
    {
        if (!IsRunning &! IsPaused) return;
        IsPaused = false;
    }

    public void TogglePause()
    {
        if (!IsRunning) return;
        IsPaused = !IsPaused;
    }

    IEnumerator Wrapper()
    {
        var enumerator = Enumerator?.Invoke();

        while (IsRunning)
        {
            if (IsPaused) yield return null;
            else
            {
                if (enumerator != null && enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                }
                else
                {
                    IsRunning = false;
                    Stopped?.Invoke(Behaviour, new MonoRoutineEventArgs(false));
                    yield break;
                }
            }
        }
    }
}