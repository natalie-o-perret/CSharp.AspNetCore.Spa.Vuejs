using System.Threading.Tasks;
using CSharp.AspNetCore.Spa.Vuejs.DevExtreme;
using CSharp.AspNetCore.Spa.Vuejs.SqliteData;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CSharp.AspNetCore.Spa.Vuejs.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class SqliteDataController
    {
        private readonly SqliteDbContext _dbContext;

        public SqliteDataController(SqliteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Return filtered SQLite records
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<LoadResult> GetAsync(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.LoadAsync(_dbContext.Records, loadOptions);
        }
    }
}
