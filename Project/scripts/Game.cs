using Godot;
public partial class Game : HBoxContainer{
    [Export] public SubViewport Viewport1;
    [Export] public SubViewport Viewport2;
    [Export] public Camera2D Camera1;
    [Export] public Camera2D Camera2;
    [Export] public PackedScene Board;
    public Board MyBoard;
    public int MapSizeF;
    public int MapSizeC;
    public override void _Ready(){
        MyBoard = (Board)Board.Instantiate<Node2D>();
        Viewport1.AddChild(MyBoard);
        Viewport2.World2D = Viewport1.World2D;
        SetCameraLimits();
    }
    public override void _PhysicsProcess(double delta){
        Camera1.GlobalPosition = Camera1.Position.Lerp(MyBoard.Player1Instance.characterInstance.GlobalPosition, 0.1f);
        Camera2.GlobalPosition = Camera2.Position.Lerp(MyBoard.Player2Instance.characterInstance.GlobalPosition, 0.1f);
    }
    private void SetCameraLimits(){
        // Calcular los límites del mapa
        int mapWidth = GlobalData.Filas * 64;
        int mapHeight = GlobalData.Columnas * 64;
        // Establecer límites para Camera1
        Camera1.LimitLeft = -64;
        Camera1.LimitRight = mapWidth;
        Camera1.LimitTop = -64;
        Camera1.LimitBottom = mapHeight;
        // Establecer límites para Camera2
        Camera2.LimitLeft = -64;
        Camera2.LimitRight = mapWidth;
        Camera2.LimitTop = -64;
        Camera2.LimitBottom = mapHeight;
    }
}