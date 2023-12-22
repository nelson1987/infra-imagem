using Dotnet.UnitTests.Pretender.Domain.Entities;

namespace Dotnet.UnitTests;

public class UserTests
{
    [Fact]
    public void Given_User_Valid_Should_Be_DateCreated_Today()
    {
    }
}
namespace Pretender.Domain.Commmon
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? DateUpdated { get; set; }
        public DateTimeOffset? DateDeleted { get; set; }
    }
}
namespace Pretender.Domain.Entities
{
    public sealed class User : BaseEntity
    {
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
namespace Pretender.Application.Repositories
{
    public interface IUserRepository
    {
        void Create(User entity);
        void Update(User entity);
        void Delete(User entity);
        Task<User> Get(Guid id, CancellationToken cancellationToken);
        Task<List<User>> GetAll(CancellationToken cancellationToken);
        Task<User> GetByEmail(string email, CancellationToken cancellationToken);
    }
    public interface IUnitOfWork
    {
        Task Save(CancellationToken cancellationToken);
    }
}
namespace Pretender.Application.Features.UserFeatures.CreateUser
{
    public sealed record CreateUserRequest(string Email, string Name) : IRequest<CreateUserResponse>;
    public sealed record CreateUserResponse(Guid Id, string Email, string Name);
    public sealed class CreateUserHandler : IRequestHandler<CreateUserRequest, CreateUserResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreateUserHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            User user = request.MapTo<User>();
            _userRepository.Create(user);
            return user.MapTo<CreateUserResponse>();
        }
    }
}