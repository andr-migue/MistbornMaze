using Godot;
public partial class Fire : Area2D {
    [Export] AnimatedSprite2D animatedFire;
    public override void _Ready(){
        animatedFire.Play("default");
    }
}