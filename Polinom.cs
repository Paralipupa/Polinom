using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polinom
{
    class Polinom
    {
        public string Name { get; set; }
        public Dictionary<int, float> Items = new Dictionary<int, float>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pol1"></param>
        /// <param name="pol2"></param>
        /// <returns></returns>
        static public Polinom operator +(Polinom pol1, Polinom pol2)
        {
            Polinom polRes = new Polinom();
            foreach (KeyValuePair<int, float> item in pol1.Items)
            {
                try
                {
                    polRes.Items.Add(item.Key, item.Value);

                    if (pol2.Items.ContainsKey(item.Key))
                    {
                        polRes.Items[item.Key] += pol2.Items[item.Key];
                        pol2.Items.Remove(item.Key);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Source}-{ex.Message}");
                }
            }

            try
            {
                foreach (KeyValuePair<int, float> item in pol2.Items)
                {
                    polRes.Items.Add(item.Key, item.Value);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Source}-{ex.Message}");
            }

            return polRes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pol1"></param>
        /// <param name="pol2"></param>
        /// <returns></returns>
        static public Polinom operator -(Polinom pol1, Polinom pol2)
        {
            Polinom polRes = new Polinom();
            foreach (KeyValuePair<int, float> item in pol1.Items)
            {
                try
                {
                    polRes.Items.Add(item.Key, item.Value);

                    if (pol2.Items.ContainsKey(item.Key))
                    {
                        polRes.Items[item.Key] -= pol2.Items[item.Key];
                        if (polRes.Items[item.Key] == 0)
                        {
                            polRes.Items.Remove(item.Key);
                        }
                        pol2.Items.Remove(item.Key);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Source}-{ex.Message}");
                }
            }

            try
            {
                foreach (KeyValuePair<int, float> item in pol2.Items)
                {
                    polRes.Items.Add(item.Key, -item.Value);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Source}-{ex.Message}");
            }

            return polRes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pol1"></param>
        /// <param name="pol2"></param>
        /// <returns></returns>
        static public Polinom operator *(Polinom pol1, Polinom pol2)
        {
            Polinom polRes = new Polinom();
            foreach (KeyValuePair<int, float> item1 in pol1.Items)
            {
                try
                {
                    foreach (KeyValuePair<int, float> item2 in pol2.Items)
                    {
                        int rank = item1.Key + item2.Key;
                        float koef = item1.Value * item2.Value;

                        if (polRes.Items.ContainsKey(rank) == false)
                        {
                            polRes.Items.Add(rank, koef);
                        }
                        else
                        {
                            polRes.Items[rank] += koef;
                        }
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Source}-{ex.Message}");
                }
            }

            return polRes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pol1"></param>
        /// <param name="pol2"></param>
        /// <returns></returns>
        static public Polinom operator /(Polinom pol1, Polinom pol2)
        {
            Polinom polRes = new Polinom();
            
            if (pol1.Items.Count == 0)
            {
                return polRes;
            }

            KeyValuePair<int, float> firstPol1 = pol1.Items.First();
            KeyValuePair<int, float> firstPol2 = pol2.Items.First();

            int rankOff = firstPol1.Key - firstPol2.Key;
            float koefOff = firstPol1.Value / firstPol2.Value;

            if (rankOff >= 0)
            {
                polRes.Items.Add(rankOff, koefOff);

                Polinom pol4 = pol2 * polRes;
                Polinom pol5 = pol1 - pol4;

                Polinom pol6 = pol5 / pol2;

                polRes += pol6;
            }
            else
            {
                polRes = pol2;
            }

            return polRes;

        }

        static public Polinom Sort(Polinom polinom)
        {
            var items = polinom.Items.OrderByDescending(p => p.Key);

            Polinom polSort = new Polinom();
            foreach (KeyValuePair<int, float> keyVal in items)
            {
                polSort.Items.Add(keyVal.Key, keyVal.Value);
            }

            return polSort;
        }
    }
}
