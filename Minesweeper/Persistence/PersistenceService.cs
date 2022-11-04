using efCoreTest;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Minesweeper.Persistence
{
    internal class PersistenceService
    {

        public void SaveOrUpdateGame(Game game)
        {

            var persitenceGame = game.ToPersistenceGame();

            using var context = new MinesweeperContext();
            
            context.Games.Add(persitenceGame);
            context.SaveChanges();
        }

        public List<PersistenceGame> LoadGames()
        {
            using var context = new MinesweeperContext();
            var query = context.Games.OrderByDescending(g => g.LastPlayedOn);

            return query.ToList();

        }

      
        public Game RestoreGame(int id)
        {
            using var context = new MinesweeperContext();
            var persistenceGame = context.Games
                .Include(g => g.Fields)
                .First(g => g.ID == id);

            return Game.FromPersistenceGame(persistenceGame);
        }

    }
}


