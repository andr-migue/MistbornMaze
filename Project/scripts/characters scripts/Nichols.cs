using Godot;
public partial class Nichols : CharacterBody2D {
    [Export] Movement movement;
    [Export] CollisionShape2D MyCollision;
    [Export] AnimatedSprite2D animatedSprite;
    [Export] HealthBox MyHealthBox;
    [Export] PlayerSensor sensor;
    [Export] Label CurrentHability;
    [Export] Label Cooldown;
    private Timer Timer;
    private Timer Timer2;
    private Vector2 inputVector;
    private Vector2 InitialPosition;
    private bool IsHability = true;
    private bool IsTransform = false;
    public override void _Ready() {
        InitialPosition = Position;
        movement.setup(this);
        InitTimer();
        InitTimer2();
    }
    public override void _Process(double delta) {
        Animation();
        CheckLimits();
        if (!Timer.IsStopped()) CurrentHability.Text = $"Desvanecimiento: {Timer.TimeLeft:F1}";
        if (!Timer2.IsStopped()) Cooldown.Text = $"Cooldown: {Timer2.TimeLeft:F1}";
        if (sensor.collisions.Count == 0 && IsTransform && Timer.IsStopped()) Timeout(); 
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
    void CheckLimits() {
        // Reubicar personaje si se sale de los límites del mapa.
        if (GlobalPosition.X < 2 * 64 || GlobalPosition.X > (GlobalData.Columnas - 2) * 64 || GlobalPosition.Y < 2 * 64 || GlobalPosition.Y > (GlobalData.Filas - 2) * 64) Position = InitialPosition;
    }
    public void Hability() {
        if (IsHability) {
            IsTransform = true;
            MyCollision.Disabled = true;
            MyHealthBox.Monitorable = false;
            SetTransparency(0.5f);
            movement.speed = 300;
            IsHability = false;
            Timer.Start();
            CurrentHability.Visible = true;
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
        if (sensor.collisions.Count == 0) {
            IsTransform = false;
            MyCollision.Disabled = false;
            MyHealthBox.Monitorable = true;
            SetTransparency(1);
            movement.speed = 200;
            CurrentHability.Visible = false;
            Cooldown.Visible = true;
            Timer2.Start();
        }
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
    public void SetTransparency(float alpha) {
        // Ajustar transparencia del sprite usando el constructor de la clase Color.
        Color currentColor = animatedSprite.Modulate;
        animatedSprite.Modulate = new Color(currentColor.R, currentColor.G, currentColor.B, alpha);
    }
}