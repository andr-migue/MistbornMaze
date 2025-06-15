using Godot;
public partial class Player1 : Node2D {
    public Vector2 InputVector;
    public CharacterBody2D characterInstance;
    private PackedScene playerScene;
    public override void _Ready() {
        playerScene = GlobalData.Player1Scene;
        characterInstance = playerScene.Instantiate<CharacterBody2D>();
        AddChild(characterInstance);
    }
    public override void _Process(double delta) {
        // Verificar si es pulsada una tecla.
        InputVector.X = Input.GetAxis("move_left", "move_right");
        InputVector.Y = Input.GetAxis("move_up", "move_down");
        if (Input.IsActionJustPressed("Habilidad Player")) characterInstance.Call("Hability");
    }
}