using Godot;
public partial class Game : HBoxContainer {
    [Export] SubViewport Viewport1;
    [Export] SubViewport Viewport2;
    [Export] Camera2D Camera1;
    [Export] Camera2D Camera2;
    [Export] PackedScene Board;
    private Board MyBoard;
    public override void _Ready() {
        MyBoard = (Board)Board.Instantiate<Node2D>();
        Viewport1.AddChild(MyBoard);
        Viewport2.World2D = Viewport1.World2D;
        SetCameraLimits();
    }
    public override void _PhysicsProcess(double delta) {
        // Hacer que las cámaras sigan a los jugadores (Lerp suaviza el movimiento de la cámara)
        Camera1.GlobalPosition = Camera1.Position.Lerp(MyBoard.Player1Instance.characterInstance.GlobalPosition, 0.1f);
        Camera2.GlobalPosition = Camera2.Position.Lerp(MyBoard.Player2Instance.characterInstance.GlobalPosition, 0.1f);
    }
    private void SetCameraLimits() {
        // Establecer límites para Camera1
        Camera1.LimitLeft = 0;
        Camera1.LimitRight = (GlobalData.Columnas - 1) * 64;
        Camera1.LimitTop = 0;
        Camera1.LimitBottom = (GlobalData.Filas - 1) * 64;
        // Establecer límites para Camera2
        Camera2.LimitLeft = 0;
        Camera2.LimitRight = (GlobalData.Columnas - 1) * 64;
        Camera2.LimitTop = 0;
        Camera2.LimitBottom = (GlobalData.Filas - 1) * 64;
    }
}