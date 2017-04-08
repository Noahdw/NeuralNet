using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace forfun
{
    class Neuron
    {

        //Weights flow down from the next topmost neuron to the bottom
        public List<double> weights = new List<double>();
        public double input;
        public double output;
       static Random rand = new Random();
        public Neuron() { }
        public Neuron(List<double> weight)
        {
            weights = weight;

        }
        public Neuron(double _input, List<double> weight)
        {
            weights = weight;
            output = _input;
        }
        //aka Activation funcion, this one is ReLU -- f(x) = max(0,x)
        public static double SquashFunction(double sum)
        {
           // if (sum <= 0)
          //  {
               // sum = 0;
            //  }
            sum = Math.Tanh(sum);
            return sum;

        }

        public static double[] softMax(double[] z)
        {
            double[] y = z;
            double sum = 0;
            for (int j = 0; j < z.Length; j++)
            {
                sum += Math.Exp(z[j]);
            }

            for (int i = 0; i < z.Length; i++)
            {
                y[i] = Math.Exp(z[i]) / sum; ;
            }

            return y;
        }

        public static double ErrorFunction(double target, double output)
        {
           double error = 0.5 * Math.Pow((target - output), 2); 
            return error;

        }

        public static double Derivative_TotalError_WRT_Output(double target, double output)
        {
            double deriv = -1*(target - output);
            return deriv;

        }

        public static double SoftMaxDerivative(double input)
        {
            double deriv = input*(1 - input);
            return deriv;

        }
        //Derivative of activation funcion
        public static double Derivative_Output_WRT_TotalInput( double output)
        {
            double deriv = 1 - (Math.Pow(Math.Tanh(output),2));
                return deriv;

        }

        public static double Derivative_TotalInput_WRT_Weight(double output)
        {
            double deriv = output;
            return deriv;

        }

        public static List<double> RandomWeights(int runs)
        {
          
            List<double> randWeights = new List<double>();
            
                for (int r = 0; r < runs; r++)
                {
                    randWeights.Add(rand.NextDouble());
                }

            return randWeights;
        }

    }
}
