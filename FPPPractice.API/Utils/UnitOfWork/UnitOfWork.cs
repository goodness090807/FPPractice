using FPPPractice.API.Repositories;
using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.Data.SqlClient;

namespace FPPPractice.API.Utils.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private bool _disposed;
        private readonly IServiceProvider _serviceProvider;

        public IDbConnection Connection => _connection;

        public IDbTransaction Transaction => _transaction;

        public UnitOfWork(IServiceProvider serviceProvider, IOptions<Appsettings> options)
        {
            _connection = new SqlConnection(options.Value.ConnectionString);
            _connection.Open();
            _serviceProvider = serviceProvider;
        }

        public void BeginTransaction()
        {
            _transaction = _connection.BeginTransaction();
        }

        public TRepo Repository<TRepo>() where TRepo : IRepositoryBase
        {
            var repo = (TRepo)_serviceProvider.GetService(typeof(TRepo));

            repo.Connection = _connection;
            repo.Transaction = _transaction;

            return repo;
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
            }
        }

        public void Rollback()
        {
            _transaction.Rollback();
            Dispose();
        }

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        // 如果交易被關掉或是下面的連線被關掉
                        // 就會將transaction做Rollback
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }


        ~UnitOfWork()
        {
            dispose(false);
        }
    }
}
