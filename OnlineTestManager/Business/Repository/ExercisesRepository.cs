using Data.Core.Domain;
using Data.Core.Interfaces;
using Data.Persistence;

namespace Business.Repository
{
    public class ExercisesRepository : GenericRepository<Exercise>, IExercisesRepository
    {
        private readonly DatabaseContext _context;

        public ExercisesRepository(DatabaseContext context) : base(context) => _context = context;

    }
}
