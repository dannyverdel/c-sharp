using System;
namespace CSharpDemos.ClassLibrary.DesignPatterns.BridgePattern
{
    /*
    * The Bridge design pattern is a structural design pattern that separates the abstraction (interface) from its implementation, 
    * so that they can be developed independently and can be changed dynamically at runtime.
    * 
    * In simple terms, the Bridge design pattern allows you to create an interface that can have multiple implementations, which can be switched out dynamically. 
    * This means that you can change the behavior of your system by swapping out the implementation of an interface, without changing the code that uses that interface.
    * 
    * In this example, we have a MediaPlayer abstraction and an MP3Player implementation. 
    * We also have an AudioOutput interface and a Headphones implementation. 
    * The MP3Player implementation takes an AudioOutput in its constructor, and when the Play method is called, 
    * it delegates the audio output to the audio output implementation. In the Main method, we create a Headphones audio output and an MP3Player media player, 
    * and we play an MP3 file through the headphones.
    */

    public class InvokeBridgePattern : IInvokeMethod
    {
        public void InvokeMethod()
        {
            IAudioOutput headphones = new Headphones();
            MediaPlayer mp3_player = new MP3Player(headphones);
            mp3_player.Play("KaiKai Kitan.mp3");
        }
    }

    public abstract class MediaPlayer
    {
        protected IAudioOutput _audio_output;
        public MediaPlayer(IAudioOutput audio_output) => _audio_output = audio_output;
        public abstract void Play(string file_name);
    }

    public class MP3Player : MediaPlayer
    {
        public MP3Player(IAudioOutput audio_output) : base(audio_output) { }
        public override void Play(string file_name) => _audio_output.PlayMp3(file_name);
    }

    public interface IAudioOutput
    {
        void PlayMp3(string file_name);
    }

    public class Headphones : IAudioOutput
    {
        public void PlayMp3(string file_name) => $"Playing {file_name} trough headphones".Dump();
    }
}

