namespace SGAM.Elfec.Model.Interfaces
{
    /// <summary>
    /// Interfaz de las entidades
    /// </summary>
    public interface IEntity
    {
        string Id { get; }
        string Name { get; }
        string Details { get; }
    }
}
