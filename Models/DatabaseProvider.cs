using Microsoft.Data.SqlClient;

namespace Messenger.Models;

public class DatabaseProvider : IDisposable
{
    private const string ConnectionString =
        "Server=localhost\\SQLEXPRESS;Database=MessengerAppDb; Trusted_Connection=True; TrustServerCertificate=True;";
    private SqlConnection _connection;
    private ProviderCollector _providerCollector;
    
    public DatabaseProvider(ProviderCollector providerCollector)
    {
        _providerCollector = providerCollector;
        _providerCollector.Add(this);
        _connection = new SqlConnection(ConnectionString);
        _connection.Open();
    }
    
    public int GetTablesCount()
    {
        // using (var connection1 = new SqlConnection(ConnectionString))
        // {
        //     using var command1 = new SqlCommand("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES", connection1);
        //     var count1 = (int)command1.ExecuteScalar();
        // }
        //
        // using var connection2 = new SqlConnection(ConnectionString);
        // using var command2 = new SqlCommand("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES", connection2);
        // var count2 = (int)command2.ExecuteScalar();
        //
        // _connection.Open();
        
        using var command3 = new SqlCommand("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES", _connection);
        var count3 = (int)command3.ExecuteScalar();
        // _connection.Close();
        // _connection.Dispose();
        
        return count3;
    }

    private bool _disposedValue;
    private void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                _providerCollector.Remove(this);
            }
            _connection?.Dispose();
            _connection = null;
            
            _disposedValue = true;
        }
    }

    ~DatabaseProvider()
    {
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}