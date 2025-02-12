namespace ToDoListApp
{
  public class ToDoItem
  {
    public int Id { get; set; } // Primary Key
    public string Title { get; set; } // タイトル
    public bool IsCompleted { get; set; } // 完了フラグ
  }
}