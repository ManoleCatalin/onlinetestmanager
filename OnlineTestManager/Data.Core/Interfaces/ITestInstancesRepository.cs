﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Core.Domain;

namespace Data.Core.Interfaces
{
    public interface ITestInstancesRepository : IGenericRepository<TestInstance>
    {
        Task<List<TestInstance>> GetAllTestInstancesOfTeacherAsync(Guid teacherId);
        Task<List<TestInstance>> GetAllTestInstancesOfStudentAsync(Guid studentId);
        Task<Exercise> GetNextExerciseAsync(Guid studentId, Guid testInstanceId);
        Task<ExerciseResponse> InsertExerciseResponseAsync(ExerciseResponse exerciseResponse);
    }
}
