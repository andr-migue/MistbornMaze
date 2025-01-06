using Godot;
public partial class Intro : Control {
    // Clase que gestiona la pantalla de introducción. 
    [Export] string text = "Bajo un cielo perpetuamente nublado, el laberinto se extiende como un cementerio sin\n fin. Cruzas entre lápidas quebradas y mausoleos cubiertos de musgo, mientras la bruma\n danza sobre el suelo, ocultando lo que tus ojos no quieren ver. La poca luz que se filtra entre\nlas nubes pinta sombras largas y distorsionadas, como si el propio cementerio tuviera vida.\nEl aire está cargado de un silencio pesado, roto solo por el crujido ocasional de tus pasos o\nel eco lejano de algo arrastrándose en la oscuridad. Aquí, entre los muertos olvidados, cada\ncamino parece conducir más profundo hacia la desesperación.\n\nEscondidos entre las criptas y sepulturas, yacen cinco fragmentos de Lerasium, brillando\ncon un resplandor antinatural, como si no pertenecieran a este mundo. Este mineral no\nsolo es la clave para tu escape, sino también el premio por tu supervivencia. Pero no estás\nsolo. Algo te sigue; tal vez sea el laberinto mismo, tal vez los ecos de quienes fracasaron\nantes. Y mientras los fragmentos se acercan, también lo hace el peligro. En este lugar de\nreposo eterno, solo uno podrá abandonar el reino de los muertos. Los demás se quedarán\naquí, para siempre.";
    [Export] Label label;
    [Export] Button Continue;
    [Export] Button Back;
    private Timer Timer;
    private int index = 0;
    public override void _Ready() {
        Continue.Visible = false;
        Back.Visible = false;
        InitTimer();
        Timer.Start();
    }
    public override void _Process(double delta){
        if (Input.IsActionJustPressed("Pausa")) {
            Continue.Visible = true;
            Back.Visible = true;
        }
    }
    private void InitTimer() {
        Timer = new Timer();
        Timer.WaitTime = 0.04f;
        Timer.OneShot = false;
        Timer.Connect("timeout", new Callable(this, nameof(Timeout)));
        AddChild(Timer);
    }
    private void Timeout(){
        if (index < text.Length) {
            label.Text += text[index];
            index++;
        }
        else {
            Timer.Stop();
            Continue.Visible = true;
            Back.Visible = true;
        }
    }
    void PressContinue() {
        SoundManager.Play(GlobalData.SongsGame);
        GetTree().ChangeSceneToFile("res://scenes/game.tscn");
    }
    void PressBack() {
        GetTree().ChangeSceneToFile("res://scenes/start_menu.tscn");
    }
}
