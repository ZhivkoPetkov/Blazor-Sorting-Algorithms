using ChartJs.Blazor;
using ChartJs.Blazor.BarChart;
using System.Threading.Tasks;

namespace AlgoVisualization.Services
{
    public class QuickSort : BaseSorting, ISortingAlgorithm
    {
        public QuickSort(int[] currentArray, Chart chart, BarConfig config, int visualizationSpeed) : 
                        base(currentArray, chart, config, visualizationSpeed)
        {
        }

        public async Task Sort()
            => await this.QuickSortProcess(base.currentArray, 0, base.currentArray.Length - 1);
        


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
    }
}
