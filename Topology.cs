using System.Collections.Generic;

namespace Neural_Network
{
    public class Topology
    {
        public int InputCount { get; }   //кол-во входов в нейронную сеть
        public int OutputCount { get; }
        public List<int> HiddenLayers { get; }

        public Topology(int inputCount, int outputCount, params int[] layers)
        {
            InputCount = inputCount;
            OutputCount = outputCount;

            HiddenLayers = new List<int>();
            HiddenLayers.AddRange(layers);
        }
    }
}
