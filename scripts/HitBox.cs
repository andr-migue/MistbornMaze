using Godot;
public partial class HitBox : Area2D {
    // Clase que funciona junto a HealtBox, gestiona el daño de las entidades.
    [Export] int damage = 30;
    public override void _Ready(){
        AreaEntered += Hit;
    }
    void Hit(Area2D body){
        if (body is HealthBox healthBox) healthBox.TakeDamage(damage);
    }
}