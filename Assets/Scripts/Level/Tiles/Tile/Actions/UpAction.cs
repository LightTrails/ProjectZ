using UnityEngine;

public class UpAction : ColorAction
{
    public UpAction(Tile tile, int newState) : base(tile, newState) {}

    public override TileIcon Icon => TileIcon.UpArrow;

    protected override Tile NextTile(Tile tile) => tile.Up;    

    protected override void UpdateRotation(Tile currentTile,float value){
        currentTile.gameObject.transform.rotation = Quaternion.Euler(-value, 0, 0);
    }
}
