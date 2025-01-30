
using UnityEngine;

/// <summary>
/// time passes at a rate of one minute per seconds
/// </summary>
public class DayTimeCycle : Singleton<DayTimeCycle>
{
    private float _totalTime;
    private float CurrentTime => _totalTime - TotalDays * 24 * 60;
    public int TotalMinutes => (int)_totalTime;
    public int TotalHours => TotalMinutes / 60;
    public int TotalDays => TotalHours / 24;

    public int CurrentMinutes => TotalMinutes % 60;
    public int CurrentHours => CurrentMinutes % 24;

    public DayTimeCycle()
    {
        _ = TimeTask();
    }

    private async System.Threading.Tasks.Task TimeTask()
    {
        while (Application.isPlaying)
        {
            int lastTime = (int)_totalTime;
            _totalTime += Time.deltaTime;
            if ((int)_totalTime - lastTime > 0)
            {
                EventManager.Instance.Notify(EventNames.TimeChanged);
            }
            await System.Threading.Tasks.Task.Yield();
        }
    }
}