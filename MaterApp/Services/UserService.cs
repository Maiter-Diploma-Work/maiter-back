using MaterApp.Models;
using MaterApp;
using Nest;
using Microsoft.EntityFrameworkCore;

public class UserService
{
    private readonly ElasticClient _elasticClient;
    private readonly ApplicationDbContext _context;
    private const string IndexName = "users";

    public UserService(ApplicationDbContext context)
    {
        var settings = new ConnectionSettings(new Uri("http://localhost:9200"));
        _elasticClient = new ElasticClient(settings);
        CreateIndexIfNotExists();
        _context = context;
    }

    private void CreateIndexIfNotExists()
    {
        if (!_elasticClient.Indices.Exists(IndexName).Exists)
        {
            _elasticClient.Indices.Create(IndexName, c => c
                .Map<UserDocument>(m => m.AutoMap())
            );
        }
    }

    public async Task SyncUsersWithElasticsearch()
    {
        var users = await _context.Users.ToListAsync();

        foreach (var user in users)
        {
            var document = new UserDocument
            {
                Id = user.Id.ToString(), // Установите идентификатор документа
                FirstName = user.FirstName,
                // Заполните остальные поля документа, соответствующие данным пользователя
                LastName = user.LastName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender,
                // Продолжайте заполнять остальные поля в соответствии с вашими потребностями
            };

            var indexResponse = await _elasticClient.IndexDocumentAsync(document);
            if (!indexResponse.IsValid)
            {
                // Обработка ошибок индексации
            }
        }
    }

    public async Task<List<UserDocument>> PartialSearchByName(string partialName)
    {
        var searchResponse = await _elasticClient.SearchAsync<UserDocument>(s => s
            .Index(IndexName)
            .Query(q => q
                .Match(m => m
                    .Field(f => f.FirstName)
                    .Query(partialName)
                )
            )
        );

        if (!searchResponse.IsValid)
        {
            // Обработка ошибок поиска
            return new List<UserDocument>();
        }

        return searchResponse.Documents.ToList();
    }

}

public class UserDocument
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    
}
