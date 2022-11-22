
using Microsoft.EntityFrameworkCore;
using Minesweeper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks.Dataflow;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;

public class PersistenceGame
{
    public int ID { get; set; }
    public TimeSpan Duration { get; set; }
    
    [MaxLength(256)]
    [NotNull]
    public string PlayerName { get; set; }

    public string Difficulty { get; set; }

    public DateTime LastPlayedOn { get; set; }
    public GameState GameState { get; set; }

    public ICollection<PersistenceField> Fields { get; set; } = new List<PersistenceField>();

}

public enum GameState
{
    Won = 0, 
    GameOver = 1, 
    Playing = 2
}




public class GetGameState
{
    public GameState gameState { get; set; }

    public void SetGameState(GameState valueGameState)
    {
        gameState = valueGameState;
    }
    public GetGameState()
    {
        return;
    }
}


public class PersistenceField
{
  
    public int ID { set; get; }
    
    public int GameID { set; get; }

    public bool IsDiscovered { get; set; }
    public bool IsBomb { get; set; }
    public bool IsFlag { get; set; }
    public int BombsAroundMe { get; set; }
    public int Position { get; set; }

}
