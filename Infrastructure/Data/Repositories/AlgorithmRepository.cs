using Core.Entities;
using Core.Interfaces.Data.Repositories;
using Core.Interfaces.Services;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Data.Repositories
{
    public class AlgorithmRepository : IAlgorithmRepository
    {
        private readonly DbContextData _context;
        private readonly IConfigurationService _configService;

        public AlgorithmRepository(DbContextData context, IConfigurationService configService)
        {
            _context = context;
            _configService = configService;
        }

        public AlgorithmEntity GetAlgorithmById(int algorithmId)
        {
            return _context.tb_algorithm.Where(x => x.algorithm_id == algorithmId).FirstOrDefault();

        }

        public VariableEntity GetVariableById(int variableId)
        {
            return _context.tb_variable.Where(x => x.variable_id == variableId).FirstOrDefault();

        }

        public AlgorithmVariableEntity GetAlgorithmVariableById(int algorithmVariableId)
        {
            return _context.tb_algorithm_variable.Where(x => x.algorithm_variable_id == algorithmVariableId).FirstOrDefault();
        }
        public List<AlgorithmVariableEntity> GetAlgorithmVariableByAlgorithmId(int algorithmId)
        {
            return _context.tb_algorithm_variable.Where(x => x.algorithm_id == algorithmId).ToList();
        }

        public AlgorithmEntity GetActiveAlgorithm()
        {
            return _context.tb_algorithm.Where(x => x.active == true).FirstOrDefault();
        }

        public void UpdateActiveAlgorithm(AlgorithmEntity algorithmEntity)
        {
            _context.tb_algorithm.Update(algorithmEntity);
            SaveChanges();
        }

        public void SaveChanges()
        {
            if (_configService.SavePermitted())
            {
                _context.SaveChanges();
            }
        }
    }
}
