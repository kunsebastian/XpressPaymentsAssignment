using Humanizer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.Xml;
using XpressPayments.Data;
using XpressPayments.DTOs;
using XpressPayments.Models;

namespace XpressPaymentsQuestions
{
    public class Question2_DatabaseQueryOptimization
    {
        private readonly ApplicationDbContext _context;
        public Question2_DatabaseQueryOptimization(ApplicationDbContext context)
        {
            _context = context;
        }

        //Here are different optimized queries to retrieve a large dataset efficiently, along with their sql queries

        #region Query 1: Retrieves Specific Columns from the table 
        //this reduces the amount of data transferred from the database      

        public List<LargeDataSetDTO> GetSpecificColumnsData()
        {
            var result = _context.LargeDataSet
                .Select(x => new LargeDataSetDTO
                {
                    Name = x.UserName,
                })
                .ToList();

            return result;
        }

        //SELECT Column1, Column2
        //FROM LargeDataSet

        #endregion

        #region Query 2: Retrieves Data with Pagination 
        //this fetches only a subset of the total result set from the database, which improves performance and reduces resource utilization

        public List<ApplicationUser> GetDataWithPagination()
        {
            var pageSize = 10;
            var pageNumber = 1;

            var result = _context.LargeDataSet
                .OrderBy(x => x.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return result;
        }

        //SELECT*FROM(
        //    SELECT*, ROW_NUMBER() OVER (ORDER BY SortColumn) AS RowNum
        //    FROM LargeDataSet
        //) AS sub
        //WHERE sub.RowNum BETWEEN 1 AND 10;

        #endregion

        #region Query 3: Retrieves Data with Joins 
        //this allows you to combine related information from multiple tables in a single query


        //public List<ApplicationUser> GetDataWithJoins()
        //{
        //    var result = _context.LargeDataSet
        //        .Join(_context.AnotherTable,
        //            lt => lt.JoinColumn,
        //            at => at.JoinColumn,
        //            (lt, at) => new
        //            {
        //                lt.Column1,
        //                at.ColumnA,
        //            })
        //        .Where(x => x.SomeCondition)
        //        .ToList();
        //} 

        //SELECT lt.Column1, at.ColumnA
        //FROM LargeTable lt
        //JOIN AnotherTable at ON lt.JoinColumn = at.JoinColumn
        //WHERE lt.SomeCondition;
        #endregion

        #region Query 4: Retrieves Data without tracking
        //Use AsNoTracking() if you are only reading the data and not modifying it to avoid the overhead of tracking changes

        public List<ApplicationUser> GetDataWithoutTracking()
        {
            var result = _context.LargeDataSet
                .AsNoTracking()
                .ToList();

            return result;
        }

        //SELECT *
        //FROM MyEntities
        //WHERE SomeCondition;

        #endregion

    }
}
