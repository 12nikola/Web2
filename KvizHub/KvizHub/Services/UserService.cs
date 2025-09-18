using AutoMapper;
using KvizHub.DTO.Quiz;
using KvizHub.DTO.User;
using KvizHub.Exceptions;
using KvizHub.Infrastructure.QuizConfiguration;
using KvizHub.Interfaces;
using KvizHub.Models.Quiz;
using KvizHub.Models.User;
using Microsoft.AspNetCore.Identity.Data;

namespace KvizHub.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly QuizContext _db;
        private readonly IAuthToken _authToken;
        private readonly IStorageService _storage;
        private readonly IPasswordService _passwords;

        public UserService(
            IMapper mapper,
            QuizContext db,
            IAuthToken authToken,
            IPasswordService passwords,
            IStorageService storage)
        {
            _mapper = mapper;
            _db = db;
            _authToken = authToken;
            _passwords = passwords;
            _storage = storage;
        }

        public string SignIn(LoginRequest loginRequest)
        {
            var user = loginRequest.Email.Contains("@")
                ? _db.Users.FirstOrDefault(u => u.Email == loginRequest.Email)
                : _db.Users.FirstOrDefault(u => u.Username == loginRequest.Email);

            if (user == null || !_passwords.Validate(loginRequest.Password, user.Password))
            {
                throw new EntityUnavailableException("Invalid credentials.");
            }

            return _authToken.CreateToken(user.Username);
        }

        public string SignUp(RegisterRequest registerRequest)
        {
            if (_db.Users.Any(u => u.Username == registerRequest.Email))
                throw new EntityConflictException("Username", registerRequest.Email);

            string profileImageName = _storage.Upload(registerRequest.);

            var newUser = _mapper.Map<Users>(registerRequest);
            newUser.Image = profileImageName;
            newUser.Password = _passwords.Encrypt(registerRequest.Password);

            _db.Users.Add(newUser);
            int changes = _db.SaveChanges();

            if (changes > 0)
            {
                return _authToken.CreateToken(newUser.Username);
            }

            throw new SaveFailedException("User", newUser.Username);
        }

        public List<string> GetAllUsers()
        {
            return _db.Users.Select(u => u.Username).ToList();
        }

        public string GetProfileImagePath(string username)
        {
            var user = _db.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
                throw new EntityNotFoundException(username);

            return user.Image;
        }
    }
}
