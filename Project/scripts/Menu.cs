
using Godot;
public partial class Menu : Control {
    public void PressPlay() {
        GetTree().ChangeSceneToFile("res://scenes/game.tscn");
    }
    void RowsChanged(string text) {
        if (IsNumber(text)) GlobalData.Filas = int.Parse(text);
    }
    void ColumnsChanged(string text) {
        if (IsNumber(text)) GlobalData.Columnas = int.Parse(text);
    }
    public void PressSelectionPlayer() {
        GetTree().ChangeSceneToFile("res://scenes/selection_player.tscn");
    }
    public void PressInfo() {
        GetTree().ChangeSceneToFile("res://scenes/info.tscn");
    }
    public void PressExit() {
        GetTree().Quit();
    }
    private bool IsNumber(string text) {
        if (string.IsNullOrEmpty(text)) return false;
        for (int i = 0; i < text.Length; i++) {
            if (char.IsDigit(text[i])) return true;
        }
        return false;
    }
}