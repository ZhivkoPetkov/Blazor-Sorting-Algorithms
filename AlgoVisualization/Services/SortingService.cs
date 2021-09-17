using AlgoVisualization.Services;
using ChartJs.Blazor;
using ChartJs.Blazor.BarChart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlgoVisualization
{
    public class SortingService : ISortingService
    {
        private int[] currentArray;
        private int[] oldArray;
        Chart chart;
        BarConfig config;
        int visualizationSpeed;

        // START OF QUICK SORT ALGO
        public async Task QuickSort(int[] array, Chart chart, BarConfig config, int visualizationSpeed)
        {
            this.chart = chart;
            this.config = config;
            this.visualizationSpeed = visualizationSpeed;
            await this.QuickSortProcess(array, 0, array.Length - 1);
        }

        private async Task QuickSortProcess(int[] array, int leftIndex, int rightIndex)
        {
            if (leftIndex < rightIndex)
            {
                var pivot = await Partition(array, leftIndex, rightIndex);
                await QuickSortProcess(array, leftIndex, pivot);
                await QuickSortProcess(array, pivot + 1, rightIndex);
            }
        }

        private async Task<int> Partition(int[] arr, int left, int right)
        {
            var pivot = arr[left];
            var modifyIndex = left;

            for (int i = left + 1; i <= right; i++)
            {
                var currentIndexValue = arr[i];

                if (currentIndexValue < pivot)
                {
                    modifyIndex++;
                    await Swap(arr, i, modifyIndex);
                }
            }
            await Swap(arr, left, modifyIndex);
            return modifyIndex;
        }
        // END OF QUICK SORT ALGO

        /// <summary>
        /// Swap the elements in the array.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        private async Task Swap(int[] array, int i, int j)
        {
            this.oldArray = array.Select(x => x).ToArray();
            var temp = array[i];
            array[i] = array[j];
            array[j] = temp;
            await Task.Delay(visualizationSpeed);
            this.UpdateChart(array,this.config, this.chart);
        }

        // START OF BUBLE SORT ALGO
        public async Task BubleSort(int[] array, Chart chart, BarConfig config, int visualizationSpeed)
        {
            this.chart = chart;
            this.config = config;
            this.visualizationSpeed = visualizationSpeed;

            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = 0; j < array.Length - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        await this.Swap(array, j, j + 1);
                    }
                }
            }
        }
        // END OF BUBLE SORT ALGO

        // START OF MERGE SORT ALGO
        public async Task MergeSort(int[] array, Chart chart, BarConfig config, int visualizationSpeed)
        {
            this.chart = chart;
            this.config = config;
            this.visualizationSpeed = visualizationSpeed;
            this.oldArray = new int[array.Length];
            Array.Copy(array, this.oldArray, array.Length);
            this.currentArray = array;
            await this.MergeSorting(array);
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
        //END OF MERGE SORT ALGO

        /// <summary>
        /// Make the color of the columns in the chart default
        /// </summary>
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


        /// <summary>
        /// Render the chart after update in the array
        /// </summary>
        /// <param name="array"></param>
        public void UpdateChart(int[] array, BarConfig config, Chart chart)
        {
            this.chart = chart;
            this.config = config;
            var numbers = new List<int>(array.ToList());
            this.config.Data.Labels.Clear();
            foreach (var num in numbers)
            {
                config.Data.Labels.Add(num.ToString());
            }
            var dataValues = new int[array.Length];
            var colors = new string[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != oldArray[i])
                {
                    dataValues[i] = array[i];
                    colors[i] = Constant.SecondaryColumnColor;
                }
                else
                {
                    dataValues[i] = array[i];
                    colors[i] = Constant.PrimaryColumnColor;
                }
            }

            var dataset = new BarDataset<int>(dataValues)
            {
                BackgroundColor = colors
            };
            config.Data.Datasets.Clear();
            config.Data.Datasets.Add(dataset);
            chart.Update();
        }
    }
}