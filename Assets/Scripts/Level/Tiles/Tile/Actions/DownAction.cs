
using UnityEngine;

public class DownAction : ColorAction
{
    public DownAction(Tile tile, int newState) : base(tile, newState) {}

    public override TileIcon Icon => TileIcon.DownArrow;

    protected override Tile NextTile(Tile tile) => tile.Down;    

    protected override void UpdateRotation(Tile currentTile,float value){
        currentTile.gameObject.transform.rotation = Quaternion.Euler(value, 0, 0);
    }
}
