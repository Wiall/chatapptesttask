using System.ComponentModel.DataAnnotations;

namespace chatappbackend;

public class AnalysisResult
{
    [Key] // Додає первинний ключ
    public int ResultID { get; set; }
    public int MessageID { get; set; }
    public DateTime AnalysisDate { get; set; }
    public string Sentiment { get; set; }
}
