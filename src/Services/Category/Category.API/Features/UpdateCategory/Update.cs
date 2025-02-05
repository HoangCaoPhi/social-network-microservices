﻿using Category.API.Infrastructure.Context;
using Category.API.Models;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Category.API.Features.UpdateCategory;

public sealed class Update : IEndpoint
{
    public string GroupEntityEndpoint() => Endpoint.Category;
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("{id}", async (Guid id, 
                              CategoryUpdateRequest updatedCategory,
                              CategoryWriteDbContext dbContext) =>
        {
            var category = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (category is null)
                return Result<Guid>.Failure(CategoryErrors.NotFound);

            category.Update(updatedCategory.Name,
                            updatedCategory.Description,
                            updatedCategory.Status);

            await dbContext.SaveChangesAsync();

            return Result<Guid>.Success(category.Id);
        });      
    }
}
