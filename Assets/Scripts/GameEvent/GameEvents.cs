
public enum GameEventType
{
    GameStart,
    CharacterSelect,
    StageStart
}

public struct GameEvent
{
    public GameEventType Type;
    public int Value;

    private static GameEvent e;

    public static void Trigger(GameEventType type)
    {
        e.Type = type;
        
        GameEventManager.TriggerGameEvent(e);
    }
    
    public static void Trigger(GameEventType type, int value)
    {
        e.Type = type;
        e.Value = value;
        
        GameEventManager.TriggerGameEvent(e);
    }}