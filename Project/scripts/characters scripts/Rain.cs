using Godot;
public partial class Rain : CharacterBody2D {
    [Export] Movement movement;
    [Export] AnimatedSprite2D animatedSprite;
    [Export] PointLight2D light;
    [Export] Label CurrentHability;
    [Export] Label Cooldown;
    private Vector2 inputVector;
    private float originalLight;
    private Timer Timer;
    private Timer Timer2;
    private bool IsHability = true;
    public override void _Ready() {
        movement.setup(this);
        originalLight = light.TextureScale;
        InitTimer();
        InitTimer2();
    }
    public override void _Process(double delta) {
        Animation();
        if (!Timer.IsStopped()) CurrentHability.Text = $"Destello: {Timer.TimeLeft:F1}";
        if (!Timer2.IsStopped()) Cooldown.Text = $"Cooldown: {Timer2.TimeLeft:F1}";
    }
    public override void _PhysicsProcess(double delta) {
        CheckInputVector();
        // Ejecutar movimiento de acuerdo al Vector.
        movement.move(inputVector.Normalized());
    }
    private void Animation() {
        CheckInputVector();
        // Ejecutar animación de acuerdo al Vector.
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
    void CheckInputVector() {
        // Obtener Vector de movimiento correspondiente.
        Node parent = GetParent();
        if (parent is Player1 player1) inputVector = player1.InputVector;
        else if (parent is Player2 player2) inputVector = player2.InputVector;
    }
    public void Hability() {
        if (IsHability) {
            light.TextureScale = light.TextureScale * 2;
            IsHability = false;
            Timer.Start();
            Timer2.Start();
            CurrentHability.Visible = true;
            Cooldown.Visible = true;
        }
    }
    private void InitTimer() {
        Timer = new Timer();
        Timer.WaitTime = 5.0f;
        Timer.OneShot = true;
        Timer.Connect("timeout", new Callable(this, nameof(Timeout)));
        AddChild(Timer);
    }
    private void Timeout() {
        light.TextureScale = originalLight;
        CurrentHability.Visible = false;
    }
    private void InitTimer2() {
        Timer2 = new Timer();
        Timer2.WaitTime = 20.0f;
        Timer2.OneShot = true;
        Timer2.Connect("timeout", new Callable(this, nameof(Timeout2)));
        AddChild(Timer2);
    }
    private void Timeout2() {
        IsHability = true;
        Cooldown.Visible = false;
    }
}