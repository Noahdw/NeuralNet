using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace forfun
{
    class Data
    {
        public static List<TestCase> testcases;
        public static List<NormalCase> normalcases = new List<NormalCase>();


        public static void readTestCSV()
        {
            testcases = new List<TestCase>();
            using (FileStream fs = File.Open("train.csv", FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    string header = sr.ReadLine();              //ignore header
                    while (!sr.EndOfStream)
                    {
                        testcases.Add(new TestCase(sr.ReadLine()));
                    }
                }
            }
            Normalize();
        }
       public static void testCaseStatistics()
        {
         
        }
        public static void Normalize()
        {

            //Normalize values (0-9)
            for (int i = 0; i < testcases.Count; i++)
            {
                double value = testcases[i].value;
                NormalCase norm = new NormalCase();
                for (int j = 0; j < 9; j++)
                {
                    
                    if (j == value)
                    {
                        norm.value[j] = 1;
                    }
                    else
                    {
                        norm.value[j] = 0;

                    }
                    
                  
                }
                for (int j = 0; j < testcases[0].key.Length; j++)
                {
                    norm.key[j] = testcases[i].key[j] / 255;
                    if (norm.key[j]==0)
                    {
                        norm.key[j] = 0.00390625;
                    }
                }
                normalcases.Add(norm);
            }

          
        }
    }

    class TestCase
    {
        public double value;
        public double[] key;

        public TestCase(string line)
        {
            string[] entries = line.Split(',');
            value = double.Parse(entries[0]);
            key = new double[entries.Length - 1];
            for (int e = 1; e < entries.Length; e++)
            {
                key[e - 1] = double.Parse(entries[e]);
            }
        }
    }
    class NormalCase
    {
        public double[] value = new double[10];
        public double[] key = new double[784];

    }

   
}