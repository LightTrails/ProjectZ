public class CompositeAnimation : IAnimation
{
    int currentIndex = 0;
    IAnimation[] animations;

    public CompositeAnimation(params IAnimation[] animations)
    {
        this.animations = animations;
    }
    
    public bool Update(float deltaTime)
    {
        if(currentIndex >= animations.Length){
            return true;
        }

        if(animations[currentIndex].Update(deltaTime)){
            currentIndex++;            
        }

        return false;
    }
}
