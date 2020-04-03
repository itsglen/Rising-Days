using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Time Variable")]
public class TimeVariable : ScriptableObject
{

    public int day = 0;
    public int minute = 0;
    public int season = 0;
    public int minuteStep = 5;

    public void IncrementMinutes()
    {
        minute += minuteStep;
    }

}

