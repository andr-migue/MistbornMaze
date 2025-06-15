using Godot;
public partial class Skeleton : CharacterBody2D {
    [Export] Movement movement;
    [Export] AnimatedSprite2D animatedSkeleton;
    [Export] Sensor sensor;
    public override void _Ready() {
        movement.setup(this);
    }
    public override void _PhysicsProcess(double delta) {
        if (sensor.target != null) {
            if (sensor.targetDistance < 2000 && sensor.targetDistance > 5) {
                movement.move(sensor.targetDirection);
                Animation(sensor.targetDirection);
            }
        }
    }
    private void Animation(Vector2 direction) {
        if (direction.Length() > 0) {
            animatedSkeleton.Play("default");
            if (direction.X < 0) animatedSkeleton.Scale = new Vector2(-2.5f, 2.5f);
            else animatedSkeleton.Scale = new Vector2(2.5f, 2.5f);
        } 
        else animatedSkeleton.Stop();
    }
}