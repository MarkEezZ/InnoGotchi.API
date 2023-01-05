using InnoGotchi.API.Contracts.IModelsRepositories;
using InnoGotchi.API.Entities;
using InnoGotchi.API.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoGotchi.API.Repositories.ModelsRepositories
{
    public class StatisticsRepository : RepositoryBase<Statistics>, IStatisticsRepository
    {
        public StatisticsRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateStatistics(Statistics statistics)
        {
            Create(statistics);
        }

        public void UpdateStatistics(Statistics statistics)
        {
            Update(statistics);
        }

        public Statistics GetStatisticsByFarmId(int farmId, bool trackChanges)
        {
            return FindByCondition(s => s.FarmId == farmId, trackChanges).FirstOrDefault();
        }
    }
}
