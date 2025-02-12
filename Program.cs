using System;
using System.Linq;

namespace ToDoListApp
{
  class Program
  {
    static void Main(string[] args)
    {
      // DbContextのインスタンスを生成
      using (var db = new ToDoContext())
      {
        // データベースが存在しない場合は作成
        db.Database.EnsureCreated();

        bool exit = false;
        while (!exit)
        {
          Console.WriteLine("\n--- ToDo リスト ---");
          Console.WriteLine("1. ToDo 項目の追加");
          Console.WriteLine("2. ToDo 項目の一覧表示");
          Console.WriteLine("3. タスクを完了にする");
          Console.WriteLine("4. 終了");
          Console.Write("オプションを選択してください: ");
          string input = Console.ReadLine();
          Console.WriteLine();

          switch (input)
          {
            case "1":
              Console.WriteLine("新しい ToDo 項目のタイトルを入力してください:");
              string title = Console.ReadLine();

              // 新規項目の作成
              var newItem = new ToDoItem { Title = title, IsCompleted = false };
              db.ToDoItems.Add(newItem);
              db.SaveChanges();
              Console.WriteLine("タスクが追加されました。");
              break;

            case "2":
              var items = db.ToDoItems.ToList();
              Console.WriteLine("現在のタスク一覧:");
              foreach (var item in items)
              {
                Console.WriteLine($"ID: {item.Id}, タイトル: {item.Title}, 完了: {item.IsCompleted}");
              }
              break;

            case "3":
              // タスクを完了にする（ID指定で更新）
              Console.Write("完了にするタスクのIDを入力してください: ");
              // 入力された文字列を整数に変換し、その結果を completeIdに格納する
              if (int.TryParse(Console.ReadLine(), out int completeId))
              {
                // 入力されたタスクのIDに該当する最初のタスクをデータベースから探し、見つかった場合はそのタスクをtaskに代入する
                var task = db.ToDoItems.FirstOrDefault(t => t.Id == completeId);
                if (task != null)
                {
                  db.ToDoItems.Remove(task);
                  db.SaveChanges();
                  Console.WriteLine("タスクが完了にマークされました。");
                }
                else
                {
                  Console.WriteLine("指定されたIDのタスクは存在しません。");
                }
              }
              else
              {
                Console.WriteLine("有効なIDを入力してください。");
              }
              break;

            case "4":
              exit = true;
              Console.WriteLine("アプリケーションを終了します。");
              break;

            default:
              Console.WriteLine("無効な選択です。再度お試しください。");
              break;
          }
        }
      }
    }
  }
}