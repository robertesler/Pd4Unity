using UnityEngine;
using static EngineGen;

[RequireComponent(typeof(AudioSource))]
public class Engine : MonoBehaviour
{
    public double speed = 0;
    private bool running = false;
    private double outputL = 0.0F;
    private double outputR = 0.0F;  
    private EngineGen engine = new EngineGen();
   
    void Start()
    {
        running = true;
    }

    ~Engine() {
        engine.Dispose();
    }

    private void runAlgorithm(double in1, double in2)
    {
        engine.setSpeed(speed * .01);
        outputL = outputR = engine.perform();
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        if (!running)
            return;

        int dataLen = data.Length / channels;
        int n = 0;
        while (n < dataLen)
        {
            this.runAlgorithm(0, 0);
            int i = 0;
            while (i < channels)
            {
                data[n * channels + i++] = (float)this.outputL;
                data[n * channels + i++] = (float)this.outputR;
            }
            n++;
        }
    }
}
