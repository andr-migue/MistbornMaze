using Godot;
public partial class HealthBox : Area2D {
    // Clase que gestiona el sistema de salud.
    [Export] int Health = 150;
    [Export] Label label;
    private int InitialHealth;
    private Vector2 InitialPosition;
    public override void _Ready() {
        InitialHealth = Health;
        InitialPosition = Position;
        UpdateHealthLabel(Health);
    }
    void SetHealth(int value) {
        Health += value;
        if (Health <= 0) Respawn();
        UpdateHealthLabel(Health);
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
        CharacterBody2D parent = (CharacterBody2D)GetParent();
        Node2D playerParent = (Node2D)parent.GetParent();
        if (GlobalData.Score1 > 0 && playerParent is Player1) GlobalData.Score1 -= 1;
        if (GlobalData.Score2 > 0 && playerParent is Player2) GlobalData.Score2 -= 1;
        Health = InitialHealth;
        parent.Position = InitialPosition;
    }
}