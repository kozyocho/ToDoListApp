using Microsoft.EntityFrameworkCore;

namespace ToDoListApp
{
  public class ToDoContext : DbContext
  {
    public DbSet<ToDoItem> ToDoItems { get; set; }

    // データベース接続設定（SQLiteを使用）
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlite("Data Source=todo.db");
    }
  }
}