using ChartJs.Blazor;
using ChartJs.Blazor.BarChart;
using System.Threading.Tasks;

namespace AlgoVisualization.Services
{
    public class BubleSort : BaseSorting, ISortingAlgorithm
    {
        public BubleSort(int[] currentArray, Chart chart, BarConfig config, int visualizationSpeed) :
                         base(currentArray, chart, config, visualizationSpeed)
        { }

        public async Task Sort()
        {
            for (int i = 0; i < currentArray.Length - 1; i++)
            {
                for (int j = 0; j < currentArray.Length - 1; j++)
                {
                    if (currentArray[j] > currentArray[j + 1])
                    {
                        await this.Swap(currentArray, j, j + 1);
                    }
                }
            }
        }
    }
}
