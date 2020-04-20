using System;
using System.Collections.Generic;

namespace Neural_Network
{
    public class Neuron
    {
        public List<double> Weights { get; }         //Список весов
        public List<double> Inputs { get; }
        public NeuronType NeuronType { get; }             //Тип нейрона
        public double Output { get; private set; }  //Храним выходной результат(после функции активации)
        public double Delta { get; private set; }


        public Neuron(int inputCount, NeuronType type = NeuronType.Normal)
        {
            //Проверка
            if (inputCount < 1)
            {
                throw new Exception("Недопустимое число входов");
            }

            NeuronType = type;
            Weights = new List<double>();
            Inputs = new List<double>();

            InitWeightsRandomValue(inputCount);
        }

        private void InitWeightsRandomValue(int inputCount)
        {
            var rnd = new Random();
            for (int i = 0; i < inputCount; i++)
            {
                if (NeuronType == NeuronType.Input)
                {
                    Weights.Add(1);
                }
                else
                {
                    Weights.Add(rnd.NextDouble());
                }
                Inputs.Add(0);
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

            for(int i = 0; i < inputs.Count; i++)
            {
                Inputs[i] = inputs[i];
            }

            var sum = 0.0;
            for (int i = 0; i < inputs.Count; i++)
            {
                sum += inputs[i] * Weights[i];
            }

            if (NeuronType != NeuronType.Input)
            {
                Output = Sigmoid(sum);
            }
            else
            {
                Output = sum;
            }

            return Output;
        }


        //Функция Сигмоида
        private double Sigmoid(double x)
        {
            var result = 1.0 / (1.0 + Math.Pow(Math.E, -x));
            return result;
        }

        //Производная сигмоидальной функции
        private double SigmoidDx(double x)
        {
            var sigmoid = Sigmoid(x);
            var result = sigmoid / (1 - sigmoid);
            return result;
        }

        //Метод для вычисления новых весов
        public void Learn(double error, double learningRate)
        {
            if(NeuronType == NeuronType.Input)
            {
                return;
            }

            Delta = error * SigmoidDx(Output);

            for(int i = 0; i < Weights.Count; i++)
            {
                var weight = Weights[i];
                var input = Inputs[i];

                var newWeight = weight - input * Delta * learningRate;
                Weights[i] = newWeight;
            }
        }

        public override string ToString()
        {
            return Output.ToString();
        }
    }
}
