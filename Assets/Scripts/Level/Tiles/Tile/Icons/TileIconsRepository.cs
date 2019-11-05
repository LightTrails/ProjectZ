using System.Collections;
using System.Collections.Generic;

public static class TileIconsRepository
{
    private static string Arrow = "Arrow";
    private static string Blank = "Blank";

    public static Dictionary<TileIcon, TileIconRenderInfo> TileIconRendererDictionary = new Dictionary<TileIcon, TileIconRenderInfo>(){
        {TileIcon.UpArrow, TileIconRenderInfo.Create(Arrow)},
        {TileIcon.DownArrow, TileIconRenderInfo.Create(Arrow, 180)},
        {TileIcon.LeftArrow, TileIconRenderInfo.Create(Arrow, 270)},
        {TileIcon.RightArrow, TileIconRenderInfo.Create(Arrow, 90)},
        {TileIcon.Blank, TileIconRenderInfo.Create(Blank)}
    };
}
