using Godot;
public partial class Cleome : CharacterBody2D {
    [Export] Movement movement;
    Vector2 inputVector;
    [Export] CollisionShape2D MyCollision;
    [Export] AnimatedSprite2D animatedSprite;
    [Export] AnimatedSprite2D animatedSpectre;
    [Export] HealthBox MyHealthBox;
    [Export] HitBox MyHitbox;
    [Export] PlayerSensor sensor;
    private bool IsHability = true;
    private Timer Timer;
    private Timer Timer2;
    [Export] Label CurrentHability;
    [Export] Label Cooldown;
    Vector2 InitialPosition;
    public override void _Ready() {
        InitialPosition = Position;
        movement.setup(this);
        InicializarTimer1();
        InicializarTimer2();
        MyHitbox.Monitoring = false;
    }
    public override void _Process(double delta) {
        CheckLimits();
        Animation();
        AnimationSpectre();
        if (!Timer.IsStopped()) CurrentHability.Text = $"Inquisidora: {Timer.TimeLeft:F1}";
        if (!Timer2.IsStopped()) Cooldown.Text = $"Cooldown: {Timer2.TimeLeft:F1}";
        if (sensor.colisiones.Count == 0 && Timer.IsStopped()) OnTimerTimeout();
    }
    public override void _PhysicsProcess(double delta) {
        CheckParent();
        movement.move(inputVector.Normalized());
    }
    private void Animation() {
        CheckParent();
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
        CheckParent();
        if (inputVector.Length() > 0) {
            animatedSpectre.Play("default");
            if (inputVector.X < 0) animatedSpectre.Scale = new Vector2(-2, 2);
            else animatedSpectre.Scale = new Vector2(2, 2);
        } 
        else animatedSpectre.Stop();
    }
    void CheckParent(){        
        Node parent = GetParent();
        if (parent is Player1 player1) inputVector = player1.InputVector;
        else if (parent is Player2 player2) inputVector = player2.InputVector;
    }
    void CheckLimits() {
        if (GlobalPosition.X < -64 || GlobalPosition.X > GlobalData.Columnas * 64 || GlobalPosition.Y < -64 || GlobalPosition.Y > GlobalData.Filas * 64) {
            Position = InitialPosition;
        }
    }
    public void Habilidad() {
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
    private void InicializarTimer1() {
        Timer = new Timer();
        Timer.WaitTime = 5.0f;
        Timer.OneShot = true;
        Timer.Connect("timeout", new Callable(this, nameof(OnTimerTimeout)));
        AddChild(Timer);
    }
    private void OnTimerTimeout() {
        if (sensor.colisiones.Count == 0) {
            MyHitbox.Monitoring = false;
            animatedSprite.Visible = true;
            animatedSpectre.Visible = false;
            MyCollision.Disabled = false;
            MyHealthBox.Monitorable = true;
            CurrentHability.Visible = false;
        }
    }
    private void InicializarTimer2() {
        Timer2 = new Timer();
        Timer2.WaitTime = 20.0f;
        Timer2.OneShot = true;
        Timer2.Connect("timeout", new Callable(this, nameof(OnTimerTimeout2)));
        AddChild(Timer2);
    }
    private void OnTimerTimeout2() {
        IsHability = true;
        Cooldown.Visible = false;
    }
}