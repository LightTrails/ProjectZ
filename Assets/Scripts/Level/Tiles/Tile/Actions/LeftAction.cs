using UnityEngine;

public class LeftAction : ColorAction
{    
    public LeftAction(Tile tile, int newState) : base(tile, newState) {}

    public override TileIcon Icon => TileIcon.LeftArrow;

    protected override Tile NextTile(Tile tile) => tile.Left;    

    protected override void UpdateRotation(Tile currentTile,float value){
        currentTile.gameObject.transform.rotation = Quaternion.Euler(0, -value, 0);
    }
}
