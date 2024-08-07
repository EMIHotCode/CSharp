interface IWorker
{
    void ShowWorker();
}

interface IPart
{
    bool Status { get; set; }
    string ShowPart();
}
