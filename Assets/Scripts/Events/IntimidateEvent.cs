using UnityEngine.Events;

public class IntimidateEvent : UnityEvent<PlayerTypeSO>
{
    public static IntimidateEvent Instance = new IntimidateEvent();
}
