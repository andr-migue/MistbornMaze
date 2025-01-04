using Godot;
public partial class HealthBox : Area2D {
    [Export] public int Health = 150;
    int InitialHealth;
    [Export] public Label label;
    Vector2 InitialPosition;
    public override void _Ready() {
        InitialHealth = Health;
        InitialPosition = GlobalPosition;
        UpdateHealthLabel(Health);
    }
    void SetHealth(int value) {
        Health += value;
        UpdateHealthLabel(Health);
        if (Health <= 0) Respawn();
    }
    public void TakeHealth(int value) {
        SetHealth(value);
    }
    public void TakeDamage(int damage) {
        SetHealth(-damage);
    }
    void UpdateHealthLabel(int health) {
        label.Text = "Salud: " + health;
    }
    void Respawn() {
        GlobalPosition = InitialPosition;
        Health = InitialHealth;
    }
}