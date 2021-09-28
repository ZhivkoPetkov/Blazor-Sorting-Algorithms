using ChartJs.Blazor;
using ChartJs.Blazor.BarChart;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlgoVisualization.Services

{
    public class BaseSorting
    {

        protected int[] currentArray;
        private int[] oldArray;
        protected Chart chart;
        protected BarConfig config;
        protected int visualizationSpeed;

        public BaseSorting(int[] currentArray, Chart chart, BarConfig config, int visualizationSpeed)
        {
            this.currentArray = currentArray;
            this.chart = chart;
            this.config = config;
            this.visualizationSpeed = visualizationSpeed;
        }

        /// <summary>
        /// Render the chart after update in the array
        /// </summary>
        /// <param name="array"></param>
        protected void UpdateChart(int[] array, BarConfig config, Chart chart)
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

        protected async Task Swap(int[] array, int i, int j)
        {
            this.oldArray = array.Select(x => x).ToArray();
            var temp = array[i];
            array[i] = array[j];
            array[j] = temp;
            await Task.Delay(visualizationSpeed);
            this.UpdateChart(array, this.config, this.chart);
        }
    }
}
