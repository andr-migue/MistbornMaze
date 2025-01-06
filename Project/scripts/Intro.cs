using Godot;
public partial class Intro : Control {
    // Clase que gestiona la pantalla de introducción. 
    [Export] string text = "Bajo un cielo perpetuamente nublado, el laberinto se extiende como un cementerio sin fin.\nCruzas entre lápidas quebradas y mausoleos cubiertos de musgo, mientras la bruma danza sobre el suelo, ocultando lo que tus ojos no quieren ver.\nLa poca luz que se filtra entre las nubes pinta sombras largas y distorsionadas, como si el propio cementerio tuviera vida.\nEl aire está cargado de un silencio pesado, roto solo por el crujido ocasional de tus pasos o el eco lejano de algo arrastrándose en la oscuridad.\nAquí, entre los muertos olvidados, cada camino parece conducir más profundo hacia la desesperación.\n\nEscondidos entre las criptas y sepulturas, yacen cinco fragmentos de Lerasium, brillando con un resplandor antinatural, como si no pertenecieran a este mundo.\nEste mineral no solo es la clave para tu escape, sino también el premio por tu supervivencia.\nPero no estás solo. Algo te sigue; tal vez sea el laberinto mismo, tal vez los ecos de quienes fracasaron antes.\nY mientras los fragmentos se acercan, también lo hace el peligro.\nEn este lugar de reposo eterno, solo uno podrá abandonar el reino de los muertos. Los demás se quedarán aquí, para siempre.\n";
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
