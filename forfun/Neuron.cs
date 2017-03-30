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
        public static double SquashFunction(double sum)
        {
            sum = 1 / (1 + Math.Exp(sum * -1));
            return sum;

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

        public static double Derivative_Output_WRT_TotalInput( double output)
        {
            double deriv = output * (1 - output);
            return deriv;

        }

        public static double Derivative_TotalInput_WRT_Weight(double output)
        {
            double deriv = output;
            return deriv;

        }


    }
}
