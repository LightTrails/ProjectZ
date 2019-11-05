public interface IAction
{
    TileIcon Icon { get; }

    void Activate();

    IAnimation CreateFadeInAnimation();

    IAnimation CreateFadeOutAnimation();
}
