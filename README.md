## Description
**MonoRoutine** is a wrapper class that adds functionality to Coroutines from the Unity Engine Framework.<br>
Any issues or contributions are more than welcome.

This is partly inspired by krockot's [Unity-TaskManager](https://github.com/krockot/Unity-TaskManager).

## Usage

Just create a new MonoRoutine object and pass your IEnumerator and the MonoBehaviour the coroutine should belong to.<br>
You can start, stop, pause and unpause routines. The routine object invokes events when the state changes.<br>
You can also specify a generic return type which allows you to return a value by yielding it.

More examples can be found in [TestComponent.cs](TestComponent.cs).

```cs
void Awake()
{
    var routine = new MonoRoutine(SomeRoutine, this);
    
    routine.Started += OnRoutineStarted;
    routine.Paused += OnRoutinePaused;
    
    routine.Start();
    
    if (routine.IsPaused)
    {
        routine.Unpause();
    }
}

IEnumerator SomeRoutine()
{
    Foo();
    yield return new WaitForSeconds(10f);
    Bar();
}
```
