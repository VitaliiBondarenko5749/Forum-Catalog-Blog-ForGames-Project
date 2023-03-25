using Catalog_of_Games_DAL.Entities;
using Dapper;
using Forum_DAL.Contracts;
using Forum_DAL.Models;
using System.Data;
using System.Data.SqlClient;

namespace Forum_DAL.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public PostRepository(SqlConnection sqlConnection, IDbTransaction dbTransaction) :
            base(sqlConnection, dbTransaction, "forum.Posts") { }
    }
}