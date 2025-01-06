using Godot;
public partial class Cleome : CharacterBody2D {
    [Export] Movement movement;
    [Export] Label CurrentHability;
    [Export] Label Cooldown;
    [Export] CollisionShape2D MyCollision;
    [Export] AnimatedSprite2D animatedSprite;
    [Export] AnimatedSprite2D animatedSpectre;
    [Export] HealthBox MyHealthBox;
    [Export] HitBox MyHitbox;
    [Export] PlayerSensor sensor;
    private Vector2 inputVector;
    private Vector2 InitialPosition;
    private Timer Timer;
    private Timer Timer2;
    private bool IsHability = true;
    public override void _Ready() {
        InitialPosition = Position;
        movement.setup(this);
        InitTimer();
        InitTimer2();
        MyHitbox.Monitoring = false;
    }
    public override void _Process(double delta) {
        CheckLimits();
        Animation();
        AnimationSpectre();
        if (!Timer.IsStopped()) CurrentHability.Text = $"Transformación: {Timer.TimeLeft:F1}";
        if (!Timer2.IsStopped()) Cooldown.Text = $"Cooldown: {Timer2.TimeLeft:F1}";
        if (sensor.collisions.Count == 0 && Timer.IsStopped()) Timeout();
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
    private void AnimationSpectre() {
        CheckInputVector();
        // Ejecutar animación de acuerdo al Vector.
        if (inputVector.Length() > 0) {
            animatedSpectre.Play("default");
            if (inputVector.X < 0) animatedSpectre.Scale = new Vector2(-2, 2);
            else animatedSpectre.Scale = new Vector2(2, 2);
        } 
        else animatedSpectre.Stop();
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
            MyCollision.Disabled = true;
            MyHealthBox.Monitorable = false;
            MyHitbox.Monitoring = true;
            animatedSprite.Visible = false;
            animatedSpectre.Visible = true;
            IsHability = false;
            Timer.Start();
            Timer2.Start();
            CurrentHability.Visible = true;
            Cooldown.Visible = true;
        }
    }
    private void InitTimer() {
        Timer = new Timer();
        Timer.WaitTime = 15.0f;
        Timer.OneShot = true;
        Timer.Connect("timeout", new Callable(this, nameof(Timeout)));
        AddChild(Timer);
    }
    private void Timeout() {
        if (sensor.collisions.Count == 0) {
            MyHitbox.Monitoring = false;
            animatedSprite.Visible = true;
            animatedSpectre.Visible = false;
            MyCollision.Disabled = false;
            MyHealthBox.Monitorable = true;
            CurrentHability.Visible = false;
        }
    }
    private void InitTimer2() {
        Timer2 = new Timer();
        Timer2.WaitTime = 30.0f;
        Timer2.OneShot = true;
        Timer2.Connect("timeout", new Callable(this, nameof(Timeout2)));
        AddChild(Timer2);
    }
    private void Timeout2() {
        IsHability = true;
        Cooldown.Visible = false;
    }
}