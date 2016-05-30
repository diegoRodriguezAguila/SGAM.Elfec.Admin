namespace SGAM.Elfec.Model.Enums
{
    /// <summary>
    /// Enum para los estados de los grupos de usuarios
    /// </summary>
    public enum UserGroupStatus
    {
        /// <summary>
        /// Estado de grupo deshabilitado
        /// </summary>
        Disabled,
        /// <summary>
        /// Estado de grupo habiliado
        /// </summary>
        Enabled,
        /// <summary>
        /// Estado de grupo lockeado, este tipo de grupo no es editable
        /// </summary>
        Sealed
    }
}
