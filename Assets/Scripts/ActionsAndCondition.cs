public enum Actions
{
    Empty,
    Walk,
    Jump,
    Turn,
    Interact,
    HighJump,
    WallTurn,
    Climb
}

public enum Conditions
{
    Empty,
    OnGround,
    OnEdge,
    OnButton,
    HitWall,
    HitCrate,
    InMidAir,
    NearBarrel,
    OnLadder,
    NearJumpPad
}