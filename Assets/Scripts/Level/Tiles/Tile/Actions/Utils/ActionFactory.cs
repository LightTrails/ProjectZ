public static class ActionFactory
{
    public static IAction CreateAction(string typeName, Tile tile, int state){
        if(typeName == nameof(UpAction)){
            return new UpAction(tile, state);
        }

        if(typeName == nameof(DownAction)){
            return new DownAction(tile, state);
        }

        if(typeName == nameof(LeftAction)){
            return new LeftAction(tile, state);
        }

        if(typeName == nameof(RightAction)){
            return new RightAction(tile, state);
        }

        return null;
    }    
}
