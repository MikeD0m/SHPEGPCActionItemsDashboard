using System.Data;
using System.Linq.Expressions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SHPEGPCActionItemsDashboard.Models;

namespace SHPEGPCActionItemsDashboard.DataAccess
{
    public class DatabaseServices
    {
        private readonly LocalDatabaseContext _localDbContext;

        public DatabaseServices(LocalDatabaseContext localDbContext) 
        { 
            _localDbContext = localDbContext;
        }
        public async Task<List<ActionItems>> LoadActionItemsListAsync()
        {
            var conn = _localDbContext.Database.GetDbConnection();
            bool weOpened = conn.State != ConnectionState.Open;

            List<ActionItems> actionItemsList = new List<ActionItems>();

            try
            {
                if (weOpened) await conn.OpenAsync();

                using var cmd = conn.CreateCommand();
                cmd.CommandText = "dbo.LoadActionItemsList";
                cmd.CommandType = CommandType.StoredProcedure;

                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    int id = reader.GetInt32(0);
                    string title = reader.GetString(1);
                    string description = reader.GetString(2);
                    string priority = reader.GetString(3);
                    DateTime dueDate = reader.GetDateTime(4);
                    bool isDone = reader.GetBoolean(5);
                    DateTime? completedDate = reader.IsDBNull(6) ? null : reader.GetDateTime(6);
                    ActionItems ai = new ActionItems
                    {
                        AI_ID = id,
                        Title = title,
                        Description = description,
                        Priority = priority,
                        DueDate = dueDate,
                        IsDone = isDone,
                        CompletedDate = completedDate
                    };
                    actionItemsList.Add(ai);
                }
                return actionItemsList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<ActionItems>();
            }
            finally
            {
                if (weOpened) await conn.CloseAsync();
            }
        }

        //CRUD BOARD MEMBERS
        public async Task<bool> CreateBoardMemberAsync(string firstName, string lastName, string email, string position, DateTime termStart, DateTime termEnd)
        {
            var parameters = new[]
            {
                new SqlParameter("@FirstName", SqlDbType.VarChar, 255){ Value = firstName},
                new SqlParameter("@LastName", SqlDbType.VarChar, 255){ Value = firstName},
                new SqlParameter("@Email", SqlDbType.VarChar, 255){ Value = firstName},
                new SqlParameter("@Position", SqlDbType.VarChar, 255){ Value = position},
                new SqlParameter("@TermStart", SqlDbType.DateTime){ Value = termStart},
                new SqlParameter("@TermEnd", SqlDbType.DateTime){ Value = termEnd},
            };
            try
            {
                await _localDbContext.Database.ExecuteSqlRawAsync(
                "EXEC dbo.CreateBoardMember @FirstName, @LastName, @Email, @Position, @TermStart, @TermEnd",
                parameters);
            }
            catch(Exception ex)
            {
                return false;
            }
            return true;

        }
        public async Task<List<BoardMembers>> LoadBoardMembersListAsync()
        {
            var conn = _localDbContext.Database.GetDbConnection();
            bool weOpened = conn.State != ConnectionState.Open;

            List<BoardMembers> boardMembersList = new List<BoardMembers>();

            try
            {
                if (weOpened) await conn.OpenAsync();

                using var cmd = conn.CreateCommand();
                cmd.CommandText = "dbo.LoadBoardMembersList";
                cmd.CommandType = CommandType.StoredProcedure;

                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    int board_id = reader.GetInt32(0);
                    string firstName = reader.GetString(1);
                    string lastName = reader.GetString(2);
                    string email = reader.GetString(3);
                    string position = reader.GetString(4);
                    DateTime termStart = reader.GetDateTime(5);
                    DateTime termEnd = reader.GetDateTime(6);
                    BoardMembers bm = new BoardMembers
                    {
                        Board_ID = board_id,
                        FirstName = firstName,
                        LastName = lastName,
                        Email = email,
                        Position = position,
                        TermStart = termStart,
                        TermEnd = termEnd
                    };
                    boardMembersList.Add(bm);
                }
                return boardMembersList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<BoardMembers>();
            }
            finally
            {
                if (weOpened) await conn.CloseAsync();
            }
        }
        public async Task<bool> UpdateBoardMemberAsync(int board_id, string firstName, string lastName, string email, string position, DateTime termStart, DateTime termEnd)
        {
            var parameters = new[]
            {
                new SqlParameter("@Board_ID", SqlDbType.Int){ Value = board_id},
                new SqlParameter("@FirstName", SqlDbType.VarChar, 255){ Value = firstName},
                new SqlParameter("@LastName", SqlDbType.VarChar, 255){ Value = firstName},
                new SqlParameter("@Email", SqlDbType.VarChar, 255){ Value = firstName},
                new SqlParameter("@Position", SqlDbType.VarChar, 255){ Value = position},
                new SqlParameter("@TermStart", SqlDbType.DateTime){ Value = termStart},
                new SqlParameter("@TermEnd", SqlDbType.DateTime){ Value = termEnd},
            };

            try
            {
                await _localDbContext.Database.ExecuteSqlRawAsync(
                "EXEC dbo.UpdateBoardMember @Board_ID, @FirstName, @LastName, @Email, @Position, @TermStart, @TermEnd",
                parameters);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;

        }
        public async Task<bool> RemoveBoardMemberAsync(int board_id)
        {
            var parameters = new[]
            {
                new SqlParameter("@Board_ID", SqlDbType.Int){ Value = board_id},
            };
            try
            {
                await _localDbContext.Database.ExecuteSqlRawAsync(
                "EXEC dbo.RemoveBoardMember @Board_ID",
                parameters);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    
    }
}
