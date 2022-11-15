
using Microsoft.EntityFrameworkCore;
using Minesweeper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

public class PersistenceGame
{
    public int ID { get; set; }
    public TimeSpan Duration { get; set; }
    
    [MaxLength(256)]
    [NotNull]
    public string PlayerName { get; set; }

    public string Difficulty { get; set; }

    public DateTime LastPlayedOn { get; set; }
    
    public ICollection<PersistenceField> Fields { get; set; } = new List<PersistenceField>();

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


public class PersistenceGrid
{

    public Field Field { get; set; }

    //private int _persistenceDiscoveredFieldCount = 0;
    //private int _persistenceFlagFieldCount = 0;

}