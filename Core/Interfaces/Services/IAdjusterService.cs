using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IAdjusterService
    {
        Task AdjustAlgorithmSettings(bool isFirstInSequence);
        Task AnalyseAlgorithmSettings();
    }
}