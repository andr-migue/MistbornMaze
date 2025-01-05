using Godot;
public partial class Spectre : CharacterBody2D {
    [Export] Movement movement;
    [Export] AnimatedSprite2D animatedSpectre;
    [Export] Sensor sensor;
    public override void _Ready() {
        movement.setup(this);
    }
    public override void _PhysicsProcess(double delta) {
        if (sensor.target != null) {
            if (sensor.target_distance < 2000) {
                movement.move(sensor.target_direction);
                Animation(sensor.target_direction);
            }
        }
    }
    private void Animation(Vector2 direction) {
        if (direction.Length() > 0) {
            animatedSpectre.Play("default");
            if (direction.X < 0) animatedSpectre.Scale = new Vector2(-2, 2);
            else animatedSpectre.Scale = new Vector2(2, 2);
        } 
        else animatedSpectre.Stop();
    }
}