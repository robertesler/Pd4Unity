using System.Diagnostics;
using PdPlusPlusSAP;
public struct MyMusic
{
    /*******************************
    This is a way to send control messages 
    from the audio thread to the control thread.
    ********************************/
    public readonly struct MusicEvents
    {
        /**********************
        Add any control variables you need 
        in the MusicEvents struct.
        Call these whatever you want.
        They can be the same as the public variables in MyMusic, 
        or they can be different.
        ***********************/
        public readonly float value;
        public readonly float frequency; //same as below
        public MusicEvents(float f, float v)
        {
            this.frequency = f; 
            this.value = v;
        } 
    }

    //keep outputL and outputR here as is.
    public float outputL;
    public float outputR;

    /**********************
    Add any control variables you need
    ***********************/
    public float frequency;
    private Oscillator osc;
    public void Create()
    {
        // Initialize any state here.
        osc.Create();
        frequency = 250;
    }

    public void Dispose()
    {
        // Clean up any state here.
        osc.Dispose();
    }

    public void runAlgorithm()
    {
        //add your algorithm here, this is just a placeholder
        outputL = outputR = (float)osc.perform(getFrequency()) * .1F;
    }

    public void setFrequency(float f)
    {
        // Set any parameters like this
        frequency = f;
    }

    private float getFrequency()
    {
        return frequency;
    }
}
