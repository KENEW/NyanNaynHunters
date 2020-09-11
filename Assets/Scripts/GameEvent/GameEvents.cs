
public enum GameEventType
{
    Open,
}

public struct GameEvent
{
    public GameEventType Type;
    public int DataIndex;

    private static GameEvent e;

    public static void Trigger(GameEventType type)
    {
        e.Type = type;
        
        GameEventManager.TriggerGameEvent(e);
    }
}