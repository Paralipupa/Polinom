using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polinom
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("polinom.exe <input.txt> <output.txt> ");
                Environment.Exit(0);
            }

            string pathInput = args[0];
            string pathOutput = args[1];

            if (File.Exists(pathInput) == false)
            {
                Console.WriteLine($"File not exist {pathInput}");
                Environment.Exit(0);
            }

            List<Polinom> items = InputData(pathInput);
            if (items.Count >= 2)
            {
                Polinom polA = items[0];
                Polinom polB = items[1];

                List<Polinom> outItems = new List<Polinom>();

                Polinom polC = Polinom.Sort(polA * polB);

                Polinom polD = Polinom.Sort(polC / polB);
                
                Polinom polE = Polinom.Sort(polC / polA);

                polA.Name = "a";
                polB.Name = "b";
                polC.Name = "c";
                polD.Name = "d";
                polE.Name = "e";

                outItems.Add(polA);
                outItems.Add(polB);
                outItems.Add(polC);
                outItems.Add(polD);
                outItems.Add(polE);

                OutputData(pathOutput, outItems);
            }
        }

        static private List<Polinom> InputData(string path)
        {
            bool bFirst = true;
            Polinom pol1 = new Polinom();
            Polinom pol2 = new Polinom();

            using (StreamReader st = new StreamReader(path))
            {
                string line;
                while ((line = st.ReadLine()) != null)
                {
                    if ((string.IsNullOrWhiteSpace(line)) && pol1.Items.Count != 0)
                    {
                        bFirst = false;
                    }
                    else
                    {
                        string[] str = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (str.Length == 2)
                        {

                            try
                            {
                                int rank = Int32.Parse(str[1]);
                                float koef = float.Parse(str[0]);
                                if (bFirst)
                                {
                                    if (pol1.Items.ContainsKey(rank) == false)
                                    {
                                        pol1.Items.Add(rank, koef);
                                    }
                                    else
                                    {
                                        pol1.Items[rank] += koef;
                                    }

                                }
                                else
                                {
                                    if (pol2.Items.ContainsKey(rank) == false)
                                    {
                                        pol2.Items.Add(rank, koef);
                                    }
                                    else
                                    {
                                        pol2.Items[rank] += koef;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"{ex.Source}- {ex.Message}");
                            }
                        }
                    }
                }
            }

            Polinom pola = Polinom.Sort(pol1);
            Polinom polb = Polinom.Sort(pol2);

            return new List<Polinom> { pola, polb };
        }

        static private void OutputData(string path, List<Polinom> items)
        {
            using (StreamWriter st = new StreamWriter(path))
            {
                foreach (Polinom polinom in items)
                {
                    st.WriteLine($"{polinom.Name}");
                    foreach (KeyValuePair<int, float> keyVal in polinom.Items)
                    {
                        st.WriteLine($"{keyVal.Value} {keyVal.Key}");
                    }
                    st.WriteLine("");
                }
            }
        }
    }
}
