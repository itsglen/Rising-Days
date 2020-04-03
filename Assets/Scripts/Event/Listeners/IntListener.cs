using UnityEngine;
using System.Collections;
using UnityEngine.Events;

[AddComponentMenu("Events/Int Event Listener")]
public class IntListener : ScriptableEventListener<int>
{
    public IntEvent eventObject;

    public UnityEventInt eventAction = new UnityEventInt();

    protected override ScriptableEvent<int> ScriptableEvent
    {
        get
        {
            return eventObject;
        }
    }

    protected override UnityEvent<int> Action
    {
        get
        {
            return eventAction;
        }
    }
}
