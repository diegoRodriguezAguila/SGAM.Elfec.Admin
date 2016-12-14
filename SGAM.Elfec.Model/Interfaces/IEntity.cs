namespace SGAM.Elfec.Model.Interfaces
{
    /// <summary>
    /// Interfaz de las entidades
    /// </summary>
    public interface IEntity
    {
        string EntityType { get; }
        string Id { get; }
        string Name { get; }
        string Details { get; }
    }
}
