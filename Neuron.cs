using System;
using System.Collections.Generic;

namespace Neural_Network
{
    public class Neuron
    {
        public List<double> Weights { get; }         //Список весов
        public NeuronType NeuronType { get; }             //Тип нейрона
        public double Output { get; private set; }  //Храним выходной результат(после функции активации)


        public Neuron(int inputCount, NeuronType type = NeuronType.Normal)
        {
            //Проверка
            if(inputCount < 1)
            {
                throw new Exception("Недопустимое число входов");
            }

            NeuronType = type;
            Weights = new List<double>();

            for(int i = 0; i < inputCount; i++)
            {
                //Для начала/Требует исправления
                Weights.Add(1);
            }
        }

        //Сети однонаправленные
        //Для конкретного нейрона
        public double FeedForward(List<double> inputs)
        {
            //Проверка
            if(inputs.Count != Weights.Count)
            {
                throw new Exception("Недопустимое число входов");
            }

            var sum = 0.0;
            for (int i = 0; i < inputs.Count; i++)
            {
                sum += inputs[i] * Weights[i];
            }

            Output = Sigmoid(sum);
            return Output;
        }


        //Функция Сигмоида
        private double Sigmoid(double x)
        {
            var result = 1.0 / (1.0 + Math.Pow(Math.E, -x));
            return result;
        }

        public void SetWeights(params double[] weights)
        {
            //TODO: Удалить после добаления возможности обучения
            for(int i = 0 ; i < weights.Length; i++)
            {
                Weights[i] = weights[i];
            }
        }

        public override string ToString()
        {
            return Output.ToString();
        }
    }
}
