using ChartJs.Blazor;
using ChartJs.Blazor.BarChart;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AlgoVisualization.Services
{
    public class MergeSort : BaseSorting, ISortingAlgorithm
    {
        public MergeSort(int[] currentArray, Chart chart, BarConfig config, int visualizationSpeed) : 
                         base(currentArray, chart, config, visualizationSpeed) {}

        public async Task Sort()
        {
            oldArray = new int[currentArray.Length];
            Array.Copy(currentArray, this.oldArray, currentArray.Length);
            await this.MergeSorting(currentArray);
            MakeDefaultColumnColor();
        }

        public async Task MergeSorting(int[] array)
        {
            if (array.Length <= 1)
            {
                return;
            }

            var (left, right) = Split(array);
            await MergeSorting(left);
            await MergeSorting(right);
            Merge(array, left, right);

            var modifIndex = this.oldArray.ToList().IndexOf(left[0]);

            if (modifIndex >= 0)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    currentArray[modifIndex++] = array[i];
                }

                await Task.Delay(visualizationSpeed);
                this.UpdateChart(this.currentArray, this.config, this.chart);
                Array.Copy(this.currentArray, this.oldArray, this.currentArray.Length);
            }
        }

        private static (int[] left, int[] right) Split(int[] array)
        {
            var middleIndex = array.Length / 2;
            return (array.Take(middleIndex).ToArray(), array.Skip(middleIndex).ToArray());
        }

        private void Merge(int[] array, int[] left, int[] right)
        {
            var mainIndex = 0;
            var leftIndex = 0;
            var rightIndex = 0;
            while (leftIndex < left.Length && rightIndex < right.Length)
            {
                if (left[leftIndex] < right[rightIndex])
                {
                    array[mainIndex] = left[leftIndex];
                    leftIndex++;
                }
                else
                {
                    array[mainIndex] = right[rightIndex];
                    rightIndex++;
                }
                mainIndex++;
            }
            while (leftIndex < left.Length)
            {
                array[mainIndex] = left[leftIndex];
                mainIndex++;
                leftIndex++;
            }

            while (rightIndex < right.Length)
            {
                array[mainIndex] = right[rightIndex];
                mainIndex++;
                rightIndex++;
            }
        }

        private void MakeDefaultColumnColor()
        {
            config.Data.Datasets.Clear();
            config.Data.Labels.Clear();
            foreach (var num in this.currentArray)
            {
                config.Data.Labels.Add(num.ToString());
            }
            var dataValues = new int[currentArray.Length];
            var colors = new string[currentArray.Length];

            for (int i = 0; i < currentArray.Length; i++)
            {
                dataValues[i] = currentArray[i];
                colors[i] = Constant.PrimaryColumnColor;
            }

            var dataset = new BarDataset<int>(dataValues)
            {
                BackgroundColor = colors
            };
            config.Data.Datasets.Add(dataset);
            chart.Update();
        }
    }
}
