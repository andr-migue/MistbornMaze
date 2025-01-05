using Godot;
public partial class RainHitBox : Area2D {
    [Export] int damage = 30;
    [Export] HealthBox MyHealthBox;
    public override void _Ready(){
        AreaEntered += Hit;
    }
    void Hit(Area2D body){
        if (body is HealthBox healthBox) {
            healthBox.TakeDamage(damage);
            MyHealthBox.TakeHealth(damage);
        }
    }
}