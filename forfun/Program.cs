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
            double[] inputs = { 0.2, 0.54, 0.73 };
            double[] actuals = { 0.01, 0.99 };
            List<int> numLayers = new List<int>();
            List<double> actualValues = new List<double>();
            List<Layer> layerList = new List<Layer>();

            Layer myLayer = new Layer();
            numLayers.Add(3);
            numLayers.Add(3);
            numLayers.Add(2);
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
            for (int i = 0; i < 100000; i++)
            {
                myLayer.FeedForward(layerList);
                myLayer.BackPropagation(layerList, actuals);
            }
            Console.WriteLine(layerList[2].neurons[0].output);
            Console.ReadLine();
        }

  
    }
}
