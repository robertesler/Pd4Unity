using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PdPlusPlus;

public class MyFilter
{

    private double CenterFrequency = 500;
    private double BandWidth = 2;
    public double outputL = 0;
    public double outputR = 0;
    private BandPass bp = new BandPass();

    // Start is called before the first frame update, put your instatiation code here
    void Start()
    {
        bp.setCenterFrequency(CenterFrequency);
        bp.setQ(BandWidth);
    }

    //The input here will be the L/R channels 
    public void runAlgorithm(double inputL, double inputR)
    {
        double sample = bp.perform( (inputL + inputR) * .5);
        outputL = outputR = sample;
    }

    /*
     Make sure to release all of your Pd++ objects here
     */
    public void Dispose()
    {
        bp.Dispose();
    }

    #region setters
    public void setCenterFrequency(double f)
    {
        CenterFrequency = f;
    }

    public void setQ(double q)
    {
        BandWidth = q;
    }

    #endregion setters
}

