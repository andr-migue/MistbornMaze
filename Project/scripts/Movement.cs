using Godot;
public partial class Movement : Node {
    // Clase que permite el movimiento de las entidades.
    [Export] public float speed = 200;
    CharacterBody2D character;
    public void setup(CharacterBody2D currentCharacter) {
        character = currentCharacter;
    }
    public void move(Vector2 inputVector) {
        character.Velocity = inputVector.Normalized() * speed;
        character.MoveAndSlide();
    }
}