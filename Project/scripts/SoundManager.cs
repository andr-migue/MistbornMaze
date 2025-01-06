using Godot;
using System;
using System.Collections.Generic;
public partial class SoundManager : Node {
    // Autoload para Reproducir la banda sonora.
    private static AudioStreamPlayer Player;
    private static Queue<string> Songs = new Queue<string>();
    public override void _Ready() {
        Player = new AudioStreamPlayer();
        AddChild(Player);
        Player.Bus = "master";
        Player.Connect("finished", new Callable(this, nameof(SongFinished)));
    }
    public static void Play(List<string> Song) {
        Player.Stop();
        Songs.Clear();
        for (int i = 0; i < Song.Count; i++) {
            Songs.Enqueue(Song[i]);
        }
        ShuffleQueue();
        PlayNextSong();
    }
    private static void ShuffleQueue() {
        // Aleatorizar cola
        var SongsList = new List<string>(Songs);
        Random rand = new Random();
        for (int i = 0; i < SongsList.Count; i++) {
            int j = rand.Next(i, SongsList.Count);
            var temp = SongsList[i];
            SongsList[i] = SongsList[j];
            SongsList[j] = temp;
        }
        Songs.Clear();
        for (int i = 0; i < SongsList.Count; i++) {
            Songs.Enqueue(SongsList[i]);
        }
    }
    private static void PlayNextSong() {
        // Reproducir siguiente canción
        if (Songs.Count > 0) {
            string nextSongPath = Songs.Dequeue();
            Player.Stream = ResourceLoader.Load<AudioStream>(nextSongPath);
            Player.Play();
        }
    }
    private void SongFinished() {
        PlayNextSong();
    }
}