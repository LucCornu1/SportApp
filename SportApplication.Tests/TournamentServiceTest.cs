using Microsoft.EntityFrameworkCore;
using SportApplication.Data;
using SportApplication.Services;

// ** Explorateur de test à utiliser

namespace SportApplication.Tests
{
	public class TournamentServiceTest
	{
		private readonly TournamentService svc;
		private readonly AppDbContext db;

		public TournamentServiceTest()
        {
			var opt = new DbContextOptionsBuilder<AppDbContext>()
				.UseInMemoryDatabase(databaseName: "SportApplication")
				.Options;
			db = new AppDbContext(opt);

			svc = new TournamentService(db);
        }

        [Fact]
		public void CreateTournamentDataIsValid_validates_data()
		{
			// Arrange (prepare test's data)
			var sportId = 1;
			var title = "test";
			var startingDate = DateTime.Now.AddDays(2);
			var endingDate = DateTime.Now.AddDays(4);

			// Act (Execute test methode)
			var result = svc.CreateTournamentDataIsValid(sportId, title, startingDate, endingDate);

			// Assert (Validate method's result)
			Assert.True(result);
		}

		[Fact]
		public async Task CreateTournamentAsync_creates_competition()
		{
			var sportId = 100;
			var title = "test";
			var startingDate = DateTime.Now.AddDays(2);
			var endingDate = DateTime.Now.AddDays(4);

			var result = await svc.CreateTournamentAsync(sportId, title, startingDate, endingDate);

			var resultDb = db.Tournaments.FirstOrDefault(t => t.Id == sportId);

			Assert.True(result > 0);
			Assert.True(resultDb != null);
			// Assert.Equal(title, resultDb.Title);
		}

		[Theory]
		[InlineData(0, "test", "2024-01-01", "2025-01-01")]
		[InlineData(1, "    ", "2024-01-01", "2025-01-01")]
		[InlineData(1, "test", "2022-01-01", "2025-01-01")]
		[InlineData(1, "test", "2024-01-01", "2022-01-01")]
		public void CreateTournamentDataIsValid_detect_errors(int sportId, string title, string start, string end)
		{
			// Arrange (prepare test's data)
			var startingDate = DateTime.Parse(start);
			var endingDate = DateTime.Parse(end);

			// Act (Execute test method)
			var result = svc.CreateTournamentDataIsValid(sportId, title, startingDate, endingDate);

			// Assert (Validate method's result)
			Assert.False(result);
		}
	}
}