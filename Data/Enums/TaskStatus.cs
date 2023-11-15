using System.ComponentModel;

namespace TodoList.Data.Enums;

public enum TaskStatus
{
    [Description("A fazer")]
    ToDo = 1,
    
    [Description("A fazer")]
    InProgress = 2,
    
    [Description("A fazer")]
    Done = 3
}