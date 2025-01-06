using Godot;
public partial class ScoreSystem : CanvasLayer {
    [Export] AnimatedSprite2D animatedGema1;
    [Export] AnimatedSprite2D animatedGema2;
    [Export] Label Label1;
    [Export] Label Label2;
    public override void _Ready() {
        animatedGema1.Play("default");
        animatedGema2.Play("default");
    }
    public override void _Process(double delta){
        Label1.Text = GlobalData.Score1 + "";
        Label2.Text = GlobalData.Score2 + "";
    }
}