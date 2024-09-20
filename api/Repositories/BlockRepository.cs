using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class BlockRepository : IBlockRepository
    {

        private readonly DataContext _dataContext;
         public BlockRepository(DataContext dataContext)
         {
            _dataContext =dataContext;
         }



          public async Task<bool> BlockPerson(string ensuranceNumber, int year , string not)
    {

        var person = await _dataContext.Persons
                                   .FirstOrDefaultAsync(p => p.EnsuranceNumber == ensuranceNumber);

        if (person == null)
            return false; // Person not found


        var blockListEntry = new BlockList
        {
            PersonId = person.Id,
           
            Year = year, // Store the year
            DateTime = DateTime.Now,
            Note=not
        };

        _dataContext.blockLists.Add(blockListEntry);
        _dataContext.SaveChanges();

        
        return true;
    }



    public async Task<bool> UnblockPerson(string ensuranceNumber)
{
    
    var blockedPerson = await _dataContext.blockLists
                                      .FirstOrDefaultAsync(b => b.person.EnsuranceNumber == ensuranceNumber);

    if (blockedPerson == null)
        return false; // Person is not blocked


    _dataContext.blockLists.Remove(blockedPerson);
    await _dataContext.SaveChangesAsync();
    return true;
}



   public async Task<List<Person>> GetAllBlockedPersons()
{


    var blockedPersons = await _dataContext.blockLists
                                       .Include(b => b.person) // Join with Person table
                                       .Select(b => b.person)
                                       .ToListAsync();
    return blockedPersons;
}




public async Task<bool> IsPersonBlocked(string ensuranceNumber)
{
    return await _dataContext.blockLists
                         .AnyAsync(b => b.person.EnsuranceNumber == ensuranceNumber);
}



        
    }
}