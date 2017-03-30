using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace forfun
{
    class Layer
    {
        public List<int> numLayers = new List<int>();
        public int position;
        public double[] input;
        public List<Neuron> neurons = new List<Neuron>();
        Random rand = new Random();
        public Layer() { }

        public Layer(List<int> num, int layer, double[] inputs)
       
        {
            //Initializes net structure.
            numLayers = num;
            position = layer;
            List<double> randWeights = new List<double>();
            if ((position + 1) < numLayers.Count)
            {
                for (int r = 0; r < numLayers[position + 1]; r++)
                {
                    randWeights.Add(rand.NextDouble());
                }
            }
         

            for (int i = 0; i < numLayers[position]; i++)
            {
            
                if (position == 0)
                {
                    neurons.Add(new Neuron(inputs[i], randWeights));
                }
                else
                neurons.Add(new Neuron(randWeights));
            }
        }
        //First sums up inputs, uses a squash function such as sigmoid to make values between 0 and 1.
        //Sets that as the output of the neuron.

        public void FeedForward(List<Layer> layerList)
        {
            int iter = 0;
            foreach (var layer in layerList)
            {
                if (layer.position == layerList.Count-1)
                {
                    //Only happens if on the final layer. All inputs/outputs have been calculated. need to use error function now.
                    break;
                }
                iter++;
                if (iter >= layer.numLayers.Count)
                {
                    iter = layer.numLayers.Count-1;
                }
                for (int w = 0; w < layer.numLayers[iter]; w++)
                {
                    double inputSum = 0;
                    foreach (var neuron in layer.neurons)
                    {
                        //Summation of Wi + Ii for all neurons connecting to another neuron.
                        inputSum += neuron.output * neuron.weights[w]; 
                    }
                    layerList[iter].neurons[w].input = inputSum;
                    layerList[iter].neurons[w].output = Neuron.SquashFunction(inputSum);
                }
              
            }
        }
        public void BackPropagation(List<Layer> layerList, double[] actuals)
        {
            double learnRate = 0.5;
            //Creates a storage container for weights
            List<Layer> storageList = layerList;

            double totalError = 0;
            for (int i = 0; i < actuals.Length; i++)
            {
                totalError += Neuron.ErrorFunction(layerList.Last().neurons[i].output,actuals[i]);
                
            }
            Console.WriteLine(totalError);
            //For now only one hidden layer is allowed. This  first loop calcs the hidden layer weights.
            for (int n = 0; n < (layerList[2].neurons.Count); n++)
            {
                double output = layerList[2].neurons[n].output;
               
                for (int w = 0; w < layerList[1].neurons.Count; w++)
                {
                   
                    double current = Neuron.Derivative_TotalError_WRT_Output(actuals[n], output);
                    current = current * Neuron.Derivative_Output_WRT_TotalInput(output);
                    
                 
                    current = current * Neuron.Derivative_TotalInput_WRT_Weight(layerList[2].neurons[n].output);
                    storageList[1].neurons[w].weights[n] -= current*learnRate;
                }

            }
            //First layer calcs
            for (int i = 0; i < layerList[1].neurons.Count; i++)
            {
                for (int f = 0; f < layerList[0].neurons.Count; f++)
                {

                    double totalErrors = 0;
                    //Adds errors
                    for (int s = 0; s < layerList[2].neurons.Count; s++)
                    {
                        double output = layerList[2].neurons[s].output;
                        double current = Neuron.Derivative_TotalError_WRT_Output(actuals[s], output);
                        current = current * Neuron.Derivative_Output_WRT_TotalInput(output);
                        current = current * layerList[1].neurons[f].weights[s];
                        totalErrors += current;

                    }

                    double newWeights = totalErrors * layerList[0].neurons[f].output * Neuron.Derivative_Output_WRT_TotalInput(layerList[1].neurons[i].output);
                    storageList[0].neurons[f].weights[i] -= newWeights * learnRate;


                }
            }
            layerList = storageList;
        }

    }
}
