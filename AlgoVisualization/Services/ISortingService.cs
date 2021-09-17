using ChartJs.Blazor;
using ChartJs.Blazor.BarChart;
using System.Threading.Tasks;

namespace AlgoVisualization.Services
{
    public interface ISortingService
    {
        Task QuickSort (int[] array, Chart chart, BarConfig config, int visualizationSpeed);
        Task BubleSort(int[] array, Chart chart, BarConfig config, int visualizationSpeed);
        Task MergeSort(int[] array, Chart chart, BarConfig config, int visualizationSpeed);
    }
}
