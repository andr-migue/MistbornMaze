using Godot;
public partial class Rain : CharacterBody2D {
    [Export] Movement movement;
    Vector2 inputVector;
    [Export] AnimatedSprite2D animatedSprite;
    private bool IsHability = true;
    private Timer Timer;
    public override void _Ready() {
        movement.setup(this);
    }
    public override void _Process(double delta) {
        Animation();
    }
    public override void _PhysicsProcess(double delta) {
        CheckParent();
        movement.move(inputVector.Normalized());
    }
    private void Animation() {
        CheckParent();
        if (inputVector.Length() > 0) {
            if (inputVector.Y != 0) {
                if (inputVector.Y < 0) animatedSprite.Play("move_up");
                else if (inputVector.Y > 0) animatedSprite.Play("move_down");
            } 
            else {
                if (inputVector.X < 0) animatedSprite.Play("move_left");
                else if (inputVector.X > 0) animatedSprite.Play("move_right");
            }
        } 
        else animatedSprite.Play("stop");
    }
    void CheckParent(){        
        Node parent = GetParent();
        if (parent is Player1 player1) inputVector = player1.InputVector;
        else if (parent is Player2 player2) inputVector = player2.InputVector;
    }
}