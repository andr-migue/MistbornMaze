using Godot;
public partial class Selection_Player : Control { 
    [Export] Node2D position1;
    [Export] Node2D position2;
    private PackedScene[] characterScenes = new PackedScene[6];
    private PackedScene[] playablecharacters = new PackedScene[6];
    private Node2D currentPlayer1Character;
    private Node2D currentPlayer2Character;
    private int currentPlayer1Index = 3;
    private int currentPlayer2Index = 4;
    public override void _Ready() {
        characterScenes[0] = GD.Load<PackedScene>("res://scenes/show_characters/asktar.tscn");
        characterScenes[1] = GD.Load<PackedScene>("res://scenes/show_characters/chaos_bismarck.tscn");
        characterScenes[2] = GD.Load<PackedScene>("res://scenes/show_characters/cleome.tscn");
        characterScenes[3] = GD.Load<PackedScene>("res://scenes/show_characters/dark_rain.tscn");
        characterScenes[4] = GD.Load<PackedScene>("res://scenes/show_characters/lasswell.tscn");
        characterScenes[5] = GD.Load<PackedScene>("res://scenes/show_characters/sophie.tscn");
        playablecharacters[0] = GD.Load<PackedScene>("res://scenes/playables_characters/asktar.tscn");
        playablecharacters[1] = GD.Load<PackedScene>("res://scenes/playables_characters/chaos_bismarck.tscn");
        playablecharacters[2] = GD.Load<PackedScene>("res://scenes/playables_characters/cleome.tscn");
        playablecharacters[3] = GD.Load<PackedScene>("res://scenes/playables_characters/dark_rain.tscn");
        playablecharacters[4] = GD.Load<PackedScene>("res://scenes/playables_characters/lasswell.tscn");
        playablecharacters[5] = GD.Load<PackedScene>("res://scenes/playables_characters/sophie.tscn");
        currentPlayer1Character = characterScenes[currentPlayer1Index].Instantiate<Node2D>();
        currentPlayer2Character = characterScenes[currentPlayer2Index].Instantiate<Node2D>();
        AddChild(currentPlayer1Character);
        AddChild(currentPlayer2Character);
        UpdateCharacterPositions();
    }
    private void UpdateCharacterPositions(){
        currentPlayer1Character.Position = position1.Position;
        currentPlayer2Character.Position = position2.Position;
    }
    public void PressLeft1(){
        currentPlayer1Index = (currentPlayer1Index - 1 + characterScenes.Length) % characterScenes.Length;
        UpdateCurrentCharacter(ref currentPlayer1Character, currentPlayer1Index);
    }
    public void PressRight1(){
        currentPlayer1Index = (currentPlayer1Index + 1) % characterScenes.Length;
        UpdateCurrentCharacter(ref currentPlayer1Character, currentPlayer1Index);
    }
    public void PressAccept1(){
        GlobalData.Player1Scene = playablecharacters[currentPlayer1Index];
}
    public void PressLeft2(){
        currentPlayer2Index = (currentPlayer2Index - 1 + characterScenes.Length) % characterScenes.Length;
        UpdateCurrentCharacter(ref currentPlayer2Character, currentPlayer2Index);
    }
    public void PressRight2(){
        currentPlayer2Index = (currentPlayer2Index + 1) % characterScenes.Length;
        UpdateCurrentCharacter(ref currentPlayer2Character, currentPlayer2Index);
    }
    public void PressAccept2(){
        GlobalData.Player1Scene = playablecharacters[currentPlayer2Index];
}
    private void UpdateCurrentCharacter(ref Node2D currentCharacter, int index){
        if (currentCharacter != null) currentCharacter.QueueFree();
        currentCharacter = characterScenes[index].Instantiate<Node2D>();
        AddChild(currentCharacter);
        UpdateCharacterPositions();
    }
    public void PressBack(){
        GetTree().ChangeSceneToFile("res://scenes/menu.tscn");
    }
}