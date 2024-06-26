using PdPlusPlus;
using static Textures;

public class Foot
{
    //for splitphase
    private LowPass lop = new LowPass();
    private LowPass lop2 = new LowPass();
    private Phasor phasor = new Phasor();
    private Line line = new Line();
    private Textures textures1 = new Textures();
    private Textures textures2 = new Textures();
    private double previousActive = 0;
    private double active = 0;
    private double heel = 0;
    private double roll = .073;
    private double ball = 0;
    private double speed = 0;

    ~Foot() { }

    public double perform(double speed, int texture)
    {
        double output = 0;
        setSpeed(speed);
        double[] steps = splitphase(speed);
        double foot1 = foot(steps[0]);
        double foot2 = foot(steps[1]);

        //we'll put our textures here
        switch (texture)
        {
            //snow
            case 0:
                {
                    output = textures1.snow(foot1) + textures2.snow(foot2);
                    break;
                }
            //grass
            case 1:
                {
                    output = textures1.grass(foot1) + textures2.grass(foot2);
                    break;
                }
            //dirt
            case 2:
                {
                    output = textures1.dirt(foot1) + textures2.dirt(foot2);
                    break;
                }
            //gravel
            case 3:
                {
                    output = textures1.gravel(foot1) + textures2.gravel(foot2);
                    break;
                }
            //wood
            case 4:
                {
                    output = textures1.wood(foot1) + textures2.wood(foot2);
                    break;
                }
            default:
                break;
        }
        return output;
    }

    /*
    This simulates the envelope of a three
    parts of a footstep envelope.
    */
    private double foot(double f)
    {
        double x = clip(f, 0, .75) * 1.3333;
        double a = clip(x, 0, .3333) * 3;
        double b = (clip(x, .125, .875) - .125) * 1.333;
        double c = (clip(x, .667, 1) - .667) * 3;
        double f1 = polycurve(a, getHeel() * 3);
        double f2 = polycurve(b, getRoll() * 3);
        double f3 = polycurve(c, getBall() * 3);

        return f1 + f2 + f3;
    }

    //used above to get the three parts of the curve
    private double polycurve(double input, double foot)
    {
        double x = input * input * input;
        double y = input * foot;
        double z = 1 - input;
        double s = ((x * foot) - y) * z;
        return s * -1.5;
    }

    //our march generator, it splits a phasor into two parts
    private double[] splitphase(double speed)
    {
        double[] output = { 0.0f, 0.0f };

        if (speed > 0)
        {
            active = 1;
            if (active != previousActive)
            {
                phasor.setPhase(0);
            }
        }
        else
        {
            active = 0;
        }
        previousActive = active;

        //left foot
        lop.setCutoff(1);
        double a = lop.perform(speed);
        double b = phasor.perform((a + .2) * 3);
        double c = 1 - (a + .02);
        double m = min(b, c);
        double w = wrap(m * (1 / c) + 1e-05);
        double mult = line.perform(active, 500);
        output[0] = w * mult;

        //right foot
        double r = min(wrap(b + .5), c);
        double x = r * (1 / c);
        double y = wrap(x + 1e-05);
        output[1] = y * mult;


        return output;
    }

    //emulate [clip~], a = input, b = low range, c = high range
    private double clip(double a, double b, double c)
    {
        if (a < b)
            return b;
        else if (a > c)
            return c;
        else
            return a;
    }

    //emulate [min~] a = input, b = input2, always return the higher value
    private double min(double a, double b)
    {
        double min = 0;
        if (a < b)
        {
            min = a;
        }
        if (a > b)
        {
            min = b;
        }
        return min;
    }

    //free our memory
    public void Dispose()
    {
        lop.Dispose();
        lop2.Dispose();
        phasor.Dispose();
        line.Dispose();
        textures1.Dispose();//our custom class
        textures2.Dispose();
    }

    //returns only the mantissa of a double (e.g the decimal part)
    private double wrap(double input)
    {
        double frac = input % 1;
        return frac;
    }

    public void setSpeed(double s)
    {
        speed = s;
    }

    public void setRoll(double r)
    {
        roll = r;
    }

    private double getSpeed()
    {
        return speed;
    }

    //changes based on speed
    private double getBall()
    {
        lop.setCutoff(.5);
        double h = getSpeed() - lop.perform(getSpeed());
        return (h * 1.7) + .5;
    }

    //this right now is static
    private double getRoll()
    {
        return roll * 3;
    }

    //changes based on the ball
    private double getHeel()
    {
        return 1 - getBall();
    }
}
