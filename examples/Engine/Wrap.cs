using System;
using PdPlusPlus;

public class Wrap
{
    public double perform(double input) {
     double output = 0;
     int k;
     double f = input;
     f = (f > Int32.MaxValue || f < Int32.MinValue) ? 0 : f;
     k = (int)f;
     if( k <= f)
       output = f-k;
     else
       output = f - (k-1);
       
     return output;
  }
}
