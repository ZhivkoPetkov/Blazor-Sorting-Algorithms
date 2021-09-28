using System.Threading.Tasks;

namespace AlgoVisualization.Services
{
    public class SortingContext
    {
        private ISortingAlgorithm sortingAlgorithm;

        public SortingContext() { }

        public void SetStrategy(ISortingAlgorithm sortingAlgorithm)
            => this.sortingAlgorithm = sortingAlgorithm;
        

        public async Task Sort()
            => await this.sortingAlgorithm.Sort();
        
    }
}
