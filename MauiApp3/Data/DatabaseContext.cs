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
        }
        private async Task<AsyncTableQuery<Users>> GetTableAsync()
        {
            await Init();
            return _dbConnection.Table<Users>();
        }
        public async Task<Users> GetItemsAsync(Users item)
        {
            await Init();
            var res = await _dbConnection.Table<Users>().Where(i => item.Id == i.Id).FirstOrDefaultAsync();
            Debug.WriteLine($"id - {res.Id} \n name - {res.UserName} \n pass - {res.Password}");
            return await _dbConnection.Table<Users>().Where(i => i.Id == item.Id).FirstOrDefaultAsync();
        }

        public async Task UpdateUser(string newValue, string oldValue)
        {
            if (newValue == null) return;
            await Init();  
            var userForUpdate = await _dbConnection.Table<Users>().Where(o => oldValue == o.UserName).FirstOrDefaultAsync();
            await _dbConnection.UpdateAsync(userForUpdate);
            Debug.WriteLine($"name - {userForUpdate.UserName} \n pass = {userForUpdate.Password} ");
        }

        public async Task<bool> CheckUser(Users operatinUser)
        {
            //var userStatus = false;
            //if(userForUpdate != null) userStatus = true;
            //return userStatus;
            
            await Init();
            var user = await _dbConnection.Table<Users>().Where(o => o.UserName == operatinUser.UserName).CountAsync();
            return user > 0;
        }
        public async Task<bool> AddItemAsync(Users item)
        {
            await Init();
            return await _dbConnection.InsertAsync(item) > 0;
        }

        public async Task<IEnumerable<Users>> GetAllUsers()
        {
            var table = await GetTableAsync();
            return await table.ToListAsync();
        }

        public async Task<Users> GetUsersIdAsync(Users item)
        {
            await Init();
            var res = await _dbConnection.Table<Users>().Where(z => z.UserName == item.UserName).FirstOrDefaultAsync();

            Debug.WriteLine($"id - {res.Id} \n name - {res.UserName} \n pass - {res.Password}");

            return await _dbConnection.Table<Users>().Where(z => z.UserName == item.UserName).FirstOrDefaultAsync();
        }



        //public async Task<List<Users>> GetLogginAsync(Users item)
        //{
        //    await Init();
        //    //var re = _dbConnection.Table<Users>().Where(i => i.Id == item.Id);
        //    return await _dbConnection.QueryAsync<Users>($"Select * FROM [Users] where [Id] = {item.Id}");
        //}

    }
}
