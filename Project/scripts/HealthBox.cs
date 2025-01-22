using Godot;
public partial class HealthBox : Area2D {
    // Clase que gestiona el sistema de salud.
    [Export] int Health = 150;
    [Export] Label label;
    [Export] Label changeLabel;
    private Timer Timer;
    private int InitialHealth;
    private Vector2 InitialPosition;
    public override void _Ready() {
        InitialHealth = Health;
        InitialPosition = Position;
        UpdateHealthLabel(Health);
        InitTimer();
        changeLabel.Visible = false;
    }
    void SetHealth(int value) {
        Health += value;
        if (Health <= 0) Respawn();
        UpdateHealthLabel(Health);
    }
    public void TakeHealth(int value) {
        SetHealth(value);
        changeLabel.Modulate = new Color(0, 1.0f, 0);
        UpdateChangeLabel(value, "+");
    }
    public void TakeDamage(int damage) {
        SetHealth(-damage);
        changeLabel.Modulate = new Color(1.0f, 0, 0);
        UpdateChangeLabel(damage, "-");
    }
    void UpdateHealthLabel(int health) {
        label.Text = "Salud: " + health;
    }
    void UpdateChangeLabel(int value, string signo) {
        changeLabel.Text = signo + value;
        changeLabel.Visible = true;
        Timer.Start();
    }
    private void InitTimer() {
        Timer = new Timer();
        Timer.WaitTime = 1.0f;
        Timer.OneShot = true;
        Timer.Connect("timeout", new Callable(this, nameof(Timeout)));
        AddChild(Timer);
    }
    private void Timeout() {
        changeLabel.Visible = false;
    }
    void Respawn() {
        CharacterBody2D parent = (CharacterBody2D)GetParent();
        Node2D playerParent = (Node2D)parent.GetParent();
        if (GlobalData.Score1 > 0 && playerParent is Player1) GlobalData.Score1 -= 1;
        if (GlobalData.Score2 > 0 && playerParent is Player2) GlobalData.Score2 -= 1;
        Health = InitialHealth;
        parent.Position = InitialPosition;
        if (changeLabel.Visible == true) changeLabel.Visible = false;
    }
}