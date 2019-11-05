public class TileIconRenderInfo
{
    public string ResourceName;
    public int Rotation;

    public static TileIconRenderInfo Create(string resourceName, int rotation = 0){
        return new TileIconRenderInfo(){
            ResourceName = resourceName,
            Rotation = rotation
        };
    }
}