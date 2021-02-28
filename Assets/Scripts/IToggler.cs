public interface IToggler
{
    bool IsActive();

    void Activate();
    void Deactivate();
    void Toggle();
}
