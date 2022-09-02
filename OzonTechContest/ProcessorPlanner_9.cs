namespace OzonTechContest
{
    /// <summary>
    /// Задача "Планировщик задач"
    /// </summary>
    /// <remarks> Требуется реализовать обработчик поступающих задач для однопоточного процессора </remarks>
    public static class ProcessorPlanner_9
    {
        /// <summary>
        /// Обработчик поступающих задач
        /// </summary>
        /// <param name="totalTasks"> Общее количество задач </param>
        /// <remarks> В качестве результата для проверки выводит временные споты, в которые была поставлена задача </remarks>
        public static void ProcessTasks(int totalTasks)
        {
            var nextFreeTime = 0;                           // Следующий свободный временной спот
            var taskDurationQueue = new Queue<int>();       // Очередь задач, содержащая продолжительность выполнения каждоый задачи
            var result = new List<int>();                   // Результат - время постановки каждой задачи в выполнение

            for (var i = 0; i < totalTasks; ++i)
            {
                var input = Console.ReadLine() ?? string.Empty;
                var splittedInput = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (splittedInput.Length != 2)
                {
                    throw new ArgumentException("Некорректные входные данные.");
                }

                var currentTime = int.Parse(splittedInput[0]);      // Текущее время поступления задачи
                var duration = int.Parse(splittedInput[1]);         // Продолжительность задачи

                if (currentTime > nextFreeTime)
                {
                    if (taskDurationQueue.Any())                    // Отрабатываем задачи в очереди
                    {
                        nextFreeTime += taskDurationQueue.Dequeue();
                        taskDurationQueue.Enqueue(duration);
                    }
                    else
                    {
                        nextFreeTime += duration;
                    }

                    result.Add(nextFreeTime);
                }
                else
                {
                    taskDurationQueue.Enqueue(duration);
                }
            }

            while (taskDurationQueue.Any())
            {
                nextFreeTime += taskDurationQueue.Dequeue();
                result.Add(nextFreeTime);
            }

            Console.WriteLine(string.Join(" ", result));
        }
    }
}