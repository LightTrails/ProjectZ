using UnityEngine;

public class RightAction : ColorAction
{    
    public RightAction(Tile tile, int newState) : base(tile, newState) {}

    public override TileIcon Icon => TileIcon.RightArrow;

    protected override Tile NextTile(Tile tile) => tile.Right;    

    protected override void UpdateRotation(Tile currentTile, float value){
        currentTile.gameObject.transform.rotation = Quaternion.Euler(0, value, 0);
    }
}
