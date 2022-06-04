using FPPPractice.API.Repositories;
using System;
using System.Data;

namespace FPPPractice.API.Utils.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
        void BeginTransaction();
        /// <summary>
        /// 代表我要取用一個Repo的物件
        /// </summary>
        TRepo Repository<TRepo>() where TRepo : IRepositoryBase;
        void Commit();
    }
}
