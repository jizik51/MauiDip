using MauiApp3.MVVM.Model;
using SQLite;
using System.Diagnostics;

namespace MauiApp3.Data
{
    public class DatabaseContext
    {
        private SQLiteAsyncConnection _dbConnection;

        private SQLiteAsyncConnection Database =>
            (_dbConnection ??= new SQLiteAsyncConnection(Constants.DbPath,
                SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache));
        private async Task Init()
        {
            if (_dbConnection is not null) return;

            _dbConnection = new SQLiteAsyncConnection(Constants.DbPath, Constants.Flags);
            await _dbConnection.CreateTableAsync<Users>();
            await _dbConnection.CreateTableAsync<Orders>();
        }
        private async Task<AsyncTableQuery<Users>> GetTableUsersAsync()
        {
            await Init();
            return _dbConnection.Table<Users>();
        }
        private async Task<AsyncTableQuery<Orders>> GetTableOrdersAsync()
        {
            await Init();
            return _dbConnection.Table<Orders>();
        }
        public async Task<Users> GetItemsAsync(Users item)
        {
            await Init();
            var res = await _dbConnection.Table<Users>().Where(i => item.Id == i.Id).FirstOrDefaultAsync();
            Debug.WriteLine($"id - {res.Id} \n name - {res.UserName} \n pass - {res.Password}");
            return await _dbConnection.Table<Users>().Where(i => i.Id == item.Id).FirstOrDefaultAsync();
        }

        public async Task UpdateUser(Users users)
        {
            if (users == null) return;
            await Init();
            await _dbConnection.UpdateAsync(users);
            //var userForUpdate = await _dbConnection.Table<Users>().Where(o => userName == o.UserName).FirstOrDefaultAsync();
            //await _dbConnection.UpdateAsync(userForUpdate);
            Debug.WriteLine($"name - {users.UserName} \n pass = {users.Password} ");
        }

        public async Task<bool> CheckUser(Users operatinUser)
        {
            await Init();
            var user = await _dbConnection.Table<Users>().Where(o => o.UserName == operatinUser.UserName).CountAsync();
            return user > 0;
        }

        public async Task DeleteUsers(Users users)
        {
            await Init();
            await _dbConnection.DeleteAsync(users);
        }
        public async Task<bool> AddItemAsync(Users item)
        {
            await Init();
            return await _dbConnection.InsertAsync(item) > 0;
        }
        public async Task<bool> AddItemAsync(Orders item)
        {
            await Init();
            return await _dbConnection.InsertAsync(item) > 0;
        }
        public async Task<IEnumerable<Users>> GetAllUsers()
        {
            var table = await GetTableUsersAsync();
            return await table.ToListAsync();
        }

        public async Task<IEnumerable<Orders>> GetAllOrders()
        {
            var table = await GetTableOrdersAsync();
            return await table.ToListAsync();
        }

        public async Task<Users> GetUsersIdAsync(Users item)
        {
            try
            {
                await Init();
                var res = await _dbConnection.Table<Users>().Where(z => z.UserName == item.UserName).FirstOrDefaultAsync();
                if (res == null)
                {
                    throw new Exception("Invalid user");
                }
                Debug.WriteLine($"id - {res.Id} \n name - {res.UserName} \n pass - {res.Password}");
                return await _dbConnection.Table<Users>().Where(z => z.UserName == item.UserName).FirstOrDefaultAsync();

            }
            catch(Exception ex)
            {
                string errorMessage = "there is no such user in the database";
                await Application.Current.MainPage.DisplayAlert("Error", errorMessage, "OK");
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }



        //public async Task<List<Users>> GetLogginAsync(Users item)
        //{
        //    await Init();
        //    //var re = _dbConnection.Table<Users>().Where(i => i.Id == item.Id);
        //    return await _dbConnection.QueryAsync<Users>($"Select * FROM [Users] where [Id] = {item.Id}");
        //}

    }
}
