public enum ThreatState
{
    ACTIVE,
    INACTIVE
}

public interface IThreat 
{
    void Init();
    void Deactivate();
    //void CallEnemy();
    float duration { get; set; }
    int level { get; set; }
    ThreatState state { get; set; }
    bool friendly { get; set; }
}
