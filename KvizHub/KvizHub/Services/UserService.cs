using AutoMapper;
using KvizHub.DTO.Quiz;
using KvizHub.Models.Quiz;

namespace QuizWebServer.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapperService;
        private readonly QuizHubDbContext _databaseContext;
        private readonly ITokenService _authTokenService;
        private readonly IFileStorageService _fileService;
        private readonly IPasswordHasherService _passwordService;

        public AccountService(IMapper mapperService, QuizHubDbContext databaseContext, ITokenService authTokenService, IPasswordHasherService passwordService, IFileStorageService fileService)
        {
            _mapperService = mapperService;
            _databaseContext = databaseContext;
            _authTokenService = authTokenService;
            _fileService = fileService;
            _passwordService = passwordService;
        }

        public string Authenticate(LoginRequestDTO loginData)
        {
            UserAccount account;

            if (loginData.Identifier.Contains("@"))
            {
                account = _databaseContext.Users.FirstOrDefault(u => u.Email == loginData.Identifier);
            }
            else
            {
                account = _databaseContext.Users.FirstOrDefault(u => u.Username == loginData.Identifier);
            }

            if (account == null)
            {
                throw new UnauthorizedException("Invalid credentials.");
            }

            if (_passwordService.VerifyPassword(loginData.Password, account.Password))
            {
                return _authTokenService.GenerateToken(account.Username);
            }
            else
            {
                throw new UnauthorizedException("Invalid credentials.");
            }
        }

        public string RegisterUser(RegistrationRequestDTO registrationData)
        {
            if (_databaseContext.Users.Any(u => u.Username == registrationData.Username))
            {
                throw new AlreadyExistsException("Username", registrationData.Username);
            }

            if (_databaseContext.Users.Any(u => u.Email == registrationData.Email))
            {
                throw new AlreadyExistsException("Email", registrationData.Email);
            }

            string profileImageName = _fileService.SaveFile(registrationData.Image);

            UserAccount newUser = _mapperService.Map<UserAccount>(registrationData);
            newUser.Image = profileImageName;
            newUser.Password = _passwordService.HashPassword(registrationData.Password);

            _databaseContext.Users.Add(newUser);

            int changesSaved = _databaseContext.SaveChanges();

            if (changesSaved > 0)
            {
                return _authTokenService.GenerateToken(newUser.Username);
            }
            else
            {
                throw new DataNotSavedException("User", newUser.Username);
            }
        }

        public List<string> ListAllUsers()
        {
            return _databaseContext.Users.Select(u => u.Username).ToList();
        }

        public string GetUserProfileImage(string username)
        {
            UserAccount account = _databaseContext.Users.FirstOrDefault(u => u.Username == username);

            if (account == null)
                throw new NotFoundException(username);

            return account.Image;
        }
    }
}
