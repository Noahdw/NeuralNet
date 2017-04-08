using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace forfun
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Data.readTestCSV();
            Data.testCaseStatistics();
            double[] inputs = Data.normalcases[0].key;
            double[] actuals = Data.normalcases[0].value;
            List<int> numLayers = new List<int>();
            List<double> actualValues = new List<double>();
            List<Layer> layerList = new List<Layer>();

            Layer myLayer = new Layer();
            numLayers.Add(784);
            numLayers.Add(15);
            numLayers.Add(10);
            for (int i = 0; i < numLayers.Count; i++)
            {
                if (i == 0)
                {
                    Layer t = new Layer(numLayers, i, inputs);
                    t.input = inputs;
                    layerList.Add(t);
                }
                else
                    layerList.Add(new Layer(numLayers, i, null));
            }
            for (int i = 0; i < Data.normalcases.Count; i++)
            {
                for (int j = 0; j < 5 ; j++)
                {
                    layerList[0].input = Data.normalcases[i].key;
                    myLayer.FeedForward(layerList);
                    myLayer.BackPropagation(layerList, Data.normalcases[i].value);
                }
              
            }
            Console.WriteLine(layerList[2].neurons[0].output);
            Console.ReadLine();
        }

  
    }
}
