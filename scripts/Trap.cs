using Godot;
public partial class Trap : Node2D {
    [Export] AnimatedSprite2D animatedTrap;
    [Export] AnimatedSprite2D animatedTrap2;
    [Export] AnimatedSprite2D animatedTrap3;
    [Export] AnimatedSprite2D animatedTrap4;
    Timer Timer;
    public override void _Ready() {
        InitTimer();
    }
    void BodyEntered(Node body) {
        animatedTrap.Play("salen");
        animatedTrap2.Play("salen");
        animatedTrap3.Play("salen");
        animatedTrap4.Play("salen");
        Timer.Start();
    }
    private void InitTimer() {
        Timer = new Timer();
        Timer.WaitTime = 5.0f;
        Timer.OneShot = true;
        Timer.Connect("timeout", new Callable(this, nameof(Timeout)));
        AddChild(Timer);
    }
    private void Timeout() {
        animatedTrap.Stop();
        animatedTrap2.Stop();
        animatedTrap3.Stop();
        animatedTrap4.Stop();
    }
}