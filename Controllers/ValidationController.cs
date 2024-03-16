using Forum.database;

namespace Forum.Controllers;

public class ValidationController
{
    private readonly ForumDbContext _db;

    public ValidationController(ForumDbContext db)
    {
        _db = db;
    }

    public bool CheckUserNameOrEmail(string emailOrUserName)
    {
        return _db.Users.Any(u => u.Email == emailOrUserName 
                                  || u.UserName == emailOrUserName);
    }

    public bool CheckUserName(string userName)
    {
        return !_db.Users.Any(u => u.UserName == userName);
    }
    
    public bool CheckEmail(string email)
    {
        return !_db.Users.Any(u => u.Email == email);
    }
}