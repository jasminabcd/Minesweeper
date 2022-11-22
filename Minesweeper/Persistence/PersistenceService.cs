using efCoreTest;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using GameState = Minesweeper.Migrations.GameState;

namespace Minesweeper.Persistence
{
    internal class PersistenceService
    {
        public void SaveOrUpdateGame(Game game)
        {
            bool isSaved = false;
            var persitenceGame = game.ToPersistenceGame();
            using var context = new MinesweeperContext();
            //var query = context.Games.Where< g. => g.ID >;

            //using var context = new MinesweeperContext();

            //context.Games.Add(persitenceGame);
            //isSaved = true;
            //context.SaveChanges();

            if (persitenceGame.ID == 0)
            {
                context.Games.Add(persitenceGame);
            }
            else
            {
                context.Games.Update(persitenceGame);
            }



            if (isSaved)
            {
            context.Games.Update(persitenceGame);
            context.SaveChanges();

            //isSaved = true;
            }

            var gameState = new GetGameState();

            context.SaveChanges();
           
        }

        public List<PersistenceGame> LoadGames()
        {
            using var context = new MinesweeperContext();
            var query = context.Games
                .OrderBy(u => u.ID);

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


