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
            int testdataCount = 5000;//Data.normalcases.Count

            Layer myLayer = new Layer();
            numLayers.Add(784);
            numLayers.Add(50);
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
            for (int i = 0; i < testdataCount; i++)
            {
                //Console.WriteLine(i);
                for (int j = 0; j < 1; j++)
                {
                    layerList[0].input = Data.normalcases[i].key;
                    myLayer.FeedForward(ref layerList);
                    myLayer.BackPropagation(ref layerList, ref Data.normalcases[i].value);
                }
              
            }
            double correct = 0;

            for (int i = 0; i < testdataCount; i++)
            {
               
                layerList[0].input = Data.normalcases[i].key;
                int outputValue =  myLayer.CalculateAccuracy(layerList);
                int actual = Array.IndexOf(Data.normalcases[i].value, 1);
                if (outputValue == actual)
                {
                    correct++;
                }

            }
            correct = correct / testdataCount * 100;
            Console.WriteLine(correct + " %");
            Console.ReadLine();
        }

  
    }
}
