using Godot;
public partial class Pause : CanvasLayer {
    [Export] public Label PausaLabel; 
    private bool PlayerNoWin = true;
    private bool GamePausable = true;
    public override void _Process(double delta) {
        if (Input.IsActionJustPressed("Pausa")) {
            PauseVoid();
        }
    }
    public override void _PhysicsProcess(double delta) {
        if (GlobalData.Score1 == 5 && PlayerNoWin) {
            PlayerNoWin = false;
            PauseVoid();
            GamePausable = false;
            PausaLabel.Text = "Jugador 1 ha ganado.";
        }
        
        if (GlobalData.Score2 == 5 && PlayerNoWin) {
            PlayerNoWin = false;
            PauseVoid();
            GamePausable = false;
            PausaLabel.Text = "Jugador 2 ha ganado.";
        }
    }
    public void PauseVoid() {
        if (GamePausable) {
            GetTree().Paused = !GetTree().Paused;
            Visible = !Visible;
        }
    }
    public void PressBack() {
        GetTree().ChangeSceneToFile("res://scenes/menu.tscn");
    }
}