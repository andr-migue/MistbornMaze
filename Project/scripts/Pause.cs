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
        if (GlobalData.Score1 >= 5 && PlayerNoWin) {
            PlayerNoWin = false;
            PauseVoid();
            GamePausable = false;
            PausaLabel.Text = "Jugador 1 ha escapado del\nLaberinto.";
            GlobalData.Score1 = 0;
            GlobalData.Score2 = 0;
        }
        
        if (GlobalData.Score2 >= 5 && PlayerNoWin) {
            PlayerNoWin = false;
            PauseVoid();
            GamePausable = false;
            PausaLabel.Text = "Jugador 2 ha escapado del\nLaberinto.";
            GlobalData.Score1 = 0;
            GlobalData.Score2 = 0;
        }
    }
    public void PauseVoid() {
        if (GamePausable) {
            GetTree().Paused = !GetTree().Paused;
            Visible = !Visible;
        }
    }
    public void PressBack() {
        GetTree().Paused = !GetTree().Paused;
        SoundManager.Play(GlobalData.SongsMenu);
        GetTree().ChangeSceneToFile("res://scenes/start_menu.tscn");
    }
}