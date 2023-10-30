using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PdPlusPlus;
using static WindSpeed;

public class WindGen
{
    private Noise noise = new Noise();
    private VariableDelay vdMaster = new VariableDelay();
    private VariableDelay vdBldg = new VariableDelay();
    private VariableDelay vdDoor = new VariableDelay();
    private VariableDelay vdDoor2 = new VariableDelay();
    private VariableDelay vdBranches = new VariableDelay();
    private VariableDelay vdBranches2 = new VariableDelay();
    private VariableDelay vdLeaves = new VariableDelay();

    private BandPass bpBldg = new BandPass();
    private BandPass bpDoor = new BandPass();
    private BandPass bpDoor2 = new BandPass();

    private LowPass lopDoor1 = new LowPass();
    private LowPass lopDoor2 = new LowPass();
    private LowPass lopLeaves1 = new LowPass();
    private LowPass lopLeaves2 = new LowPass();

    private HighPass hipTree = new HighPass();

    private VoltageControlFilter vcfBranches = new VoltageControlFilter();
    private VoltageControlFilter vcfBranches2 = new VoltageControlFilter();
    private VoltageControlFilter.vcfOutput vcfOutput;
    private VoltageControlFilter.vcfOutput vcfOutput2;

    private Oscillator osc = new Oscillator();
    private Oscillator osc2 = new Oscillator();

    private Cosine cos = new Cosine();
    private Cosine cos2 = new Cosine();

    private RealZero rzero = new RealZero();

    private WindSpeed windspeed = new WindSpeed();

    private double[] doorwaysOut = new double[2];
    private double[] doorways2Out = new double[2];
    private double[] buildingsOut = new double[2];
    private double[] branchesOut = new double[2];
    private double[] branches2Out = new double[2];
    private double[] leavesOut = new double[2];

    private double windFreq = .1;

    public void Dispose()
    {
        noise.Dispose();
        vdMaster.Dispose();
        vdBldg.Dispose();
        vdDoor.Dispose();
        vdDoor2.Dispose();
        vdBranches.Dispose();
        vdBranches2.Dispose();
        vdLeaves.Dispose();

        bpBldg.Dispose();
        bpDoor.Dispose();
        bpDoor2.Dispose();

        lopDoor1.Dispose();
        lopDoor2.Dispose();
        lopLeaves1.Dispose();
        lopLeaves2.Dispose();

        hipTree.Dispose();

        vcfBranches.Dispose();
        vcfBranches2.Dispose();

        osc.Dispose();
        osc2.Dispose();

        cos.Dispose();
        cos2.Dispose();

        windspeed.Dispose();
        Debug.Log("WindGen: Memory Deleted");
    }

    ~WindGen()
    {
       
    }

    public double[] perform()
    {

        double ws = windspeed.perform(getWindFreq());//generate our windspeed

        //generate our delays
        vdLeaves.delayWrite(ws);
        vdDoor.delayWrite(ws);
        vdDoor2.delayWrite(ws);
        vdBldg.delayWrite(ws);
        vdBranches.delayWrite(ws);
        vdBranches2.delayWrite(ws);
        double wsLeaves = vdLeaves.perform(3000);
        double wsDoor = vdDoor.perform(100);
        double wsDoor2 = vdDoor2.perform(300);
        double wsBldg = vdBldg.perform(0);
        double wsBranches = vdBranches.perform(500);
        double wsBranches2 = vdBranches2.perform(900);

        double n = noise.perform();//shared white noise generator
        double[] windOut = new double[2];//left = [0] right = [1]

        //get our wind portfolio
        doorwaysOut = doorways(wsDoor, n);
        doorways2Out = doorways2(wsDoor2, n);
        buildingsOut = buildings(wsBldg, n);
        branchesOut = branches(wsBranches, n);
        branches2Out = branches2(wsBranches2, n);
        leavesOut = leaves(wsLeaves, n);

        //add them all together and scale
        windOut[0] = (doorwaysOut[0] + doorways2Out[0] + buildingsOut[0] + branchesOut[0] + branches2Out[0]
        + leavesOut[0]);
        windOut[1] = (doorwaysOut[1] + doorways2Out[1] + buildingsOut[1] + branchesOut[1] + branches2Out[1]
        + leavesOut[1]);

        return windOut;
    }


    private double[] buildings(double ws, double n)
    {

        double[] windOut = new double[2];

        bpBldg.setCenterFrequency(800);
        double a = ws + .2;
        double b = a * bpBldg.perform(n);
        double c = rzero.perform(b, clip(a * .6, 0, .99)) * .2;
        windOut = fcpan(c, .51); // left [0], right [1]
        return windOut;
    }

    /*
     Doorway #1
     */
    private double[] doorways(double ws, double n)
    {

        double[] windOut = new double[2];

        bpDoor.setCenterFrequency(200);
        bpDoor.setQ(40);
        lopDoor1.setCutOff(.5);
        double a = lopDoor1.perform(cos.perform(((clip(ws, .35, .6) - .35) * 2) - .25));
        double b = (bpDoor.perform(n) * a) * 2;
        double c = osc.perform((a * 200) + 30);
        double d = b * c;
        // println(d);
        windOut = fcpan(d, .91);
        return windOut;
    }

    /*
      Doorway #2
      */
    private double[] doorways2(double ws, double n)
    {

        double[] windOut = new double[2];

        bpDoor2.setCenterFrequency(100);
        bpDoor2.setQ(40);
        lopDoor2.setCutOff(.1);
        double e = lopDoor2.perform(cos.perform(((clip(ws, .25, .5) - .25) * 2) - .25));
        double f = (bpDoor.perform(n) * e) * 2;
        double g = osc2.perform((e * 100) + 20);
        double h = f * g;
        windOut = fcpan(h, .03);

        return windOut;
    }

    /*
      Branches/Wires #1
    */
    private double[] branches(double ws, double n)
    {

        double[] windOut = new double[2];
        

        vcfBranches.setQ(60);
        double vd = ws;
        double cf = ((vd * 400) + 600);
        vcfOutput = vcfBranches.perform(n, cf);
        double a = vcfOutput.real * ((vd + .12) * (vd + .12));
        double b = a * 1.2;
        windOut = fcpan(b, .28);

        return windOut;

    }

    /*
      Branches/Wires #2
    */
    private double[] branches2(double ws, double n)
    {

        double[] windOut = new double[2];

        vcfBranches2.setQ(60);
        double vd2 = ws;
        double cf2 = ((vd2 * 1000) + 1000);
        vcfOutput2 = vcfBranches2.perform(n, cf2);
        double c = vcfOutput2.real * (vd2 * vd2);
        double d = c * 2;
        windOut = fcpan(d, .64);

        return windOut;
    }

    private double[] leaves(double ws, double n)
    {

        double[] windOut = new double[2];

        lopLeaves1.setCutOff(.07);
        lopLeaves2.setCutOff(4000);
        hipTree.setCutOff(200);
        double a = lopLeaves1.perform(ws + .3);
        double b = 1 - (a * .4);
        double c = lopLeaves2.perform(hipTree.perform((max(n, b) - b) * b)) * (a - .2);
        double d = c * .8;
        windOut = fcpan(d, .71);
        return windOut;
    }

    //This pans our signal left or right based on the val
    private double[] fcpan(double input, double val)
    {
        double left = 0;
        double right = 0;
        double[] windOut = new double[2];

        left = input *cos.perform(((val * .25) - .25) - .25);
        right = input *cos.perform(((val * .25) - .25));
        windOut[0] = left;
        windOut[1] = right;
        return windOut;
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

    //emulate [max~] a = input, b = input2, always return the higher value
    private double max(double a, double b)
    {
        double max = 0;
        if (a < b)
        {
            max = b;
        }
        if (a > b)
        {
            max = a;
        }
        return max;
    }

    public void setWindFreq(double f)
    {
        windFreq = f;
    }

    public double getWindFreq()
    {
        return windFreq;
    }

    
}
