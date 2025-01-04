using Godot;
public partial class Player2 : Node2D {
    public Vector2 InputVector;
    private PackedScene playerScene;
    public CharacterBody2D characterInstance;
    public override void _Ready() {
        playerScene = GlobalData.Player2Scene;
        characterInstance = playerScene.Instantiate<CharacterBody2D>();
        AddChild(characterInstance);
    }
    public override void _Process(double delta) {
        InputVector.X = Input.GetAxis("ui_left", "ui_right");
        InputVector.Y = Input.GetAxis("ui_up", "ui_down");
        if (Input.IsActionJustPressed("Habilidad Player2")) characterInstance.Call("Habilidad");
    }
}