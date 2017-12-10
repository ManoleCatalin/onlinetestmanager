using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Core.Domain;
using Data.Core.Interfaces;
using Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Business.Repository
{
    public class GroupsRepository : IGroupsRepository
    {
        private readonly DatabaseContext _context;

        public GroupsRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Group>> GetGroupsAsync()
        {
            return await _context.Groups.ToListAsync();
        }

        public async Task<Group> GetGroupByIdAsync(Guid id)
        {
            return await _context.Groups.FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<Group> InsertGroupAsync(Group caseGroup)
        {
            _context.Groups.Add(caseGroup);
            await _context.SaveChangesAsync();
            return caseGroup;
        }

        public async Task<bool> UpdateGroupAsync(Group caseGroup)
        {
            _context.Groups.Update(caseGroup);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteGroupAsync(Guid id)
        {
            _context.Groups.Remove(_context.Groups.FirstOrDefault(g => g.Id == id));
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
