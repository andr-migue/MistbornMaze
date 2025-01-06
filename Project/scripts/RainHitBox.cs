using Godot;
public partial class RainHitBox : Area2D {
    // Variación de la clase HitBox para el personaje Rain que le permite absorber salud de otros personajes.
    [Export] int damage = 100;
    [Export] HealthBox MyHealthBox;
    [Export] Label Cooldown;
    private Timer Timer;
    private bool CanHit = true;
    public override void _Ready(){
        AreaEntered += Hit;
        InitTimer();
    }
    public override void _Process(double delta) {
        if (!Timer.IsStopped()) Cooldown.Text = $"Cooldown: {Timer.TimeLeft:F1}";
    }
    void Hit(Area2D body){
        if (CanHit && body is HealthBox healthBox) {
            healthBox.TakeDamage(damage);
            MyHealthBox.TakeHealth(damage);
            Timer.Start();
            Cooldown.Visible = true;
            CanHit = false;
        }
    }
    private void InitTimer() {
        Timer = new Timer();
        Timer.WaitTime = 20.0f;
        Timer.OneShot = true;
        Timer.Connect("timeout", new Callable(this, nameof(Timeout)));
        AddChild(Timer);
    }
    private void Timeout() {
        CanHit = true;
        Cooldown.Visible = false;
    }
}