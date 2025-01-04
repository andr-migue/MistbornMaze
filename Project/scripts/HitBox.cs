using Godot;
public partial class HitBox : Area2D {
    [Export] int damage = 30;
    public override void _Ready(){
        BodyEntered += Hit;
    }
    void Hit(Node2D body){
        if (body is HealthBox healthBox) healthBox.TakeDamage(damage);
    }
}