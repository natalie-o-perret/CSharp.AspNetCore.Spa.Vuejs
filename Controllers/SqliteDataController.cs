using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharp.AspNetCore.Spa.Vuejs.DevExtreme;
using CSharp.AspNetCore.Spa.Vuejs.SqliteData;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CSharp.AspNetCore.Spa.Vuejs.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/sqlite-data")]
    [Produces("application/json")]
    public class SqliteDataController
    {
        private readonly SqliteDbContext _dbContext;

        public SqliteDataController(SqliteDbContext dbContext) =>
            _dbContext = dbContext;

        /// <summary>
        /// Return filtered SQLite records
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<LoadResult> GetAsync(DataSourceLoadOptions loadOptions)
        {
            // It works
            var records = await _dbContext.Records.Select(x => new
            {
                Id = x.Id,
                Integer = x.Integer,
                Real = x.Real,
                NullableText = x.NullableText,
                NonNullableText = x.NonNullableText,
                FSharpOptionText = x.FSharpOptionText!.DefaultFromOption()
            }).ToListAsync();

            // It Doesn't
            return await DataSourceLoader.LoadAsync(_dbContext.Records.Select(x => new
                {
                    Id = x.Id,
                    Integer = x.Integer,
                    Real = x.Real,
                    NullableText = x.NullableText,
                    NonNullableText = x.NonNullableText,
                    FSharpOptionText = x.FSharpOptionText!.DefaultFromOption()
                }
            ), loadOptions);
        }


        /// <summary>
        /// Return all SQLite records
        /// </summary>
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<List<SqliteRecord>> GetAllAsync() =>
            _dbContext.Records.ToListAsync();
    }
}
