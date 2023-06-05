﻿using Catalog_of_Games_DAL.Data;
using Catalog_of_Games_DAL.Entities;
using Catalog_of_Games_DAL.Repositories.Contracts;

namespace Catalog_of_Games_DAL.Repositories
{
    public class CommentReplyRepository : GenericRepository<CommentReply>, ICommentReplyRepository
    {
        public CommentReplyRepository(CatalogOfGamesContext dbContext) : base(dbContext) { }
    }
}