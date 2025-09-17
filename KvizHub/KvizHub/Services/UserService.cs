using AutoMapper;
using KvizHub.DTO.Quiz;
using KvizHub.DTO.User;
using KvizHub.Exceptions;
using KvizHub.Infrastructure.QuizConfiguration;
using KvizHub.Interfaces;
using KvizHub.Models.Quiz;
using KvizHub.Models.User;

namespace KvizHub.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapperService;
        private readonly QuizContext _databaseContext;
        private readonly IAuthToken _authTokenService;
        private readonly IStorageService _fileService;
        private readonly IPasswordService _passwordService;

        public UserService(IMapper mapperService, QuizContext databaseContext, IAuthToken authTokenService, IPasswordService passwordService, IStorageService fileService)
        {
            _mapperService = mapperService;
            _databaseContext = databaseContext;
            _authTokenService = authTokenService;
            _fileService = fileService;
            _passwordService = passwordService;
        }

        public string Authenticate(LoginDTO loginData)
        {
            Users account;

            if (loginData.Email.Contains("@"))
            {
                account = _databaseContext.Users.FirstOrDefault(u => u.Email == loginData.Email);
            }
            else
            {
                account = _databaseContext.Users.FirstOrDefault(u => u.Username == loginData.Email);
            }

            if (account == null)
            {
                throw new EntityUnavailableException("Invalid credentials.");
            }

            if (_passwordService.Validate(loginData.Password, account.Password))
            {
                return _authTokenService.CreateToken(account.Username);
            }
            else
            {
                throw new EntityUnavailableException("Invalid credentials.");
            }
        }

        public string RegisterUser(RegistrationDTO registrationData)
        {
            if (_databaseContext.Users.Any(u => u.Username == registrationData.Username))
            {
                throw new EntityConflictException("Username", registrationData.Username);
            }

            if (_databaseContext.Users.Any(u => u.Email == registrationData.Email))
            {
                throw new EntityConflictException("Email", registrationData.Email);
            }

            string profileImageName = _fileService.SaveFile(registrationData.Image);

            UserAccount newUser = _mapperService.Map<UserAccount>(registrationData);
            newUser.Image = profileImageName;
            newUser.Password = _passwordService.HashPassword(registrationData.Password);

            _databaseContext.Users.Add(newUser);

            int changesSaved = _databaseContext.SaveChanges();

            if (changesSaved > 0)
            {
                return _authTokenService.CreateToken(newUser.Username);
            }
            else
            {
                throw new SaveFailedException("User", newUser.Username);
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
                throw new EntityNotFoundException(username);

            return account.Image;
        }
    }
}
